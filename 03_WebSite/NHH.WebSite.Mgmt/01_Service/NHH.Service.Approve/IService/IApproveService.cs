using System.Collections.Generic;
using NHH.Models.Approve;

namespace NHH.Service.Approve.IService
{
    public interface IApproveService
    {
        /// <summary>
        /// 添加审批数据
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        bool UpdateApprove(ApproveModel approve);

        /// <summary>
        /// 判断流程是否完全通过
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        bool IsApprovePass(ApproveModel approve);

        /// <summary>
        /// 获取单据审核记录
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        List<ApproveRecordInfo> GetDocumentApproveProcess(int refID, int configType);

        /// <summary>
        /// 获取当前审核人需要勾选的项
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        List<CheckOptions> GetCheckOptions(int refID, int configType);
    }
}
