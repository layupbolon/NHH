using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    public class ProjectUnitInfo
    {
        public int UnitID { get; set; }
        public int ProjectID { get; set; }
        public int BuildingID { get; set; }
        public int FloorID { get; set; }
        public string UnitNumber { get; set; }
        public decimal UnitAera { get; set; }
        public int UnitStatus { get; set; }
        public int UnitType { get; set; }
        public int BizTypeID { get; set; }
        public int BizCategoryID { get; set; }
        public int StoreID { get; set; }
        public int ContractStatus { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string UnitMapFileName { get; set; }
        public string FloorMapLocation { get; set; }
        public string FloorMapDimension { get; set; }
        public string Tag { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public DateTime EditDate { get; set; }
        public int EditUser { get; set; }
    }
}
