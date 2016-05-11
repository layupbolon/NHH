using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    public class ProjectKioskInfo
    {
        public int KioskID { get; set; }
        public int ProjectID { get; set; }
        public int BuildingID { get; set; }
        public int FloorID { get; set; }
        public string KioskNumber { get; set; }
        public string Location { get; set; }
        public string FloorMapLocation { get; set; }
        public Nullable<decimal> Area { get; set; }
        public Nullable<int> BizTypeID { get; set; }
        public Nullable<decimal> OccupyRate { get; set; }
        public decimal Rent { get; set; }
        public int ContractStatus { get; set; }
        public System.DateTime ContractEnd { get; set; }
        public int Status { get; set; }
        //public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        //public Nullable<System.DateTime> EditDate { get; set; }
        //public Nullable<int> EditUser { get; set; }
    }
}
