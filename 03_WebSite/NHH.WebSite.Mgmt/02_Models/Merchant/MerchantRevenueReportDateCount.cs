using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 日期合计
    /// </summary>
    public class MerchantRevenueReportDateCount
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 销售额合计
        /// </summary>
        public decimal TotalRevenue { get; set; }
    }
}
