using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Store
{
    public class MerchantRevenueListQuery
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public int? StoreID { get; set; }

        /// <summary>
        /// 帐期开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 帐期结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int Size { get; set; }
    }
}
