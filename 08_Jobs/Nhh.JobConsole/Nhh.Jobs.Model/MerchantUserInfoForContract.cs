using System;

namespace Nhh.Jobs.Model
{
    public class MerchantUserInfoForContract : MerchantUserInfo
    {
        public int ContractID { get; set; }

        public string ContractCode { get; set; }

        public string ContractEndDate { get; set; }

        public string StoreName { get; set; }
    }
}
