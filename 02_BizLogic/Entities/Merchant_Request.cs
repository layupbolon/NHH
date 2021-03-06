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
    
    
    public partial class Merchant_Request : IStatusFlag,IEditable
    {
        public int RequestID { get; set; }
        public Nullable<int> MerchantID { get; set; }
        public string BrandID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string BuildingIDs { get; set; }
        public string FloorIDs { get; set; }
        public string UnitIDs { get; set; }
        public string SpecialRequest { get; set; }
        public Nullable<decimal> AreaRequest { get; set; }
        public Nullable<int> FloorBearing { get; set; }
        public Nullable<int> FloorHeight { get; set; }
        public Nullable<int> RentLength { get; set; }
        public string RentMethod { get; set; }
        public string PaymentMethod { get; set; }
        public string Condition { get; set; }
        public Nullable<int> DecorationLength { get; set; }
        public Nullable<int> ElectricityUsage { get; set; }
        public Nullable<int> WaterUsage { get; set; }
        public Nullable<int> DrainUsage { get; set; }
        public Nullable<int> GasUsage { get; set; }
        public Nullable<int> SmokeUsage { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    }
}
