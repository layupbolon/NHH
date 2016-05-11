using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 商户
    /// </summary>
    public class MerchantEntity : BaseEntity
    {
        public string MerchantName { get; set; }
        public int MerchantID { get; set; }
        public int MerchantType { get; set; }
        public string MerchantCode { get; set; }
        public string BriefName { get; set; }
        public int? ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public string AddressLine { get; set; }
        public string Zipcode { get; set; }
        public string LicenseID { get; set; }
        public int? RegisterProvinceID { get; set; }
        public string RegisterProvinceName { get; set; }
        public int? RegisterCityID { get; set; }
        public string RegisterCityName { get; set; }
        public string RegisterAddressLine { get; set; }
        public string OwnerName { get; set; }
        public string ContactName { get; set; }
        public string ContactIDNumber { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        #region 商户财务信息
        public string BankAccount { get; set; }

        public string BankName { get; set; }

        public string SubBranch { get; set; }

        public string AccountName { get; set; }

        public string AlipayAccount { get; set; }

        public string WechatAccount { get; set; }

        public string FinanceContact { get; set; }

        public string FinancePhone { get; set; }
        #endregion

        /// <summary>
        /// 附件文件列表
        /// </summary>
        public string AttachmentFiles { get; set; }

    }
}
