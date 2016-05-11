using NHH.Entities;
using NHH.Models.Common;
using NHH.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 项目信息服务接口
    /// </summary>
    public interface IProjectInfoService
    {
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectListModel GetProjectList(ProjectListQueryInfo queryInfo);

        /// <summary>
        /// 获取项目详细信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        ProjectDetailModel GetProjectDetail(int projectId);

        /// <summary>
        /// 获取项目基础信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        ProjectBasicInfoModel GetProjectBasicInfo(int projectId);

        /// <summary>
        /// 获取项目的详细信息
        /// 包括楼宇，楼宇等
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        ProjectDetailInfoModel GetProjectDetailInfo(int projectId);

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        BuildingListModel GetBuildingList(int projectId);

        /// <summary>
        /// 获取楼宇详细信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        BuildingDetailInfoModel GetBuildingDetail(int buildingId);

        /// <summary>
        /// 获取楼层列表
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        FloorListModel GetFloorList( int buildingId);

        /// <summary>
        /// 获取楼层详细信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        FloorDetailModel GetFloorDetail(int floorId);
        
        /// <summary>
        /// 获取楼层信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        FloorInfo GetFloorInfo(int floorId);

        /// <summary>
        /// 获取项目效果图
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        string GetRenderingFileName(int projectId);
    }
}
