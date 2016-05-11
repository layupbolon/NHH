using System;

namespace NHH.Models.Approve
{
    public class ApproveModel
    {
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
        /// 审批人
        /// </summary>
        public int Approver { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ApproveTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 审批结果
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 当前操作人
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 勾选情况
        /// </summary>
        public string CheckOptions { get; set; }
    }
}
