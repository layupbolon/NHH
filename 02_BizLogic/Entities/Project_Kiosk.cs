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
    
    
    public partial class Project_Kiosk : IStatusFlag,IEditable
    {
        public int KioskID { get; set; }
        public int ProjectID { get; set; }
        public int BuildingID { get; set; }
        public int FloorID { get; set; }
        public string KioskNumber { get; set; }
        public int KioskType { get; set; }
        public string Location { get; set; }
        public string FloorMapLocation { get; set; }
        public Nullable<decimal> Area { get; set; }
        public Nullable<int> BizTypeID { get; set; }
        public Nullable<decimal> OccupyRate { get; set; }
        public decimal Rent { get; set; }
        public int ContractStatus { get; set; }
        public Nullable<System.DateTime> ContractEnd { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual Project_Building Project_Building { get; set; }
        public virtual Project_Floor Project_Floor { get; set; }
    }
}
