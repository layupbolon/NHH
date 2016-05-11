using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户商铺详情
    /// </summary>
    public class MerchantStoreDetailModel : MerchantStoreInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo { };

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get { return _crumbInfo; }
        }

        /// <summary>
        /// 商户信息
        /// </summary>
        public MerchantInfo MerchantInfo { get; set; }
    }
}
