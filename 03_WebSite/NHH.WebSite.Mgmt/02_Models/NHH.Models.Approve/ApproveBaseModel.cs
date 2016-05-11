using System.Collections.Generic;
using NHH.Models.Common;

namespace NHH.Models.Approve
{
    /// <summary>
    /// 审核流程实体基类
    /// </summary>
    public abstract class ApproveBaseModel : BaseModel
    {
        /// <summary>
        /// 单据审批信息列表
        /// </summary>
        public ApproveRecordListInfo ApproveRecordInfos { get; set; }

        /// <summary>
        /// 当前可以审核的人
        /// </summary>
        public List<int> CurrentApprover { get; set; }

        /// <summary>
        /// 当前审核人需要的勾选项
        /// </summary>
        public List<CheckOptions> CheckOptions { get; set; }

        /// <summary>
        /// 业务数据ID
        /// </summary>
        public abstract int RefID { get; set; }

        /// <summary>
        /// 审核Url
        /// </summary>
        public abstract string ApproveUrl { get; }

        /// <summary>
        /// 操作成功后重定向的Url
        /// </summary>
        public abstract string RedirectUrl { get; }
    }
}
