using System;
using NHH.Models.Common;

namespace NHH.Models.Official.Feedback
{
    public class FeedbackModel : BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int FeedbackID { get; set; }

        /// <summary>
        /// 反馈类型
        /// 1.投诉  2.建议
        /// </summary>
        public int FeedbackType { get; set; }
        
        public string FeedbackTypeName { get; set; }

        /// <summary>
        /// 用户角色
        /// 1.商户 2.业主 3.顾客
        /// </summary>
        public int UserType { get; set; }

        public string UserTypeName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// -1 作废  1 未受理 2 已受理
        /// </summary>
        public int Status { get; set; }

        public string FeedbackStatusName { get; set; }

        /// <summary>
        /// 受理人
        /// </summary>
        public int? Accepter { get; set; }

        public string AccepterName { get; set; }

        /// <summary>
        /// 受理时间
        /// </summary>
        public DateTime? AcceptTime { get; set; }

        /// <summary>
        /// 受理结果
        /// </summary>
        public string AcceptResult { get; set; }
    }
}
