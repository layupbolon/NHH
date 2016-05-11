using NHH.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Wechat;
using System.Data.SqlClient;
namespace NhhCampaignService
{
    /// <summary>
    /// 基础任务
    /// </summary>
    public abstract class BaseTask : ITask
    {
        protected string TaskName = string.Empty;

        /// <summary>
        /// 消息发送
        /// </summary>
        public void SendMessage()
        {
            var messages = GetMessage();
            if (messages == null) return;

            foreach (var message in messages)
            {
                var flag = true;

                try
                {
                    SendMessage(message);
                    UpdateStatus(message.PageId);
                }
                catch (Exception ex)
                {

                    ServiceLog.Log(TaskName, ex.Message, "Exception");
                    flag = false;
                }
                //更新状态

            }
        }

        /// <summary>
        /// 获取需要发送的消息
        /// </summary>
        /// <returns></returns>
        protected List<CampaignPageInfo> GetMessage()
        {

            var strCmd = string.Format(@"SELECT cp.[PageID]
                                         ,cp.[CampaignID]
                                         ,c.[ProjectId]
                                         ,cp.[PageTitle]
                                         ,cp.[PageCover]
                                         ,cp.[PageContent]
                                         ,cp.[PublishStatus]
                                         ,cp.[PublishTime]
                                         FROM [dbo].[Campaign_Page](Nolock) cp
                                         INNER JOIN [dbo].[Campaign](Nolock) c on cp.[CampaignID]=c.[CampaignID]
                                         WHERE  cp.PublishStatus=1 And GETDATE() between DATEADD(MINUTE,-30,cp.PublishTime) and DATEADD(MINUTE,30,cp.PublishTime) And cp.PageType=1");
            var campaignPage = SqlHelper.ExecuteDataTable(strCmd);
            if (campaignPage == null || campaignPage.Rows.Count == 0) return null;

            var campaignPageList = new List<CampaignPageInfo>();
            foreach (DataRow row in campaignPage.Rows)
            {
                var info = new CampaignPageInfo();
                info.PageId = (int)row["PageID"];
                info.CampaignId = (int)row["CampaignID"];
                info.PageTitle = row["PageTitle"].ToString();
                info.PageCover = row["PageCover"].ToString();
                info.PageContent = row["PageContent"].ToString();
                info.ProjectId = (int)row["ProjectId"];
                info.MerchantUserList = GetMerchantUserList(info.ProjectId);

                var publishTime = DateTime.Now;
                DateTime.TryParse(row["PublishTime"].ToString(), out publishTime);

                info.PublishTime = publishTime;

                campaignPageList.Add(info);
            }
            return campaignPageList;
        }

        /// <summary>
        /// 获取项目下所有的商户用户的微信信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<MerchantUserInfo> GetMerchantUserList(int projectId)
        {
            var merchantUserList = new List<MerchantUserInfo>();
            var strCmd = string.Format(@"SELECT mu.[UserID]
                                               ,mu.[UserName]
                                               ,mu.[MerchantID]
                                               ,mu.[StoreID]
                                               ,mu.[WechatOpenID]
                                               FROM [dbo].[Merchant_User](Nolock) mu
					                           INNER JOIN [dbo].[Contract](Nolock) c on mu.MerchantID=c.MerchantID
					                           WHERE c.ProjectID={0} AND c.ContractStatus=3 AND mu.status=1 And c.status=1", projectId);

            var merchantUserInfo = SqlHelper.ExecuteDataTable(strCmd);
            if (merchantUserInfo.Rows.Count == 0 || merchantUserInfo == null) return null;

            foreach (DataRow row in merchantUserInfo.Rows)
            {
                var info = new MerchantUserInfo();
                info.WechatOpenId = row["WechatOpenID"].ToString();
                info.UserId = (int)row["UserID"];
                info.UserName = row["UserName"].ToString();
                info.MerchantId = (int)row["MerchantID"];
                if (row["StoreID"] != DBNull.Value)
                {
                    info.StoreId = (int)row["StoreID"];
                }

                merchantUserList.Add(info);
            }
            return merchantUserList;
        }

        /// <summary>
        /// 更新企划页面发布状态
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private int UpdateStatus(int pageId)
        {
            string strCmd = string.Format(@"Update [dbo].[Campaign_Page] set PublishStatus = {0},EditDate = '{1}' where PageID = {2}", 2, DateTime.Now, pageId);
            return SqlHelper.ExecuteNonQuery(strCmd);
        }

        /// <summary>
        /// 获取微信公众号开发者Id
        /// </summary>
        /// <param name="bizConfigType"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public configData GetWechatDevelopIdInfo(int bizConfigType, int projectId)
        {

            var strCmd = string.Format(@"Select pb.[Param1] as AppId
                                               ,pb.[Param2] as AppSecrect
                                               From [dbo].[Project_BizConfig](Nolock) pb
                                               Where pb.[BizConfigType]={0} And pb.[ProjectID]={1} and status=1", bizConfigType, projectId);
            var developIdInfo = SqlHelper.ExecuteDataTable(strCmd);
            if (developIdInfo.Rows.Count == 0 || developIdInfo == null) return null;

            var info = new configData();
            info.AppId = developIdInfo.Rows[0]["AppId"].ToString();
            info.AppSecrect = developIdInfo.Rows[0]["AppSecrect"].ToString();

            return info;
        }

        /// <summary>
        /// 插入系统消息
        /// </summary>
        /// <param name="msg"></param>
        public void InsertMessage(MessageInfo msg)
        {
            var strCmd = string.Format(@"INSERT INTO [dbo].[Message]
                                        ([MessageType]
                                        ,[Priority]
                                        ,[Recipient]
                                        ,[Subject]
                                        ,[Content]
                                        ,[Link]
                                        ,[Status]
                                        ,[InDate]
                                        ,[InUser]
                                        ,[EditDate]
                                        ,[EditUser])
                                         VALUES
                                        (@MessageType
                                        ,@Priority
                                        ,@Recipient
                                        ,@Subject
                                        ,@Content
                                        ,@Link
                                        ,@Status
                                        ,@InDate
                                        ,@InUser
                                        ,@EditDate
                                        ,@EditUser)");

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@MessageType",msg.MessageType),  
                new SqlParameter("@Priority",msg.Priority),  
                new SqlParameter("@Recipient",msg.Recipient),  
                new SqlParameter("@Subject",msg.Subject), 
                new SqlParameter("@Content",msg.Content),
                new SqlParameter("@Link",msg.Link),
                new SqlParameter("@Status",msg.Status),//默认为1
                new SqlParameter("@InDate",msg.InDate),
                new SqlParameter("@InUser",msg.InUser),
                new SqlParameter("@EditDate",msg.EditDate),
                new SqlParameter("@EditUser",msg.EditUser),
            };

            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="row"></param>
        protected abstract void SendMessage(CampaignPageInfo info);
    }
}
