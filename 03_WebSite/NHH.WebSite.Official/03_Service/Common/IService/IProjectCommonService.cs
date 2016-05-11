using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 项目服务接口
    /// </summary>
    public interface IProjectCommonService
    {
        /// <summary>
        /// 获取项目基本信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        ProjectCommonInfo GetProjectInfo(int projectId);

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        List<ProjectCommonInfo> GetProjectList();

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        List<ProjectCommonInfo> GetProjectList(string projectName);

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        List<BuildingCommonInfo> GetBuildingList(int projectId);

        /// <summary>
        /// 获取楼宇信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        BuildingCommonInfo GetBuildingInfo(int buildingId);

        /// <summary>
        /// 获取楼层信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        FloorCommonInfo GetFloorInfo(int floorId);
        
        /// <summary>
        /// 获取楼宇楼层列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        List<FloorCommonInfo> GetAllFloor(int projectId);

        /// <summary>
        /// 获取楼层列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        List<FloorCommonInfo> GetFloorList(int projectId, int? buildingId);
    }
}
