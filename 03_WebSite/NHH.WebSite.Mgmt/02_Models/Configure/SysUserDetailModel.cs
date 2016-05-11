using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 个人基础信息
    /// </summary>
    public class SysUserDetailModel : SysUserInfo
    {

        private CrumbInfo _crumbInfo = new CrumbInfo { };

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get
            { return _crumbInfo; }
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// 员工身份证号
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "请填写手机号")]
        public string Mobile { get; set; }

        /// <summary>
        /// 座机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 分机号
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public string CompanyName { get; set; }

        public List<SysRoleInfo> RoleList { get; set; }

        public string RemoveRoleList { get; set; }

        public string AddRoleList { get; set; }

    }
}
