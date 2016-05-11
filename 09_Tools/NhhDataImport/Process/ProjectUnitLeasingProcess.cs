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
    /// 商铺
    /// </summary>
    public class ProjectUnitLeasingProcess
    {
        /// <summary>
        /// 保存商铺
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void SaveProjectUnitLeasing(SqlTransaction trans, ProjectUnitEntity entity)
        {
            #region Project_UnitLeasing
            string strCmd = @"INSERT INTO [dbo].[Project_UnitLeasing]
                               ([UnitID]
                               ,[LeasingDepartmentID]
                               ,[LeasingPersonID]
                               ,[LeasingFinishDate]
                               ,[FireProtectionAuth]
                               ,[MeasureReviewDate]
                               ,[DesignDate]
                               ,[FireProtectionAuthDate]
                               ,[AccessDate]
                               ,[DecorationStartDate]
                               ,[DecorationEndDate]
                               ,[Status]
                               ,[InDate]
                               ,[InUser]
                               ,[EditDate]
                               ,[EditUser])
                         VALUES
                               (@UnitID
                               ,@LeasingDepartmentID
                               ,@LeasingPersonID
                               ,@LeasingFinishDate
                               ,@FireProtectionAuth
                               ,@MeasureReviewDate
                               ,@DesignDate
                               ,@FireProtectionAuthDate
                               ,@AccessDate
                               ,@DecorationStartDate
                               ,@DecorationEndDate
                               ,@Status
                               ,@InDate
                               ,@InUser
                               ,@EditDate
                               ,@EditUser)";
            var paramList = new SqlParameter[]
            {
                new SqlParameter("@UnitID", entity.UnitID),
                new SqlParameter("@LeasingDepartmentID", entity.UnitLeasing.LeasingDepartmentID),
                new SqlParameter("@LeasingPersonID", entity.UnitLeasing.LeasingPersonID),
                new SqlParameter("@LeasingFinishDate", entity.UnitLeasing.LeasingFinishDate),
                new SqlParameter("@FireProtectionAuth", entity.UnitLeasing.FireProtectionAuth),
                new SqlParameter("@MeasureReviewDate", entity.UnitLeasing.MeasureReviewDate),
                new SqlParameter("@DesignDate", entity.UnitLeasing.DesignDate),
                new SqlParameter("@FireProtectionAuthDate", entity.UnitLeasing.FireProtectionAuthDate),
                new SqlParameter("@AccessDate", entity.UnitLeasing.AccessDate),
                new SqlParameter("@DecorationStartDate", entity.UnitLeasing.DecorationStartDate),
                new SqlParameter("@DecorationEndDate", entity.UnitLeasing.DecorationEndDate),
                new SqlParameter("@Status", entity.UnitLeasing.Status),
                new SqlParameter("@InDate", entity.UnitLeasing.InDate),
                new SqlParameter("@InUser", entity.UnitLeasing.InUser),
                new SqlParameter("@EditDate", entity.UnitLeasing.EditDate),
                new SqlParameter("@EditUser", entity.UnitLeasing.EditUser),
            };
            #endregion
            SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);
        }

        /// <summary>
        /// 更新商铺
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void UpdateProjectUnitLeasing(SqlTransaction trans, ProjectUnitEntity entity)
        {
            string strCmd = @"UPDATE [dbo].[Project_UnitLeasing]
                               SET [LeasingDepartmentID] = @LeasingDepartmentID
                                  ,[LeasingPersonID] = @LeasingPersonID
                                  ,[LeasingFinishDate] = @LeasingFinishDate
                                  ,[FireProtectionAuth] = @FireProtectionAuth
                                  ,[MeasureReviewDate] = @MeasureReviewDate
                                  ,[DesignDate] = @DesignDate
                                  ,[FireProtectionAuthDate] = @FireProtectionAuthDate
                                  ,[AccessDate] = @AccessDate
                                  ,[DecorationStartDate] = @DecorationStartDate
                                  ,[DecorationEndDate] = @DecorationEndDate
                                  ,[EditDate] = @EditDate
                                  ,[EditUser] = @EditUser
                             WHERE [UnitID] = @UnitID";

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@UnitID", entity.UnitID),
                new SqlParameter("@LeasingDepartmentID", entity.UnitLeasing.LeasingDepartmentID),
                new SqlParameter("@LeasingPersonID", entity.UnitLeasing.LeasingPersonID),
                new SqlParameter("@LeasingFinishDate", entity.UnitLeasing.LeasingFinishDate),
                new SqlParameter("@FireProtectionAuth", entity.UnitLeasing.FireProtectionAuth),
                new SqlParameter("@MeasureReviewDate", entity.UnitLeasing.MeasureReviewDate),
                new SqlParameter("@DesignDate", entity.UnitLeasing.DesignDate),
                new SqlParameter("@FireProtectionAuthDate", entity.UnitLeasing.FireProtectionAuthDate),
                new SqlParameter("@AccessDate", entity.UnitLeasing.AccessDate),
                new SqlParameter("@DecorationStartDate", entity.UnitLeasing.DecorationStartDate),
                new SqlParameter("@DecorationEndDate", entity.UnitLeasing.DecorationEndDate),
                new SqlParameter("@EditDate", entity.UnitLeasing.EditDate),
                new SqlParameter("@EditUser", entity.UnitLeasing.EditUser),
            };

            SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);
        }
    }
}
