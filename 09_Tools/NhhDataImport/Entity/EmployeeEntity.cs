using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 员工
    /// </summary>
    public class EmployeeEntity : BaseEntity
    {
        public int EmployeeID { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Moblie { get; set; }

        public string Phone { get; set; }

        public string Ext { get; set; }

        public int Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string IDNumber { get; set; }

        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        /// <summary>
        /// 登录授权 是：true，否:false
        /// </summary>
        public bool LoginAccountOrNot { get; set; }



        public int UserType { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


    }
}
