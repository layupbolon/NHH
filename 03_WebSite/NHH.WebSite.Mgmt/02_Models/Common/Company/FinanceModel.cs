using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Company
{
    /// <summary>
    /// 公司财务信息
    /// </summary>
    public class FinanceModel
    {
        /// <summary>
        /// 银行帐号
        /// </summary>
        [Required(ErrorMessage = "请填写银行帐号")]
        public string BankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        [Required(ErrorMessage = "请填写开户银行")]
        public string OpeningBank { get; set; }

        /// <summary>
        /// 开户行名称
        /// </summary>
        [Required(ErrorMessage = "请填写开户行名称")]
        public string OpeninBankName { get; set; }

        /// <summary>
        /// 开户支行名称
        /// </summary>
        [Required(ErrorMessage = "请填写开户支行名称")]
        public string SubBranch { get; set; }

        public string AlipayAccount { get; set; }

        public string WechatAccount { get; set; }

        /// <summary>
        /// 财务联系人
        /// </summary>
        [Required(ErrorMessage = "请填写财务联系人")]
        public string FinanceContact { get; set; }

        /// <summary>
        /// 财务联系方式
        /// </summary>
        [Required(ErrorMessage = "请填写财务联系方式")]
        public string FinancePhone { get; set; }

        public int CompanyID { get; set; }

        public int InUser { get; set; }

    }
}
