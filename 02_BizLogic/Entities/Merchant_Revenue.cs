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
    
    
    public partial class Merchant_Revenue : IStatusFlag,IEditable
    {
        public int RevenueID { get; set; }
        public int StoreID { get; set; }
        public int MerchantID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public decimal Revenue { get; set; }
        public Nullable<decimal> AfterTax { get; set; }
        public Nullable<decimal> TaxRate { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Merchant Merchant { get; set; }
        public virtual Merchant_Store Merchant_Store { get; set; }
    }
}
