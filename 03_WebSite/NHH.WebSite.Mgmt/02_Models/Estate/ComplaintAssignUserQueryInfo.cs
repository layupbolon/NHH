using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉指派用户查询条件
    /// </summary>
    public class ComplaintAssignUserQueryInfo : QueryInfo
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public int? DeptId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 系统用户所在公司Id
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }
    }
}
