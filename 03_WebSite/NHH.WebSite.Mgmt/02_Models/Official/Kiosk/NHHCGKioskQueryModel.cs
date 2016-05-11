using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.Kiosk
{
    public class NHHCGKioskQueryModel : QueryInfo
    {
        public NHHCGKioskQueryModel()
        {
            OrderBy = "KioskID";
        }
        public int? ProjectID { get; set; }

        public int? FloorID { get; set; }

        public int? Status { get; set; }

        public string KioskNumber { get; set; }

        public string Building { get; set; }

        public int? Region { get; set; }

        //public int? BusinessScope { get; set; }

        public int? AreaScope { get; set; }
    }
}
