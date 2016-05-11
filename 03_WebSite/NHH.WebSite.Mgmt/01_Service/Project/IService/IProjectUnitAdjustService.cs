using NHH.Framework.Service;
using NHH.Models.Project.ProjectUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 铺位调整服务接口
    /// </summary>
    public interface IProjectUnitAdjustService
    {
        /// <summary>
        /// 合铺
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        MessageBag<int> Merge(ProjectUnitMergeData data);

        /// <summary>
        /// 拆铺
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        MessageBag<bool> Split(ProjectUnitSplitData data);

        /// <summary>
        /// 获取铺位调整列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectUnitAdjustListModel GetAdjustList(ProjectUnitAdjustListQueryInfo queryInfo);

        /// <summary>
        /// 获取铺位调整详情
        /// </summary>
        /// <param name="adjustId"></param>
        /// <returns></returns>
        ProjectUnitAdjustModel GetAdjustInfo(int adjustId);
    }
}
