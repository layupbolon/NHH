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
    
    
    public partial class View_ProjectInfoShow
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int RegionID { get; set; }
        public int ProvinceID { get; set; }
        public int CityID { get; set; }
        public int Stage { get; set; }
        public Nullable<System.DateTime> GrandOpeningDate { get; set; }
        public Nullable<int> ParkingLotNum { get; set; }
        public Nullable<decimal> SumTotalConstructionArea { get; set; }
        public Nullable<decimal> TotalRentArea { get; set; }
        public string CompanyName { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public Nullable<int> AdPointNum { get; set; }
        public string RegionName { get; set; }
    }
}
