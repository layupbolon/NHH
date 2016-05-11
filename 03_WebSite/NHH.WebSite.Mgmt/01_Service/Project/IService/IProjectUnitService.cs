using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Project.ProjectUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 铺位管理服务接口
    /// </summary>
    public interface IProjectUnitService
    {
        /// <summary>
        /// 获取商铺列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectUnitListModel GetProjectUnitList(ProjectUnitListQueryInfo queryInfo);

        /// <summary>
        /// 添加商铺
        /// </summary>
        /// <param name="model"></param>
        MessageBag<bool> AddProjectUnit(ProjectUnitInfoModel model);

        /// <summary>
        /// 更新商铺信息
        /// </summary>
        /// <param name="model"></param>
        void UpdateProjectUnit(ProjectUnitInfoModel model);

        /// <summary>
        /// 删除商铺
        /// </summary>
        /// <param name="unitId"></param>
        MessageBag<bool> DelProjectUnit(int unitId);

        /// <summary>
        /// 获取商铺信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectUnitInfoModel GetProjectUnitInfo(int unitId);
        
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
        /// 根据floorId获取商铺列表
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        List<ProjectUnitInfoModel> GetProjectUnitModelList(int floorId, int page, PagingInfo pagingInfo);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitNumber"></param>
        /// <returns></returns>
        bool IsExists(int projectId, string unitNumber);

        /// <summary>
        /// 获取铺位公共信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitNumber"></param>
        /// <returns></returns>
        ProjectUnitCommonInfo GetCommonInfo(int projectId, string unitNumber);
    }
}
