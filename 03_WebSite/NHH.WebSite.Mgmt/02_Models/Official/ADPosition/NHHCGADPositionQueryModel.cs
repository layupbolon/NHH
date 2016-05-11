using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.ADPosition
{
    public class NHHCGADPositionQueryModel : QueryInfo
    {
        public int? ProjectID { get; set; }

        //public int? FloorID { get; set; }

        public int? ADType { get; set; }

        public int? Status { get; set; }

        public string ADPositionNumber { get; set; }

        public string Building { get; set; }

        public int? Region { get; set; }

        public int? ElectricityType { get; set; }
    }
}
