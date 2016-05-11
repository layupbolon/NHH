using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户品牌列表
    /// </summary>
    public class MerchantBrandListModel : BaseModel
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 品牌列表
        /// </summary>
        public List<MerchantBrandInfo> BrandList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
