using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Employee
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class EmployeeInfo
    {
        /// <summary>
        /// 员工列表model
        /// </summary>
        public int EmployeeId { set; get; }


        [Required(ErrorMessage = "请填写员工编号")]
        public string EmployeeCode { set; get; }

        [Required(ErrorMessage = "员工姓名")]
        public string EmployeeName { set; get; }

        [Required(ErrorMessage = "职务")]
        public string Title { set; get; }

        [Required(ErrorMessage = "请填写邮箱地址")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage = "请输入正确的Email格式\n示例：abc@nhhchina.com")]
        public string Email { set; get; }

        [Required(ErrorMessage = "请填写手机")]
        public string Moblie { set; get; }

        public string Phone { set; get; }

        public string Ext { set; get; }

        public int GenderId { set; get; }

        //public string GenderName { set; get; }
        public GenderTypeInfo GenderType { get; set; }

        public DateTime? Birthday { set; get; }

        public int DepartmentId { set; get; }

        public DepartmentCommonInfo DeptInfo { get; set; }

        public int CompanyId { set; get; }

        public CompanyCommonInfo CompanyInfo { set; get; }
    }
}
