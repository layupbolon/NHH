using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    public class MerChantAndBrandModel
    {
        public int MerchantID { get; set; }
        //public int ProjectID { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string MerchantName { get; set; }

        public int ProjectID{ get; set; }
    }
}
