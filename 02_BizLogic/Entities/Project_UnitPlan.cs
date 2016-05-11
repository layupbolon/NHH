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
    
    
    public partial class Project_UnitPlan : IStatusFlag,IEditable
    {
        public int UnitPlanID { get; set; }
        public int UnitID { get; set; }
        public Nullable<int> BizTypeID { get; set; }
        public Nullable<int> BizCategoryID { get; set; }
        public Nullable<int> UnitType { get; set; }
        public Nullable<int> IsBenchmarking { get; set; }
        public Nullable<decimal> StandardRent { get; set; }
        public Nullable<decimal> RecommendedRent { get; set; }
        public Nullable<decimal> QuotationRent { get; set; }
        public Nullable<decimal> StandardMgmtFee { get; set; }
        public Nullable<int> RentLengthUpper { get; set; }
        public Nullable<int> RentLengthBottom { get; set; }
        public Nullable<int> IncreaseType { get; set; }
        public Nullable<int> IncreaseAmountType { get; set; }
        public Nullable<decimal> IncreaseAmount { get; set; }
        public Nullable<int> IncreaseStartYears { get; set; }
        public Nullable<int> IncreaseStepYears { get; set; }
        public Nullable<int> DepositMonthly { get; set; }
        public Nullable<int> PaymentPeriod { get; set; }
        public Nullable<decimal> BidBond { get; set; }
        public Nullable<decimal> ManageBond { get; set; }
        public Nullable<int> RentFreeLength { get; set; }
        public Nullable<int> DecorationLength { get; set; }
        public Nullable<decimal> DecorationMgmtFee { get; set; }
        public Nullable<decimal> DecorationBond { get; set; }
        public Nullable<decimal> QualityBond { get; set; }
        public Nullable<int> ParkingLotNum { get; set; }
        public Nullable<int> AdPointNum { get; set; }
        public Nullable<int> Condition { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Project_Unit Project_Unit { get; set; }
    }
}