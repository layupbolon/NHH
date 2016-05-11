using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectUnitLeasingEntity : BaseEntity
    {
        public string LeasingCompany { get; set; }

        public int? LeasingCompanyID { get; set; }

        public string LeasingDepartment { get; set; }

        public int? LeasingDepartmentID { get; set; }


        public string LeasingPerson { get; set; }

        public int? LeasingPersonID { get; set; }

        public DateTime? LeasingFinishDate { get; set; }

        public int? FireProtectionAuth { get; set; }

        public DateTime? MeasureReviewDate { get; set; }

        public DateTime? DesignDate { get; set; }

        public DateTime? FireProtectionAuthDate { get; set; }

        public DateTime? AccessDate { get; set; }

        public DateTime? DecorationStartDate { get; set; }

        public DateTime? DecorationEndDate { get; set; }
    }
}
