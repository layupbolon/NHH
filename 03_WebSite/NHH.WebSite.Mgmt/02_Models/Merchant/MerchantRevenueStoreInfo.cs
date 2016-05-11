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
    /// 商户销售商铺信息
    /// </summary>
    public class MerchantRevenueStoreInfo : BaseModel
    {
        /// <summary>
        /// 商铺Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }
        
        /// <summary>
        /// 铺位列表
        /// </summary>
        public string ProjectUnitList { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public string BizTypeName { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 商户简称
        /// </summary>
        public string BriefName { get; set; }
        
        /// <summary>
        /// 营业额总计
        /// </summary>
        public decimal Revenue { get; set; }
        
        /// <summary>
        /// 每日营业额列表
        /// </summary>
        public List<MerchantRevenueItem> RevenueList { get; set; }
    }
}
