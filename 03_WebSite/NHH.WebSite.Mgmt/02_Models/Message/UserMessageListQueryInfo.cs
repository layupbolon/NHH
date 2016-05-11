using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Message
{
    /// <summary>
    /// 用户消息列表查询信息
    /// </summary>
    public class UserMessageListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 用户消息列表查询信息
        /// </summary>
        public UserMessageListQueryInfo()
        {
            OrderBy = "MessageID";
        }

        public string EmployeeName { get; set; }
        public int? CompanyId { get; set; }
        //public string DepartmentName { get; set; }
        //public int DepartmentID { get; set; }
        public string DepartAndEmName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }


        public DateTime? SendMessageTime { get; set; }
    }
}
