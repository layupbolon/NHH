using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户用户查询信息
    /// </summary>
    public class MerchantUserGroupQueryInfo : QueryInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 铺位类型
        /// </summary>
        public int? UnitType { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public int? BizType { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int? Role { get; set; }

        /// <summary>
        /// 商铺编号
        /// </summary>
        public string UnitNumber { get; set; }
    }
}
