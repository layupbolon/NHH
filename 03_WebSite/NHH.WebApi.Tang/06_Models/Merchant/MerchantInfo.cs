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
    /// 商户信息
    /// </summary>
    public class MerchantInfo
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 商户类型
        /// </summary>
        public int MerchantType { get; set; }

        /// <summary>
        /// 商户类型名称
        /// </summary>
        public string MerchantTypeInfo { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string BriefName { get; set; }

        /// <summary>
        /// 商户编码
        /// </summary>
        public string MerchantCode { get; set; }

        /// <summary>
        /// 商户地址（省份+城市）
        /// </summary>
        public string MerchantAddress { get; set; }

        /// <summary>
        /// 法人
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }

    }
}
