using System;

namespace NHH.Models.Official
{
    public class NewsInfo
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        public int NewsID { get; set; }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 新闻简介
        /// </summary>
        public string NewsBrief { get; set; }

        /// <summary>
        /// 新闻图片
        /// </summary>
        public string NewsCover { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int Stick { get; set; }
    }
}
