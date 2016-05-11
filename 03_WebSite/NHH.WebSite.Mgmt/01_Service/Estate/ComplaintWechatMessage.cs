using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Framework.Wechat;
using NHH.Message.Models.Wechat;
using NHH.Models.Common;
using NHH.Models.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate
{
    /// <summary>
    /// 投诉微信消息
    /// </summary>
    public class ComplaintWechatMessage : NHHService<NHHEntities>
    {
        /// <summary>
        /// 指派
        /// </summary>
        /// <param name="info"></param>
        public void Assgin(ComplaintInfo info)
        {
            var recipient = (from mu in Context.Merchant_User
                             where mu.UserID == info.RequestUserId
                             select mu.WechatOpenID).FirstOrDefault();
            if (string.IsNullOrEmpty(recipient) || recipient.Length == 0)
                return;
            
            var message = new MessageInfo();
            message.Priority = 2;
            message.Recipient = recipient;//提报人
            message.Subject = "投诉派单";
            message.Link = string.Format(ParamManager.GetStringValue("ComplaintDetailLink"), info.ComplaintId);

            #region 微信端通知内容
            var content = new ComplaintWechatContentInfo();
            content.Title = "您好，您的投诉有新的进展";
            content.Location = info.Location;
            content.Reason = message.Subject;
            content.RequestTime = info.RequestTime.HasValue ? info.RequestTime.Value : DateTime.Now;
            content.ComplaintStatus = "正在调查处理中";
            content.Remarks = "感谢您对我们服务的支持和监督，祝您生活愉快。";
            message.Content = GetComplaintWechatContent(content);
            #endregion

            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = info.UserInfo.UserId;

            SaveWechatMessage(message);
        }

        /// <summary>
        /// 重新指派
        /// </summary>
        /// <param name="info"></param>
        public void ReAssgin(ComplaintInfo info)
        {
            var recipient = (from mu in Context.Merchant_User
                             where mu.UserID == info.RequestUserId
                             select mu.WechatOpenID).FirstOrDefault();//提报人oI_yCswwiV3MfSSoB0Qv2OV_NlLo 
            if (string.IsNullOrEmpty(recipient) || recipient.Length == 0)
                return;

            var message = new MessageInfo();
            message.Priority = 2;
            message.Recipient = recipient;//提报人
            message.Subject = "投诉重新指派";
            message.Link = string.Format(ParamManager.GetStringValue("ComplaintDetailLink"), info.ComplaintId);

            #region 微信端通知内容
            var content = new ComplaintWechatContentInfo();
            content.Title = "您好，您的投诉有新的进展";
            content.Location = info.Location;
            content.Reason = message.Subject;
            content.RequestTime = info.RequestTime.HasValue ? info.RequestTime.Value : DateTime.Now;
            content.ComplaintStatus = message.Subject;
            content.Remarks = "感谢您对我们服务的支持和监督，祝您生活愉快。";
            message.Content = GetComplaintWechatContent(content);
            #endregion

            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = info.UserInfo.UserId;

            SaveWechatMessage(message);//发送给提报人
        }

        /// <summary>
        /// 搁置
        /// </summary>
        /// <param name="shelveInfo"></param>
        public void Shelve(ComplaintShelveInfo shelveInfo)
        {
            var recipient = (from mu in Context.Merchant_User
                             where mu.UserID == shelveInfo.RequestUserId
                             select mu.WechatOpenID).FirstOrDefault();//提报人oI_yCswwiV3MfSSoB0Qv2OV_NlLo
            if (string.IsNullOrEmpty(recipient) || recipient.Length == 0)//商户投诉
                return;

            var message = new MessageInfo();
            message.Priority = 2;
            message.Recipient = recipient;
            message.Subject = "投诉搁置";
            message.Link = string.Format(ParamManager.GetStringValue("ComplaintDetailLink"), shelveInfo.ComplaintId);

            #region 微信端通知内容
            var content = new ComplaintWechatContentInfo();
            content.Title = "您好，您的投诉有新的进展";
            content.Location = shelveInfo.Location;
            content.Reason = message.Subject;
            content.RequestTime = shelveInfo.RequestTime.HasValue ? shelveInfo.RequestTime.Value : DateTime.Now; ;
            content.ComplaintStatus = message.Subject;
            content.Remarks = "感谢您对我们服务的支持和监督，祝您生活愉快。";
            message.Content = GetComplaintWechatContent(content);
            #endregion

            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = shelveInfo.UserInfo.UserId;

            SaveWechatMessage(message);
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="info"></param>
        public void Finish(ComplaintInfo info)
        {
            var recipient = (from mu in Context.Merchant_User
                             where mu.UserID == info.RequestUserId
                             select mu.WechatOpenID).FirstOrDefault();//提报人oI_yCswwiV3MfSSoB0Qv2OV_NlLo
            if (string.IsNullOrEmpty(recipient) || recipient.Length == 0)//商户投诉
                return;

            var message = new MessageInfo();
            message.Priority = 2;
            message.Recipient = recipient;
            message.Subject = "结束投诉";
            message.Link = string.Format(ParamManager.GetStringValue("ComplaintDetailLink"), info.ComplaintId);

            #region 微信端通知内容
            var content = new ComplaintWechatContentInfo();
            content.Title = "您好，您的投诉有新的进展";
            content.Location = info.Location;
            content.Reason = message.Subject;
            content.RequestTime = info.RequestTime.HasValue ? info.RequestTime.Value : DateTime.Now;
            content.ComplaintStatus = message.Subject;
            content.Remarks = "感谢您对我们服务的支持和监督，祝您生活愉快。";
            message.Content = GetComplaintWechatContent(content);
            #endregion

            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = info.UserInfo.UserId;
            SaveWechatMessage(message);
        }

        /// <summary>
        /// 保存微信消息
        /// </summary>
        /// <param name="message"></param>
        private void SaveWechatMessage(MessageInfo message)
        {
            if (message == null)
                return;

            var instance = CreateBizObject<NHH.Entities.Message>();
            var entity = new NHH.Entities.Message();
            entity.MessageType = 3;//微信端
            entity.Priority = message.Priority;
            entity.Recipient = message.Recipient;
            entity.Subject = message.Subject;
            entity.Content = message.Content;
            entity.Link = message.Link;
            entity.InDate = DateTime.Now;
            entity.InUser = message.UserInfo.UserId;
            entity.EditDate = DateTime.Now;
            entity.EditUser = message.UserInfo.UserId;
            entity.Status = 0;

            instance.Insert(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 序列化投诉微信端content
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetComplaintWechatContent(ComplaintWechatContentInfo info)
        {
            var data = new MessageData();
            data.Type = "Complaint";
            data.TemplateId = ParamManager.GetStringValue("ComplaintTemplateId");

            var content = new RepairRemindMessage();
            content.first = new requestFieldDetail();
            content.first.value = info.Title;
            content.first.color = info.Color;

            content.keyword1 = new requestFieldDetail();
            content.keyword1.value = info.Location;
            content.keyword1.color = info.Color;

            content.keyword2 = new requestFieldDetail();
            content.keyword2.value = info.Reason;
            content.keyword2.color = info.Color;

            content.keyword3 = new requestFieldDetail();
            content.keyword3.value = info.RequestTime.ToString();
            content.keyword3.color = info.Color;

            content.keyword4 = new requestFieldDetail();
            content.keyword4.value = info.ComplaintStatus;
            content.keyword4.color = info.Color;

            content.remark = new requestFieldDetail();
            content.remark.value = info.Remarks;
            content.remark.color = info.Color;

            data.Content = JsonConvert.SerializeObject(content);

            return JsonConvert.SerializeObject(data);
        }
    }
}
