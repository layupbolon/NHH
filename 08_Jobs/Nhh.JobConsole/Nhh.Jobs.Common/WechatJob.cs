using log4net;
using Newtonsoft.Json;
using Nhh.Jobs.Utility;
using NHH.Message.Models.Wechat;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Common
{
    /// <summary>
    /// 微信JOB
    /// </summary>
    public sealed class WechatJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(WechatJob));

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var db = new Nhh.Entities.NHHEntities();
            var statusList = new List<int> { -3, -2, -1, 0, 1 };
            var query = from m in db.Message
                        where m.MessageType == 3 && statusList.Any(status => status == m.Status)
                        select m;

            if (!query.Any())
                return;

            int messageNumber = ParamHelper.GetValue("MessageNumber");
            var messageList = query.Take(messageNumber).ToList();

            var sendStatus = true;
            messageList.ForEach(message =>
            {
                sendStatus = SendWechat(message);
                UpdateMessageStatus(sendStatus, 3, message);

            });
        }

        /// <summary>
        /// 发送微信
        /// </summary>
        /// <param name="message"></param>
        private bool SendWechat(Nhh.Entities.Message message)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<MessageData>(message.Content);
                var template = new NHH.Framework.Wechat.requestTemplate();
                template.data = JsonConvert.DeserializeObject(data.Content);
                template.touser = message.Recipient;
                template.template_id = data.TemplateId;
                template.url = message.Link;
                UpdateMessageStatus(true, 2, message);
                new NHH.Framework.Wechat.WechatPush().SendWechat(template);
            }
            catch (Exception ex)
            {
                _logger.Error("微信发送失败", ex);
                UpdateMessageStatus(true, 5, message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更改微信发送状态
        /// messageStatus
        /// 1：待发送
        /// 2：发送中
        /// 3：已发送
        /// 4：已取消
        /// 5：发送失败
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sendStatus"></param>
        /// <param name="messageStatus"></param>
        /// <param name="message"></param>
        public void UpdateMessageStatus(bool sendStatus, int messageStatus, Entities.Message message)
        {
            var db = new Nhh.Entities.NHHEntities();
            //更改状态
            var entity = db.Message.Find(message.MessageID);
            if (entity == null)
                return;

            entity.Status = sendStatus ? messageStatus : message.Status - 1;
            entity.EditDate = DateTime.Now;
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
