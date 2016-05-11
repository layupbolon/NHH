using System.Collections.Generic;

namespace NHH.Models.Official
{
    public class KioskInfo
    {
        /// <summary>
        /// 多经点位ID
        /// </summary>
        public int KioskID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int FloorID { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int Region { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 多经点位号
        /// </summary>
        public string KioskNumber { get; set; }

        /// <summary>
        /// 面积范围
        /// </summary>
        public int AreaScope { get; set; }

        /// <summary>
        /// 面积范围
        /// </summary>
        public string AreaScopeName { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        public string RentRemark { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 经营范围
        /// </summary>
        public string BusinessScopes { get; set; }

        /// <summary>
        /// 经营范围
        /// </summary>
        public string BusinessScopesName { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 照片列表
        /// </summary>
        public List<string> PicList { get; set; } 
    }
}
