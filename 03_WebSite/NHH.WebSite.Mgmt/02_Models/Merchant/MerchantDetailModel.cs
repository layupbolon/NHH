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
    /// 商户详细信息Model
    /// </summary>
    public class MerchantDetailModel : MerchantInfo
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


        [Required(ErrorMessage = "请选择省份")]
        public int ProvinceId { get; set; }

        public ProvinceInfo ProvinceInfo { get; set; }

        [Required(ErrorMessage = "请选择城市")]
        public int CityId { get; set; }

        public CityInfo CityInfo { get; set; }

        [Required(ErrorMessage = "请填写详细地址")]
        public string AddressLine { get; set; }

        public string Zipcode { get; set; }

        [Required(ErrorMessage = "请填写营业执照注册号")]
        public string LicenseId { get; set; }

        [Required(ErrorMessage = "请填写注册省份")]
        public int RegisterProvinceId { get; set; }

        public ProvinceInfo RegisterProvinceInfo { get; set; }

        [Required(ErrorMessage = "请填写注册城市")]
        public int RegisterCityId { get; set; }

        public CityInfo RegisterCityInfo { get; set; }

        [Required(ErrorMessage = "请填写注册详细地址")]
        public string RegisterAddressLine { get; set; }

        [Required(ErrorMessage = "请填写联系人身份证号码")]
        [RegularExpression(@"\d{17}[\d|X]|\d{15}", ErrorMessage = "身份证号码的格式不正确")]
        public string ContactIDNumber { get; set; }

        [Required(ErrorMessage = "请填写联系人邮箱")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage = "请输入正确的Email格式\n示例：abc@nhhchina.com")]
        public string ContactEmail { get; set; }


        /// <summary>
        ///商户财务信息
        /// </summary>
        public MerchantFinanceModel FinanceInfo { get; set; }

        /// <summary>
        /// 商户证照列表
        /// </summary>
        public List<AttachmentInfo> AttachmentList { get; set; }
    }
}
