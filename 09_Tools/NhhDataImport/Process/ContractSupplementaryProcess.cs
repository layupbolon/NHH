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
    /// 租约补充协议
    /// </summary>
    public class ContractSupplementaryProcess
    {
        /// <summary>
        /// 保存租约补充协议
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void SaveSupplementary(SqlTransaction trans, ContractEntity entity)
        {
            if (entity.Supplementary == null)
                return;

            entity.Supplementary.ManageCompanyID = entity.ManageCompanyID;

            #region Supplementary
            string strCmd = @"INSERT INTO [dbo].[Contract_Supplementary]
                                   ([ContractID]
                                   ,[SupplementaryType]
                                   ,[SignerName]
                                   ,[SignerIDNumber]
                                   ,[SignerPhone]
                                   ,[ManageCompanyID]
                                   ,[OperatorName]
                                   ,[OperatorPhone]
                                   ,[SupplementaryContent]
                                   ,[SupplementaryStartDate]
                                   ,[SupplementaryEndDate]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@ContractID
                                   ,@SupplementaryType
                                   ,@SignerName
                                   ,@SignerIDNumber
                                   ,@SignerPhone
                                   ,@ManageCompanyID
                                   ,@OperatorName
                                   ,@OperatorPhone
                                   ,@SupplementaryContent
                                   ,@SupplementaryStartDate
                                   ,@SupplementaryEndDate
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)";
            #endregion

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@ContractID", entity.ContractID),
                new SqlParameter("@SupplementaryType", entity.Supplementary.SupplementaryType),
                new SqlParameter("@SignerName", entity.Supplementary.SignerName),
                new SqlParameter("@SignerIDNumber", entity.Supplementary.SignerIDNumber),
                new SqlParameter("@SignerPhone", entity.Supplementary.SignerPhone),
                new SqlParameter("@ManageCompanyID", entity.Supplementary.ManageCompanyID),
                new SqlParameter("@OperatorName", entity.Supplementary.OperatorName),
                new SqlParameter("@OperatorPhone", entity.Supplementary.OperatorPhone),
                new SqlParameter("@SupplementaryContent", entity.Supplementary.SupplementaryContent),
                new SqlParameter("@SupplementaryStartDate", entity.Supplementary.SupplementaryStartDate),
                new SqlParameter("@SupplementaryEndDate", entity.Supplementary.SupplementaryEndDate),
                new SqlParameter("@Status", entity.Supplementary.Status),
                new SqlParameter("@InDate", entity.Supplementary.InDate),
                new SqlParameter("@InUser", entity.Supplementary.InUser),
                new SqlParameter("@EditDate", entity.Supplementary.EditDate),
                new SqlParameter("@EditUser", entity.Supplementary.EditUser),
            };
            SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);
        }
    }
}
