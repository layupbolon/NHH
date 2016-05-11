using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhCampaignService
{
    /// <summary>
    /// 系统消息表信息
    /// </summary>
    public class MessageInfo : BaseInfo
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// 优先级别
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息附加链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 1：待发送
        /// 2：发送中
        /// 3：已发送
        /// 4：已取消
        /// 5：发送失败
        /// </summary>
        public int Status { get; set; }
    }
}
