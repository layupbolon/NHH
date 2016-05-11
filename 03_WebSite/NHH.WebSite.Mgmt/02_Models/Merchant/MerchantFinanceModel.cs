using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户财务信息
    /// </summary>
    public class MerchantFinanceModel
    {
        public int MerchantFinanceID { get; set; }

        public int MerchantId { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        [Required(ErrorMessage = "请填写银行账号")]
        public string BankAccount { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        [Required(ErrorMessage = "请填写开户银行")]
        public string BankName { get; set; }

        /// <summary>
        /// 开户支行
        /// </summary>
        [Required(ErrorMessage = "请填写开户支行")]
        public string SubBranch { get; set; }

        /// <summary>
        /// 开户名称
        /// </summary>
        [Required(ErrorMessage = "请填写开户名称")]
        public string AccountName { get; set; }

        public string AlipayAccount { get; set; }

        public string WechatAccount { get; set; }

        [Required(ErrorMessage = "请填写财务联系人")]
        public string FinanceContact { get; set; }

        [Required(ErrorMessage = "请填写财务联系方式")]
        public string FinancePhone { get; set; }

        public int UserId { get; set; }
    }
}
