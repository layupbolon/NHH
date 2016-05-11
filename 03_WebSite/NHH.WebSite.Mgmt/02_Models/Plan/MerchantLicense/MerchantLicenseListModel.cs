using System.Collections.Generic;
using NHH.Models.Common;

namespace NHH.Models.Plan.MerchantLicense
{
    public class MerchantLicenseListModel : BaseModel
    {
        public List<MerchantLicenseModel> MerchantLicenseList { get; set; }

        public MerchantLicenseQueryModel QueryInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
