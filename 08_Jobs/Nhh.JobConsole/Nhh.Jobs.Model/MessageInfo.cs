using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Model
{
    /// <summary>
    /// 消息信息
    /// </summary>
    public class MessageInfo : BaseEntity
    {
        private int priority = 2;

        /// <summary>
        /// 消息类型
        /// </summary>
        public int MessageType { get; set; }

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
        /// 优先级
        /// </summary>
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }
    }
}
