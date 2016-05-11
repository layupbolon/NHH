using NHH.Models.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Plan.IService
{
    /// <summary>
    /// 项上招商批次铺位服务接口
    /// </summary>
    public interface IProjectBatchUnitService
    {
        /// <summary>
        /// 获取铺位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectBatchUnitListModel GetUnitList(ProjectBatchUnitListQueryInfo queryInfo);
    }
}
