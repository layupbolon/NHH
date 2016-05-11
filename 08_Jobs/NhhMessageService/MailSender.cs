using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NhhMessageService
{
    /// <summary>
    /// 邮件发送
    /// </summary>
    public class MailSender
    {
        static readonly string smtpServer = ConfigurationManager.AppSettings["MailServer"].ToString();
        static readonly string mailFrom = ConfigurationManager.AppSettings["MailId"].ToString();
        static readonly string userPassword = ConfigurationManager.AppSettings["MailPwd"].ToString();

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        public static void SendMail(string recipient, string subject, string content)
        {
            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host = smtpServer;
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);

            MailMessage mailMessage = new MailMessage(mailFrom, recipient);
            mailMessage.Subject = subject;
            mailMessage.Body = content;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            smtpClient.Send(mailMessage);
        }
    }
}
