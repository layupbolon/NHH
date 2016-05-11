using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhComplaintService
{
    public class UserMessage
    {
        public int MessageId { get; set; }

        public int UserId { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 来源 投诉：7
        /// </summary>
        public int SourceType { get; set; }

        /// <summary>
        /// 投诉单据 即ComplaintId
        /// </summary>
        public int? SourceRefId { get; set; }


        public int Status { get { return 1; } }

        public DateTime EditDate { get { return DateTime.Now; } }

        public int EditUser { get { return 0; } }

        public DateTime InDate { get { return DateTime.Now; } }

        public int InUser { get { return 0; } }
    }
}
