using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 商户公共信息
    /// </summary>
    public class MerchantCommonInfo
    {
        /// <summary>
        /// 商户Id
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }
    }
}
