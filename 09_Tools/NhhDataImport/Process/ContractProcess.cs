using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NhhDataImport.Entity;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 商铺合约
    /// </summary>
    public class ContractProcess : BaseEntityProcess<ContractEntity>
    {
        public ContractProcess()
        {
            SheetName = "商铺合约";
            HeaderRowNum = 2;
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected override ContractEntity ToEntity(IRow row)
        {
            var entity = new ContractEntity();
            entity.ContractType = 1;
            entity.ContractStatus = 1;
            entity.MerchantStore = new MerchantStoreEntity();
            entity.UnitList = new List<ContractUnitEntity>();
            entity.UnitSpec = new ContractUnitSpecEntity();
            entity.UnitSpec.SpecType = 1;
            entity.StoreSpec = new ContractUnitSpecEntity();
            entity.StoreSpec.SpecType = 2;
            entity.RentPaymentTerms = new ContractPaymentTermsEntity();
            entity.RentPaymentTerms.PaymentItemID = 1;
            entity.MgmtPaymentTerms = new ContractPaymentTermsEntity();
            entity.MgmtPaymentTerms.PaymentItemID = 2;
            entity.AppendixList = new List<ContractAppendixEntity>();
            entity.MerchantBrandList = new List<MerchantBrandEntity>();
            entity.ContractBrandList = new List<ContractBrandEntity>();

            //合给基本信息
            entity.ContractCode = GetValue<string>(row, 0);
            entity.MerchantName = GetValue<string>(row, 1);
            entity.BrandNames = GetValue<string>(row, 2);
            entity.ProjectName = GetValue<string>(row, 3);
            entity.BuildingName = GetValue<string>(row, 4);
            entity.UnitNumbers = GetValue<string>(row, 5);
            entity.ContractArea = GetValue<decimal>(row, 6);

            //基本信息
            entity.SignerName = GetValue<string>(row, 7);
            entity.SignerIDNumber = GetValue<string>(row, 8);
            entity.SignerPhone = GetValue<string>(row, 9);

            entity.BrandTypeName = GetValue<string>(row, 10);
            entity.MerchantStore.BizType = GetValue<string>(row, 11);
            entity.MerchantStore.BizCategory = GetValue<string>(row, 12);
            entity.ConditionText = GetValue<string>(row, 14);
            //计租方式
            entity.RentPaymentTerms.PaymentTermsType1 = GetValue<string>(row, 15);
            entity.RentPaymentTerms.PaymentTermsType2 = GetValue<string>(row, 16);
            //租押方式
            entity.RentPaymentTerms.DepositMonthly = GetValue<int>(row, 17);
            entity.RentPaymentTerms.PaymentPeriod = GetValue<int>(row, 18);
            //经营免租期
            entity.RentFreeLength = GetValue<int>(row, 19);
            //签约租金
            entity.RentPaymentTerms.PaymentMonthlyAmount = GetValue<decimal>(row, 20);
            entity.RentPaymentTerms.PaymentDailyAmount = GetValue<decimal>(row, 21);
            //递增方式
            string IncreaseExpression = string.Format("{0};{1}", GetValue<string>(row, 22), GetValue<string>(row, 23));
            entity.RentPaymentTerms.IncreaseExpression = IncreaseExpression;
            //签约扣点
            entity.RentPaymentTerms.CommissionTypeText = GetValue<string>(row, 24);
            entity.RentPaymentTerms.CommissionWithTax = GetValue<string>(row, 25) == "是" ? 1 : 0;
            entity.RentPaymentTerms.CommissionExpression = GetValue<string>(row, 26);            
            //签约物业费
            entity.MgmtPaymentTerms.PaymentMonthlyAmount = GetValue<decimal>(row, 27);
            entity.MgmtPaymentTerms.PaymentDailyAmount = GetValue<decimal>(row, 28);
            //合同期限
            entity.ContractLength = GetValue<int>(row, 29) * 365;
            entity.ContractStartDate = GetValue<DateTime>(row, 30);
            entity.ContractEndDate = GetValue<DateTime>(row, 31);
            //进场日期
            entity.AccessDate = GetValue<DateTime>(row, 32);
            //装修期限
            entity.DecorationLength = GetValue<int>(row, 33);
            entity.DecorationStartDate = GetValue<DateTime>(row, 34);
            entity.DecorationEndDate = GetValue<DateTime>(row, 35);
            //履约保证金
            entity.BidBond = GetValue<decimal>(row, 36);
            //物业费保证金
            entity.ManageBond = GetValue<decimal>(row, 37);
            //装修管理费
            entity.DecorationMgmtFee = GetValue<decimal>(row, 38);
            //装修保证金
            entity.DecorationBond = GetValue<decimal>(row, 39);
            //质量保证金
            entity.QualityBond = GetValue<decimal>(row, 40);
            //免费停车位
            entity.ParkingLotNum = GetValue<int>(row, 41);
            //免费广告位
            entity.AdPointNum = GetValue<int>(row, 42);
            //扫描件附件
            entity.AttachmentList = GetValue<string>(row, 43);

            entity.OperatorName = "";
            entity.OperatorPhone = "";
            //签订补充协议
            if (GetValue<string>(row, 44) == "是")
            {
                entity.Supplementary = new ContractSupplementaryEntity();
                entity.Supplementary.SupplementaryType = 1;
                entity.Supplementary.SignerName = entity.SignerName;
                entity.Supplementary.SignerIDNumber = entity.SignerIDNumber;
                entity.Supplementary.SignerPhone = entity.SignerPhone;
                entity.Supplementary.OperatorName = entity.OperatorName;
                entity.Supplementary.OperatorPhone = entity.OperatorPhone;
                entity.Supplementary.SupplementaryStartDate = entity.ContractStartDate.Value;
                entity.Supplementary.SupplementaryEndDate = entity.ContractEndDate.Value;
                entity.Supplementary.SupplementaryContent = GetValue<string>(row, 45);
            }


            entity.MerchantStore.RentStartDate = entity.ContractStartDate.HasValue ? entity.ContractStartDate.Value : new DateTime(1900, 1, 1);
            entity.MerchantStore.RentEndDate = entity.ContractEndDate.HasValue ? entity.ContractEndDate.Value : DateTime.Now;
            entity.MerchantStore.RentLength = entity.ContractLength.HasValue ? entity.ContractLength.Value : 0;


            entity.RentFreeStartDate = entity.ContractStartDate;
            entity.RentFreeEndDate = entity.ContractStartDate.Value.AddMonths(entity.RentFreeLength.Value);
            return entity;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(ContractEntity entity)
        {
            if (string.IsNullOrEmpty(entity.ContractCode) || entity.ContractCode.Length == 0)
            {
                throw new NhhException("请输入合约编号");
            }

            if (string.IsNullOrEmpty(entity.MerchantName) || entity.MerchantName.Length == 0)
            {
                throw new NhhException("请输入商户名称");
            }

            if (string.IsNullOrEmpty(entity.BrandNames) || entity.BrandNames.Length == 0)
            {
                throw new NhhException("请输入品牌名称");
            }

            if (string.IsNullOrEmpty(entity.ProjectName) || entity.ProjectName.Length == 0)
            {
                throw new NhhException("请输入项目名称");
            }

            if (string.IsNullOrEmpty(entity.BuildingName) || entity.BuildingName.Length == 0)
            {
                throw new NhhException("请输入楼宇名称");
            }

            if (string.IsNullOrEmpty(entity.UnitNumbers) || entity.UnitNumbers.Length == 0)
            {
                throw new NhhException("请输入铺位号");
            }

            if (entity.ContractArea <= 0)
            {
                throw new NhhException("请输入计租面积");
            }

            if (string.IsNullOrEmpty(entity.SignerName) || entity.SignerName.Length == 0)
            {
                entity.SignerName = "";
                //throw new NhhException("请输入签约人");
            }

            if (string.IsNullOrEmpty(entity.SignerIDNumber) || entity.SignerIDNumber.Length == 0)
            {
                entity.SignerIDNumber = "";
                //throw new NhhException("请输入签约人证件号");
            }

            if (string.IsNullOrEmpty(entity.BrandTypeName) || entity.BrandTypeName.Length == 0)
            {
                throw new NhhException("请输入经营形式");
            }

            if (string.IsNullOrEmpty(entity.MerchantStore.BizType) || entity.MerchantStore.BizType.Length == 0)
            {
                throw new NhhException("请输入经营业态");
            }

            if (string.IsNullOrEmpty(entity.MerchantStore.BizCategory) || entity.MerchantStore.BizCategory.Length == 0)
            {
                throw new NhhException("请输入经营品牌");
            }

            if (string.IsNullOrEmpty(entity.ConditionText) || entity.ConditionText.Length == 0)
            {
                throw new NhhException("请输入交付标准");
            }

            if (entity.RentPaymentTerms.DepositMonthly < 0)
            {
                throw new NhhException("请输入租押方式-押");
            }

            if (entity.RentPaymentTerms.PaymentPeriod <= 0)
            {
                throw new NhhException("请输入租押方式-付");
            }

            if (entity.RentFreeLength < 0)
            {
                throw new NhhException("经营免租期无效");
            }

            if (entity.RentPaymentTerms.PaymentMonthlyAmount < 0 ||
                entity.RentPaymentTerms.PaymentDailyAmount < 0)
            {
                throw new NhhException("请输入租约租金");
            }

            if (entity.MgmtPaymentTerms.PaymentDailyAmount < 0 ||
                entity.MgmtPaymentTerms.PaymentMonthlyAmount < 0)
            {
                throw new NhhException("请输入签约物业费");
            }

            if (entity.ContractLength <= 0 || !entity.ContractStartDate.HasValue)
            {
                throw new NhhException("请输入合同期限");
            }

            if (!entity.AccessDate.HasValue)
            {
                throw new NhhException("进场日期无效");
            }

            if (entity.DecorationLength < 0 ||
                !entity.DecorationStartDate.HasValue)
            {
                throw new NhhException("装修期限无效");
            }

            if (entity.BidBond < 0)
            {
                throw new NhhException("履约保证金无效");
            }

            if (entity.ManageBond < 0)
            {
                throw new NhhException("物业费保证金无效");
            }

            if (entity.DecorationMgmtFee < 0)
            {
                throw new NhhException("装修管理费无效");
            }

            if (entity.DecorationBond < 0)
            {
                throw new NhhException("装修保证金无效");
            }

            if (entity.QualityBond < 0)
            {
                throw new NhhException("质量保证金无效");
            }

            entity.MerchantID = MerchantUtil.GetMerchantId(entity.MerchantName);
            if (entity.MerchantID <= 0)
            {
                throw new NhhException("商户名称无效，" + entity.MerchantName);
            }

            int BrandType = BrandUtil.GetBrandType(entity.BrandTypeName);
            if (BrandType <= 0)
            {
                throw new NhhException("经营形式无效，" + entity.BrandTypeName);
            }

            string[] brandNames = entity.BrandNames.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string brandName in brandNames)
            {
                int brandId = BrandUtil.GetBrandId(brandName);
                if (brandId <= 0)
                {
                    throw new NhhException(brandName + " 品牌名称无效");
                }

                var merchantBrand = new MerchantBrandEntity
                {
                    MerchantID = entity.MerchantID,
                    BrandName = brandName,
                    BrandType = BrandType,
                    BrandID = brandId,
                };
                entity.MerchantBrandList.Add(merchantBrand);
            }


            entity.ProjectID = ProjectUtil.GetProjectId(entity.ProjectName);
            if (entity.ProjectID <= 0)
            {
                throw new NhhException("项目名称无效，" + entity.ProjectName);
            }
            var projectInfo = ProjectUtil.GetProjectInfo(entity.ProjectID);
            entity.ManageCompanyID = projectInfo.ManageCompanyID;

            entity.BuildingID = ProjectUtil.GetBuildingId(entity.ProjectName, entity.BuildingName);
            if (entity.BuildingID <= 0)
            {
                throw new NhhException("楼宇名称无效，" + entity.BuildingName);
            }

            entity.UnitIdList = new List<int>();
            string[] unitList = entity.UnitNumbers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string unitNumber in unitList)
            {
                int unitId = ProjectUtil.GetUnitId(entity.BuildingID, unitNumber);
                if (unitId <= 0)
                {
                    throw new NhhException(string.Format("{0} {1} 铺位号无效", entity.BuildingName, unitNumber));
                }
                entity.UnitIdList.Add(unitId);
            }


            entity.MerchantStore.BizTypeID = CommonUtil.GetBizTypeId(entity.MerchantStore.BizType);
            if (entity.MerchantStore.BizTypeID <= 0)
            {
                throw new NhhException("经营业态无效，" + entity.MerchantStore.BizType);
            }

            entity.MerchantStore.BizCategoryID = CommonUtil.GetBizCategoryId(entity.MerchantStore.BizCategory);
            if (entity.MerchantStore.BizCategoryID <= 0)
            {
                throw new NhhException("经营品类无效，" + entity.MerchantStore.BizCategory);
            }

            entity.Condition = CommonUtil.GetDictionaryValue("Condition", entity.ConditionText);
            if (entity.Condition <= 0)
            {
                throw new NhhException("交付标准无效，" + entity.ConditionText);
            }

            entity.MerchantStore.MerchantID = entity.MerchantID;

            entity.ContractStatus = 2;
            entity.RentPaymentTerms.CommissionExpression = string.Format("{0}#{1}#{2}", entity.RentPaymentTerms.CommissionTypeText, entity.RentPaymentTerms.CommissionWithTax, entity.RentPaymentTerms.CommissionExpression);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(ContractEntity entity)
        {
            string strCmd = string.Format(@"SELECT COUNT(0) as Number
                                          FROM [dbo].[Contract](Nolock)
                                          Where ContractCode='{0}'", entity.ContractCode);
            return SqlHelper.ExecuteScalar(strCmd) > 0;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(ContractEntity entity)
        {
            //商户品牌
            MerchantBrandProcess.SaveMerchantBrand(entity);

            #region Contract
            entity.ContractUnitRent = entity.RentPaymentTerms.PaymentDailyAmount.Value;
            entity.ContractMgmtFee = entity.MgmtPaymentTerms.PaymentDailyAmount.Value;
            string strCmd1 = @"INSERT INTO [dbo].[Contract]
                                   ([ContractCode]
                                   ,[ContractType]
                                   ,[ContractStatus]
                                   ,[ProjectID]
                                   ,[ContractArea]
                                   ,[ContractUnitRent]
                                   ,[ContractMgmtFee]
                                   ,[MerchantID]
                                   ,[SignerName]
                                   ,[SignerIDNumber]
                                   ,[SignerPhone]
                                   ,[ManageCompanyID]
                                   ,[OperatorName]
                                   ,[OperatorPhone]
                                   ,[ContractLength]
                                   ,[ContractStartDate]
                                   ,[ContractEndDate]
                                   ,[RentFreeLength]
                                   ,[RentFreeStartDate]
                                   ,[RentFreeEndDate]
                                   ,[AccessDate]
                                   ,[DecorationLength]
                                   ,[DecorationStartDate]
                                   ,[DecorationEndDate]
                                   ,[DecorationMgmtFee]
                                   ,[DecorationBond]
                                   ,[Condition]
                                   ,[BidBond]
                                   ,[ManageBond]
                                   ,[QualityBond]
                                   ,[ParkingLotNum]
                                   ,[ParkingLotMemo]
                                   ,[AdPointNum]
                                   ,[AdPointMemo]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@ContractCode
                                   ,@ContractType
                                   ,@ContractStatus
                                   ,@ProjectID
                                   ,@ContractArea
                                   ,@ContractUnitRent
                                   ,@ContractMgmtFee
                                   ,@MerchantID
                                   ,@SignerName
                                   ,@SignerIDNumber
                                   ,@SignerPhone
                                   ,@ManageCompanyID
                                   ,@OperatorName
                                   ,@OperatorPhone
                                   ,@ContractLength
                                   ,@ContractStartDate
                                   ,@ContractEndDate
                                   ,@RentFreeLength
                                   ,@RentFreeStartDate
                                   ,@RentFreeEndDate
                                   ,@AccessDate
                                   ,@DecorationLength
                                   ,@DecorationStartDate
                                   ,@DecorationEndDate
                                   ,@DecorationMgmtFee
                                   ,@DecorationBond
                                   ,@Condition
                                   ,@BidBond
                                   ,@ManageBond
                                   ,@QualityBond
                                   ,@ParkingLotNum
                                   ,@ParkingLotMemo
                                   ,@AdPointNum
                                   ,@AdPointMemo
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser);
                            Select SCOPE_IDENTITY();";
            var paramList1 = new SqlParameter[]
            {
                new SqlParameter("@ContractCode", entity.ContractCode),
                new SqlParameter("@ContractType", entity.ContractType),
                new SqlParameter("@ContractStatus", entity.ContractStatus),
                new SqlParameter("@ProjectID", entity.ProjectID),
                new SqlParameter("@ContractArea", entity.ContractArea),
                new SqlParameter("@ContractUnitRent", entity.ContractUnitRent),
                new SqlParameter("@ContractMgmtFee", entity.ContractMgmtFee),
                new SqlParameter("@MerchantID", entity.MerchantID),
                new SqlParameter("@SignerName", entity.SignerName),
                new SqlParameter("@SignerIDNumber", entity.SignerIDNumber),
                new SqlParameter("@SignerPhone", entity.SignerPhone),
                new SqlParameter("@ManageCompanyID", entity.ManageCompanyID),
                new SqlParameter("@OperatorName", entity.OperatorName),
                new SqlParameter("@OperatorPhone", entity.OperatorPhone),
                new SqlParameter("@ContractLength", entity.ContractLength),
                new SqlParameter("@ContractStartDate", entity.ContractStartDate),
                new SqlParameter("@ContractEndDate", entity.ContractEndDate),
                new SqlParameter("@RentFreeLength", entity.RentFreeLength),
                new SqlParameter("@RentFreeStartDate", entity.RentFreeStartDate),
                new SqlParameter("@RentFreeEndDate", entity.RentFreeEndDate),
                new SqlParameter("@AccessDate", entity.AccessDate),
                new SqlParameter("@DecorationLength", entity.DecorationLength),
                new SqlParameter("@DecorationStartDate", entity.DecorationStartDate),
                new SqlParameter("@DecorationEndDate", entity.DecorationEndDate),
                new SqlParameter("@DecorationMgmtFee", entity.DecorationMgmtFee),
                new SqlParameter("@DecorationBond", entity.DecorationBond),
                new SqlParameter("@Condition", entity.Condition),
                new SqlParameter("@BidBond", entity.BidBond),
                new SqlParameter("@ManageBond", entity.ManageBond),
                new SqlParameter("@QualityBond", entity.QualityBond),
                new SqlParameter("@ParkingLotNum", entity.ParkingLotNum),
                new SqlParameter("@ParkingLotMemo", entity.ParkingLotMemo),
                new SqlParameter("@AdPointNum", entity.AdPointNum),
                new SqlParameter("@AdPointMemo", entity.AdPointMemo),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };
            #endregion

            using (var conn = SqlHelper.GetConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                var trans = conn.BeginTransaction("Contract");

                entity.ContractID = SqlHelper.ExecuteScalar(trans, strCmd1, paramList1);
                //商户商铺
                MerchantStoreProcess.SaveMerchantStore(trans, entity);
                //合同品牌
                ContractBrandProcess.SaveContractBrand(trans, entity);
                //合同商铺
                ContractUnitProcess.SaveContractUnit(trans, entity);
                //交付标准
                ContractUnitSpecProcess.SaveContractUnitSpec(trans, entity);
                //支付条款
                ContractPaymentTermsProcess.SaveContractPaymentTerms(trans, entity);
                //补充协议
                ContractSupplementaryProcess.SaveSupplementary(trans, entity);

                trans.Commit();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

            //合同附件
            new ContractAppendixProcess().ProcessPicture(entity, _worker);
        }
    }
}
