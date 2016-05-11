using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    public class ProjectAdPointInfo
    {
        public int AdPointID { get; set; }
        public int ProjectID { get; set; }
        public int BuildingID { get; set; }
        public int FloorID { get; set; }
        public string AdPointNumber { get; set; }
        public string Location { get; set; }
        public string FloorMapLocation { get; set; }
        public int AdPointType { get; set; }
        public decimal Rent { get; set; }
        public int ContractStatus { get; set; }
        public System.DateTime ContractEnd { get; set; }
        public int Status { get; set; }
    }
}
