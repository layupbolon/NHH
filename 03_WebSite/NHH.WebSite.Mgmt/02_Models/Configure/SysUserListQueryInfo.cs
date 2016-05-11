using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 用户列表查询信息
    /// </summary>
    public class SysUserListQueryInfo
    {
        /// <summary>
        /// 公司
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public int? Page { get; set; }
    }
}
