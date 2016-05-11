using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Department
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public class DepartmentInfo
    {
        public int CompanyID { get; set; }

        public int CompanyType { get; set; }

        public string CompanyName { get; set; }

        public string BriefName { get; set; }

        public string CompanyCode { get; set; }

        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "请填写部门名称")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "请填写联系电话")]
        public string Phone { get; set; }

        public int Status { get; set; }

        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime? EditDate { get; set; }

        public int? EditUser { get; set; }
    }
}
