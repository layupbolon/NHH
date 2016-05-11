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
    /// 任务
    /// </summary>
    public abstract class BaseTask : ITask
    {
        protected string TaskName = string.Empty;
        protected int MessageType = 0;

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMessage()
        {
            var messages = GetMessages();
            if (messages == null || messages.Rows.Count == 0)
            {
                return;
            }

            int messageId = 0;
            int status = 0;
            var result = true;
            foreach (DataRow row in messages.Rows)
            {
                try
                {
                    int.TryParse(row["MessageID"].ToString(), out messageId);
                    int.TryParse(row["Status"].ToString(), out status);

                    SendMessage(row);
                    result = true;
                }
                catch (Exception ex)
                {
                    ServiceLog.Log(TaskName, ex.Message, "Exception");
                    result = false;
                }
                //更新状态
                UpdateStatus(messageId, result, status);
            }
        }


        /// <summary>
        /// 获取需要发送的数据
        /// </summary>
        /// <returns></returns>
        protected virtual DataTable GetMessages()
        {
            string strCmd = string.Format(@"SELECT TOP ({0}) *
                                              FROM [dbo].[Message](Nolock)
                                              Where MessageType={1} And Status in (1,0,-1,-2,-3)
                                              Order by MessageID", ConfigurationManager.AppSettings["MessageNumber"], MessageType);

            return NHH.Framework.Utility.SqlHelper.ExecuteDataTable(strCmd);
        }

        /// <summary>
        /// 更新消息状态
        /// 发送成功则更新至1，否则状态减1
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="isSendSuccess"></param>
        /// <param name="lastStatus"></param>
        /// <returns></returns>
        private int UpdateStatus(int messageId, bool isSendSuccess, int lastStatus)
        {
            lastStatus -= 1;
            string strCmd = string.Format(@"Update [dbo].[Message] set Status = {0},EditDate = '{1}' where MessageID = {2}", isSendSuccess ? 3 : lastStatus, DateTime.Now, messageId);
            return NHH.Framework.Utility.SqlHelper.ExecuteNonQuery(strCmd);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="row"></param>
        protected abstract void SendMessage(DataRow row);
    }
}
