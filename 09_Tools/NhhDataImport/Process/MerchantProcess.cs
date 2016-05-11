using NhhDataImport.Entity;
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
    /// 商户
    /// </summary>
    public class MerchantProcess : BaseDataProcess<MerchantEntity>
    {
        public MerchantProcess()
        {
            SheetName = "商户";
        }

        protected override MerchantEntity ToEntity(DataRow row)
        {
            var entity = new MerchantEntity();
            entity.MerchantType = GetValue<string>(row, "商户类型") == "自然人" ? 2 : 1;
            entity.MerchantCode = GetValue<string>(row, "商户编码");
            entity.BriefName = GetValue<string>(row, "商户简称");
            entity.MerchantName = GetValue<string>(row, "商户名称");
            entity.ProvinceName = GetValue<string>(row, "所在省份");
            entity.CityName = GetValue<string>(row, "所在城市");
            entity.AddressLine = GetValue<string>(row, "详细地址");
            entity.Zipcode = GetValue<string>(row, "邮编");
            entity.LicenseID = GetValue<string>(row, "营业执照注册号");
            entity.RegisterProvinceName = GetValue<string>(row, "注册省");
            entity.RegisterCityName = GetValue<string>(row, "注册市");
            entity.RegisterAddressLine = GetValue<string>(row, "注册地址");
            entity.OwnerName = GetValue<string>(row, "法人姓名");
            entity.ContactName = GetValue<string>(row, "联系人姓名");
            entity.ContactIDNumber = GetValue<string>(row, "联系人证件号");
            entity.ContactEmail = GetValue<string>(row, "联系邮箱");
            entity.ContactPhone = GetValue<string>(row, "联系电话");

            entity.BankAccount = GetValue<string>(row, "银行账户");
            entity.BankName = GetValue<string>(row, "开户银行");
            entity.SubBranch = GetValue<string>(row, "开户支行");
            entity.AccountName = GetValue<string>(row, "开户名称");
            entity.FinanceContact = GetValue<string>(row, "财务联系人");
            entity.FinancePhone = GetValue<string>(row, "财务电话");

            entity.AttachmentFiles = GetValue<string>(row, "附件文件列表");

            return entity;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(MerchantEntity entity)
        {
            if (string.IsNullOrEmpty(entity.MerchantName) || entity.MerchantName.Length == 0)
            {
                throw new NhhException("商户名称为空");
            }

            if (string.IsNullOrEmpty(entity.BriefName) || entity.BriefName.Length == 0)
            {
                throw new NhhException("商户简称为空");
            }

            entity.ProvinceID = CommonUtil.GetProvinceId(entity.ProvinceName);
            if (entity.ProvinceID == 0)
            {
                throw new NhhException("所在省份为空");
            }
            if (entity.ProvinceID < 0)
            {
                throw new NhhException(string.Format("所在省份 {0} 输入错误", entity.ProvinceName));
            }

            entity.CityID = CommonUtil.GetCityId(entity.ProvinceName, entity.CityName);
            if (entity.CityID == 0)
            {
                throw new NhhException("所在城市为空");
            }
            if (entity.CityID < 0)
            {
                throw new NhhException(string.Format("所在城市 {0} 输入错误", entity.CityName));
            }

            //if (string.IsNullOrEmpty(entity.AddressLine) || entity.AddressLine.Length == 0)
            //{
            //    throw new NhhException("详细地址为空");
            //}

            //if (string.IsNullOrEmpty(entity.ContactIDNumber) || entity.ContactIDNumber.Length == 0)
            //{
            //    throw new NhhException("联系人证件号");
            //}

            //if (string.IsNullOrEmpty(entity.ContactEmail) || entity.ContactEmail.Length == 0)
            //{
            //    throw new NhhException("联系邮箱为空");
            //}

            //if (string.IsNullOrEmpty(entity.ContactPhone) || entity.ContactPhone.Length == 0)
            //{
            //    throw new NhhException("联系电话为空");
            //}

            #region 当商户是法人时则增加以下验证

            if (entity.MerchantType == 1)
            {
                entity.RegisterProvinceID = CommonUtil.GetProvinceId(entity.RegisterProvinceName);
                if (entity.RegisterProvinceID == 0)
                {
                    throw new NhhException("注册省为空");
                }
                if (entity.RegisterProvinceID < 0)
                {
                    throw new NhhException(string.Format("注册省 {0} 输入错误", entity.RegisterProvinceName));
                }

                entity.RegisterCityID = CommonUtil.GetCityId(entity.RegisterProvinceName, entity.RegisterCityName);
                if (entity.RegisterCityID == 0)
                {
                    throw new NhhException("注册市为空");
                }
                if (entity.RegisterCityID < 0)
                {
                    throw new NhhException(string.Format("注册城市 {0} 输入错误", entity.RegisterCityName));
                }

                //if (string.IsNullOrEmpty(entity.RegisterAddressLine) || entity.RegisterAddressLine.Length == 0)
                //{
                //    throw new NhhException("注册地址为空");
                //}

                //if (string.IsNullOrEmpty(entity.LicenseID) || entity.LicenseID.Length == 0)
                //{
                //    throw new NhhException("营业执照注册号为空");
                //}

                //if (string.IsNullOrEmpty(entity.OwnerName) || entity.AddressLine.Length == 0)
                //{
                //    throw new NhhException("法人姓名为空");
                //}

                //if (string.IsNullOrEmpty(entity.ContactName) || entity.ContactName.Length == 0)
                //{
                //    throw new NhhException("联系人姓名为空");
                //}

                //if (string.IsNullOrEmpty(entity.BankAccount) || entity.BankAccount.Length == 0)
                //{
                //    throw new NhhException("银行账户为空");
                //}

                //if (string.IsNullOrEmpty(entity.BankName) || entity.BankName.Length == 0)
                //{
                //    throw new NhhException("开户银行为空");
                //}

                //if (string.IsNullOrEmpty(entity.AccountName) || entity.AccountName.Length == 0)
                //{
                //    throw new NhhException("开户名称为空");
                //}

                //if (string.IsNullOrEmpty(entity.SubBranch) || entity.SubBranch.Length == 0)
                //{
                //    throw new NhhException("开户支行为空");
                //}

                //if (string.IsNullOrEmpty(entity.FinanceContact) || entity.FinanceContact.Length == 0)
                //{
                //    throw new NhhException("财务联系人为空");
                //}

                //if (string.IsNullOrEmpty(entity.FinancePhone) || entity.FinancePhone.Length == 0)
                //{
                //    throw new NhhException("财务电话为空");
                //}
            }
            #endregion


        }

        /// <summary>
        /// 判断商户名是否重名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(NhhDataImport.Entity.MerchantEntity entity)
        {
            var strCmd = string.Format("SELECT COUNT(0) as Num FROM [dbo].[Merchant](Nolock) Where MerchantName='{0}'", entity.MerchantName);
            return SqlHelper.ExecuteScalar(strCmd) > 0;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(MerchantEntity entity)
        {
            #region 商户sql

            string strCmd1 = string.Format(@"INSERT INTO [dbo].[Merchant]
                                           ([MerchantType]
                                           ,[MerchantCode]
                                           ,[BriefName]
                                           ,[MerchantName]
                                           ,[ProvinceID]
                                           ,[CityID]
                                           ,[AddressLine]
                                           ,[Zipcode]
                                           ,[LicenseID]
                                           ,[RegisterProvinceID]
                                           ,[RegisterCityID]
                                           ,[RegisterAddressLine]
                                           ,[OwnerName]
                                           ,[ContactName]
                                           ,[ContactIDNumber]
                                           ,[ContactEmail]
                                           ,[ContactPhone]
                                           ,[Status]
                                           ,[InDate]
                                           ,[InUser]
                                           ,[EditDate]
                                           ,[EditUser])
                                     VALUES
                                           (@MerchantType
                                           ,@MerchantCode
                                           ,@BriefName
                                           ,@MerchantName
                                           ,@ProvinceID
                                           ,@CityID
                                           ,@AddressLine
                                           ,@Zipcode
                                           ,@LicenseID
                                           ,@RegisterProvinceID
                                           ,@RegisterCityID
                                           ,@RegisterAddressLine
                                           ,@OwnerName
                                           ,@ContactName
                                           ,@ContactIDNumber
                                           ,@ContactEmail
                                           ,@ContactPhone
                                           ,@Status
                                           ,@InDate
                                           ,@InUser
                                           ,@EditDate
                                           ,@EditUser);
                                    SELECT SCOPE_IDENTITY();");
            #endregion

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@MerchantType", entity.MerchantType),
                new SqlParameter("@MerchantCode", entity.MerchantCode),
                new SqlParameter("@BriefName", entity.BriefName),
                new SqlParameter("@MerchantName", entity.MerchantName),
                new SqlParameter("@ProvinceID", entity.ProvinceID),
                new SqlParameter("@CityID", entity.CityID),
                new SqlParameter("@AddressLine", entity.AddressLine),
                new SqlParameter("@Zipcode", entity.Zipcode),
                new SqlParameter("@LicenseID", entity.LicenseID),
                new SqlParameter("@RegisterProvinceID", entity.RegisterProvinceID),
                new SqlParameter("@RegisterCityID", entity.RegisterCityID),
                new SqlParameter("@RegisterAddressLine", entity.RegisterAddressLine),
                new SqlParameter("@OwnerName", entity.OwnerName),
                new SqlParameter("@ContactName", entity.ContactName),
                new SqlParameter("@ContactIDNumber", entity.ContactIDNumber),
                new SqlParameter("@ContactEmail", entity.ContactEmail),
                new SqlParameter("@ContactPhone", entity.ContactPhone),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };

            entity.MerchantID = SqlHelper.ExecuteScalar(strCmd1, paramList);
            MerchantFinanceProcess.SaveMerchantFinance(entity);
            
            //商户用户
            MerchantUserProcess.AddMerchantUser(entity);

            //附件上传
            new MerchantAttachmentProcess().ProcessPicture(entity, _worker);
        }

        /// <summary>
        /// 更新商户数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(MerchantEntity entity)
        {

            entity.MerchantID = MerchantUtil.GetMerchantId(entity.MerchantName);

            string strCmd = string.Format(@"UPDATE [dbo].[Merchant] SET 
                                            [MerchantType] = @MerchantType
                                           ,[MerchantCode] = @MerchantCode
                                           ,[BriefName] = @BriefName
                                           ,[ProvinceID]=@ProvinceID
                                           ,[CityID] = @CityID
                                           ,[AddressLine] = @AddressLine
                                           ,[Zipcode] = @Zipcode
                                           ,[LicenseID] = @LicenseID
                                           ,[RegisterProvinceID] = @RegisterProvinceID
                                           ,[RegisterCityID] = @RegisterCityID
                                           ,[RegisterAddressLine] =@RegisterAddressLine
                                           ,[OwnerName] = @OwnerName
                                           ,[ContactName] = @ContactName
                                           ,[ContactIDNumber] = @ContactIDNumber
                                           ,[ContactEmail] = @ContactEmail
                                           ,[ContactPhone] = @ContactPhone
                                           ,[EditDate] = @EditDate
                                           ,[EditUser] = @EditUser
                                           WHERE MerchantID=@MerchantID");

            var paramList = new SqlParameter[] 
            {
                new SqlParameter("@MerchantType", entity.MerchantType),
                new SqlParameter("@MerchantCode", entity.MerchantCode),
                new SqlParameter("@BriefName", entity.BriefName),
                new SqlParameter("@ProvinceID", entity.ProvinceID),
                new SqlParameter("@CityID", entity.CityID),
                new SqlParameter("@AddressLine", entity.AddressLine),
                new SqlParameter("@Zipcode", entity.Zipcode),
                new SqlParameter("@LicenseID", entity.LicenseID),
                new SqlParameter("@RegisterProvinceID", entity.RegisterProvinceID),
                new SqlParameter("@RegisterCityID", entity.RegisterCityID),
                new SqlParameter("@RegisterAddressLine", entity.RegisterAddressLine),
                new SqlParameter("@OwnerName", entity.OwnerName),
                new SqlParameter("@ContactName", entity.ContactName),
                new SqlParameter("@ContactIDNumber", entity.ContactIDNumber),
                new SqlParameter("@ContactEmail", entity.ContactEmail),
                new SqlParameter("@ContactPhone", entity.ContactPhone),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
                new SqlParameter("@MerchantID", entity.MerchantID),
            };


            SqlHelper.ExecuteNonQuery(strCmd, paramList);
            MerchantFinanceProcess.SaveMerchantFinance(entity);

            //附件上传
            new MerchantAttachmentProcess().ProcessPicture(entity, _worker);
        }
    }
}
