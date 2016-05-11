using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 支付条款信息
    /// </summary>
    public class PaymentTermInfo
    {
        public int PaymentTermsID { get; set; }

        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContractID { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 租约面积
        /// </summary>
        public decimal ContractArea { get; set; }

        public int PaymentTermsType { get; set; }

        public string PaymentTermsTypeText { get; set; }

        public int PaymentItemID { get; set; }

        public int PaymentPeriod { get; set; }

        public int DepositMonthly { get; set; }

        public decimal PaymentDailyAmount { get; set; }

        public decimal PaymentMonthlyAmount { get; set; }

        public string IncreaseExpression { get; set; }

        public string CommissionExpression { get; set; }

        public PaymentIncreaseInfo IncreaseInfo { get; set; }

        public PaymentCommissionInfo CommissionInfo { get; set; }
    }
}
