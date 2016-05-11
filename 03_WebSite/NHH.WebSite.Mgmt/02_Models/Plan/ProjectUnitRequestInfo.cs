using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 铺位租决信息
    /// </summary>
    public class ProjectUnitRequestInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 铺位ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 铺位面积
        /// </summary>
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 租决面积
        /// </summary>
        public decimal RequestArea { get; set; }

        /// <summary>
        /// 铺位类型
        /// </summary>
        public string UnitTypeName { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public int BizType { get; set; }

        /// <summary>
        /// 业态名称
        /// </summary>
        public string BizTypeName { get; set; }

        /// <summary>
        /// 品类
        /// </summary>
        public int BizCategory { get; set; }

        /// <summary>
        /// 品类名称
        /// </summary>
        public string BizCategoryName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandName { get; set; }
        
        /// <summary>
        /// 租金
        /// </summary>
        public decimal StandardRent { get; set; }

        /// <summary>
        /// 物业费
        /// </summary>
        public decimal StandardMgmtFee { get; set; }

        /// <summary>
        /// 年合计
        /// </summary>
        public decimal YearTotal
        {
            get
            {
                var yearTotal = StandardRent * RequestArea * 360 + StandardMgmtFee * RequestArea * 360;
                return yearTotal / 10000;
            }
        }

        /// <summary>
        /// 租决状态
        /// </summary>
        public int RequestStatus { get; set; }

        /// <summary>
        /// 租决状态名称
        /// </summary>
        public string RequestStatusName { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
