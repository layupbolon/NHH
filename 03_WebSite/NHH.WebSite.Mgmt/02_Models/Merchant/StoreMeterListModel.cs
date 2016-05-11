using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商铺计量表列表
    /// </summary>
    public class StoreMeterListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public StoreMeterListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 商铺列表
        /// </summary>
        public List<MerchantStoreInfo> StoreList { get; set; }

        /// <summary>
        /// 计量表列表
        /// </summary>
        public List<StoreMeterInfo> MeterList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

    }
}
