using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 公司
    /// </summary>
    public class CompanyEntity : BaseEntity
    {
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public int CompanyType { get; set; }

        public string CompanyTypeName { get; set; }

        public string CompanyCode { get; set; }

        public string BriefName { get; set; }

        public string Province { get; set; }

        public int? ProvinceID { get; set; }

        public string City { get; set; }

        public int? CityID { get; set; }

        public string AddressLine { get; set; }

        public string Zipcode { get; set; }

        public string LicenseID { get; set; }

        public string RegisterProvince { get; set; }

        public int? RegisterProvinceID { get; set; }

        public string RegisterCity { get; set; }

        public int? RegisterCityID { get; set; }

        public string RegisterAddressLine { get; set; }

        public string OwnerName { get; set; }

        public string ContactName { get; set; }

        public string ContactIDNumber { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string OwnerCompany { get; set; }

        public int? OwnerCompanyID { get; set; }

        public string BankAccount { get; set; }

        public string BankName { get; set; }

        public string SubBranch { get; set; }

        public string AccountName { get; set; }

        public string AlipayAccount { get; set; }

        public string WechatAccount { get; set; }

        public string FinanceContact { get; set; }

        public string FinancePhone { get; set; }
    }
}
