using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using NHH.Wechat.Models.Message;
using NHH.Framework.Wechat;
using NHH.Framework.Utility;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Net;
using System.IO;
namespace NhhCampaignService
{
    /// <summary>
    /// 企划活动任务
    /// </summary>
    public class CampaignTask : BaseTask
    {
        /// <summary>
        /// 企划活动任务
        /// </summary>
        public CampaignTask()
        {
            TaskName = "CampaignTask";
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="info"></param>
        protected override void SendMessage(CampaignPageInfo info)
        {

            try
            {
                ServiceLog.Log("CampaignTask", "开始发送微信通知...", "Task");
                SendWechatMsg(info);//微信通知
            }
            catch (Exception ex)
            {

                ServiceLog.Log(TaskName, ex.Message, "微信通知出问题");
            }
            try
            {
                ServiceLog.Log("CampaignTask", "开始发送唐小二端商户消息...", "Task");
                //  SendMerchantMsg(info);//唐小二端商户消息
            }
            catch (Exception ex)
            {

                ServiceLog.Log(TaskName, ex.Message, "商户通知出问题");
            }

            try
            {
                ServiceLog.Log("CampaignTask", "开始群发通知...", "Task");
                //  SendWechatMsgAll(info);//群发到大庆那边所有关注大庆公众号的微信用户
            }
            catch (Exception ex)
            {

                ServiceLog.Log(TaskName, ex.Message, "群发出问题");
            }

        }

        /// <summary>
        /// 群发消息
        /// </summary>
        /// <param name="info"></param>
        public void SendWechatMsgAll(CampaignPageInfo info)
        {
            WechatPush push = new WechatPush();
            string fileUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings["ImageSite"].ToString(), info.PageCover);
            //获取开发者Id,110:大庆公众号
            var configData = GetWechatDevelopIdInfo(110, info.ProjectId);
            //上传图片获取MediaId
            string thumb_media_id = push.UploadPicture(configData, fileUrl);
            ServiceLog.Log("CampaignTask", "上传图片获取MediaId...", "Task");

            //构建图文信息
            var picText = new CampaignTemplate();
            picText.articles = new List<CampaignPicTextInfo>();
            var picTextInfo = new CampaignPicTextInfo()
            {
                thumb_media_id = thumb_media_id,
                title = info.PageTitle,
                content = info.PageContent,
                show_cover_pic = "1"
            };
            picText.articles.Add(picTextInfo);
            var media_id = push.UploadNews(configData, JsonConvert.SerializeObject(picText));
            ServiceLog.Log("CampaignTask", "构建图文信息...", "Task");

            //把图文信息群发出去
            var sendMsg = new CampaignSendAllMessage();
            sendMsg.filter = new Filter() { is_to_all = true };
            sendMsg.mpnews = new Mpnews() { media_id = media_id };
            sendMsg.msgtype = "mpnews";
            var msg = JsonConvert.SerializeObject(sendMsg);
            push.SendAll(configData, msg);
            ServiceLog.Log("CampaignTask", "把图文信息群发出去...", "Task");
        }

        /// <summary>
        /// 发送唐小二端商户消息
        /// </summary>
        /// <param name="info"></param>
        public void SendMerchantMsg(CampaignPageInfo info)
        {
            if (info.MerchantUserList == null || info.MerchantUserList.Count == 0) return;

            var msg = new MerchantMessageInfo()
            {
                Subject = info.PageTitle,
                SourceType = 5,//5：活动
                SourceRefId = info.PageId
            };

            foreach (var userInfo in info.MerchantUserList)
            {
                msg.MerchantId = userInfo.MerchantId;
                msg.StoreId = userInfo.StoreId;
                msg.UserId = userInfo.UserId;
                msg.Content = string.Format("亲爱的{0}，你收到一条新的行政通知。请点击此处查看", userInfo.UserName);
                try
                {
                    InsertMerchantMessage(msg);
                }
                catch (Exception ex)
                {

                    ServiceLog.Log(TaskName, ex.Message, "插入商户消息表出问题");
                }
            }
            ServiceLog.Log("CampaignTask", "发送唐小二端商户消息完成...", "Task");
        }

        /// <summary>
        /// 插入商户消息
        /// </summary>
        /// <param name="msg"></param>
        public void InsertMerchantMessage(MerchantMessageInfo msg)
        {
            var strCmd = string.Format(@"INSERT INTO [dbo].[Merchant_Message]
                                        ([MerchantID]
                                        ,[StoreID]
                                        ,[UserID]
                                        ,[Subject]
                                        ,[Content]
                                        ,[SourceType]
                                        ,[SourceRefID]
                                        ,[Status]
                                        ,[InDate]
                                        ,[InUser]
                                        ,[EditDate]
                                        ,[EditUser])
                                         VALUES
                                        (@MerchantID
                                        ,@StoreID
                                        ,@UserID
                                        ,@Subject
                                        ,@Content
                                        ,@SourceType
                                        ,@SourceRefID
                                        ,@Status
                                        ,@InDate
                                        ,@InUser
                                        ,@EditDate
                                        ,@EditUser)");

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@MerchantID",msg.MerchantId),  
                new SqlParameter("@StoreID",msg.StoreId),  
                new SqlParameter("@UserID",msg.UserId),  
                new SqlParameter("@Subject",msg.Subject), 
                new SqlParameter("@Content",msg.Content),
                new SqlParameter("@SourceType",msg.SourceType),
                new SqlParameter("@SourceRefID",msg.SourceRefId),
                new SqlParameter("@Status",msg.Status),
                new SqlParameter("@InDate",msg.InDate),
                new SqlParameter("@InUser",msg.InUser),
                new SqlParameter("@EditDate",msg.EditDate),
                new SqlParameter("@EditUser",msg.EditUser),
            };

            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }

        /// <summary>
        /// 发送微信通知
        /// </summary>
        /// <param name="info"></param>
        public void SendWechatMsg(CampaignPageInfo info)
        {
            if (info.MerchantUserList == null || info.MerchantUserList.Count == 0) return;
            foreach (var userInfo in info.MerchantUserList)
            {
                var msg = new MessageInfo();
                try
                {
                    if (!string.IsNullOrEmpty(userInfo.WechatOpenId))
                    {
                        msg.Priority = 2;
                        msg.MessageType = 3;
                        msg.Recipient = userInfo.WechatOpenId;
                        msg.Subject = "企划活动发布通知";
                        msg.Link = string.Format(ConfigurationManager.AppSettings["CampaignDetailLink"].ToString(), info.CampaignId);
                        #region 微信端通知内容
                        msg.Content = GetWeiChatContent(info, userInfo.UserName);
                        #endregion
                        InsertMessage(msg);//发送给项目下所有商户用户
                    }
                }
                catch (Exception ex)
                {
                    ServiceLog.Log(TaskName, ex.Message, "微信发送有问题，Recipient=" + msg.Recipient);
                }
            }
            ServiceLog.Log("CampaignTask", "发送微信通知完成...", "Task");
        }

        /// <summary>
        /// 获取微信通知端显示内容
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetWeiChatContent(CampaignPageInfo info, string userName)
        {
            var msg = new MessageData();
            msg.Type = "Campaign";
            msg.TemplateId = ConfigurationManager.AppSettings["CampaignTemplateId"].ToString();

            var content = new CampaignRemindMessage();
            content.first = new requestFieldDetail();
            content.first.value = "企划活动发布通知";
            content.first.color = "#173177";

            content.keyword1 = new requestFieldDetail();
            content.keyword1.value = info.PageTitle;
            content.keyword1.color = "#173177";

            content.keyword2 = new requestFieldDetail();
            content.keyword2.value = info.PublishTime.ToString();
            content.keyword2.color = "#173177";

            content.keyword3 = new requestFieldDetail();
            content.keyword3.value = string.Format("亲爱的{0}，我们策划的新活动就要上线啦，赶紧点击详情来了解", userName);
            content.keyword3.color = "#173177";

            content.remark = new requestFieldDetail();
            content.remark.value = "";
            content.remark.color = "#173177";

            msg.Content = JsonConvert.SerializeObject(content);

            return JsonConvert.SerializeObject(msg);
        }
    }
}
