using log4net;
using Newtonsoft.Json;
using Nhh.Jobs.Model;
using Nhh.Jobs.Utility;
using NHH.Framework.Wechat;
using NHH.Message.Models.Wechat;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Campaign
{
    /// <summary>
    /// 企划JOB
    /// </summary>
    public sealed class CampaignJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(CampaignJob));

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var campaignList = CampaignPageDA.GetCampaignList();
            if (campaignList == null) return;

            foreach (var campaign in campaignList)
            {
                //唐小二
                if (campaign.PageType == 1)
                {
                    #region 微信消息
                    try
                    {
                        SendWchatMessage(campaign);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("发送微信通知出问题", ex);
                    }
                    #endregion

                    #region 商户消息
                    try
                    {
                        SendMerchantMessage(campaign);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("商户消息发送失败", ex);
                    }
                    #endregion

                    #region 群发消息
                    try
                    {
                        SendWechatMsgToAll(campaign);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("群发出问题", ex);
                    }
                    #endregion
                }

                try
                {
                    CampaignPageDA.UpdateStatus(campaign.PageId, 3);
                }
                catch (Exception ex)
                {
                    _logger.Error("企划状态更新失败", ex);
                }
            }
        }

        /// <summary>
        /// 发送微信通知
        /// </summary>
        /// <param name="info"></param>
        private void SendWchatMessage(CampaignPageInfo info)
        {
            if (info.MerchantUserList == null || info.MerchantUserList.Count == 0)
                return;
            foreach (var userInfo in info.MerchantUserList)
            {
                if (string.IsNullOrEmpty(userInfo.WechatOpenId) || userInfo.WechatOpenId.Length == 0)
                    continue;
                var message = new MessageInfo
                {
                    Priority = 2,
                    MessageType = 3,
                    Recipient = userInfo.WechatOpenId,
                    Subject = "企划活动发布通知",
                };
                try
                {
                    message.Link = string.Format(ParamHelper.GetString("CampaignDetailLink"), info.CampaignId);
                    message.Content = GetWeiChatContent(info, userInfo.UserName);
                    MessageDA.Add(message);
                }
                catch (Exception ex)
                {
                    _logger.Error("企划微信消息插入失败，Recipient=" + message.Recipient, ex);
                }
            }
        }

        /// <summary>
        /// 发送唐小二商户用户消息
        /// </summary>
        /// <param name="info"></param>
        private void SendMerchantMessage(CampaignPageInfo info)
        {
            if (info.MerchantUserList == null || info.MerchantUserList.Count == 0)
                return;

            var message = new MerchantMessageInfo()
            {
                Subject = info.PageTitle,
                SourceType = 5,//5：活动
                SourceRefId = info.PageId,
                Content = info.CampaignBrief
            };

            foreach (var userInfo in info.MerchantUserList)
            {
                message.MerchantId = userInfo.MerchantId;
                message.StoreId = userInfo.StoreId;
                message.UserId = userInfo.UserId;
                try
                {
                    MerchantMessageDA.Add(message);
                }
                catch (Exception ex)
                {
                    _logger.Error("商户消息插入失败", ex);
                }
            }
        }

        /// <summary>
        /// 群发消息
        /// 群发到大庆那边所有关注大庆公众号的微信用户
        /// 每个月只能发四次
        /// </summary>
        /// <param name="info"></param>
        private void SendWechatMsgToAll(CampaignPageInfo info)
        {
            //获取开发者Id,110:大庆公众号
            var configData = ProjectBizConfigDA.GetWechatDevelopIdInfo(info.ProjectId, 110);
            if (configData == null)
                throw new Exception("未获取到项目微信开发者帐号");

            WechatPush push = new WechatPush();
            //上传图片获取MediaId
            string fileUrl = string.Format("{0}{1}", ParamHelper.GetString("ImageSite"), info.PageCover);
            string thumb_media_id = push.UploadPicture(configData, fileUrl);


            //构建图文信息
            var picTextModel = new CampaignPicTextModel();
            picTextModel.articles = new List<CampaignPicTextInfo>();
            var picTextInfo = new CampaignPicTextInfo()
            {
                thumb_media_id = thumb_media_id,
                title = info.PageTitle,
                content = info.PageContent,
                show_cover_pic = "1"
            };
            picTextModel.articles.Add(picTextInfo);
            var media_id = push.UploadNews(configData, JsonConvert.SerializeObject(picTextModel));

            //把图文信息群发出去
            var sendMsg = new CampaignAllMessage();
            sendMsg.filter = new Filter() { is_to_all = true };
            sendMsg.mpnews = new Mpnews() { media_id = media_id };
            sendMsg.msgtype = "mpnews";
            var msgContent = JsonConvert.SerializeObject(sendMsg);
            push.SendAll(configData, msgContent);
        }

        /// <summary>
        /// 获取微信通知端显示内容
        /// </summary>
        /// <param name="info"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetWeiChatContent(CampaignPageInfo info, string userName)
        {
            var msg = new MessageData();
            msg.Type = "Campaign";
            msg.TemplateId = ParamHelper.GetString("CampaignTemplateId");

            var content = new CampaignRemindMessage();
            content.first = new requestFieldDetail();
            content.first.value = "企划活动发布通知";
            content.first.color = "#173177";

            content.keyword1 = new requestFieldDetail();
            content.keyword1.value = info.PageTitle;
            content.keyword1.color = "#173177";

            content.keyword2 = new requestFieldDetail();
            content.keyword2.value = info.Location;
            content.keyword2.color = "#173177";

            content.keyword3 = new requestFieldDetail();
            content.keyword3.value = info.PublishTime.ToString();
            content.keyword3.color = "#173177";

            content.remark = new requestFieldDetail();
            content.remark.value = string.Format("亲爱的{0}，我们策划的新活动就要上线啦，赶紧点击详情来了解", userName); ;
            content.remark.color = "#173177";

            msg.Content = JsonConvert.SerializeObject(content);

            return JsonConvert.SerializeObject(msg);
        }

    }
}
