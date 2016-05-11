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

        /// <summary>
        /// 商户ID 
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 开户支行
        /// </summary>
        public string SubBranch { get; set; }

        /// <summary>
        /// 开户名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        public string AlipayAccount { get; set; }

        /// <summary>
        /// 微信商家账号
        /// </summary>
        public string WechatAccount { get; set; }

        /// <summary>
        /// 财务联系人
        /// </summary>
        public string FinanceContact { get; set; }

        /// <summary>
        /// 财务联系方式
        /// </summary>
        public string FinancePhone { get; set; }

        public int InUser { get; set; }

        public DateTime? InDate { get; set; }

        public int EditUser { get; set; }

        public DateTime? EditDate{ get; set; }

    }
}
