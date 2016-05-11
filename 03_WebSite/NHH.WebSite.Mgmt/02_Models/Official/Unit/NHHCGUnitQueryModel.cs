using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.Unit
{
    public class NHHCGUnitQueryModel : QueryInfo
    {
        public NHHCGUnitQueryModel()
        {
            OrderBy = "ProjectID";
        }
        public int? ProjectID { get; set; }

        //public int? Floor { get; set; }

        public int? BizType { get; set; }

        public int? Status { get; set; }

        public string UnitNumber { get; set; }

        public string Building { get; set; }

        public int? AreaScope { get; set; }
    }
}
