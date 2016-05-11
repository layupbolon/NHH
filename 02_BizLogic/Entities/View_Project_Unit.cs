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
    
    
    public partial class View_Project_Unit : IStatusFlag,IEditable
    {
        public int UnitID { get; set; }
        public int ProjectID { get; set; }
        public int BuildingID { get; set; }
        public int FloorID { get; set; }
        public string UnitNumber { get; set; }
        public decimal UnitAera { get; set; }
        public int UnitStatus { get; set; }
        public int UnitType { get; set; }
        public Nullable<int> BizTypeID { get; set; }
        public Nullable<int> BizCategoryID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public int ContractStatus { get; set; }
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        public string UnitMapFileName { get; set; }
        public string FloorMapLocation { get; set; }
        public string FloorMapDimension { get; set; }
        public string Tag { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
        public string ProjectName { get; set; }
        public string BuildingName { get; set; }
        public int FloorNumber { get; set; }
        public string FloorName { get; set; }
        public string FloorMapFileName { get; set; }
    }
}
