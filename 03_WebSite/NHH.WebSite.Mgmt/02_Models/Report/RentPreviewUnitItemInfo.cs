using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// 租金预估单个商铺信息
    /// </summary>
    public class RentPreviewUnitItemInfo
    {
        /// <summary>
        /// 商铺ID
        /// </summary>
        public int UnitID { get; set; }

        /// <summary>
        /// 商铺编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 商铺面积
        /// </summary>
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public string BizTypeName { get; set; }

        /// <summary>
        /// 商铺类型
        /// </summary>
        public string UnitTypeName { get; set; }

        /// <summary>
        /// 总部租金
        /// </summary>
        public decimal StandardRent { get; set; }

        /// <summary>
        /// 物业费
        /// </summary>
        public decimal StandardMgmtFee { get; set; }

        /// <summary>
        /// 楼宇
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public decimal FloorNumber { get; set; }        

        /// <summary>
        /// 商铺状态
        /// </summary>
        public int UnitStatus { get; set; }

        /// <summary>
        /// 商铺状态
        /// </summary>
        public string UnitStatusName { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName
        {
            get
            {
                string strFormat = FloorNumber < 1 ? "{0} B{1}" : "{0} {1}F";
                return string.Format(strFormat, BuildingName, (int)(Math.Abs(FloorNumber)));
            }
        }

        /// <summary>
        /// 月租金
        /// </summary>
        public decimal MonthlyRent
        {
            get
            {
                return UnitArea * StandardRent * 30M;
            }
        }

        /// <summary>
        /// 月管理费
        /// </summary>
        public decimal MonthlyMgmt
        {
            get
            {
                return UnitArea * StandardMgmtFee * 30M;
            }
        }
    }
}
