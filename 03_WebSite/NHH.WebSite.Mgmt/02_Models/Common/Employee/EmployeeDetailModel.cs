using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Employee
{
    /// <summary>
    /// 员工详情
    /// </summary>
    public class EmployeeDetailModel : EmployeeInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo();

        /// <summary>
        /// 面包屑
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get { return _crumbInfo; }
        }

        [Required(ErrorMessage = "请输入身份证号码")]
        [RegularExpression(@"\d{17}[\d|X]|\d{15}", ErrorMessage = "身份证号码的格式不正确")]
        public string IDNumber { get; set; }

        /// <summary>
        /// 是否分配系统账号
        /// </summary>
        public bool SysUserOrNot { get; set; }
    }
}
