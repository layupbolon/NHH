using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Message
{
    /// <summary>
    /// 员工消息列表查询信息
    /// </summary>
    public class EmployeeMessageListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 员工消息列表查询信息
        /// </summary>
        public EmployeeMessageListQueryInfo()
        {
            OrderBy = "MessageID";
        }

        public int? CompanyId { get; set; }

        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
