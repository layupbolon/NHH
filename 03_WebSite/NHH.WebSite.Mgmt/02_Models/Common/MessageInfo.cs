using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 消息信息
    /// </summary>
    public class MessageInfo
    {
        public int MessageId { get; set; }

        /// <summary>
        /// 消息类型
        /// 1：邮件
        /// 2：短信
        /// 3：微信
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 详情链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 状态
        /// -2：发送失败
        /// -1：已取消（删除）
        /// 0：待发送
        /// 1：发送成功
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }

    }
}
