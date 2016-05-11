using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Service.Common.IService
{
    public interface IWorkflowService
    {
        /// <summary>
        /// 获取审核工作流流程列表
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="groupNum">如果GroupNum为0则取所有Group的</param>
        /// <returns></returns>
        List<ApproveProcess> GetApproveProcessList(int configType, int groupNum);

        /// <summary>
        /// 初始化工作流审批流程
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        bool InitProcess(int configType, int refId);

        /// <summary>
        /// 重新初始化工作流审批流程
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="refId"></param>
        /// <remarks>也就是Approve_Process表中重新创建一套，原有GroupNum+1</remarks>
        void ResetProcess(int configType, int refId);

        /// <summary>
        /// 获取工作流当前已审核人列表
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="refId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        List<CommonSysUserInfo> GetCurrentApprovers(int configType, int refId, int companyId);

        /// <summary>
        /// 获取工作流下个已审核人列表
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="refId"></param>
        /// <param name="comanyId"></param>
        /// <returns></returns>
        List<CommonSysUserInfo> GetNextApprovers(int configType, int refId, int comanyId);
    }
}
