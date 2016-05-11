using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using NHH.Framework.Wechat;
using Newtonsoft.Json;
using NHH.Wechat.Models.Message;

namespace NhhMessageService
{
    /// <summary>
    /// 微信公众平台推送
    /// </summary>
    public class WechatSender
    {
        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="recipient"></param>
        /// <param name="data"></param>
        public void SendWechet(string detailLink, string recipient, MessageData data)
        {
            var template = new NHH.Framework.Wechat.requestTemplate();
            template.data = JsonConvert.DeserializeObject(data.Content);
            template.touser = recipient;
            template.template_id = data.TemplateId;
            template.url = detailLink;
            new NHH.Framework.Wechat.WechatPush().SendWechat(template);
        }
    }
}
