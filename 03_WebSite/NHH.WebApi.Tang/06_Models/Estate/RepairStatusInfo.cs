using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    public class RepairStatusInfo
    {
        /// <summary>
        /// 商铺ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 待处理数量
        /// </summary>
        public int PendingQty { get; set; }

        /// <summary>
        /// 处理中数量
        /// </summary>
        public int ProcessingQty { get; set; }

        /// <summary>
        /// 未评论数量
        /// </summary>
        public int NoCommentsQty { get; set; }
    }
}
