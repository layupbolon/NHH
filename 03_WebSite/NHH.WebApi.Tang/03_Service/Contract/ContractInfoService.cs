using System.Security.Cryptography;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Contract;
using NHH.Models.Merchant;
using NHH.Service.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ContractListModel GetContractList(ContractListQuery queryInfo)
        {
            var model = new ContractListModel();
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page;
            model.PagingInfo.PageSize = queryInfo.Size;
            model.ContractList = new List<ContractInfo>();
            #region Linq查询

            var query = from c in Context.Contract 
                        where c.MerchantID == queryInfo.MerchantID
                        select new
                {
                    c.ContractID,
                    c.ContractType,
                    c.ContractCode,
                    c.ContractStatus,
                    //b.BrandName,
                    c.ContractStartDate,
                    c.ContractEndDate,
                    //c.MerchantBrandID,
                    UnitList = (from cu in Context.Contract_Unit
                                        join pu in Context.View_Project_Unit on cu.UnitID equals pu.UnitID
                                        where cu.ContractID == c.ContractID
                                        select new
                                        {
                                            pu.UnitNumber
                                        }),
                    BrandList = (from cb in Context.Contract_Brand 
                                     join mb in Context.Merchant_Brand
                                         on cb.MerchantBrandID equals mb.MerchantBrandID into t_mb from mb in t_mb.DefaultIfEmpty()
                                     join b in Context.Brand 
                                         on mb.BrandID equals b.BrandID into t_b from b in t_b.DefaultIfEmpty()
                                     where cb.ContractID == c.ContractID
                                     select new
                                     {
                                         b.BrandName
                                     })
                };
            #endregion
            #region 查询条件
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(item => item.ContractStatus == queryInfo.Status.Value);
            }
            //if (queryInfo.BeginTime.HasValue)
            //{
            //    query = query.Where(item => item.ContractStartDate <= queryInfo.BeginTime.Value);
            //}
            //if (queryInfo.EndTime.HasValue)
            //{
            //    query = query.Where(item => item.ContractEndDate >= queryInfo.EndTime.Value);
            //}
            #endregion
            model.PagingInfo.TotalCount = query.Count();
            switch (queryInfo.Sort)
            {
                case 1:
                    query = query.OrderBy(item => item.ContractEndDate).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
                case 2:
                    query = query.OrderBy(item => item.ContractStatus).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
                default:
                    query = query.OrderBy(item => item.ContractEndDate).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
            }
            
            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new ContractInfo();

                    contract.ContractID = entity.ContractID;
                    contract.ContractType = entity.ContractType;
                    contract.ContractCode = entity.ContractCode;
                    contract.ContractStatus = entity.ContractStatus;
                    contract.ContractStartDate =
                        entity.ContractStartDate.HasValue ? entity.ContractStartDate.Value : DateTime.MinValue;
                    contract.ContractEndDate =
                        entity.ContractEndDate.HasValue ? entity.ContractEndDate.Value : DateTime.MinValue;
                    contract.UnitNumbers = string.Join(",", entity.UnitList);
                    if (entity.BrandList != null)
                    {
                        contract.BrandList = new List<string>();
                        entity.BrandList.ToList().ForEach(item => contract.BrandList.Add(item.BrandName));
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
        public ContractInfo GetContractDetail(int contractId)
        {
            var model = new ContractInfo();
            var query = from c in Context.Contract
                        where c.ContractID == contractId && c.Status == 1
                //join oc in Context.Company
                //    on c.OwnerCompanyID equals oc.CompanyID
                //join mc in Context.Company
                //    on c.ManageCompanyID equals mc.CompanyID
                select new
                {
                    c.ContractID,
                    c.ContractType,
                    c.ContractCode,
                    c.ContractStatus,
                    c.ContractArea,
                    c.ContractUnitRent,
                    c.ContractMgmtFee,
                    //c.MerchantBrandID,
                    //b.BrandName,
                    c.SignerName,
                    c.SignerIDNumber,
                    c.SignerPhone,
                    //c.OwnerCompanyID,
                    //OwnerCompany = oc.CompanyName,
                    //c.ManageCompanyID,
                    //ManageCompany = mc.CompanyName,
                    c.ContractStartDate,
                    c.ContractEndDate,
                    c.RentFreeStartDate,
                    c.RentFreeEndDate,
                    c.DecorationStartDate,
                    c.DecorationEndDate,
                    UnitList = (from cu in Context.Contract_Unit
                        join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                        join pb in Context.Project_Building on pu.BuildingID equals pb.BuildingID
                        join pf in Context.Project_Floor on pu.FloorID equals pf.FloorID
                        where cu.ContractID == c.ContractID
                        select new
                        {
                            pu.UnitID,
                            UnitNumber = pb.BuildingName+" "+pf.FloorName+" "+ pu.UnitNumber,
                        }),
                    ContractPaymentTermsList = (from cpt in Context.Contract_PaymentTerms
                        join fi in Context.Finance_Item on cpt.PaymentItemID equals fi.ItemID
                        where cpt.ContractID == c.ContractID && cpt.Status == 1
                        select new
                        {
                            cpt.PaymentTermsType,
                            cpt.PaymentItemID,
                            fi.ItemName,
                            cpt.PaymentPeriod
                        }),
                    ContractUnitSpecList = (from cus in Context.Contract_UnitSpec
                        where cus.ContractID == c.ContractID && cus.Status == 1
                        select new
                        {
                            cus.UnitSpecID,
                            cus.UnitID,
                            cus.SpecType,
                            cus.Floor,
                            cus.Ceiling,
                            cus.Wall,
                            cus.Pillar,
                            cus.FloorBearing,
                            cus.WaterSupply,
                            cus.WaterDrain,
                            cus.Door,
                            cus.Logo,
                            cus.ElectricityUsage,
                            cus.FireProtection,
                            cus.Broadcasting,
                            cus.AirCondition,
                            cus.Smoke,
                            cus.Security,
                            cus.Wiring,
                            cus.Water,
                            cus.Gas
                        }),
                    ContractSupplementaryList = (from cs in Context.Contract_Supplementary
                        where cs.ContractID == c.ContractID && cs.Status == 1
                        select new
                        {
                            cs.SupplementaryID,
                            cs.SupplementaryType,
                            cs.SignerName,
                            cs.SignerIDNumber,
                            cs.SignerPhone,
                            cs.SupplementaryContent,
                            cs.SupplementaryStartDate,
                            cs.SupplementaryEndDate
                        }),
                    ContractAppendixList = (from ca in Context.Contract_Appendix
                        where ca.ContractID == c.ContractID && ca.Status == 1
                        select new
                        {
                            ca.AppendixID,
                            ca.AppendixType,
                            ca.AppendixTemplate,
                            ca.AppendixName,
                            ca.AppendixPath
                        }),
                    BrandList = (from cb in Context.Contract_Brand
                                 join mb in Context.Merchant_Brand
                                     on cb.MerchantBrandID equals mb.MerchantBrandID into t_mb
                                 from mb in t_mb.DefaultIfEmpty()
                                 join b in Context.Brand
                                     on mb.BrandID equals b.BrandID into t_b
                                 from b in t_b.DefaultIfEmpty()
                                 where cb.ContractID == c.ContractID
                                 select new
                                 {
                                     b.BrandName
                                 })
                };

            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.ContractID = entity.ContractID;
                model.ContractType = entity.ContractType;
                model.ContractCode = entity.ContractCode;
                model.ContractStatus = entity.ContractStatus;
                model.ContractArea = entity.ContractArea;
                model.ContractUnitRent = entity.ContractUnitRent;
                model.ContractMgmtFee = entity.ContractMgmtFee;
                //model.MerchantBrandID = entity.MerchantBrandID;
                //model.BrandName = entity.BrandName;
                model.SignerName = entity.SignerName;
                model.SignerIDNumber = entity.SignerIDNumber;
                model.SignerPhone = entity.SignerPhone;
                //model.OwnerCompanyID = entity.OwnerCompanyID;
                //model.OwnerCompany = entity.OwnerCompany;
                //model.ManageCompanyID = entity.ManageCompanyID;
                //model.ManageCompany = entity.ManageCompany;
                model.ContractStartDate = entity.ContractStartDate;
                model.ContractEndDate = entity.ContractEndDate;
                model.RentFreeStartDate = entity.RentFreeStartDate;
                model.RentFreeEndDate = entity.RentFreeEndDate;
                model.DecorationStartDate = entity.DecorationStartDate;
                model.DecorationEndDate = entity.DecorationEndDate;

                //合约铺位列表
                if (entity.UnitList != null)
                {
                    model.UnitList = new List<ProjectUnitInfo>();
                    entity.UnitList.ToList().ForEach(unit => model.UnitList.Add(new ProjectUnitInfo
                    {
                        UnitID = unit.UnitID,
                        UnitNumber = unit.UnitNumber,
                    }));
                }
                //支付条款列表
                if (entity.ContractPaymentTermsList != null)
                {
                    model.ContractPaymentTermsInfoList = new List<ContractPaymentTermsInfo>();
                    entity.ContractPaymentTermsList.ToList()
                        .ForEach(item => model.ContractPaymentTermsInfoList.Add(new ContractPaymentTermsInfo
                        {
                            PaymentItemID = item.PaymentItemID,
                            PaymentItemName = item.ItemName,
                            PaymentPeriod = item.PaymentPeriod,
                            PaymentTermsType = item.PaymentTermsType
                        }));
                }
                //交付标准&商户责任列表
                if (entity.ContractUnitSpecList != null)
                {
                    model.ContractUnitSpecList = new List<ContractUnitSpecInfo>();
                    entity.ContractUnitSpecList.ToList()
                        .ForEach(item => model.ContractUnitSpecList.Add(new ContractUnitSpecInfo
                        {
                            UnitID = item.UnitID,
                            SpecType = item.SpecType,
                            Floor = item.Floor,
                            Ceiling = item.Ceiling,
                            Wall = item.Wall,
                            Pillar = item.Pillar,
                            FloorBearing = item.FloorBearing,
                            WaterSupply = item.WaterSupply,
                            WaterDrain = item.WaterDrain,
                            Door = item.Door,
                            Logo = item.Logo,
                            ElectricityUsage = item.ElectricityUsage,
                            FireProtection = item.FireProtection,
                            Broadcasting = item.Broadcasting,
                            AirCondition = item.AirCondition,
                            Smoke = item.Smoke,
                            Security = item.Security,
                            Wiring = item.Wiring,
                            Water = item.Water,
                            Gas = item.Gas
                        }));
                }
                //补充协议列表
                if (entity.ContractSupplementaryList != null)
                {
                    model.ContractSupplementaryList=new List<ContractSupplementaryInfo>();
                    entity.ContractSupplementaryList.ToList()
                        .ForEach(item => model.ContractSupplementaryList.Add(new ContractSupplementaryInfo
                        {
                            SupplementaryID = item.SupplementaryID,
                            SupplementaryType = item.SupplementaryType,
                            SignerName = item.SignerName,
                            SignerIDNumber = item.SignerIDNumber,
                            SignerPhone = item.SignerPhone,
                            SupplementaryContent = item.SupplementaryContent,
                            SupplementaryStartDate = item.SupplementaryStartDate,
                            SupplementaryEndDate = item.SupplementaryEndDate
                        }));
                }
                //合约附件列表
                if (entity.ContractAppendixList != null)
                {
                    model.ContractAppendixList=new List<ContractAppendixInfo>();
                    entity.ContractAppendixList.ToList()
                        .ForEach(item => model.ContractAppendixList.Add(new ContractAppendixInfo
                        {
                            AppendixID = item.AppendixID,
                            AppendixType = item.AppendixType,
                            AppendixTemplate = item.AppendixTemplate,
                            AppendixName = item.AppendixName,
                            AppendixPath = item.AppendixPath
                        }));
                }
                //品牌列表
                if (entity.BrandList != null)
                {
                    model.BrandList = new List<string>();
                    entity.BrandList.ToList().ForEach(item => model.BrandList.Add(item.BrandName));
                }
            }

            return model;
        }

        ///// <summary>
        ///// 作废指定合约（合约状态必须为1【待签约】才可作废）
        ///// </summary>
        ///// <param name="contractId"></param>
        ///// <returns></returns>
        //public bool CancelContract(int contractId)
        //{
        //    var instance = CreateBizObject<NHH.Entities.Contract>();
        //    var entity = instance.GetBySysNo(contractId);
        //    if (entity != null && entity.ContractStatus==1)
        //    {
        //        entity.Status = -1;

        //        instance.Update(entity);
        //        this.SaveChanges();
        //        return  true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 添加租赁合约附件
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public bool AddContractAppendix(ContractAppendixInfo model)
        //{
        //    var instance = CreateBizObject<Contract_Appendix>();
        //    var entity = new Contract_Appendix()
        //    {
        //        ContractID = model.ContractID,
        //        AppendixType = model.AppendixType,
        //        AppendixTemplate = model.AppendixTemplate,
        //        AppendixName = model.AppendixName,
        //        AppendixPath = model.AppendixPath,
        //        Status = 1,
        //        InDate = DateTime.Now,
        //        InUser = 1,
        //        EditDate = DateTime.Now,
        //        EditUser = 1
        //    };
        //    instance.Insert(entity);
        //    this.SaveChanges();
        //    model.AppendixID = entity.AppendixID;
        //    return model.AppendixID > 0;
        //}

        /// <summary>
        /// 查询指定的租赁合约附件
        /// </summary>
        /// <param name="appendixId"></param>
        /// <returns></returns>
        public ContractAppendixInfo GetContractAppendix(int appendixId)
        {
            ContractAppendixInfo model = null;
            var query = from ca in Context.Contract_Appendix
                where ca.AppendixID == appendixId && ca.Status == 1
                select new
                {
                    ca.AppendixID,
                    ca.ContractID,
                    ca.AppendixType,
                    ca.AppendixTemplate,
                    ca.AppendixName,
                    ca.AppendixPath
                };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model=new ContractAppendixInfo();
                model.AppendixID = entity.AppendixID;
                model.ContractID = entity.ContractID;
                model.AppendixType = entity.AppendixType;
                model.AppendixTemplate = entity.AppendixTemplate;
                model.AppendixName = entity.AppendixName;
                model.AppendixPath = entity.AppendixPath;
            }
            return model;
        }

        ///// <summary>
        ///// 作废租赁合约附件
        ///// </summary>
        ///// <param name="appendixId"></param>
        ///// <returns></returns>
        //public bool CancelContractAppendix(int appendixId)
        //{
        //    var instance = CreateBizObject<NHH.Entities.Contract_Appendix>();
        //    var entity = instance.GetBySysNo(appendixId);
        //    if (entity != null)
        //    {
        //        entity.Status = -1;

        //        instance.Update(entity);
        //        this.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}
    }
}
