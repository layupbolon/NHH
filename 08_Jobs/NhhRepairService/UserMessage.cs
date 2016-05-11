using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhRepairService
{
    /// <summary>
    /// 用户消息
    /// </summary>
    public class UserMessage
    {
        public int Status { get { return 1; } }

        public DateTime EditDate { get { return DateTime.Now; } }

        public int EditUser { get { return 0; }}

        public DateTime InDate { get { return DateTime.Now; } }

        public int InUser { get { return 0; } }


        public int MessageId { get; set; }

        public int UserId { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 来源 报修：6
        /// </summary>
        public int SourceType { get; set; }

        /// <summary>
        /// 报修单据 即RepairId
        /// </summary>
        public int? SourceRefId { get; set; }

    }
}
