using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Company
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class CompanyInfo : FinanceModel
    {
        /// <summary>
        /// 公司类型
        /// </summary>
        [Required(ErrorMessage = "请填写公司类型")]
        public int CompanyType { get; set; }

        /// <summary>
        /// 公司类型名称
        /// </summary>
        public CompanyTypeInfo CompanyTypeInfo { get; set; }

        /// <summary>
        /// 公司类型名称
        /// </summary>
        //public string CompanyTypeName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Required(ErrorMessage = "请填写公司名称")]

        public string CompanyName { get; set; }

        /// <summary>
        /// 公司英文编码
        /// </summary>
        [Required(ErrorMessage = "请填写公司英文编码")]
        [RegularExpression(@"[a-zA-Z0-9]+$", ErrorMessage = "请填写英文或数字")]
        public string CompanyCode { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [Required(ErrorMessage = "请填写公司中文简称")]
        public string BriefName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Required(ErrorMessage = "请填写联系人姓名")]
        public string ContactName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "请填写联系人电话")]
        public string ContactPhone { get; set; }
    }
}
