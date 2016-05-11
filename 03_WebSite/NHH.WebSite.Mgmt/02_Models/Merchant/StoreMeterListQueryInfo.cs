using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商铺计量表查询信息
    /// </summary>
    public class StoreMeterListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 商铺计量表查询信息
        /// </summary>
        public StoreMeterListQueryInfo()
        {
            OrderBy = "StoreName";
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 商铺ID
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }
    }
}
