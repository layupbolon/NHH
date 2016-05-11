using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Message.Models.Wechat
{
    /// <summary>
    /// 消息数据
    /// </summary>
    public class MessageData
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 数据内容
        /// </summary>
        public string Content { get; set; }
    }
}
