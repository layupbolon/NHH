using System.Data;

namespace NhhMessageService
{
    /// <summary>
    /// 邮件任务
    /// </summary>
    public class MailTask : BaseTask
    {
        /// <summary>
        /// 邮件任务
        /// </summary>
        public MailTask()
        {
            TaskName = "MailTask";
            MessageType = 1;
        }

        /// <summary>
        /// 发送邮件消息
        /// </summary>
        /// <param name="row"></param>
        protected override void SendMessage(DataRow row)
        {
            MailSender.SendMail(row["Recipient"].ToString(), row["Subject"].ToString(), row["Content"].ToString());
        }
    }
}
