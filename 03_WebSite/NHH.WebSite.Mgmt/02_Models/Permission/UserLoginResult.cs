using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Permission
{
    public class UserLoginResult
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        public string UserName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int[] RoleIDs { get; set; }

        public string[] RoleNames { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public int? EmployeeID { get; set; }

        public string EmployeeName { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int? DepartmentID { get; set; }

        public string DepartmentName { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int? CompanyID { get; set; }

        public string CompanyName { get; set; }
    }
}
