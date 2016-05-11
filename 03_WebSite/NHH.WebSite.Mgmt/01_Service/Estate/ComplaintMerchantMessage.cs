using NHH.Entities;
using NHH.Framework.Service;
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
    /// 投诉商户消息
    /// </summary>
    public class ComplaintMerchantMessage : NHHService<NHHEntities>
    {
        /// <summary>
        /// 指派
        /// </summary>
        /// <param name="info"></param>
        public void Assgin(ComplaintInfo info)
        {
            var user = (from mu in Context.Merchant_User
                        where mu.UserID == info.RequestUserId
                        select new
                        {
                            mu.UserID,
                            mu.StoreID,
                            mu.MerchantID
                        }).FirstOrDefault();
            if (user == null)
                return;

            var message = new MerchantMessageInfo();
            message.Subject = "您好！您的投诉单据已经安排客服人员了";
            message.Content = "您好！您的投诉单据已经安排客服人员了。";
            message.StoreID = user.StoreID;
            message.MerchantID = user.MerchantID;
            message.UserID = user.UserID;
            message.InUser = info.UserInfo.UserId;

            SaveMerchantMessage(message);
        }

        /// <summary>
        /// 重新指派
        /// </summary>
        /// <param name="info"></param>
        public void ReAssgin(ComplaintInfo info)
        {
            var user = (from mu in Context.Merchant_User
                        where mu.UserID == info.RequestUserId
                        select new
                        {
                            mu.UserID,
                            mu.StoreID,
                            mu.MerchantID
                        }).FirstOrDefault();
            if (user == null)
                return;

            var message = new MerchantMessageInfo();
            message.Subject = "您好！您的投诉单据已经重新安排客服人员了";
            message.Content = "您好！您的投诉单据已经重新安排客服人员了。";
            message.StoreID = user.StoreID;
            message.MerchantID = user.MerchantID;
            message.UserID = user.UserID;
            message.InUser = info.UserInfo.UserId;

            SaveMerchantMessage(message);
        }

        /// <summary>
        /// 搁置
        /// </summary>
        /// <param name="shelveInfo"></param>
        public void Shelve(ComplaintShelveInfo shelveInfo)
        {
            
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="info"></param>
        public void Finish(ComplaintInfo info)
        {
            var user = (from mu in Context.Merchant_User
                        where mu.UserID == info.RequestUserId
                        select new
                        {
                            mu.UserID,
                            mu.StoreID,
                            mu.MerchantID
                        }).FirstOrDefault();
            if (user == null)
                return;

            var message = new MerchantMessageInfo();
            message.Subject = "您好！您的投诉单据已经处理完成";
            message.Content = "您好！您的投诉单据已经处理完成。";
            message.StoreID = user.StoreID;
            message.MerchantID = user.MerchantID;
            message.UserID = user.UserID;
            message.InUser = info.UserInfo.UserId;

            SaveMerchantMessage(message);
        }


        /// <summary>
        /// 保存商户消息
        /// </summary>
        /// <param name="message"></param>
        private void SaveMerchantMessage(MerchantMessageInfo message)
        {
            if (message == null)
                return;

            var bll = CreateBizObject<Merchant_Message>();

            var entity = new Merchant_Message
            {
                MerchantID = message.MerchantID,
                StoreID = message.StoreID,
                UserID = message.UserID,
                SourceRefID = 0,
                SourceType = 1,
                Subject = message.Subject,
                Content = message.Content,
                Status = 1,
                InDate = DateTime.Now,
                InUser = message.InUser,
                EditDate = DateTime.Now,
                EditUser = 0,
            };
            bll.Insert(entity);
            this.SaveChanges();
        }
    }
}
