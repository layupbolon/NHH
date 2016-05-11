using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Official.Banner
{
    public class BannerInfoModel
    {
        /// <summary>
        /// 详细ID
        /// </summary>
        public int InfoID { get; set; }

        /// <summary>
        /// 广告位ID
        /// </summary>
        public int BannerID { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 图片或视频路径
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        public int InUser { get; set; }

        public DateTime InDate { get; set; }

        /// <summary>
        /// 当前操作用户
        /// </summary>
        public int UserID { get; set; }
    }
}
