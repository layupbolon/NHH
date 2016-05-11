using NHH.Models.Common;
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
    public class EmployeeMessageListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public EmployeeMessageListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 用户消息信息
        /// </summary>
        public List<UserMessage> UserMessageList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }  
    }
}
