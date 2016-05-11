//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NHH.Entities
{
    using System;
    using System.Collections.Generic;
    using NHH.Framework;
    using NHH.Framework.Data;
    
    
    public partial class Project_Building : IStatusFlag,IEditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project_Building()
        {
            this.Project_AdPoint = new HashSet<Project_AdPoint>();
            this.Project_BizConfig = new HashSet<Project_BizConfig>();
            this.Project_Floor = new HashSet<Project_Floor>();
            this.Project_Kiosk = new HashSet<Project_Kiosk>();
            this.Project_Unit = new HashSet<Project_Unit>();
        }
    
        public int BuildingID { get; set; }
        public int ProjectID { get; set; }
        public string BuildingCode { get; set; }
        public string BuildingName { get; set; }
        public Nullable<int> GroundFloorNum { get; set; }
        public Nullable<int> UndergroundFloorNum { get; set; }
        public Nullable<decimal> BuildingGroundArea { get; set; }
        public Nullable<decimal> BuildingUndergroundArea { get; set; }
        public Nullable<decimal> TotalConstructionArea { get; set; }
        public Nullable<decimal> TotalRentArea { get; set; }
        public Nullable<int> TotalRentUnit { get; set; }
        public string PlanSummary { get; set; }
        public string PlanHighlight { get; set; }
        public string RenderingFileName { get; set; }
        public string ContractStore { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Project Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project_AdPoint> Project_AdPoint { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project_BizConfig> Project_BizConfig { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project_Floor> Project_Floor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project_Kiosk> Project_Kiosk { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project_Unit> Project_Unit { get; set; }
    }
}