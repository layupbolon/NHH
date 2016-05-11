using Newtonsoft.Json;
using NHH.Wechat.Models.Message;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhMessageService
{
    /// <summary>
    /// 微信任务
    /// </summary>
    public class WechatTask : BaseTask
    {
        /// <summary>
        /// 微信任务
        /// </summary>
        public WechatTask()
        {
            TaskName = "WechatTask";
            MessageType = 3;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="row"></param>
        protected override void SendMessage(DataRow row)
        {
            var data = JsonConvert.DeserializeObject<MessageData>(row["Content"].ToString());
            var detailLink = row["Link"].ToString();
            new WechatSender().SendWechet(detailLink, row["Recipient"].ToString(), data);

        }
    }
}
