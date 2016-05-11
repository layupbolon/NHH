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
    /// 合同品牌处理
    /// </summary>
    public class ContractBrandProcess
    {
        /// <summary>
        /// 保存合同品牌
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void SaveContractBrand(SqlTransaction trans, ContractEntity entity)
        {
            foreach (var contractBrand in entity.ContractBrandList)
            {
                contractBrand.ContractID = entity.ContractID;

                #region 保存合同品牌
                string strCmd = @"INSERT INTO [dbo].[Contract_Brand]
                                       ([ContractID]
                                       ,[MerchantBrandID]
                                       ,[Status]
                                       ,[InDate]
                                       ,[InUser]
                                       ,[EditDate]
                                       ,[EditUser])
                                 VALUES
                                       (@ContractID
                                       ,@MerchantBrandID
                                       ,@Status
                                       ,@InDate
                                       ,@InUser
                                       ,@EditDate
                                       ,@EditUser)";
                var paramList = new SqlParameter[]
                {
                    new SqlParameter("@ContractID" ,contractBrand.ContractID),
                    new SqlParameter("@MerchantBrandID" ,contractBrand.MerchantBrandID),
                    new SqlParameter("@Status" ,contractBrand.Status),
                    new SqlParameter("@InDate" ,contractBrand.InDate),
                    new SqlParameter("@InUser" ,contractBrand.InUser),
                    new SqlParameter("@EditDate" ,contractBrand.EditDate),
                    new SqlParameter("@EditUser" ,contractBrand.EditUser),
                };
                SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);
                #endregion
            }
        }
    }
}
