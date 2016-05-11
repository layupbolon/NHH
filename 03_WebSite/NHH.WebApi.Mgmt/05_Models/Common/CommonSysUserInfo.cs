using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class CommonSysUserInfo
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户登陆名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 员工职位
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// 部门名
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
