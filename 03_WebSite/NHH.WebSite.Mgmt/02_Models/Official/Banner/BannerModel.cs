using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.Banner
{
    public class BannerModel : BaseModel
    {
        /// <summary>
        /// 广告位ID
        /// </summary>
        public int BannerID { get; set; }

        /// <summary>
        /// 目标
        /// 1唐小二  2官网
        /// </summary>
        public int BannerTarget { get; set; }

        /// <summary>
        /// 目标名称
        /// </summary>
        public string BannerTargetName { get; set; }

        /// <summary>
        /// 项目ID
        /// 若是唐小二则必填
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 类型
        /// 文字、图片、图文、其他
        /// </summary>
        public int BannerType { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string BannerTypeName { get; set; }

        /// <summary>
        /// 位置ID
        /// </summary>
        public int LocationID { get; set; }

        /// <summary>
        /// Banner位置
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// 状态
        /// -1无效  1有效
        /// </summary>
        public int Status { get; set;}

        /// <summary>
        /// 状态名称
        /// </summary>
        public string BannerStatusName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set;}

        public int InUser { get; set; }

        public DateTime InDate { get; set; }

        /// <summary>
        /// 当前操作用户
        /// </summary>
        public int UserID { get; set; }
    }
}
