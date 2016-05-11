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
    /// 公司处理
    /// </summary>
    public class CompanyProcess : BaseDataProcess<CompanyEntity>
    {
        public CompanyProcess()
        {
            SheetName = "公司";
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected override CompanyEntity ToEntity(DataRow row)
        {
            var entity = new CompanyEntity();
            entity.CompanyCode = GetValue<string>(row, "公司编码");
            entity.BriefName = GetValue<string>(row, "公司简称");
            entity.CompanyName = GetValue<string>(row, "公司名称");
            entity.CompanyTypeName = GetValue<string>(row, "公司类型");
            entity.Province = GetValue<string>(row, "所在省份");
            entity.City = GetValue<string>(row, "所在城市");
            entity.AddressLine = GetValue<string>(row, "详细地址");
            entity.Zipcode = GetValue<string>(row, "邮编");
            entity.LicenseID = GetValue<string>(row, "营业执照注册号");
            entity.OwnerName = GetValue<string>(row, "法人姓名");
            entity.RegisterProvince = GetValue<string>(row, "注册省");
            entity.RegisterCity = GetValue<string>(row, "注册市");
            entity.RegisterAddressLine = GetValue<string>(row, "注册地址");
            entity.ContactName = GetValue<string>(row, "公司联系人");
            entity.ContactIDNumber = GetValue<string>(row, "联系人身份证号码");
            entity.ContactEmail = GetValue<string>(row, "联系邮箱");
            entity.ContactPhone = GetValue<string>(row, "联系电话");
            entity.OwnerCompany = GetValue<string>(row, "母公司");

            entity.BankAccount = GetValue<string>(row, "银行账户");
            entity.BankName = GetValue<string>(row, "开户银行");
            entity.SubBranch = GetValue<string>(row, "开户支行");
            entity.AccountName = GetValue<string>(row, "开户名称");
            entity.FinanceContact = GetValue<string>(row, "财务联系人");
            entity.FinancePhone = GetValue<string>(row, "财务电话");

            return entity;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(CompanyEntity entity)
        {
            if (string.IsNullOrEmpty(entity.CompanyName) || entity.CompanyName.Length == 0)
            {
                throw new NhhException("公司名称为空");
            }

            if (string.IsNullOrEmpty(entity.CompanyTypeName) || entity.CompanyTypeName.Length == 0)
            {
                throw new NhhException("公司类型为空");
            }

            entity.CompanyType = CompanyUtil.GetCompanyTypeId(entity.CompanyTypeName);
            if (entity.CompanyType < 0)
            {
                throw new NhhException(string.Format("公司类型 {0} 输入错误", entity.CompanyTypeName));
            }

            if (string.IsNullOrEmpty(entity.BriefName) || entity.BriefName.Length == 0)
            {
                throw new NhhException("公司中文简称为空");
            }

            if (string.IsNullOrEmpty(entity.CompanyCode) || entity.CompanyCode.Length == 0)
            {
                throw new NhhException("公司编码为空");
            }

            entity.ProvinceID = CommonUtil.GetProvinceId(entity.Province);
            if (entity.ProvinceID == 0)
            {
                throw new NhhException("所在省份为空");
            }
            if (entity.ProvinceID < 0)
            {
                throw new NhhException(string.Format("所在省份 {0} 输入错误", entity.Province));
            }

            entity.CityID = CommonUtil.GetCityId(entity.Province, entity.City);
            if (entity.CityID == 0)
            {
                throw new NhhException("所在城市为空");
            }
            if (entity.CityID < 0)
            {
                throw new NhhException(string.Format("所在城市 {0} 输入错误", entity.City));
            }

            if (string.IsNullOrEmpty(entity.AddressLine) || entity.AddressLine.Length == 0)
            {
                throw new NhhException("详细地址为空");
            }

            //if (string.IsNullOrEmpty(entity.LicenseID) || entity.LicenseID.Length == 0)
            //{
            //    throw new NhhException("营业执照注册号为空");
            //}

            entity.RegisterProvinceID = CommonUtil.GetProvinceId(entity.RegisterProvince);
            if (entity.RegisterProvinceID == 0)
            {
                throw new NhhException("注册省为空");
            }
            if (entity.RegisterProvinceID < 0)
            {
                throw new NhhException(string.Format("注册省 {0} 输入错误", entity.RegisterProvince));
            }

            entity.RegisterCityID = CommonUtil.GetCityId(entity.RegisterProvince, entity.RegisterCity);
            if (entity.RegisterCityID == 0)
            {
                throw new NhhException("注册市为空");
            }
            if (entity.RegisterCityID < 0)
            {
                throw new NhhException(string.Format("注册城市 {0} 输入错误", entity.RegisterCity));
            }

            if (string.IsNullOrEmpty(entity.RegisterAddressLine) || entity.RegisterAddressLine.Length == 0)
            {
                throw new NhhException("注册地址为空");
            }

            if (string.IsNullOrEmpty(entity.OwnerName) || entity.AddressLine.Length == 0)
            {
                throw new NhhException("法人姓名为空");
            }

            if (string.IsNullOrEmpty(entity.ContactName) || entity.ContactName.Length == 0)
            {
                throw new NhhException("联系人为空");
            }

            //if (string.IsNullOrEmpty(entity.ContactEmail) || entity.ContactEmail.Length == 0)
            //{
            //    throw new NhhException("联系邮箱为空");
            //}

            //if (string.IsNullOrEmpty(entity.ContactPhone) || entity.ContactPhone.Length == 0)
            //{
            //    throw new NhhException("联系电话为空");
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

            entity.OwnerCompanyID = CompanyUtil.GetCompanyId(entity.OwnerCompany);
            if (entity.OwnerCompanyID == 0) entity.OwnerCompanyID = null;//母公司字段不能为0，可以为null
            if (entity.OwnerCompanyID < 0)
            {
                throw new NhhException(string.Format("母公司 {0} 输入错误", entity.OwnerCompany));
            }



        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(CompanyEntity entity)
        {
            string strCmd = string.Format(@"SELECT COUNT(0) as Number
                                          FROM [dbo].[Company](Nolock)
                                          Where CompanyName='{0}'", entity.CompanyName);
            return SqlHelper.ExecuteScalar(strCmd) > 0;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(CompanyEntity entity)
        {
            #region SQL1
            string strCmd1 = string.Format(@"INSERT INTO [dbo].[Company]
                                           ([CompanyType]
                                           ,[CompanyCode]
                                           ,[BriefName]
                                           ,[CompanyName]
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
                                           ,[OwnerCompanyID]
                                           ,[Status]
                                           ,[InDate]
                                           ,[InUser]
                                           ,[EditDate]
                                           ,[EditUser])
                                     VALUES
                                           (@CompanyType
                                           ,@CompanyCode
                                           ,@BriefName
                                           ,@CompanyName
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
                                           ,@OwnerCompanyID
                                           ,@Status
                                           ,@InDate
                                           ,@InUser
                                           ,@EditDate
                                           ,@EditUser);
		                                   Select @@IDENTITY;");
            #endregion

            #region SQL2
            string strCmd2 = string.Format(@"INSERT INTO [dbo].[Company_Finance]
                                   ([CompanyID]
                                   ,[BankAccount]
                                   ,[BankName]
                                   ,[SubBranch]
                                   ,[AccountName]
                                   ,[AlipayAccount]
                                   ,[WechatAccount]
                                   ,[FinanceContact]
                                   ,[FinancePhone]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@CompanyID
                                   ,@BankAccount
                                   ,@BankName
                                   ,@SubBranch
                                   ,@AccountName
                                   ,@AlipayAccount
                                   ,@WechatAccount
                                   ,@FinanceContact
                                   ,@FinancePhone
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)");
            #endregion

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@CompanyType", entity.CompanyType),
                new SqlParameter("@CompanyCode", entity.CompanyCode),
                new SqlParameter("@BriefName", entity.BriefName),
                new SqlParameter("@CompanyName", entity.CompanyName),
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
                new SqlParameter("@OwnerCompanyID", entity.OwnerCompanyID),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };

            var paramList2 = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", entity.CompanyID),
                new SqlParameter("@BankAccount", entity.BankAccount),
                new SqlParameter("@BankName", entity.BankName),
                new SqlParameter("@SubBranch", entity.SubBranch),
                new SqlParameter("@AccountName", entity.AccountName),
                new SqlParameter("@AlipayAccount", entity.AlipayAccount),
                new SqlParameter("@WechatAccount", entity.WechatAccount),
                new SqlParameter("@FinanceContact", entity.FinanceContact),
                new SqlParameter("@FinancePhone", entity.FinancePhone),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };

            using (var conn = SqlHelper.GetConnection())
            {
                //打开
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var trans = conn.BeginTransaction("Company");
                entity.CompanyID = SqlHelper.ExecuteScalar(trans, strCmd1, paramList);
                //重新给CompanyId赋值
                paramList2[0].Value = entity.CompanyID;
                SqlHelper.ExecuteNonQuery(trans, strCmd2, paramList2);
                trans.Commit();
                //关闭
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(CompanyEntity entity)
        {
            entity.CompanyID = CompanyUtil.GetCompanyId(entity.CompanyName);

            string strCmd = string.Format(@"UPDATE [dbo].[Company] SET 
                                            [CompanyType] = @CompanyType
                                           ,[CompanyCode] = @CompanyCode
                                           ,[BriefName] = @BriefName
                                           ,[ProvinceID] = @ProvinceID
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
                                           ,[OwnerCompanyID] = @OwnerCompanyID
                                           ,[EditDate] = @EditDate
                                           ,[EditUser] = @EditUser
                                           WHERE CompanyID=@CompanyID");

            string strCmd2 = string.Format(@"UPDATE [dbo].[Company_Finance] SET 
                                            [BankAccount] = @BankAccount
                                           ,[BankName] = @BankName
                                           ,[SubBranch] = @SubBranch
                                           ,[AccountName] = @AccountName
                                           ,[AlipayAccount] = @AlipayAccount
                                           ,[WechatAccount] = @WechatAccount
                                           ,[FinanceContact] = @FinanceContact
                                           ,[FinancePhone] = @FinancePhone
                                           ,[EditDate] = @EditDate
                                           ,[EditUser] = @EditUser
                                           WHERE CompanyID=@CompanyID");


            var paramList = new SqlParameter[] {
                new SqlParameter("@CompanyType", entity.CompanyType),
                new SqlParameter("@CompanyCode", entity.CompanyCode),
                new SqlParameter("@BriefName", entity.BriefName),
                new SqlParameter("@CompanyID",entity.CompanyID ),
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
                new SqlParameter("@OwnerCompanyID", entity.OwnerCompanyID),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };

            var paramList2 = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", entity.CompanyID),
                new SqlParameter("@BankAccount", entity.BankAccount),
                new SqlParameter("@BankName", entity.BankName),
                new SqlParameter("@SubBranch", entity.SubBranch),
                new SqlParameter("@AccountName", entity.AccountName),
                new SqlParameter("@AlipayAccount", entity.AlipayAccount),
                new SqlParameter("@WechatAccount", entity.WechatAccount),
                new SqlParameter("@FinanceContact", entity.FinanceContact),
                new SqlParameter("@FinancePhone", entity.FinancePhone),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };

            using (var conn = SqlHelper.GetConnection())
            {
                //打开
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var trans = conn.BeginTransaction("Company");
                SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);
                SqlHelper.ExecuteNonQuery(trans, strCmd2, paramList2);
                trans.Commit();
                //关闭
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }
    }
}
