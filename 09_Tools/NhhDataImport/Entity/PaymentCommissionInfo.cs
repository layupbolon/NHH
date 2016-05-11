using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 费用扣点信息
    /// </summary>
    public class PaymentCommissionInfo
    {
        /// <summary>
        /// 是否含税
        /// 0：税后
        /// 1：税前
        /// </summary>
        public int WithTax { get; set; }

        /// <summary>
        /// 营业年限扣点列表
        /// </summary>
        public List<PaymentCommissionTimeItem> TimeList { get; set; }

        /// <summary>
        /// 营业金额扣点列表
        /// </summary>
        public List<PaymentCommissionAmountItem> AmountList { get; set; }

        /// <summary>
        /// 扣点备注
        /// </summary>
        public string CommissionRemark { get; set; }
    }

    /// <summary>
    /// 租金扣点营业时间项
    /// </summary>
    public class PaymentCommissionTimeItem
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 扣点比例
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 折扣扣点列表
        /// </summary>
        public List<DiscountRateInfo> DiscountRateList { get; set; }
    }

    /// <summary>
    /// 租金扣点营业金额项
    /// </summary>
    public class PaymentCommissionAmountItem
    {
        /// <summary>
        /// 金额1
        /// </summary>
        public decimal Amount1 { get; set; }

        /// <summary>
        /// 金额2
        /// </summary>
        public decimal Amount2 { get; set; }

        /// <summary>
        /// 扣点比例
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 金额备注
        /// </summary>
        public string AmountRemark { get; set; }
    }

    /// <summary>
    /// 折扣扣点信息
    /// </summary>
    public class DiscountRateInfo
    {
        /// <summary>
        /// 折扣1
        /// </summary>
        public decimal Discount1 { get; set; }

        /// <summary>
        /// 折扣2
        /// </summary>
        public decimal Discount2 { get; set; }

        /// <summary>
        /// 扣点比例
        /// </summary>
        public decimal Rate { get; set; }
    }
}
