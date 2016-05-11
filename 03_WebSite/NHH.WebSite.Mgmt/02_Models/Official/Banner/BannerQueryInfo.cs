using NHH.Models.Common;

namespace NHH.Models.Official.Banner
{
    public class BannerQueryInfo : QueryInfo
    {
        private int? _bannerTarget = 1;

        /// <summary>
        /// 目标
        /// </summary>
        public int? BannerTarget
        {
            get { return _bannerTarget; }
            set { _bannerTarget = value; }
        }

        /// <summary>
        /// 项目ID
        /// 若是唐小二则必填
        /// </summary>
        public int? ProjectID { get; set; }

        /// <summary>
        /// 类型
        /// 文字、图片、图文、其他
        /// </summary>
        public int? BannerType { get; set; }
    }
}
