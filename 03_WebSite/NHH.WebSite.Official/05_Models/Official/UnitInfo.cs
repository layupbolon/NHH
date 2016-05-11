using System.Collections.Generic;

namespace NHH.Models.Official
{
    public class UnitInfo
    {
        /// <summary>
        /// 铺位ID
        /// </summary>
        public int UnitID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int projectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectName { get; set; }

        /// <summary>
        /// 楼宇
        /// </summary>
        public string building { get; set; }

        /// <summary>
        /// 铺位号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public decimal Area { get; set; }

        /// <summary>
        /// 物业费
        /// </summary>
        public decimal ProperyCosts { get; set; }

        /// <summary>
        /// 商铺位置
        /// </summary>
        public string UnitPosition { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 店铺封面图片
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 楼层描述
        /// </summary>
        public string FloorRemark { get; set; }

        /// <summary>
        /// 业态类型
        /// </summary>
        public string bizTypes { get; set; }

        /// <summary>
        /// 业态类型
        /// </summary>
        public string bizTypesName { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        public string RentRemark { get; set; }

        /// <summary>
        /// 店铺图片列表
        /// </summary>
        public List<string> PicList { get; set; }

        public string Position { get; set; }
    }
}
