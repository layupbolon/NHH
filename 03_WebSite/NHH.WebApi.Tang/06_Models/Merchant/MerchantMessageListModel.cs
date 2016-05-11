using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Merchant
{
    public class MerchantMessageListModel
    {
        public List<MerchantMessageInfo> MerchantMessageInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
