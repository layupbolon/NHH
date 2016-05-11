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
    /// 项目招商管理服务接口
    /// </summary>
    public interface IProjectPlanMgmtService
    {
        /// <summary>
        /// 获取铺位筹划列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        List<ProjectUnitPlanInfo> GetUnitPlanList(ProjectPlanListQueryInfo queryInfo);

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        MessageBag<bool> Import(List<ProjectUnitPlanInfo> list);

        /// <summary>
        /// 初始化一个空的铺位招商筹划信息
        /// </summary>
        /// <param name="unitId"></param>
        void InitEmptyUnitPlan(int unitId);
    }
}
