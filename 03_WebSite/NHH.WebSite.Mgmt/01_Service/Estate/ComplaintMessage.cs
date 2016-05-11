using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Framework.Wechat;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Models.Message;
using NHH.Message.Models.Wechat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate
{
    /// <summary>
    /// 投诉消息
    /// </summary>
    public class ComplaintMessage : NHHService<NHHEntities>
    {
        /// <summary>
        /// 指派
        /// </summary>
        /// <param name="info"></param>
        public void Assgin(ComplaintInfo info)
        {
            if (info.RequestSrcType == 1)
            {
                new ComplaintWechatMessage().Assgin(info);
                new ComplaintMerchantMessage().Assgin(info);
            }
            else if (info.RequestSrcType == 2)
            {
                new ComplaintUserMessage().Assgin(info);
            }
        }

        /// <summary>
        /// 重新指派
        /// </summary>
        /// <param name="info"></param>
        public void ReAssgin(ComplaintInfo info)
        {
            if (info.RequestSrcType == 1)
            {
                new ComplaintWechatMessage().ReAssgin(info);
                new ComplaintMerchantMessage().ReAssgin(info);
            }
            else if (info.RequestSrcType == 2)
            {
                new ComplaintUserMessage().ReAssgin(info);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        public void Add(ComplaintInfo info)
        {
            new ComplaintUserMessage().Add(info);
        }

        /// <summary>
        /// 搁置
        /// </summary>
        /// <param name="shelveInfo"></param>
        public void Shelve(ComplaintShelveInfo shelveInfo)
        {
            var userMessage = new ComplaintUserMessage();
            if (shelveInfo.RequestSrcType == 1)
            {
                new ComplaintWechatMessage().Shelve(shelveInfo);
                new ComplaintMerchantMessage().Shelve(shelveInfo);
            }
            else if (shelveInfo.RequestSrcType == 2)
            {
                userMessage.Shelve(shelveInfo);
            }

            var message = new UserMessage();
            message.Subject = "投诉搁置";
            message.SourceRefID = shelveInfo.ComplaintId;
            message.UserInfo = new UserInfo();
            message.UserInfo.UserId = shelveInfo.UserInfo.UserId;
            message.DetailUrl = shelveInfo.ComplaintDetailUrl;
            message.Content = string.Format("你好！投诉单据{0}由于{1}原因所以无法完成，进入搁置列表", shelveInfo.ComplaintId, shelveInfo.ShelveReason);

            //第二个发给工程经理
            var projectManagerUserId = (from su in Context.Sys_User
                                        join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                                        join ssu in Context.Sys_User on em.SupervisorID equals ssu.EmployeeID
                                        where su.UserID == shelveInfo.UserInfo.UserId
                                        select ssu.UserID).FirstOrDefault();

            if (projectManagerUserId > 0)
            {
                message.UserID = projectManagerUserId;//工程经理
                userMessage.SaveUserMessage(message);//发送给工程经理
            }

            //第三个发给项目总经理
            var projectGenernalManagerUserId = (from su in Context.Sys_User
                                                join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                                                join ssu in Context.Sys_User on em.SupervisorID equals ssu.EmployeeID
                                                where su.UserID == projectManagerUserId
                                                select ssu.UserID).FirstOrDefault();
            if (projectGenernalManagerUserId > 0)
            {
                message.UserID = shelveInfo.UserInfo.UserId;//项目总经理
                userMessage.SaveUserMessage(message);//发送给项目总经理
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="info"></param>
        public void Finish(ComplaintInfo info)
        {
            if (info.RequestSrcType == 1)
            {
                new ComplaintWechatMessage().Finish(info);
                new ComplaintMerchantMessage().Finish(info);
            }
            else if (info.RequestSrcType == 2)
            {
                new ComplaintUserMessage().Finish(info);
            }
        }
    }
}
