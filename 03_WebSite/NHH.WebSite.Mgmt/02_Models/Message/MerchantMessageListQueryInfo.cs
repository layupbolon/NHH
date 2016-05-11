using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Message
{
    /// <summary>
    /// 商户消息列表查询信息
    /// </summary>
    public class MerchantMessageListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 商户消息列表查询信息
        /// </summary>
        public MerchantMessageListQueryInfo()
        {
            OrderBy = "MessageID";
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
        /// 业态
        /// </summary>
        public int? BizType { get; set; }

        /// <summary>
        /// 铺位类型
        /// </summary>
        public int? UnitType { get; set; }

        /// <summary>
        /// 铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        public int? UserId { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int? MerchantId { get; set; }

        /// <summary>
        /// 商铺ID
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }
        
        public DateTime? StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }
    }
}
