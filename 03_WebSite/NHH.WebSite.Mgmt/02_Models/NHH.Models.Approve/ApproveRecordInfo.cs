using System;

namespace NHH.Models.Approve
{
    /// <summary>
    /// 审批信息
    /// </summary>
    public class ApproveRecordInfo
    {
        /// <summary>
        /// 流程ID
        /// </summary>
        public int ProcessID { get; set; }

        /// <summary>
        /// 流程配置模板ID
        /// </summary>
        public int ConfigID { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public int GroupNum { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public int ConfigType { get; set; }

        /// <summary>
        /// 业务数据ID
        /// </summary>
        public int RefID { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public int? Approver { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        public string ApproverName { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string ShowDeptName { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApproveTime { get; set; }

        /// <summary>
        /// 审批结果
        /// </summary>
        public int? Result { get; set; }

        /// <summary>
        /// 勾选项
        /// </summary>
        public string CheckOptions { get; set; }
    }
}
