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
    /// 租约铺位
    /// </summary>
    public class ContractUnitProcess
    {
        /// <summary>
        /// 保存租约铺位
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void SaveContractUnit(SqlTransaction trans, ContractEntity entity)
        {
            string strCmd = @"INSERT INTO [dbo].[Contract_Unit]
                                   ([ContractID]
                                   ,[UnitID]
                                   ,[UnitAvgAera]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@ContractID
                                   ,@UnitID
                                   ,@UnitAvgAera
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)";

            foreach (var unitId in entity.UnitIdList)
            {
                var paramList = new SqlParameter[] 
                    {
                        new SqlParameter("@ContractID", entity.ContractID),
                        new SqlParameter("@UnitID", unitId),
                        new SqlParameter("@UnitAvgAera", entity.ContractArea/entity.UnitIdList.Count),
                        new SqlParameter("@Status", entity.Status),
                        new SqlParameter("@InDate", entity.InDate),
                        new SqlParameter("@InUser", entity.InUser),
                        new SqlParameter("@EditDate", entity.EditDate),
                        new SqlParameter("@EditUser", entity.EditUser),
                    };
                SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);
            }

            //数据回写
            strCmd = @"UPDATE [dbo].[Project_Unit]
                       SET [UnitStatus] = @UnitStatus
                          ,[BizCategoryID] = @BizCategoryID
                          ,[StoreID] = @StoreID
                          ,[ContractStatus] = @ContractStatus
                          ,[ContractEndDate] = @ContractEndDate
                     WHERE UnitID=@UnitID";

            foreach (var unitId in entity.UnitIdList)
            {
                var paramList1 = new SqlParameter[]
                {
                    new SqlParameter("@UnitID", unitId),
                    new SqlParameter("@UnitStatus", 4),
                    new SqlParameter("@BizCategoryID", entity.MerchantStore.BizCategoryID),
                    new SqlParameter("@StoreID", entity.MerchantStore.StoreID),
                    new SqlParameter("@ContractStatus", 3),
                    new SqlParameter("@ContractEndDate", entity.ContractEndDate),
                };
                SqlHelper.ExecuteNonQuery(trans, strCmd, paramList1);
            }
        }
    }
}
