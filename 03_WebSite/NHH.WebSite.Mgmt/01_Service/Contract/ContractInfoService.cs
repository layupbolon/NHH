using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Contract;
using NHH.Models.Merchant;
using NHH.Service.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract
{
    /// <summary>
    /// 合同信息服务
    /// </summary>
    public class ContractInfoService : NHHService<NHHEntities>, IContractInfoService
    {
        /// <summary>
        /// 获取合同列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ContractListModel GetContractList(ContractListQueryInfo queryInfo)
        {
            var model = new ContractListModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;

            model.ContractList = new List<ContractInfo>();
            #region Linq查询
            var query = from c in Context.Contract
                        join p in Context.Project on c.ProjectID equals p.ProjectID
                        join m in Context.Merchant on c.MerchantID equals m.MerchantID
                        join cs in Context.View_ContractStatus on c.ContractStatus equals cs.ContractStatusID
                        where c.Status == 1
                        select new
                        {
                            c.ContractID,
                            c.ContractCode,
                            c.ProjectID,
                            p.ProjectName,
                            c.ContractArea,
                            c.ContractStatus,
                            ContractStatusName = cs.ContractStatusName,
                            c.MerchantID,
                            c.InDate,
                            c.ContractStartDate,
                            c.ContractEndDate,
                            c.ContractLength,
                            c.DecorationStartDate,
                            c.DecorationEndDate,
                            m.BriefName,
                            MonthlyRent = c.ContractArea * c.ContractUnitRent * 30,
                            UnitList = (from cu in Context.Contract_Unit
                                        join pu in Context.View_Project_Unit on cu.UnitID equals pu.UnitID
                                        where cu.ContractID == c.ContractID
                                        select new
                                        {
                                            cu.ContractID,
                                            pu.UnitID,
                                            pu.UnitNumber,
                                            pu.FloorID,
                                            pu.FloorNumber,
                                            pu.BuildingID,
                                            pu.BuildingName,
                                        }),
                        };
            #endregion
            #region 查询条件
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(item => item.ContractStatus == queryInfo.Status.Value);
            }
            if (queryInfo.StartTime.HasValue)
            {
                query = query.Where(item => item.ContractStartDate >= queryInfo.StartTime);
            }
            if (queryInfo.EndTime.HasValue)
            {
                query = query.Where(item => item.ContractStartDate <= queryInfo.EndTime);
            }
            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.BuildingId.HasValue)
            {
                query = query.Where(item => item.UnitList.Any(pu => pu.BuildingID == queryInfo.BuildingId.Value));
            }
            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => item.UnitList.Any(pu => pu.FloorID == queryInfo.FloorId.Value));
            }
            if (!string.IsNullOrEmpty(queryInfo.UnitNumber))
            {
                query = query.Where(item => item.UnitList.Any(pu => pu.UnitNumber == queryInfo.UnitNumber));
            }
            if (!string.IsNullOrEmpty(queryInfo.StoreName))
            {
                query = query.Where(item => item.BriefName.Contains(queryInfo.StoreName));
            }
            #endregion
            model.PagingInfo.TotalCount = query.Count();

            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new ContractInfo
                    {
                        ContractID = entity.ContractID,
                        ContractCode = entity.ContractCode,
                        ContractStatus = entity.ContractStatus,
                        ContractStatusName = entity.ContractStatusName,
                        MonthlyRent = entity.MonthlyRent,
                        MerchantID = entity.MerchantID,
                        MerchantName = entity.BriefName,
                        InDate = entity.InDate,
                        ContractLength = entity.ContractLength.HasValue ? entity.ContractLength.Value : 0,
                        ContractStartDate = entity.ContractStartDate.HasValue ? entity.ContractStartDate.Value : DateTime.MinValue,
                        ContractEndDate = entity.ContractEndDate.HasValue ? entity.ContractEndDate.Value : DateTime.MinValue,
                        DecorationStartDate = entity.DecorationStartDate.HasValue ? entity.DecorationStartDate.Value : DateTime.MinValue,
                        DecorationEndDate = entity.DecorationEndDate.HasValue ? entity.DecorationEndDate.Value : DateTime.MinValue,
                    };
                    contract.UnitInfo = new ContractUnitInfo();
                    contract.UnitInfo.ProjectID = entity.ProjectID;
                    contract.UnitInfo.ProjectName = entity.ProjectName;
                    contract.UnitInfo.ContractArea = entity.ContractArea;
                    //商铺列表
                    contract.UnitInfo.UnitList = new List<ProjectUnitListItem>();
                    if (entity.UnitList != null)
                    {
                        entity.UnitList.ToList().ForEach(unit =>
                        {
                            contract.UnitInfo.UnitList.Add(new ProjectUnitListItem
                            {
                                BuildingName = unit.BuildingName,
                                FloorNumber = unit.FloorNumber,
                                UnitId = unit.UnitID,
                                UnitNumber = unit.UnitNumber,
                            });
                        });
                    }

                    model.ContractList.Add(contract);
                });
            }

            return model;
        }

        /// <summary>
        /// 获取合同详细信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public ContractDetailModel GetContractDetail(int contractId)
        {
            var model = new ContractDetailModel();
            model.UnitSpec1 = GetContractUnitSpec(contractId, 1);
            model.UnitSpec2 = GetContractUnitSpec(contractId, 2);

            var query = from c in Context.Contract
                        join p in Context.Project on c.ProjectID equals p.ProjectID
                        join d in Context.Dictionary on c.Condition equals d.FieldValue
                        where c.Status == 1 && c.ContractID == contractId && d.FieldType == "Condition"
                        select new
                        {
                            c.MerchantID,
                            c.Merchant.MerchantType,
                            c.Merchant.MerchantName,
                            c.Merchant.BriefName,
                            c.Merchant.OwnerName,
                            c.Merchant.ContactName,
                            c.Merchant.AddressLine,
                            c.ContractID,
                            c.ContractCode,
                            c.ContractStatus,
                            c.SignerName,
                            c.SignerIDNumber,
                            c.OperatorName,
                            c.OperatorPhone,
                            c.SignerPhone,
                            c.ContractLength,
                            c.ContractStartDate,
                            c.ContractEndDate,
                            c.RentFreeStartDate,
                            c.RentFreeEndDate,
                            c.RentFreeLength,
                            c.AccessDate,
                            c.DecorationStartDate,
                            c.DecorationEndDate,
                            c.DecorationLength,
                            c.DecorationMgmtFee,
                            c.DecorationBond,
                            c.BidBond,
                            c.ManageBond,
                            c.QualityBond,
                            c.ParkingLotNum,
                            c.ParkingLotMemo,
                            c.AdPointNum,
                            c.AdPointMemo,
                            p.ProjectID,
                            p.ProjectName,
                            c.ContractArea,
                            c.ContractUnitRent,
                            c.ContractMgmtFee,
                            ConditionText = d.FieldName,
                            UnitList = (from cu in Context.Contract_Unit
                                        join pu in Context.View_Project_Unit on cu.UnitID equals pu.UnitID
                                        where cu.ContractID == c.ContractID
                                        select new
                                        {
                                            pu.BuildingName,
                                            pu.FloorNumber,
                                            pu.UnitID,
                                            pu.UnitNumber,
                                        })
                        };

            var entity = query.FirstOrDefault();
            if (entity == null)
            {
                return model;
            }

            model.MerchantID = entity.MerchantID;
            model.MerchantName = entity.MerchantName;
            model.MerchantInfo = new MerchantInfo();
            model.MerchantInfo.MerchantTypeInfo = new MerchantTypeInfo
            {
                Name = entity.MerchantType == 1 ? "公司" : "自然人"
            };
            model.MerchantInfo.BriefName = entity.BriefName;
            model.MerchantInfo.OwnerName = entity.OwnerName;
            model.MerchantInfo.ContactName = entity.ContactName;
            model.MerchantInfo.MerchantAddress = entity.AddressLine;

            model.ContractID = entity.ContractID;
            model.ContractCode = entity.ContractCode;
            model.ContractStatus = entity.ContractStatus;
            model.SignerName = entity.SignerName;
            model.SignerIDNumber = entity.SignerIDNumber;
            model.SignerPhone = entity.SignerPhone;
            model.OperatorName = entity.OperatorName;
            model.OperatorPhone = entity.OperatorPhone;
            model.RentFreeLength = entity.RentFreeLength.HasValue ? entity.RentFreeLength.Value : 0;
            model.RentFreeStartDate = entity.RentFreeStartDate.HasValue ? entity.RentFreeStartDate.Value : DateTime.MinValue;
            model.RentFreeEndDate = entity.RentFreeEndDate.HasValue ? entity.RentFreeEndDate.Value : DateTime.MinValue;
            model.ContractLength = entity.ContractLength.HasValue ? entity.ContractLength.Value : 0;
            model.ContractStartDate = entity.ContractStartDate.HasValue ? entity.ContractStartDate.Value : DateTime.MinValue;
            model.ContractEndDate = entity.ContractEndDate.HasValue ? entity.ContractEndDate.Value : DateTime.MinValue;
            model.AccessDate = entity.AccessDate.HasValue ? entity.AccessDate.Value : DateTime.MinValue;
            model.DecorationLength = entity.DecorationLength.HasValue ? entity.DecorationLength.Value : 0;
            model.DecorationStartDate = entity.DecorationStartDate.HasValue ? entity.DecorationStartDate.Value : DateTime.MinValue;
            model.DecorationEndDate = entity.DecorationEndDate.HasValue ? entity.DecorationEndDate.Value : DateTime.MinValue;
            model.DecorationBond = entity.DecorationBond.HasValue ? entity.DecorationBond.Value : 0;
            model.QualityBond = entity.QualityBond.HasValue ? entity.QualityBond.Value : 0;
            model.ManageBond = entity.ManageBond.HasValue ? entity.ManageBond.Value : 0;
            model.BidBond = entity.BidBond.HasValue ? entity.BidBond.Value : 0;
            model.ParkingLotMemo = entity.ParkingLotMemo;
            model.ParkingLotNum = entity.ParkingLotNum.HasValue ? entity.ParkingLotNum.Value : 0;
            model.AdPointMemo = entity.AdPointMemo;
            model.AdPointNum = entity.AdPointNum.HasValue ? entity.AdPointNum.Value : 0;
            model.UnitInfo = new ContractUnitInfo();
            model.UnitInfo.ProjectID = entity.ProjectID;
            model.UnitInfo.ProjectName = entity.ProjectName;
            model.UnitInfo.ContractArea = entity.ContractArea;
            model.ContractUnitRent = entity.ContractUnitRent;
            model.ContractMgmtFee = entity.ContractMgmtFee;
            model.ConditionText = entity.ConditionText;
            //商铺列表
            model.UnitInfo.UnitList = new List<ProjectUnitListItem>();
            if (entity.UnitList != null)
            {
                entity.UnitList.ToList().ForEach(unit =>
                {
                    model.UnitInfo.UnitList.Add(new ProjectUnitListItem
                    {
                        BuildingName = unit.BuildingName,
                        FloorNumber = unit.FloorNumber,
                        UnitNumber = unit.UnitNumber,
                        UnitId = unit.UnitID,
                    });
                });
            }
            //品牌列表

            var query1 = from cb in Context.Contract_Brand
                         join mb in Context.Merchant_Brand on cb.MerchantBrandID equals mb.MerchantBrandID
                         join d in Context.Dictionary on mb.BrandType equals d.FieldValue
                         where cb.ContractID == entity.ContractID && d.FieldType == "BrandType"
                         select new
                         {
                             mb.BrandID,
                             mb.BrandName,
                             mb.BrandType,
                             BrandTypeName = d.FieldName
                         };

            model.BrandList = new List<MerchantBrandInfo>();
            if (query1.Any())
            {
                query1.ToList().ForEach(brand =>
                {
                    model.BrandList.Add(new MerchantBrandInfo
                    {
                        BrandID = brand.BrandID,
                        BrandName = brand.BrandName,
                        BrandType = brand.BrandTypeName,
                    });
                });
            }

            model.RentPaymentTerm = GetPaymentTermInfo(contractId, 1);
            model.MgmtPaymentTerm = GetPaymentTermInfo(contractId, 2);

            model.SupplementaryList = GetSupplementaryList(contractId);
            model.AppendixList = GetAppendixList(contractId);

            return model;
        }

        /// <summary>
        /// 获取合同铺位信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public ContractUnitInfo GetContractUnitInfo(int contractId)
        {
            var info = new ContractUnitInfo();
            var query = from c in Context.Contract
                        join p in Context.Project on c.ProjectID equals p.ProjectID
                        where c.Status == 1 && c.ContractID == contractId
                        select new
                        {
                            p.ProjectID,
                            p.ProjectName,
                            c.ContractID,
                            c.ContractArea,
                            c.ContractStatus,
                            UnitList = (from cu in Context.Contract_Unit
                                        join pu in Context.View_Project_Unit on cu.UnitID equals pu.UnitID
                                        where cu.ContractID == c.ContractID
                                        select new
                                        {
                                            pu.BuildingName,
                                            pu.FloorNumber,
                                            pu.UnitID,
                                            pu.UnitNumber,
                                        })
                        };

            var entity = query.FirstOrDefault();
            if (entity == null)
            {
                return info;
            }
            info.ProjectID = entity.ProjectID;
            info.ProjectName = entity.ProjectName;
            info.ContractID = entity.ContractID;
            info.ContractArea = entity.ContractArea;
            info.ContractStatus = entity.ContractStatus;
            //商铺列表
            info.UnitList = new List<ProjectUnitListItem>();
            if (entity.UnitList != null)
            {
                entity.UnitList.ToList().ForEach(unit =>
                {
                    info.UnitList.Add(new ProjectUnitListItem
                    {
                        BuildingName = unit.BuildingName,
                        FloorNumber = unit.FloorNumber,
                        UnitNumber = unit.UnitNumber,
                        UnitId = unit.UnitID,
                    });
                });
            }

            return info;
        }

        /// <summary>
        /// 获取合同信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public ContractInfo GetContractInfo(int contractId)
        {
            var info = new ContractInfo();
            var query = from c in Context.Contract
                        join d in Context.Dictionary on c.Condition equals d.FieldValue
                        where c.Status == 1 && c.ContractID == contractId && d.FieldType == "Condition"
                        select new
                        {
                            c.MerchantID,
                            c.ContractID,
                            c.ContractStatus,
                            c.SignerName,
                            c.SignerIDNumber,
                            c.OperatorName,
                            c.OperatorPhone,
                            c.SignerPhone,
                            c.ContractLength,
                            c.ContractStartDate,
                            c.ContractEndDate,
                            c.RentFreeStartDate,
                            c.RentFreeEndDate,
                            c.RentFreeLength,
                            c.AccessDate,
                            c.DecorationStartDate,
                            c.DecorationEndDate,
                            c.DecorationLength,
                            c.DecorationMgmtFee,
                            c.DecorationBond,
                            c.BidBond,
                            c.ManageBond,
                            c.QualityBond,
                            c.ParkingLotNum,
                            c.ParkingLotMemo,
                            c.AdPointNum,
                            c.AdPointMemo,
                            c.ContractArea,
                            c.ContractUnitRent,
                            c.ContractMgmtFee,
                            ConditionText = d.FieldName,
                        };

            var entity = query.FirstOrDefault();
            if (entity == null)
            {
                return info;
            }

            info.MerchantID = entity.MerchantID;
            info.ContractID = entity.ContractID;
            info.ContractStatus = entity.ContractStatus;
            info.ContractUnitRent = entity.ContractUnitRent;
            info.ContractMgmtFee = entity.ContractMgmtFee;
            info.SignerName = entity.SignerName;
            info.SignerIDNumber = entity.SignerIDNumber;
            info.SignerPhone = entity.SignerPhone;
            info.OperatorName = entity.OperatorName;
            info.OperatorPhone = entity.OperatorPhone;
            info.RentFreeLength = entity.RentFreeLength.HasValue ? entity.RentFreeLength.Value : 0;
            info.RentFreeStartDate = entity.RentFreeStartDate.HasValue ? entity.RentFreeStartDate.Value : DateTime.MinValue;
            info.RentFreeEndDate = entity.RentFreeEndDate.HasValue ? entity.RentFreeEndDate.Value : DateTime.MinValue;
            info.ContractLength = entity.ContractLength.HasValue ? entity.ContractLength.Value : 0;
            info.ContractStartDate = entity.ContractStartDate.HasValue ? entity.ContractStartDate.Value : DateTime.MinValue;
            info.ContractEndDate = entity.ContractEndDate.HasValue ? entity.ContractEndDate.Value : DateTime.MinValue;
            info.AccessDate = entity.AccessDate.HasValue ? entity.AccessDate.Value : DateTime.MinValue;
            info.DecorationLength = entity.DecorationLength.HasValue ? entity.DecorationLength.Value : 0;
            info.DecorationStartDate = entity.DecorationStartDate.HasValue ? entity.DecorationStartDate.Value : DateTime.MinValue;
            info.DecorationEndDate = entity.DecorationEndDate.HasValue ? entity.DecorationEndDate.Value : DateTime.MinValue;
            info.DecorationBond = entity.DecorationBond.HasValue ? entity.DecorationBond.Value : 0;
            info.QualityBond = entity.QualityBond.HasValue ? entity.QualityBond.Value : 0;
            info.ManageBond = entity.ManageBond.HasValue ? entity.ManageBond.Value : 0;
            info.BidBond = entity.BidBond.HasValue ? entity.BidBond.Value : 0;
            info.ParkingLotMemo = entity.ParkingLotMemo;
            info.ParkingLotNum = entity.ParkingLotNum.HasValue ? entity.ParkingLotNum.Value : 0;
            info.AdPointMemo = entity.AdPointMemo;
            info.AdPointNum = entity.AdPointNum.HasValue ? entity.AdPointNum.Value : 0;
            return info;
        }


        /// <summary>
        /// 获取支付条款
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="paymentItemId"></param>
        /// <returns></returns>
        public PaymentTermInfo GetPaymentTermInfo(int contractId, int paymentItemId)
        {
            var query = from p in Context.Contract_PaymentTerms
                        join c in Context.Contract on p.ContractID equals c.ContractID
                        where p.ContractID == contractId && p.PaymentItemID == paymentItemId
                        select new
                        {
                            c.ContractArea,
                            c.ContractStatus,
                            p.PaymentTermsID,
                            p.ContractID,
                            p.PaymentTermsType,
                            p.PaymentItemID,
                            p.PaymentPeriod,
                            p.DepositMonthly,
                            p.PaymentDailyAmount,
                            p.PaymentMonthlyAmount,
                            p.IncreaseExpression,
                            p.CommissionExpression,
                            PaymentTermsTypeText = (from d in Context.Dictionary
                                                    where d.FieldValue == p.PaymentTermsType && d.FieldType == "PaymentTermsType"
                                                    select d.FieldName).FirstOrDefault(),
                        };
            var entity = query.FirstOrDefault();
            var info = new PaymentTermInfo();
            if (entity == null)
                return info;

            info.PaymentTermsID = entity.PaymentTermsID;
            info.ContractID = entity.ContractID;
            info.ContractArea = entity.ContractArea;
            info.ContractStatus = entity.ContractStatus;
            info.PaymentTermsType = entity.PaymentTermsType;
            info.PaymentTermsTypeText = entity.PaymentTermsTypeText;
            info.PaymentItemID = entity.PaymentTermsID;
            info.PaymentPeriod = entity.PaymentPeriod;
            info.DepositMonthly = entity.DepositMonthly.HasValue ? entity.DepositMonthly.Value : 0;
            info.PaymentDailyAmount = entity.PaymentDailyAmount.HasValue ? entity.PaymentDailyAmount.Value : 0;
            info.PaymentMonthlyAmount = entity.PaymentMonthlyAmount.HasValue ? entity.PaymentMonthlyAmount.Value : 0;
            info.IncreaseExpression = entity.IncreaseExpression;
            info.CommissionExpression = entity.CommissionExpression;

            info.IncreaseInfo = new PaymentIncreaseInfo(entity.IncreaseExpression);
            info.CommissionInfo = new PaymentCommissionInfo(entity.CommissionExpression);

            return info;
        }

        /// <summary>
        /// 获取合同的交付标准和商户责任
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="specType"></param>
        /// <returns></returns>
        public ContractUnitSpecInfo GetContractUnitSpec(int contractId, int specType)
        {
            var query = from s in Context.Contract_UnitSpec
                        join c in Context.Contract on s.ContractID equals c.ContractID
                        where s.ContractID == contractId && s.SpecType == specType
                        select new
                        {
                            c.ContractStatus,
                            s.ContractID,
                            s.UnitID,
                            s.Floor,
                            s.Ceiling,
                            s.Wall,
                            s.Pillar,
                            s.FloorBearing,
                            s.WaterSupply,
                            s.WaterDrain,
                            s.Door,
                            s.Logo,
                            s.ElectricityUsage,
                            s.FireProtection,
                            s.Broadcasting,
                            s.AirCondition,
                            s.Smoke,
                            s.Security,
                            s.Wiring,
                            s.Gas,
                        };

            var entity = query.FirstOrDefault();
            var info = new ContractUnitSpecInfo();
            if (entity != null)
            {
                info.ContractID = entity.ContractID;
                info.ContractStatus = entity.ContractStatus;
                info.UnitID = entity.UnitID;
                info.Floor = entity.Floor;
                info.Ceiling = entity.Ceiling;
                info.Wall = entity.Wall;
                info.Pillar = entity.Pillar;
                info.FloorBearing = entity.FloorBearing;
                info.WaterSupply = entity.WaterSupply;
                info.WaterDrain = entity.WaterDrain;
                info.Door = entity.Door;
                info.Logo = entity.Logo;
                info.ElectricityUsage = entity.ElectricityUsage;
                info.FireProtection = entity.FireProtection;
                info.Broadcasting = entity.Broadcasting;
                info.AirCondition = entity.AirCondition;
                info.Smoke = entity.Smoke;
                info.Security = entity.Security;
                info.Wiring = entity.Wiring;
                info.Gas = entity.Gas;
            }
            return info;
        }

        #region 私有方法

        /// <summary>
        /// 获取合同附件列表
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        private List<AppendixInfo> GetAppendixList(int contractId)
        {
            var query = from a in Context.Contract_Appendix
                        where a.ContractID == contractId && a.Status == 1
                        select new
                        {
                            a.AppendixID,
                            a.ContractID,
                            a.AppendixName,
                            a.AppendixPath,
                        };
            var entityList = query.ToList();

            var list = new List<AppendixInfo>();
            if (entityList != null && entityList.Count > 0)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new AppendixInfo
                    {
                        ContractID = entity.ContractID,
                        AppendixID = entity.AppendixID,
                        AppendixName = entity.AppendixName,
                        AppendixPath = entity.AppendixPath,
                    });
                });
            }

            return list;
        }

        /// <summary>
        /// 获取补充协议列表
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        private List<SupplementaryInfo> GetSupplementaryList(int contractId)
        {
            var query = from a in Context.Contract_Supplementary
                        where a.ContractID == contractId && a.Status == 1
                        select new
                        {
                            a.ContractID,
                            a.SupplementaryID,
                            a.SupplementaryStartDate,
                            a.SupplementaryEndDate,
                            a.SupplementaryContent,
                            a.SupplementaryType,
                            a.SignerName,
                            a.SignerPhone,
                            a.SignerIDNumber,
                        };
            var entityList = query.ToList();

            var list = new List<SupplementaryInfo>();
            if (entityList != null && entityList.Count > 0)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new SupplementaryInfo
                    {
                        ContractID = entity.ContractID,
                        SupplementaryID = entity.SupplementaryID,
                        SupplementaryStartDate = entity.SupplementaryStartDate,
                        SupplementaryEndDate = entity.SupplementaryEndDate,
                        SupplementaryContent = entity.SupplementaryContent,
                        SignerName = entity.SignerName,
                        SignerPhone = entity.SignerPhone,
                        SignerIDNumber = entity.SignerIDNumber,
                    });
                });
            }

            return list;
        }

        #endregion
    }
}
