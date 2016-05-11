using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 部门
    /// </summary>
    public class DepartmentEntity : BaseEntity
    {
        public int DepartmentID { get; set; }

        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string DepartmentName { get; set; }

        public string Phone { get; set; }
        
    }
}
