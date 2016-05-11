using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    public class RepairCreateInfo
    {
        /// <summary>
        /// 排水
        /// </summary>
        public int? RepairWater { get; set; }

        /// <summary>
        /// 强电
        /// </summary>
        public int? RepairStrongElectrical { get; set; }

        /// <summary>
        /// 弱电
        /// </summary>
        public int? RepairWeakElectrical { get; set; }

        /// <summary>
        /// 暖通
        /// </summary>
        public int? RepairHVAC { get; set; }

        /// <summary>
        /// 装饰
        /// </summary>
        public int? RepairDecoration { get; set; }

        /// <summary>
        /// 消防设备
        /// </summary>
        public int? RepairFireControl { get; set; }

        /// <summary>
        /// 安防设备
        /// </summary>
        public int? RepairSecurityControl { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public int? Other { get; set; }

        /// <summary>
        /// 我的铺内
        /// </summary>
        public int? Location_MyOwnStore { get; set; }

        /// <summary>
        /// 公共区域
        /// </summary>
        public int? Location_Public { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 报修描述
        /// </summary>
        public string RequestDesc { get; set; }


        /// <summary>
        /// 是否是公共区域
        /// </summary>
        public int? IsCommon { get; set; }
         
    }
}
