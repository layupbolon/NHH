using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 项目
    /// </summary>
    public class ProjectUtil
    {
        /// <summary>
        /// 获取项目ID
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static int GetProjectId(string projectName)
        {
            if (string.IsNullOrEmpty(projectName) || projectName.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [ProjectID]
                                          FROM [dbo].[Project](Nolock)
                                          Where ProjectName='{0}'", projectName);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static ProjectInfo GetProjectInfo(int projectId)
        {
            var info = new ProjectInfo();
            string strCmd = string.Format(@"SELECT TOP 1 [ProjectID]
                                              ,[ManageCompanyID]
                                          FROM [dbo].[Project](Nolock)
                                          Where ProjectID={0}", projectId);
            var table = SqlHelper.ExecuteQuery(strCmd);
            if (table == null || table.Rows.Count <= 0)
                return info;
            var row = table.Rows[0];
            info.ProjectID = Convert.ToInt32(row["ProjectID"].ToString());
            info.ManageCompanyID = Convert.ToInt32(row["ManageCompanyID"].ToString());
            return info;
        }

        /// <summary>
        /// 获取楼宇ID
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="buildingName"></param>
        /// <returns></returns>
        public static int GetBuildingId(string projectName, string buildingName)
        {
            if (string.IsNullOrEmpty(projectName) || projectName.Length == 0 ||
                string.IsNullOrEmpty(buildingName) || buildingName.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [BuildingID]
                                          FROM [dbo].[Project_Building](Nolock) AS A
                                          Inner join dbo.Project(Nolock) as B on A.ProjectID=B.ProjectID
                                          Where ProjectName='{0}' And BuildingName='{1}'", projectName, buildingName);

            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取楼层ID
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="buildingName"></param>
        /// <param name="floorName"></param>
        /// <returns></returns>
        public static int GetFloorId(string projectName, string buildingName, string floorName)
        {
            if (string.IsNullOrEmpty(projectName) || projectName.Length == 0 ||
                string.IsNullOrEmpty(buildingName) || buildingName.Length == 0 ||
                string.IsNullOrEmpty(floorName) || floorName.Length == 0)
                return 0;

            string strCmd = string.Format(@"SELECT TOP 1 [FloorID]
                                          FROM [dbo].[Project_Floor](Nolock) as A
                                          Inner join dbo.Project_Building(Nolock) as B on A.BuildingID=B.BuildingID
                                          Inner join dbo.Project(Nolock) as C on B.ProjectID=C.ProjectID
                                          Where ProjectName='{0}' And BuildingName='{1}' And FloorName='{2}'", projectName, buildingName, floorName);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取铺位ID
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="unitNumber"></param>
        /// <returns></returns>
        public static int GetUnitId(int buildingId, string unitNumber)
        {
            if (buildingId <= 0 ||
                string.IsNullOrEmpty(unitNumber) || unitNumber.Length == 0)
            {
                return 0;
            }
            string strCmd = string.Format(@"SELECT TOP 1 [UnitID]
                                          FROM [dbo].[Project_Unit](Nolock)
                                          Where BuildingID={0} And UnitNumber='{1}'", buildingId, unitNumber);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取商铺类型ID
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static int GetUnitTypeId(string unitType)
        {
            if (string.IsNullOrEmpty(unitType) || unitType.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [FieldValue]
                                          FROM [dbo].[Dictionary](Nolock)
                                          Where FieldType='ProjectUnitType' And FieldName='{0}'", unitType);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取项目阶段
        /// </summary>
        /// <param name="stageStr"></param>
        /// <returns></returns>
        public static int GetStage(string stageStr) 
        {
            if (string.IsNullOrEmpty(stageStr) || stageStr.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [FieldValue]
                                          FROM [dbo].[Dictionary](Nolock)
                                          Where FieldType='ProjectStage' And FieldName='{0}'", stageStr);
            return SqlHelper.ExecuteScalar(strCmd);
        }
    }
}
