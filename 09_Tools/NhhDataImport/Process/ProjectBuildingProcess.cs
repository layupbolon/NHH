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
    /// 项目楼宇
    /// </summary>
    public class ProjectBuildingProcess : BaseDataProcess<ProjectBuildingEntity>
    {
        public ProjectBuildingProcess()
        {
            SheetName = "楼宇";
        }

        protected override ProjectBuildingEntity ToEntity(DataRow row)
        {
            var entity = new ProjectBuildingEntity();
            entity.BuildingName = GetValue<string>(row, "楼宇名称");
            entity.GroundFloorNum = GetValue<int>(row, "地上楼层数量");
            entity.UndergroundFloorNum = GetValue<int>(row, "地下楼层数量");
            entity.BuildingGroundArea = GetValue<decimal>(row, "地上建筑面积");
            entity.BuildingUndergroundArea = GetValue<decimal>(row, "地下建筑面积");
            entity.TotalConstructionArea = GetValue<decimal>(row, "总建筑面积");
            entity.TotalRentArea = GetValue<decimal>(row, "总计租面积");
            entity.TotalRentUnit = GetValue<int>(row, "总出租单元数量");
            entity.PlanSummary = GetValue<string>(row, "规划定位");
            entity.PlanHighlight = GetValue<string>(row, "规划亮点");
            entity.ContractStore = GetValue<string>(row, "入住商家");
            entity.RenderingFileName = GetValue<string>(row, "效果图");
            entity.ProjectName = GetValue<string>(row, "项目名称");
            entity.BuildingCode = GetValue<string>(row, "楼宇编码");

            return entity;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(ProjectBuildingEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BuildingName) || entity.BuildingName.Length == 0)
            {
                throw new NhhException("楼宇名称不能为空");
            }

            if (entity.GroundFloorNum < 0)
            {
                throw new NhhException(string.Format("地上楼层 {0} 输入错误", entity.GroundFloorNum));
            }
            if (entity.UndergroundFloorNum < 0)
            {
                throw new NhhException(string.Format("地下楼层 {0} 输入错误", entity.UndergroundFloorNum));
            }
            if (entity.BuildingUndergroundArea <= 0)
            {
                throw new NhhException(string.Format("地下建筑面积 {0} 输入错误", entity.BuildingUndergroundArea));
            }
            if (entity.BuildingGroundArea <= 0)
            {
                throw new NhhException(string.Format("地上建筑面积 {0} 输入错误", entity.BuildingGroundArea));
            }
            if (entity.TotalConstructionArea <= 0)
            {
                throw new NhhException(string.Format("总建筑面积 {0} 输入错误", entity.TotalConstructionArea));
            }

            entity.ProjectID = ProjectUtil.GetProjectId(entity.ProjectName);
            if (entity.ProjectID == 0)
            {
                throw new NhhException("项目名称不能为空");
            }
            if (entity.ProjectID < 0)
            {
                throw new NhhException(string.Format("项目名称 {0} 输入错误", entity.ProjectName));
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(ProjectBuildingEntity entity)
        {
            string strCmd = string.Format(@"SELECT COUNT(0) as Number
                                          FROM [dbo].[Project_Building](Nolock)
                                          Where BuildingName='{0}' and ProjectID='{1}'", entity.BuildingName, entity.ProjectID);
            return SqlHelper.ExecuteScalar(strCmd) > 0;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(ProjectBuildingEntity entity)
        {
            string strCmd = string.Format(@"INSERT INTO [dbo].[Project_Building]
                                           ([ProjectID]
                                          ,[BuildingCode]
                                          ,[BuildingName]
                                          ,[GroundFloorNum]
                                          ,[UndergroundFloorNum]
                                          ,[BuildingGroundArea]
                                          ,[BuildingUndergroundArea]
                                          ,[TotalConstructionArea]
                                          ,[TotalRentArea]
                                          ,[TotalRentUnit]
                                          ,[PlanSummary]
                                          ,[PlanHighlight]
                                          ,[ContractStore]
                                          ,[Status]
                                          ,[InDate]
                                          ,[InUser]
                                          ,[EditDate]
                                          ,[EditUser])
                                     VALUES
                                           (@ProjectID
                                           ,@BuildingCode
                                           ,@BuildingName
                                           ,@GroundFloorNum
                                           ,@UndergroundFloorNum
                                           ,@BuildingGroundArea
                                           ,@BuildingUndergroundArea
                                           ,@TotalConstructionArea
                                           ,@TotalRentArea
                                           ,@TotalRentUnit
                                           ,@PlanSummary
                                           ,@PlanHighlight
                                           ,@ContractStore
                                           ,@Status
                                           ,@InDate
                                           ,@InUser
                                           ,@EditDate
                                           ,@EditUser);SELECT SCOPE_IDENTITY()");
            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@ProjectID", entity.ProjectID),
                 new SqlParameter("@BuildingCode", entity.BuildingCode),
                new SqlParameter("@BuildingName", entity.BuildingName),
                new SqlParameter("@GroundFloorNum", entity.GroundFloorNum),
                new SqlParameter("@UndergroundFloorNum", entity.UndergroundFloorNum),
                new SqlParameter("@BuildingGroundArea", entity.BuildingGroundArea),
                new SqlParameter("@BuildingUndergroundArea", entity.BuildingUndergroundArea),
                new SqlParameter("@TotalConstructionArea", entity.TotalConstructionArea),
                 new SqlParameter("@TotalRentArea", entity.TotalRentArea),
                new SqlParameter("@TotalRentUnit", entity.TotalRentUnit),
                new SqlParameter("@PlanSummary", entity.PlanSummary),
                new SqlParameter("@PlanHighlight", entity.PlanHighlight),
                new SqlParameter("@ContractStore", entity.ContractStore),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser)
            };

            entity.BuildingID = SqlHelper.ExecuteScalar(strCmd, paramList);

            //图片上传
            new ProjectBuildingPictureProcess().ProcessPicture(entity, _worker);
        }
        /// <summary>
        /// 更新楼宇
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(ProjectBuildingEntity entity)
        {
            //获取楼宇id
            entity.BuildingID = ProjectUtil.GetBuildingId(entity.ProjectName, entity.BuildingName);

            string strCmd = string.Format(@"update [dbo].[Project_Building]
                                           set [BuildingCode]=@BuildingCode
                                          ,[BuildingName]=@BuildingName
                                          ,[GroundFloorNum]=@GroundFloorNum
                                          ,[UndergroundFloorNum]=@UndergroundFloorNum
                                          ,[BuildingGroundArea]=@BuildingGroundArea
                                          ,[BuildingUndergroundArea]=@BuildingUndergroundArea
                                          ,[TotalConstructionArea]=@TotalConstructionArea
                                          ,[TotalRentArea]=@TotalRentArea
                                          ,[TotalRentUnit]=@TotalRentUnit
                                          ,[PlanSummary]=@PlanSummary
                                          ,[PlanHighlight]=@PlanHighlight
                                          ,[ContractStore]=@ContractStore
                                          ,[EditDate]=@EditDate
                                          ,[EditUser]=@EditUser
                                          where [BuildingID]=@BuildingID");
            var paramList = new SqlParameter[] 
            { 
                 new SqlParameter("@BuildingCode", entity.BuildingCode),
                new SqlParameter("@BuildingName", entity.BuildingName),
                new SqlParameter("@GroundFloorNum", entity.GroundFloorNum),
                new SqlParameter("@UndergroundFloorNum", entity.UndergroundFloorNum),
                new SqlParameter("@BuildingGroundArea", entity.BuildingGroundArea),
                new SqlParameter("@BuildingUndergroundArea", entity.BuildingUndergroundArea),
                new SqlParameter("@TotalConstructionArea", entity.TotalConstructionArea),
                new SqlParameter("@TotalRentArea", entity.TotalRentArea),
                new SqlParameter("@TotalRentUnit", entity.TotalRentUnit),
                new SqlParameter("@PlanSummary", entity.PlanSummary),
                new SqlParameter("@PlanHighlight", entity.PlanHighlight),
                new SqlParameter("@ContractStore", entity.ContractStore),
                new SqlParameter("@EditDate",DateTime.Now),
                new SqlParameter("@EditUser",entity.EditUser),
                new SqlParameter("@BuildingID",entity.BuildingID)
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
            //图片上传
            new ProjectBuildingPictureProcess().ProcessPicture(entity, _worker);
        }
    }
}
