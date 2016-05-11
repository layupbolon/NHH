using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 商铺
    /// </summary>
    public class ProjectUnitEntity : BaseEntity
    {
        public int UnitID { get; set; }
        
        public string UnitNumber { get; set; }

        public string ProjectName { get; set; }

        public int ProjectID { get; set; }

        public string BuildingName { get; set; }
        
        public int BuildingID { get; set; }

        public string FloorName { get; set; }
        
        public int FloorID { get; set; }
        
        public decimal UnitAera { get; set; }
        
        public int UnitStatus { get; set; }
        
        public int UnitType { get; set; }

        public string UnitTypeName { get; set; }
        
        public int? BizTypeID { get; set; }

        public string BizTypeName { get; set; }
        
        public int? BizCategoryID { get; set; }
        
        public int? StoreID { get; set; }
        
        public int ContractStatus { get; set; }
        
        public DateTime? ContractEndDate { get; set; }
        
        public string UnitMapFileName { get; set; }
        
        public string FloorMapLocation { get; set; }
        
        public string FloorMapDimension { get; set; }
        
        public string Tag { get; set; }

        public ProjectUnitLeasingEntity UnitLeasing { get; set; }

        public ProjectUnitPlanEntity UnitPlan { get; set; }
    }
}
