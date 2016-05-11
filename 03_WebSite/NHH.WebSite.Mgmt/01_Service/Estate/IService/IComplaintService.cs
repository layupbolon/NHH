using NHH.Models.Common;
using NHH.Models.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate.IService
{
    /// <summary>
    /// 投诉服务接口
    /// </summary>
    public interface IComplaintService
    {
        /// <summary>
        /// 投诉任务列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ComplaintListModel GetComplaintList(ComplaintListQueryInfo queryInfo);

        /// <summary>
        /// 获取投诉详情
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        ComplaintDetailModel GetComplaintDetail(int complaintId);

        /// <summary>
        /// 更新指派人员信息
        /// </summary>
        /// <param name="complaintInfo"></param>
        /// <returns></returns>
        bool AssginServiceUser(ComplaintInfo complaintInfo);

        /// <summary>
        /// 重新指派客服人员
        /// </summary>
        /// <param name="complaintInfo"></param>
        /// <returns></returns>
        bool ReAssginServiceUser(ComplaintInfo complaintInfo);

        /// <summary>
        /// 获取客服人员详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ComplaintDetailModel GetServiceUserById(int userId);

        /// <summary>
        /// 更新投诉信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateComplaint(ComplaintDetailModel model);

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddComplaint(ComplaintDetailModel model);

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="complaintId"></param>
        void Finish(ComplaintInfo model);

        /// <summary>
        /// 投诉搁置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ComplaintShelve(ComplaintShelveInfo model);

        /// <summary>
        /// 获取投诉指派员工信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ComplaintAssignUserListModel GetComplaintAssignUserList(ComplaintAssignUserQueryInfo queryInfo);

        /// <summary>
        /// 获取当前系统用户所在公司的**部所有人
        /// </summary>
        /// <param name="projectConfig"></param>
        /// <returns></returns>
        List<EmployeeCommonInfo> GetComplaintServiceUserList(ProjectBizConfigInfo projectConfig);
    }
}
