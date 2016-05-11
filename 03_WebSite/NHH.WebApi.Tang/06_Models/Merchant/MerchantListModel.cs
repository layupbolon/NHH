using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{

    public class MerchantListModel:BaseModel
    {
        /// <summary>
        /// 商户列表Model
        /// </summary>
        public List<MerchantInfo> MerchantList { get; set; }

        /// <summary>
        ///分页信息 
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
