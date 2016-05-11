using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Estate;
using NHH.Models.Common;

namespace NHH.Service.Estate.IService
{
    /// <summary>
    /// 维修服务接口
    /// </summary>
    public interface IRepairService
    {
        /// <summary>
        /// 获取维修列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        RepairListModel GetRepairList(RepairListQueryInfo queryInfo);

        /// <summary>
        /// 添加维修
        /// </summary>
        /// <param name="model"></param>
        int AddRepair(RepairDetailModel model);

        /// <summary>
        /// 获取维修详情
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        RepairDetailModel GetRepairDetail(int repairId);

        /// <summary>
        /// 根据姓名获取维修员工信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        RepairDetailModel GetRepairUserById(int userId);

        /// <summary>
        /// 根据单号指派维修人信息
        /// </summary>
        /// <param name="model"></param>
        bool RepairAssign(RepairAssignInfo model);

        /// <summary>
        /// 重新指派
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool RepairReAssign(RepairAssignInfo info);

        /// <summary>
        /// 维修结束
        /// </summary>
        /// <param name="repairId"></param>
        void RepairFinish(RepairFinishInfo info);

        /// <summary>
        /// 维修搁置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool RepairShelve(RepairShelveInfo model);

        /// <summary>
        /// 延迟维修时间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool RepairDelay(RepairDelayInfo model);

        /// <summary>
        /// 更新维修报价
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool UpdateQuoteAmount(RepairDetailEditInfo info);

        /// <summary>
        /// 获取项目下工程部所有人
        /// </summary>
        /// <param name="projectConfig"></param>
        /// <returns></returns>
        List<EmployeeCommonInfo> GetRepairAssignUserList(ProjectBizConfigInfo projectConfig);

    }
}
