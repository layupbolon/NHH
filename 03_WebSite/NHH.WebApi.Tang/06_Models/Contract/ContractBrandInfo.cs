using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    public class ContractBrandInfo
    {
        public int ContractBrandID { get; set; }

        public int ContractID { get; set; }

        public int MerchantBrandID { get; set; }

        public string BrandName { get; set; }

        public string BrandIcon { get; set; }

        public int Status { get; set; }

    }
}
