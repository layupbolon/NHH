using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Message
{
    /// <summary>
    /// 系统用户消息查询条件
    /// </summary>
    public class UserMessageQueryInfo : QueryInfo
    {
        /// <summary>
        /// 系统用户消息查询条件
        /// </summary>
        public UserMessageQueryInfo()
        {
            OrderBy = "MessageID";
        }

        public int? CompanyId { get; set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        public int? MessageStatus { get; set; }

        //public int? Page { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }

    }
}
