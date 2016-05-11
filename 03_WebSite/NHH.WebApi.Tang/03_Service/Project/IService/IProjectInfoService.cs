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
        /// 获取项目详细信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        ProjectInfoModel GetProjectInfo(int projectId);
        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        List<BuildingInfo> GetBuildingList(int projectId);
        /// <summary>
        /// 获取楼层列表
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        List<FloorInfo> GetFloorList(int buildingId);
    }
}
