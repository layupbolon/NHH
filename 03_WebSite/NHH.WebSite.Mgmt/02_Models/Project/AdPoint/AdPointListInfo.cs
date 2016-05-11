using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.Adpoint
{
    public class AdPointListInfo
    {
        /// <summary>
        /// 广告位编号
        /// </summary>
        public int AdPointId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorName { get; set; }

        public int AdPointType { get; set; }

        public string AdPointTypeName { get; set; }

        /// <summary>
        /// 广告媒介 例如：灯箱、柱体、墙体、LOGO、侧旗、吊幔等
        /// </summary>
        public string AdPointMediaName { get; set; }

        /// <summary>
        /// 广告位号
        /// </summary>
        public string AdPointNumber { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 计租面积
        /// </summary>
        public decimal? Area { get; set; }

        public string FloorMapLocation { get; set; }

        /// <summary>
        /// 楼层的图片
        /// </summary>
        public string FloorMapFileName { get; set; }

        public int? FloorMapX { get; set; }

        public int? FloorMapY { get; set; }
    }
}
