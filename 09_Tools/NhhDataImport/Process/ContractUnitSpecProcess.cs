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
    /// 租约铺位交付标准
    /// </summary>
    public class ContractUnitSpecProcess
    {
        /// <summary>
        /// 保存租约交付标准
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void SaveContractUnitSpec(SqlTransaction trans, ContractEntity entity)
        {
            #region SQL
            string strCmd = @"INSERT INTO [dbo].[Contract_UnitSpec]
                                   ([ContractID]
                                   ,[UnitID]
                                   ,[SpecType]
                                   ,[Floor]
                                   ,[Ceiling]
                                   ,[Wall]
                                   ,[Pillar]
                                   ,[FloorBearing]
                                   ,[WaterSupply]
                                   ,[WaterDrain]
                                   ,[Door]
                                   ,[Logo]
                                   ,[ElectricityUsage]
                                   ,[FireProtection]
                                   ,[Broadcasting]
                                   ,[AirCondition]
                                   ,[Smoke]
                                   ,[Security]
                                   ,[Wiring]
                                   ,[Water]
                                   ,[Gas]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@ContractID
                                   ,@UnitID
                                   ,@SpecType
                                   ,@Floor
                                   ,@Ceiling
                                   ,@Wall
                                   ,@Pillar
                                   ,@FloorBearing
                                   ,@WaterSupply
                                   ,@WaterDrain
                                   ,@Door
                                   ,@Logo
                                   ,@ElectricityUsage
                                   ,@FireProtection
                                   ,@Broadcasting
                                   ,@AirCondition
                                   ,@Smoke
                                   ,@Security
                                   ,@Wiring
                                   ,@Water
                                   ,@Gas
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)";
            #endregion

            foreach (int unitId in entity.UnitIdList)
            {
                var paramList1 = new SqlParameter[]
                {
                    new SqlParameter("@ContractID", entity.ContractID),
                    new SqlParameter("@UnitID", unitId),
                    new SqlParameter("@SpecType", 1),
                    new SqlParameter("@Floor", entity.UnitSpec.Floor),
                    new SqlParameter("@Ceiling", entity.UnitSpec.Ceiling),
                    new SqlParameter("@Wall", entity.UnitSpec.Wall),
                    new SqlParameter("@Pillar", entity.UnitSpec.Pillar),
                    new SqlParameter("@FloorBearing", entity.UnitSpec.FloorBearing),
                    new SqlParameter("@WaterSupply", entity.UnitSpec.WaterSupply),
                    new SqlParameter("@WaterDrain", entity.UnitSpec.WaterDrain),
                    new SqlParameter("@Door", entity.UnitSpec.Door),
                    new SqlParameter("@Logo", entity.UnitSpec.Logo),
                    new SqlParameter("@ElectricityUsage", entity.UnitSpec.ElectricityUsage),
                    new SqlParameter("@FireProtection", entity.UnitSpec.FireProtection),
                    new SqlParameter("@Broadcasting", entity.UnitSpec.Broadcasting),
                    new SqlParameter("@AirCondition", entity.UnitSpec.AirCondition),
                    new SqlParameter("@Smoke", entity.UnitSpec.Smoke),
                    new SqlParameter("@Security", entity.UnitSpec.Security),
                    new SqlParameter("@Wiring", entity.UnitSpec.Wiring),
                    new SqlParameter("@Water", entity.UnitSpec.Water),
                    new SqlParameter("@Gas", entity.UnitSpec.Gas),
                    new SqlParameter("@Status", entity.UnitSpec.Status),
                    new SqlParameter("@InDate", entity.UnitSpec.InDate),
                    new SqlParameter("@InUser", entity.UnitSpec.InUser),
                    new SqlParameter("@EditDate", entity.UnitSpec.EditDate),
                    new SqlParameter("@EditUser", entity.UnitSpec.EditUser),
                };
                SqlHelper.ExecuteNonQuery(trans, strCmd, paramList1);

                var paramList2 = new SqlParameter[]
                {
                    new SqlParameter("@ContractID", entity.ContractID),
                    new SqlParameter("@UnitID", unitId),
                    new SqlParameter("@SpecType", 2),
                    new SqlParameter("@Floor", entity.StoreSpec.Floor),
                    new SqlParameter("@Ceiling", entity.StoreSpec.Ceiling),
                    new SqlParameter("@Wall", entity.StoreSpec.Wall),
                    new SqlParameter("@Pillar", entity.StoreSpec.Pillar),
                    new SqlParameter("@FloorBearing", entity.StoreSpec.FloorBearing),
                    new SqlParameter("@WaterSupply", entity.StoreSpec.WaterSupply),
                    new SqlParameter("@WaterDrain", entity.StoreSpec.WaterDrain),
                    new SqlParameter("@Door", entity.StoreSpec.Door),
                    new SqlParameter("@Logo", entity.StoreSpec.Logo),
                    new SqlParameter("@ElectricityUsage", entity.StoreSpec.ElectricityUsage),
                    new SqlParameter("@FireProtection", entity.StoreSpec.FireProtection),
                    new SqlParameter("@Broadcasting", entity.StoreSpec.Broadcasting),
                    new SqlParameter("@AirCondition", entity.StoreSpec.AirCondition),
                    new SqlParameter("@Smoke", entity.StoreSpec.Smoke),
                    new SqlParameter("@Security", entity.StoreSpec.Security),
                    new SqlParameter("@Wiring", entity.StoreSpec.Wiring),
                    new SqlParameter("@Water", entity.StoreSpec.Water),
                    new SqlParameter("@Gas", entity.StoreSpec.Gas),
                    new SqlParameter("@Status", entity.StoreSpec.Status),
                    new SqlParameter("@InDate", entity.StoreSpec.InDate),
                    new SqlParameter("@InUser", entity.StoreSpec.InUser),
                    new SqlParameter("@EditDate", entity.StoreSpec.EditDate),
                    new SqlParameter("@EditUser", entity.StoreSpec.EditUser),
                };
                SqlHelper.ExecuteNonQuery(trans, strCmd, paramList2);
            }
        }
    }
}
