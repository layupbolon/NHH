using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户销售详情
    /// </summary>
    public class MerchantRevenueDetailModel : BaseModel
    {
        /// <summary>
        ///销售Id
        /// </summary>
        public int RevenueId { get; set; }

        /// <summary>
        /// 商铺Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 商户简称
        /// </summary>
        public string MerchantBriefName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

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

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }

    }
}
