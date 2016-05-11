namespace NHH.Models.Official
{
    /// <summary>
    /// 官网首页顶部banner
    /// </summary>
    public class BannerInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 背景图片地址
        /// </summary>
        public string BackgroundImageUrl { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Seq { get; set; }
    }
}
