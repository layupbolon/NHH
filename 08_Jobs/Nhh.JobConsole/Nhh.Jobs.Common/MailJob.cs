using log4net;
using Nhh.Jobs.Utility;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Common
{
    /// <summary>
    /// 邮件JOB
    /// </summary>
    public sealed class MailJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MailJob));
        private static readonly string smtpServer = ParamHelper.GetString("MailServer");
        private static readonly string mailFrom = ParamHelper.GetString("MailFrom");
        private static readonly string mailPwd = ParamHelper.GetString("MailPwd");

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            _logger.Info("开始发送邮件");

            var db = new Nhh.Entities.NHHEntities();
            var statusList = new List<int> { -3, -2, -1, 0, 1 };
            var query = from m in db.Message
                        where m.MessageType == 1 && statusList.Any(status => status == m.Status)
                        select m;

            if (!query.Any())
                return;

            int messageNumber = ParamHelper.GetValue("MessageNumber");
            var mailList = query.Take(messageNumber).ToList();

            var sendStatus = true;
            var mailStatus = 0;
            mailList.ForEach(mail =>
            {
                mailStatus = mail.Status;
                sendStatus = SendMail(mail);                

                //更改状态
                var entity = db.Message.Find(mail.MessageID);
                if (entity == null)
                    return;

                entity.Status = sendStatus ? 3 : mailStatus - 1;
                entity.EditDate = DateTime.Now;
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            });
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool SendMail(Nhh.Entities.Message message)
        {
            try
            {
                // 邮件服务设置
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = smtpServer;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);

                MailMessage mailMessage = new MailMessage(mailFrom, message.Recipient);
                mailMessage.Subject = message.Subject;
                mailMessage.Body = message.Content;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.Error("邮件发送失败", ex);
                return false;
            }
            return true; 
        }
    }
}
