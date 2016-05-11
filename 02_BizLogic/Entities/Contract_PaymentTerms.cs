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
    
    
    public partial class Contract_PaymentTerms : IStatusFlag,IEditable
    {
        public int PaymentTermsID { get; set; }
        public int ContractID { get; set; }
        public int PaymentTermsType { get; set; }
        public int PaymentItemID { get; set; }
        public int PaymentPeriod { get; set; }
        public Nullable<int> DepositMonthly { get; set; }
        public Nullable<decimal> PaymentDailyAmount { get; set; }
        public Nullable<decimal> PaymentMonthlyAmount { get; set; }
        public string IncreaseExpression { get; set; }
        public string CommissionExpression { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Contract Contract { get; set; }
        public virtual Finance_Item Finance_Item { get; set; }
    }
}
