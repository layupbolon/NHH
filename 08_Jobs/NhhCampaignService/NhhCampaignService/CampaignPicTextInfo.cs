using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhCampaignService
{
    public class CampaignPicTextInfo
    {
        /// <summary>
        /// 上传图片media_id
        /// </summary>
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 发布的作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string digest { get; set; }

        /// <summary>
        /// 是否显示封面，1为显示，0为不显示
        /// </summary>
        public string show_cover_pic { get; set; }
    }
}
