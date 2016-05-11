using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Message
{
    /// <summary>
    /// 员工消息
    /// </summary>
    public class EmployeeMessageInfo : UserMessage
    {
        /// <summary>
        /// 用户ID列表
        /// </summary>
        public string UserIdList { get; set; }
    }
}
