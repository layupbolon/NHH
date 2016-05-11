using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate
{
    /// <summary>
    /// 投诉智能平台用户消息
    /// </summary>
    public class ComplaintUserMessage : NHHService<NHHEntities>
    {
        /// <summary>
        /// 指派
        /// </summary>
        /// <param name="info"></param>
        public void Assgin(ComplaintInfo info)
        {
            var message = new UserMessage();
            message.UserID = info.RequestUserId;//提报人
            message.Subject = "投诉派单";
            message.Content = string.Format("你好！你提交的投诉单据{0}已经派单，处理人员将尽快安排工作", info.ComplaintId);
            message.DetailUrl = info.ComplaintDetailUrl;
            message.SourceRefID = info.ComplaintId;
            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = info.UserInfo.UserId;

            SaveUserMessage(message);
        }

        /// <summary>
        /// 重新指派
        /// </summary>
        /// <param name="info"></param>
        public void ReAssgin(ComplaintInfo info)
        {
            var message = new UserMessage();
            message.UserID = info.RequestUserId;//提报人
            message.Subject = "投诉重新指派";
            message.Content = string.Format("你好！你提交的投诉单据{0}已经投诉重新指派，处理人员将尽快安排工作", info.ComplaintId);
            message.DetailUrl = info.ComplaintDetailUrl;
            message.SourceRefID = info.ComplaintId;
            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = info.UserInfo.UserId;

            SaveUserMessage(message);//发送给提报人
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        public void Add(ComplaintInfo info)
        {
            if (!info.ServiceUserId.HasValue || info.ServiceUserId.Value <= 0)
                return;

            var userMessage = new UserMessage();
            userMessage.UserID = info.ServiceUserId.Value;//提报人的主管
            userMessage.Subject = "投诉提报";
            userMessage.Content = "你好！您收到一条新的投诉";
            userMessage.DetailUrl = info.ComplaintDetailUrl;
            userMessage.SourceType = 6;//投诉
            userMessage.SourceRefID = info.ComplaintId;
            userMessage.UserInfo = new UserInfo();
            userMessage.UserInfo.UserId = info.UserInfo.UserId;

            SaveUserMessage(userMessage);
        }

        /// <summary>
        /// 搁置
        /// </summary>
        /// <param name="shelveInfo"></param>
        public void Shelve(ComplaintShelveInfo shelveInfo)
        {
            var message = new UserMessage();
            message.Subject = "投诉搁置";
            message.SourceRefID = shelveInfo.ComplaintId;
            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = shelveInfo.UserInfo.UserId;
            message.DetailUrl = shelveInfo.ComplaintDetailUrl;

            message.UserID = shelveInfo.RequestUserId;//提报人
            message.Content = string.Format("你好！你提交的投诉单据{0}由于{1}原因所以无法完成，进入搁置列表", shelveInfo.ComplaintId, shelveInfo.ShelveReason);
            SaveUserMessage(message);//发送给提报人
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="info"></param>
        public void Finish(ComplaintInfo info)
        {
            var message = new UserMessage();
            message.UserID = info.RequestUserId;//提报人
            message.Subject = "结束投诉";
            message.Content = string.Format("你好！你提交的投诉单据{0}已经结束投诉", info.ComplaintId);
            message.DetailUrl = info.ComplaintDetailUrl;
            message.SourceRefID = info.ComplaintId;
            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = info.UserInfo.UserId;

            SaveUserMessage(message);
        }


        /// <summary>
        /// 发送消息到智能平台
        /// </summary>
        /// <param name="message"></param>
        public void SaveUserMessage(UserMessage message)
        {
            if (message == null)
                return;

            var instance = CreateBizObject<Sys_User_Message>();
            var entity = new Sys_User_Message();

            entity.UserID = message.UserID;
            entity.Subject = message.Subject;
            entity.Content = message.Content;
            entity.Content += string.Format("<a href='{0}?complaintId={1}' class='blue'>详情请点击此处>></a>", message.DetailUrl, message.SourceRefID);
            entity.SourceType = 7;//投诉
            entity.SourceRefID = message.SourceRefID;
            entity.Status = 1;
            entity.InDate = DateTime.Now;
            entity.InUser = message.UserInfo.UserId;
            entity.EditDate = DateTime.Now;
            entity.EditUser = message.UserInfo.UserId;

            instance.Insert(entity);
            this.SaveChanges();
        }
    }
}
