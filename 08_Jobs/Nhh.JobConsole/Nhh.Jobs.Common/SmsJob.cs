using log4net;
using Newtonsoft.Json;
using Nhh.Jobs.Utility;
using NHH.Message.Models.Sms;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Request;

namespace Nhh.Jobs.Common
{
    /// <summary>
    /// 短信JOB
    /// </summary>
    public class SmsJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(SmsJob));
        private readonly string url = ParamHelper.GetString("SmsUrl");
        private readonly string key = ParamHelper.GetString("SmsKey");
        private readonly string secret = ParamHelper.GetString("SmsSecret");

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var db = new Nhh.Entities.NHHEntities();
            var statusList = new List<int> { -3, -2, -1, 0, 1 };
            var query = from m in db.Message
                        where m.MessageType == 2 && statusList.Any(status => status == m.Status)
                        select m;

            if (!query.Any())
                return;

            _logger.Info(string.Format("共有{0}条短信需要发送", query.Count()));

            int messageNumber = ParamHelper.GetValue("MessageNumber");
            var smsList = query.Take(messageNumber).ToList();

            var sendStatus = true;
            var smsStatus = 0;
            smsList.ForEach(sms =>
            {
                smsStatus = sms.Status;
                sendStatus = SendSms(sms);

                //更改状态
                var entity = db.Message.Find(sms.MessageID);
                if (entity == null)
                    return;

                entity.Status = sendStatus ? 3 : smsStatus - 1;
                entity.EditDate = DateTime.Now;
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            });
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="message"></param>
        private bool SendSms(Nhh.Entities.Message message)
        {
            if (string.IsNullOrEmpty(message.Recipient) || message.Recipient.Length == 0)
                return false;

            if (string.IsNullOrEmpty(message.Content) || message.Content.Length == 0)
                return false;

            ITopClient client = new DefaultTopClient(url, key, secret);
            var req = new AlibabaAliqinFcSmsNumSendRequest();

            SmsMessage content = null;
            try
            {
                content = JsonConvert.DeserializeObject<SmsMessage>(message.Content);
            }
            catch (Exception)
            {
                return false;
            }
            if (content == null)
                return false;

            req.Extend = "";
            req.SmsType = "normal";
            req.SmsFreeSignName = content.SignName;
            req.SmsParam = content.Param;
            req.RecNum = message.Recipient;
            req.SmsTemplateCode = content.TemplateCode;
            var rsp = client.Execute(req);
            return rsp.Result.Success;
            
        }
    }
}
