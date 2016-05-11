using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户商铺每日营业额
    /// </summary>
    public class MerchantRevenueItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int RevenueId { get; set; } 

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 税前营业额
        /// </summary>
        public decimal Revenue { get; set; }

        /// <summary>
        /// 税后营业额
        /// </summary>
        public decimal? AfterTax { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal? TaxRate { get; set; }
    }
}
