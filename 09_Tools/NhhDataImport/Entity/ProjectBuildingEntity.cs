using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 楼宇
    /// </summary>
    public class ProjectBuildingEntity : BaseEntity
    {
        public int FloorID { get; set; }
        public string BuildingName { get; set; }
        public int ProjectID { get; set; }
        public int BuildingID { get; set; }
        public string BuildingCode { get; set; }
        public int? GroundFloorNum { get; set; }
        public int? UndergroundFloorNum { get; set; }
        public decimal? BuildingGroundArea { get; set; }
        public decimal? BuildingUndergroundArea { get; set; }
        public decimal? TotalConstructionArea { get; set; }
        public decimal? TotalRentArea { get; set; }
        public int? TotalRentUnit { get; set; }
        public string PlanSummary { get; set; }
        public string PlanHighlight { get; set; }
        public string RenderingFileName { get; set; }
        public string ContractStore { get; set; }
        public string ProjectName { get; set; }
    }
}
