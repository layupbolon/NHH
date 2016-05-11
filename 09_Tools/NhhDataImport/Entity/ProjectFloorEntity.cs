using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 楼层
    /// </summary>
    public class ProjectFloorEntity : BaseEntity
    {
        public string FloorName { get; set; }
        public string ProjectName { get; set; }
        public string BuildingName { get; set; }
        public int FloorID { get; set; }
        public int BuildingID { get; set; }
        public int ProjectID { get; set; }
        public string FloorMapFileName { get; set; }
        public int FloorNumber { get; set; }
        public string FloorDescription { get; set; }
        public decimal? TotalRentArea { get; set; }
        public int? TotalRentUnit { get; set; }
        public decimal? ContractRentArea { get; set; }
        public int? ContractRentUnit { get; set; }
    }
}
