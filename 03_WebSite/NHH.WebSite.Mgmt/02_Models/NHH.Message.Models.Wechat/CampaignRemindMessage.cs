using NHH.Framework.Wechat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Message.Models.Wechat
{
    /// <summary>
    /// 企划消息提醒
    /// </summary>
    public class CampaignRemindMessage : NHH.Framework.Wechat.requestData
    {
        /// <summary>
        /// 大标题
        /// </summary>
        public requestFieldDetail first { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public requestFieldDetail keyword1 { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public requestFieldDetail keyword2 { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public requestFieldDetail keyword3 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public requestFieldDetail remark { get; set; }
    }
}
