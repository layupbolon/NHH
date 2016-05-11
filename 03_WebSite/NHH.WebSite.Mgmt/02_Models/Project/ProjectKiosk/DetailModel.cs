using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectKiosk
{
    public class KioskDetailModel
    {
        public int KioskId { get; set; }
        public int ProjectID { get; set; }
        public int BuildingID { get; set; }
        public string BuildingName { get; set; }


        public int FloorID { get; set; }
        public int FloorNumber { get; set; }
        public string KioskNumber { get; set; }
        public string Location { get; set; }
        public decimal Area { get; set; }
        public string FloorMapLocation { get; set; }
        public int BizTypeID { get; set; }
        public decimal OccupyRate { get; set; }
        public decimal Rent { get; set; }
        public int ContractStatus { get; set; }
        public decimal ContractEnd { get; set; }
        public int Status { get; set; }

    }
}
