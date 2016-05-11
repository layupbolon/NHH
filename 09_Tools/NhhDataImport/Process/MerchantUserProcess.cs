using NHH.Framework.Security.Cryptography;
using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 商户用户处理
    /// </summary>
    public class MerchantUserProcess
    {
        /// <summary>
        /// 添加商户用户
        /// </summary>
        /// <param name="entity"></param>
        public static void AddMerchantUser(MerchantEntity entity)
        {
            string strCmd = @"INSERT INTO [dbo].[Merchant_User]
                                   ([MerchantID]
                                   ,[RoleID]
                                   ,[UserName]
                                   ,[Password]
		                           ,[Email]
                                   ,[Gender]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@MerchantID
                                   ,@RoleID
                                   ,@UserName
                                   ,@Password
		                           ,@Email
                                   ,@Gender
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)";
            var paramList = new SqlParameter[]
            {
                new SqlParameter("@MerchantID", entity.MerchantID),
                new SqlParameter("@RoleID", 1),
                new SqlParameter("@UserName", string.Format("{0}-001", entity.MerchantID)),
                new SqlParameter("@Password", Cryptographer.SHA1.Encrypt(CommonUtil.GetStringValue("InitPassword"))),
                new SqlParameter("@Email", entity.ContactEmail),
                new SqlParameter("@Gender", 1),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };

            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
