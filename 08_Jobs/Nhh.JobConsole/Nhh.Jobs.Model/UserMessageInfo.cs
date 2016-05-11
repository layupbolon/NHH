using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Model
{
    /// <summary>
    /// 用户消息
    /// </summary>
    public class UserMessageInfo : BaseEntity
    {
        public int MessageId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 来源
        /// 报修：6
        /// 投诉：7
        /// </summary>
        public int SourceType { get; set; }

        /// <summary>
        /// 单据ID
        /// </summary>
        public int? SourceRefId { get; set; }
    }
}
