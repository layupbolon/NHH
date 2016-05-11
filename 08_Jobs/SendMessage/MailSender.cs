using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendMessage
{
    public class MailSender
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        public static void SendMail()
        {
            string strCmd = string.Format(@"SELECT TOP ({0}) *
                                              FROM [NHH].[dbo].[Message](Nolock)
                                              Where MessageType=1 And Status=0
                                              Order by MessageID", ConfigurationManager.AppSettings["MessageNumber"]);
            var connStr = ConfigurationManager.ConnectionStrings["NHHConn"].ToString();
            var conn = new System.Data.SqlClient.SqlConnection(connStr);
            var cmd = new System.Data.SqlClient.SqlCommand(strCmd, conn);
            var adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);

            if (table == null || table.Rows.Count == 0)
                return;

            foreach (DataRow row in table.Rows)
            {
                SendMail(row);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="row"></param>
        private static void SendMail(DataRow row)
        {
            string smtpServer = ConfigurationManager.AppSettings["MailServer"].ToString();
            string mailFrom = ConfigurationManager.AppSettings["MailId"].ToString();
            string userPassword = ConfigurationManager.AppSettings["MailPwd"].ToString();
            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host = smtpServer;
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);

            MailMessage mailMessage = new MailMessage(mailFrom, row["Recipient"].ToString());
            mailMessage.Subject = row["Subject"].ToString();
            mailMessage.Body = row["Content"].ToString();
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            int status = 1;
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                status = -2;
            }
            //更改状态
            int messageId = 0;
            int.TryParse(row["MessageID"].ToString(), out messageId);

            string strCmd = string.Format("Update NHH..Message Set Status={1} Where MessageID={0}", messageId, status);
            var connStr = ConfigurationManager.ConnectionStrings["NHHConn"].ToString();
            var conn = new System.Data.SqlClient.SqlConnection(connStr);
            var cmd = new System.Data.SqlClient.SqlCommand(strCmd, conn);

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }
}
