using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Company
{
    /// <summary>
    /// 公司详情Model
    /// </summary>
    public class CompanyDetailModel : CompanyInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo { };

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get
            { return _crumbInfo; }
        }

        /// <summary>
        /// 省份
        /// </summary>
        [Required(ErrorMessage = "请选择省份")]
        public int ProvinceID { get; set; }

        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [Required(ErrorMessage = "请选择城市")]
        public int CityID { get; set; }

        public string CityName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Required(ErrorMessage = "请填写详细地址")]
        public string AddressLine { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 营业执照注册号
        /// </summary>
        [Required(ErrorMessage = "请填写营业执照注册号")]
        public string LicenseID { get; set; }

        /// <summary>
        /// 注册省份
        /// </summary>
        [Required(ErrorMessage = "请选择注册省份")]
        public int RegisterProvinceID { get; set; }

        public string RegisterProvinceName { get; set; }

        public string RegisterCityName { get; set; }

        /// <summary>
        /// 注册城市
        /// </summary>
        [Required(ErrorMessage = "请选择注册城市")]
        public int RegisterCityID { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Required(ErrorMessage = "请填写详细地址")]
        public string RegisterAddressLine { get; set; }

        /// <summary>
        /// 公司注册地址（身份，城市，具体地址拼接）
        /// </summary>
        public string CompanyRegisterAddress { get; set; }

        /// <summary>
        /// 公司法人姓名
        /// </summary>
        [Required(ErrorMessage = "请填写公司法人姓名")]
        public string OwnerName { get; set; }

        /// <summary>
        /// 联系人身份证号码 
        /// </summary>
        [Required(ErrorMessage = "请输入身份证号码")]
        [RegularExpression(@"\d{17}[\d|X]|\d{15}", ErrorMessage = "身份证号码的格式不正确")]
        public string ContactIDNumber { get; set; }

        /// <summary>
        /// 联系人邮箱
        /// </summary>
        [Required(ErrorMessage = "请填写联系人邮箱")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage = "请输入正确的Email格式\n示例：abc@nhhchina.com")]
        public string ContactEmail { get; set; }

        public int? OwnerCompanyID { get; set; }

        public FinanceModel FinanceInfo { get; set; }

       
    }
}
