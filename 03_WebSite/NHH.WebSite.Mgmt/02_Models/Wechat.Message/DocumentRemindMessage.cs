using NHH.Framework.Wechat;

namespace NHH.Wechat.Models.Message
{
    /// <summary>
    /// 特殊单据审核结果提醒
    /// </summary>
    public class DocumentRemindMessage : requestData
    {
        /// <summary>
        /// 标题
        /// </summary>
        public requestFieldDetail first { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public requestFieldDetail keyword1 { get; set; }

        /// <summary>
        /// 流程
        /// </summary>
        public requestFieldDetail keyword2 { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public requestFieldDetail keyword3 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public requestFieldDetail remark { get; set; }
    }
}
