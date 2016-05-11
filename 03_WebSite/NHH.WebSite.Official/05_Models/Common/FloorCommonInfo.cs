using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 楼层公共信息
    /// </summary>
    public class FloorCommonInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 所属楼宇ID
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 所属楼宇名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int FloorId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FloorName
        {
            get
            {
                string str = (FloorNumber < 0) ? "B" : "";
                return string.Format("{0} {1}{2}F", BuildingName, str, (int)Math.Abs(FloorNumber));
            }
        }

        /// <summary>
        /// 楼层数
        /// </summary>
        public decimal FloorNumber { get; set; }

        /// <summary>
        /// 商铺数量
        /// </summary>
        public int UnitNumber { get; set; }

        /// <summary>
        /// 平面图文件名称
        /// </summary>
        public string FloorMapFileName { get; set; }
    }
}
