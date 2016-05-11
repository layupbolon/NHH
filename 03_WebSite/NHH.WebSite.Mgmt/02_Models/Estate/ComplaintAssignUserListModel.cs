using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉指派用户列表
    /// </summary>
    public class ComplaintAssignUserListModel
    {
        /// <summary>
        /// 投诉指派用户列表
        /// </summary>
        public List<ComplaintAssignUserInfo> ComplaintAssignUserList { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public ComplaintAssignUserQueryInfo QueryInfo { get; set; }


    }
}
