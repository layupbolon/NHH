using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 商铺筹划
    /// </summary>
    public class ProjectUnitPlanEntity : BaseEntity
    {
        public int? BizTypeID { get; set; }

        public int? BizCategoryID { get; set; }

        public int? UnitType { get; set; }

        public int? IsBenchmarking { get; set; }

        public decimal? StandardRent { get; set; }

        public decimal? RecommendedRent { get; set; }

        public decimal? QuotationRent { get; set; }

        public decimal? StandardMgmtFee { get; set; }

        public int? RentLengthUpper { get; set; }

        public int? RentLengthBottom { get; set; }

        public int? IncreaseType { get; set; }

        public int? IncreaseAmountType { get; set; }

        public decimal? IncreaseAmount { get; set; }

        public int? IncreaseStartYears { get; set; }

        public int? IncreaseStepYears { get; set; }

        public int? DepositMonthly { get; set; }

        public int? PaymentPeriod { get; set; }

        public decimal? BidBond { get; set; }

        public decimal? ManageBond { get; set; }

        public int? RentFreeLength { get; set; }

        public int? DecorationLength { get; set; }

        public decimal? DecorationMgmtFee { get; set; }

        public decimal? DecorationBond { get; set; }

        public decimal? QualityBond { get; set; }

        public int? ParkingLotNum { get; set; }

        public int? AdPointNum { get; set; }

        public int? Condition { get; set; }
    }
}
