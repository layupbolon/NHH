using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 商铺列表项
    /// </summary>
    public class ProjectUnitListItem
    {
        /// <summary>
        /// 商铺ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 商铺编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public decimal FloorNumber { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName
        {
            get
            {
                string str = (FloorNumber < 0) ? "B" : "";
                return string.Format("{0} {1}{2}F", BuildingName, str, (int)Math.Abs(FloorNumber));
            }
        }
    }
}
