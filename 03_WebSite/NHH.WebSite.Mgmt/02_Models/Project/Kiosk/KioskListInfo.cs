using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.Kiosk
{
    public class KioskListInfo
    {
        /// <summary>
        /// 多经点编号
        /// </summary>
        public int KioskId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 多经点位编号
        /// </summary>
        public string KioskNumber { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 计租面积
        /// </summary>
        public decimal? Area { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public string FloorMapLocation { get; set; }

        /// <summary>
        /// 平面图X
        /// </summary>
        public int? FloorMapX { get; set; }

        /// <summary>
        /// 平面图Y
        /// </summary>
        public int? FloorMapY { get; set; }

        /// <summary>
        /// 平面图位置
        /// </summary>
        public string FloorMapFileName { get; set; }

        /// <summary>
        /// 多经点位类型名
        /// </summary>
        public string KioskTypeName { get; set; }
    }
}
