using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 楼层合计信息
    /// </summary>
    public class MerchantRevenueReportFloorCount
    {
        /// <summary>
        /// 日期列表
        /// </summary>
        public List<MerchantRevenueReportDateCount> DateList { get; set; }

        /// <summary>
        /// 销售额合计
        /// </summary>
        public decimal TotalRevenue { get; set; }
    }
}
