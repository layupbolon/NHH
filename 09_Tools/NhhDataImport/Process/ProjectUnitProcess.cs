using NhhDataImport.Entity;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 商铺
    /// </summary>
    class ProjectUnitProcess : BaseEntityProcess<ProjectUnitEntity>
    {
        public ProjectUnitProcess()
        {
            SheetName = "商铺筹划";
            HeaderRowNum = 2;
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected override ProjectUnitEntity ToEntity(IRow row)
        {
            var entity = new ProjectUnitEntity();
            entity.UnitLeasing = new ProjectUnitLeasingEntity();
            entity.UnitPlan = new ProjectUnitPlanEntity();

            entity.UnitStatus = 1;
            entity.ProjectName = GetValue<string>(row, 0);
            entity.BuildingName = GetValue<string>(row, 1);
            entity.FloorName = GetValue<string>(row, 2);
            entity.UnitNumber = GetValue<string>(row, 3);
            entity.UnitTypeName = GetValue<string>(row, 4);
            entity.BizTypeName = GetValue<string>(row, 5);
            entity.UnitAera = GetValue<decimal>(row, 6);

            #region UnitPlan
            entity.UnitPlan.StandardRent = GetValue<decimal>(row, 8);
            entity.UnitPlan.RecommendedRent = GetValue<decimal>(row, 10);
            entity.UnitPlan.QuotationRent = GetValue<decimal>(row, 12);
            entity.UnitPlan.StandardMgmtFee = GetValue<decimal>(row, 14);
            entity.UnitPlan.DepositMonthly = GetValue<int>(row, 15);
            entity.UnitPlan.PaymentPeriod = GetValue<int>(row, 16);

            entity.UnitPlan.IncreaseStartYears = GetValue<int>(row, 17);
            entity.UnitPlan.IncreaseStepYears = GetValue<int>(row, 18);
            entity.UnitPlan.IncreaseType = 1;
            entity.UnitPlan.IncreaseAmount = GetValue<decimal>(row, 19);
            if (entity.UnitPlan.IncreaseAmount < 1)
            {
                entity.UnitPlan.IncreaseAmountType = 1;
            }
            else
            {
                entity.UnitPlan.IncreaseAmountType = 2;
            }
            entity.UnitPlan.RentLengthUpper = GetValue<int>(row, 20) * 12;
            entity.UnitPlan.RentLengthBottom = 1;
            entity.UnitPlan.RentFreeLength = GetValue<int>(row, 21);
            entity.UnitPlan.DecorationLength = GetValue<int>(row, 22);
            entity.UnitPlan.DecorationMgmtFee = GetValue<decimal>(row, 23);
            entity.UnitPlan.DecorationBond = GetValue<decimal>(row, 24);
            entity.UnitPlan.QualityBond = GetValue<decimal>(row, 25);
            entity.UnitPlan.ParkingLotNum = GetValue<int>(row, 26);
            entity.UnitPlan.AdPointNum = GetValue<int>(row, 27);
            #endregion

            #region UnitLeasing
            entity.UnitLeasing.LeasingCompany = GetValue<string>(row, 28);
            entity.UnitLeasing.LeasingDepartment = GetValue<string>(row, 29);
            entity.UnitLeasing.LeasingPerson = GetValue<string>(row, 30);
            entity.UnitLeasing.LeasingFinishDate = GetValue<DateTime>(row, 31);
            entity.UnitLeasing.FireProtectionAuth = GetValue<string>(row, 32) == "是" ? 1 : 0;
            entity.UnitLeasing.MeasureReviewDate = GetValue<DateTime>(row, 33);
            entity.UnitLeasing.DesignDate = GetValue<DateTime>(row, 34);
            entity.UnitLeasing.FireProtectionAuthDate = GetValue<DateTime>(row, 35);
            entity.UnitLeasing.AccessDate = GetValue<DateTime>(row, 36);
            entity.UnitLeasing.DecorationEndDate = GetValue<DateTime>(row, 37);
            #endregion

            return entity;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(ProjectUnitEntity entity)
        {
            if (string.IsNullOrEmpty(entity.ProjectName) || entity.ProjectName.Length == 0)
            {
                throw new NhhException("项目名称为空");
            }

            if (string.IsNullOrEmpty(entity.BuildingName) || entity.BuildingName.Length == 0)
            {
                throw new NhhException("楼宇名称为空");
            }

            if (string.IsNullOrEmpty(entity.FloorName) || entity.FloorName.Length == 0)
            {
                throw new NhhException("楼层名称为空");
            }

            if (string.IsNullOrEmpty(entity.UnitNumber) || entity.UnitNumber.Length == 0)
            {
                throw new NhhException("铺位号为空");
            }

            if (string.IsNullOrEmpty(entity.UnitTypeName) || entity.UnitTypeName.Length == 0)
            {
                throw new NhhException("商铺类型为空");
            }

            if (entity.UnitAera <= 0)
            {
                throw new NhhException("请输入有效计租面积");
            }


            entity.ProjectID = ProjectUtil.GetProjectId(entity.ProjectName);
            if (entity.ProjectID <= 0)
            {
                throw new NhhException(string.Format("项目名称 {0} 无效", entity.ProjectName));
            }

            entity.BuildingID = ProjectUtil.GetBuildingId(entity.ProjectName, entity.BuildingName);
            if (entity.BuildingID <= 0)
            {
                throw new NhhException(string.Format("楼宇名称 {0} 无效", entity.BuildingName));
            }

            entity.FloorID = ProjectUtil.GetFloorId(entity.ProjectName, entity.BuildingName, entity.FloorName);
            if (entity.FloorID <= 0)
            {
                throw new NhhException(string.Format("楼层名称 {0} 无效", entity.FloorName));
            }

            entity.UnitType = ProjectUtil.GetUnitTypeId(entity.UnitTypeName);
            if (entity.UnitType <= 0)
            {
                throw new NhhException(string.Format("商铺类型 {0} 无效", entity.UnitTypeName));
            }
            entity.UnitPlan.UnitType = entity.UnitType;

            entity.BizTypeID = CommonUtil.GetBizTypeId(entity.BizTypeName);
            if (entity.BizTypeID <= 0)
            {
                throw new NhhException(string.Format("规划业态 {0} 无效", entity.BizTypeName));
            }
            entity.UnitStatus = 2;
            entity.UnitPlan.BizTypeID = entity.BizTypeID;

            entity.UnitLeasing.LeasingCompanyID = CompanyUtil.GetCompanyId(entity.UnitLeasing.LeasingCompany);
            entity.UnitLeasing.LeasingDepartmentID = CompanyUtil.GetDepartmentId(entity.UnitLeasing.LeasingCompany, entity.UnitLeasing.LeasingDepartment);
            entity.UnitLeasing.LeasingPersonID = CompanyUtil.GetEmployeeId(entity.UnitLeasing.LeasingCompany, entity.UnitLeasing.LeasingDepartment, entity.UnitLeasing.LeasingPerson);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(ProjectUnitEntity entity)
        {
            string strCmd = string.Format(@"SELECT Top 1 UnitID
                                          FROM [dbo].[Project_Unit](Nolock)
                                          Where FloorID={0} And UnitNumber='{1}'", entity.FloorID, entity.UnitNumber);
            entity.UnitID = SqlHelper.ExecuteScalar(strCmd);
            return entity.UnitID > 0;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(ProjectUnitEntity entity)
        {
            #region Project_Unit
            string strCmd1 = @"INSERT INTO [dbo].[Project_Unit]
                                   ([ProjectID]
                                   ,[BuildingID]
                                   ,[FloorID]
                                   ,[UnitNumber]
                                   ,[UnitAera]
                                   ,[UnitStatus]
                                   ,[UnitType]
                                   ,[BizTypeID]
                                   ,[BizCategoryID]
                                   ,[StoreID]
                                   ,[ContractStatus]
                                   ,[ContractEndDate]
                                   ,[UnitMapFileName]
                                   ,[FloorMapLocation]
                                   ,[FloorMapDimension]
                                   ,[Tag]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@ProjectID
                                   ,@BuildingID
                                   ,@FloorID
                                   ,@UnitNumber
                                   ,@UnitAera
                                   ,@UnitStatus
                                   ,@UnitType
                                   ,@BizTypeID
                                   ,@BizCategoryID
                                   ,@StoreID
                                   ,@ContractStatus
                                   ,@ContractEndDate
                                   ,@UnitMapFileName
                                   ,@FloorMapLocation
                                   ,@FloorMapDimension
                                   ,@Tag
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser);
                            Select SCOPE_IDENTITY();";
            var paramList1 = new SqlParameter[] 
            {
                new SqlParameter("@ProjectID", entity.ProjectID),
                new SqlParameter("@BuildingID", entity.BuildingID),
                new SqlParameter("@FloorID", entity.FloorID),
                new SqlParameter("@UnitNumber", entity.UnitNumber),
                new SqlParameter("@UnitAera", entity.UnitAera),
                new SqlParameter("@UnitStatus", entity.UnitStatus),
                new SqlParameter("@UnitType", entity.UnitType),
                new SqlParameter("@BizTypeID", entity.BizTypeID),
                new SqlParameter("@BizCategoryID", entity.BizCategoryID),
                new SqlParameter("@StoreID", entity.StoreID),
                new SqlParameter("@ContractStatus", entity.ContractStatus),
                new SqlParameter("@ContractEndDate", entity.ContractEndDate),
                new SqlParameter("@UnitMapFileName", entity.UnitMapFileName),
                new SqlParameter("@FloorMapLocation", entity.FloorMapLocation),
                new SqlParameter("@FloorMapDimension", entity.FloorMapDimension),
                new SqlParameter("@Tag", entity.Tag),
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
                var trans = conn.BeginTransaction("ProjectUnit");
                entity.UnitID = SqlHelper.ExecuteScalar(trans, strCmd1, paramList1);

                ProjectUnitPlanProcess.SaveProjectUnitPlan(trans, entity);
                ProjectUnitLeasingProcess.SaveProjectUnitLeasing(trans, entity);

                trans.Commit();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(ProjectUnitEntity entity)
        {
            string strCmd = @"UPDATE [dbo].[Project_Unit]
                           SET [UnitAera] = @UnitAera
                              ,[BizTypeID] = @BizTypeID
                              ,[EditDate] = @EditDate
                              ,[EditUser] = @EditUser
                         WHERE UnitID=@UnitID";

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@UnitID", entity.UnitID),
                new SqlParameter("@UnitAera", entity.UnitAera),
                new SqlParameter("@BizTypeID", entity.BizTypeID),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };
            using (var conn = SqlHelper.GetConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var trans = conn.BeginTransaction("ProjectUnit");
                SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);

                ProjectUnitLeasingProcess.UpdateProjectUnitLeasing(trans, entity);
                ProjectUnitPlanProcess.UpdateProjectUnitPlan(trans, entity);

                trans.Commit();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
    }
}
