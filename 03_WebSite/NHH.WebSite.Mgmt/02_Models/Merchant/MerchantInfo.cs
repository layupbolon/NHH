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
    public class MerchantInfo : MerchantFinanceModel
    {

        /// <summary>
        /// 商户类型
        /// </summary>
        public int MerchantType { get; set; }

        /// <summary>
        /// 商户类型名称
        /// </summary>
        public string MerchantTypeName { get; set; }

        /// <summary>
        /// 商户类型名称
        /// </summary>
        public MerchantTypeInfo MerchantTypeInfo { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        [Required(ErrorMessage = "请填写商户名称")]
        public string MerchantName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [Required(ErrorMessage = "请填写商户中文简称")]
        public string BriefName { get; set; }

        /// <summary>
        /// 商户编码
        /// </summary>
        [Required(ErrorMessage = "请填写公司编码")]
        [RegularExpression(@"[a-zA-Z0-9]+$", ErrorMessage = "请填写英文或数字")]
        public string MerchantCode { get; set; }

        /// <summary>
        /// 商户地址（省份+城市）
        /// </summary>
        public string MerchantAddress { get; set; }

        /// <summary>
        /// 法人
        /// </summary>
        [Required(ErrorMessage = "请填写注册法人姓名")]
        public string OwnerName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Required(ErrorMessage = "请填写联系人姓名")]
        public string ContactName { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [Required(ErrorMessage = "请填写联系人电话")]
        public string ContactPhone { get; set; }
    }
}
