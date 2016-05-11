using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class SysUserMessage
    {
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int SourceType { get; set; }
        public int? SourceRefID { get; set; }
        public int Status { get; set; }
        public DateTime InDate { get; set; }
        public int InUser { get; set; }
        public DateTime? EditDate { get; set; }
        public int? EditUser { get; set; }
    }
}
