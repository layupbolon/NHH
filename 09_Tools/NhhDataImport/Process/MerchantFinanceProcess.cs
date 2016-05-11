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
    /// 商户财务信息
    /// </summary>
    public class MerchantFinanceProcess
    {
        /// <summary>
        /// 保存商户财务信息
        /// </summary>
        /// <param name="entity"></param>
        public static void SaveMerchantFinance(MerchantEntity entity)
        {
            string strCmd = string.Format(@"SELECT TOP 1 [MerchantFinanceID]
                                            FROM [dbo].[Merchant_Finance](Nolock)
                                            Where MerchantID={0}", entity.MerchantID);
            int merchantFinanceID = SqlHelper.ExecuteScalar(strCmd);

            if (merchantFinanceID > 0)
            {
                UpdateMerchantFinance(entity);
            }
            else
            {
                AddMerchantFinance(entity);
            }
        }

        /// <summary>
        /// 添加商户财务信息
        /// </summary>
        /// <param name="entity"></param>
        private static void AddMerchantFinance(MerchantEntity entity)
        {
            #region strCmd
            string strCmd = string.Format(@"INSERT INTO [dbo].[Merchant_Finance]
                                   ([MerchantID]
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
                                   (@MerchantID
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
            var paramList = new SqlParameter[]
            {
                new SqlParameter("@MerchantID", entity.MerchantID),
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
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
            #endregion
        }

        /// <summary>
        /// 更新商户财务信息
        /// </summary>
        /// <param name="entity"></param>
        private static void UpdateMerchantFinance(MerchantEntity entity)
        {
            string strCmd = string.Format(@"UPDATE [dbo].[Merchant_Finance] SET 
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
                                           WHERE MerchantID=@MerchantID");
            var paramList = new SqlParameter[]
            {
                new SqlParameter("@MerchantID", entity.MerchantID),
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
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
