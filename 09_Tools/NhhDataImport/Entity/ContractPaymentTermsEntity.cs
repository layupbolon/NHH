using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 合同支付条款
    /// </summary>
    public class ContractPaymentTermsEntity : BaseEntity
    {
        public int PaymentTermsID { get; set; }

        public int ContractID { get; set; }

        public int PaymentTermsType { get; set; }
        public string PaymentTermsType1 { get; set; }
        public string PaymentTermsType2 { get; set; }

        public int PaymentItemID { get; set; }

        public int PaymentPeriod { get; set; }

        public int? DepositMonthly { get; set; }

        public decimal? PaymentDailyAmount { get; set; }

        public decimal? PaymentMonthlyAmount { get; set; }

        /// <summary>
        /// 费用递增信息
        /// </summary>
        public PaymentIncreaseInfo PaymentIncreaseInfo { get; set; }

        /// <summary>
        /// 费用递增表达式
        /// </summary>
        public string IncreaseExpression { get; set; }

        /// <summary>
        /// 费用扣点信息
        /// </summary>
        public PaymentCommissionInfo PaymentCommissionInfo { get; set; }

        /// <summary>
        /// 费用扣点表达式
        /// </summary>
        public string CommissionExpression { get; set; }

        public string CommissionTypeText { get; set; }

        public int CommissionWithTax { get; set; }
    }
}
