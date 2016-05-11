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
    
    
    public partial class View_Project_UnitRequest : IStatusFlag
    {
        public string BizTypeName { get; set; }
        public string BizCategoryName { get; set; }
        public string BuildingName { get; set; }
        public string FloorName { get; set; }
        public int UnitID { get; set; }
        public int ProjectID { get; set; }
        public int BuildingID { get; set; }
        public int FloorID { get; set; }
        public string UnitNumber { get; set; }
        public decimal UnitAera { get; set; }
        public int UnitType { get; set; }
        public Nullable<int> BizTypeID { get; set; }
        public Nullable<int> BizCategoryID { get; set; }
        public string UnitTypeName { get; set; }
        public Nullable<decimal> RequestArea { get; set; }
        public Nullable<int> RequestID { get; set; }
        public string BrandName { get; set; }
        public Nullable<decimal> StandardRent { get; set; }
        public Nullable<decimal> StandardMgmtFee { get; set; }
        public Nullable<int> RequestStatus { get; set; }
        public string RequestStatusName { get; set; }
        public int Status { get; set; }
    }
}
