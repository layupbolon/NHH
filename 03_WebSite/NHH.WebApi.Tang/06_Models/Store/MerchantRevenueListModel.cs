using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Store
{
    public class MerchantRevenueListModel
    {
        public List<MerchantRevenueInfo> MerchantRevenueInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
