using NHH.Framework.Service;
using NHH.Models.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Plan.IService
{
    /// <summary>
    /// 铺位租决服务接口
    /// </summary>
    public interface IProjectUnitRequestService
    {
        /// <summary>
        /// 获取租决列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        List<ProjectUnitRequestInfo> GetRequestList(ProjectUnitRequestListQueryInfo queryInfo);

        /// <summary>
        /// 获取铺位租决列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectUnitRequestListModel GetList(ProjectUnitRequestListQueryInfo queryInfo);

        /// <summary>
        /// 导入招商租决
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        MessageBag<bool> Import(List<ProjectUnitRequestInfo> list);

        /// <summary>
        /// 导入租位招商租决
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool Import(ProjectUnitRequestInfo info);

        /// <summary>
        /// 铺位租决进度
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectUnitRequestScheduleModel Schedule(ProjectUnitRequestListQueryInfo queryInfo);

        /// <summary>
        /// 铺位铺决跟踪
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectUnitRequestTrackModel Track(ProjectUnitRequestListQueryInfo queryInfo);
    }
}
