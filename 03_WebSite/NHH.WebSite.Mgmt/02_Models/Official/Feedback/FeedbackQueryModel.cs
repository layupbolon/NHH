using NHH.Models.Common;

namespace NHH.Models.Official.Feedback
{
    public class FeedbackQueryModel : QueryInfo
    {
        /// <summary>
        /// 反馈类型
        /// 1.投诉  2.建议
        /// </summary>
        public int? FeedbackType { get; set; }

        /// <summary>
        /// 用户角色
        /// 1.商户 2.业主 3.顾客
        /// </summary>
        public int? UserType { get; set; }

        /// <summary>
        /// 状态
        /// -1 作废  1 未受理 2 已受理
        /// </summary>
        public int? Status { get; set; }
    }
}
