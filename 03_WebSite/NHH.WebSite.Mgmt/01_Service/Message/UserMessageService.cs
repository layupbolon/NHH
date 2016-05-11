using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Message;
using NHH.Service.Message.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Message
{
    public class UserMessageService : NHHService<NHHEntities>, IUserMessageService
    {

        /// <summary>
        /// 获取用户消息信息
        /// </summary>
        /// <param name="messageStatus"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public UserMessageListModel GetUserMessageList(UserMessageQueryInfo queryInfo)
        {
            var model = new UserMessageListModel { UserMessageList = new List<UserMessage>() };
            model.QueryInfo = queryInfo;
            var pagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from um in Context.Sys_User_Message
                        join dcSourceType in Context.Dictionary on um.SourceType equals dcSourceType.FieldValue
                        join dcMesStatus in Context.Dictionary on um.Status equals dcMesStatus.FieldValue
                        where dcSourceType.FieldType == "SourceType" && dcMesStatus.FieldType == "UserMessageStatus"
                        select new
                        {
                            um.MessageID,
                            um.UserID,
                            um.Subject,
                            um.Content,
                            um.SourceType,
                            um.SourceRefID,
                            SourceTypeName = dcSourceType.FieldName,
                            MessageStatus = dcMesStatus.FieldName,
                            um.Status,
                            um.InDate,
                            um.InUser,
                            um.EditDate,
                            um.EditUser
                        };

            if (queryInfo.MessageStatus.HasValue)
            {
                query = query.Where(a => a.Status == queryInfo.MessageStatus.Value);
            }
            if (queryInfo.UserInfo.UserId != 1)//超级管理可以看到所有信息
            {
                query = query.Where(m => m.UserID == queryInfo.UserInfo.UserId);
            }

            //分页信息
            pagingInfo.TotalCount = query.Count();
            model.PagingInfo = pagingInfo;

            query = query.OrderBy(queryInfo.OrderExpression).Skip(pagingInfo.SkipNum).Take(pagingInfo.TakeNum);
            var userMessageList = query.ToList();
            if (!userMessageList.Any())
            {
                return model;
            }

            userMessageList.ForEach(item =>
            {
                var userMessage = new UserMessage()
                {
                    MessageID = item.MessageID,
                    UserID = item.UserID,
                    Subject = item.Subject,
                    Content = item.Content,
                    SourceType = item.SourceType,
                    SourceTypeName = item.SourceTypeName,
                    SourceRefID = item.SourceRefID,
                    Status = item.Status,
                    MessageStatus = item.MessageStatus,
                    InDate = item.InDate
                };

                model.UserMessageList.Add(userMessage);
            });
            return model;
        }

        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public UserMessageDetailModel GetUserMessageDetail(int messageId)
        {
            var model = new UserMessageDetailModel();

            var query = from um in Context.Sys_User_Message
                        join dcSourceType in Context.Dictionary on um.SourceType equals dcSourceType.FieldValue
                        join dcMesStatus in Context.Dictionary on um.Status equals dcMesStatus.FieldValue
                        join user in Context.Sys_User on um.UserID equals user.UserID
                        where um.MessageID == messageId && dcSourceType.FieldType == "SourceType" && dcMesStatus.FieldType == "UserMessageStatus"
                        select new
                        {
                            um.MessageID,
                            um.UserID,
                            user.UserName,
                            um.Subject,
                            um.Content,
                            um.SourceType,
                            um.SourceRefID,
                            SourceTypeName = dcSourceType.FieldName,
                            MessageStatus = dcMesStatus.FieldName,
                            um.Status,
                            um.InDate,
                            um.InUser,
                            um.EditDate,
                            um.EditUser
                        };

            var entity = query.FirstOrDefault();
            if (entity == null)
            {
                return model;
            }
            model.MessageID = entity.MessageID;
            model.UserID = entity.UserID;
            model.Subject = entity.Subject;
            model.Content = entity.Content;
            model.SourceType = entity.SourceType;
            model.SourceTypeName = entity.SourceTypeName;
            model.SourceRefID = entity.SourceRefID.HasValue ? entity.SourceRefID.Value : 0;
            model.Status = entity.Status;
            model.MessageStatus = entity.MessageStatus;

            return model;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userMessage"></param>
        /// <returns></returns>
        public void UpdateUserMessageStatus(UserMessage userMessage)
        {
            var instance = CreateBizObject<Sys_User_Message>();
            var entity = instance.GetBySysNo(userMessage.MessageID);
            if (entity == null)
            {
                return;
            }
            entity.Status = 2;//表示已读
            entity.EditDate = DateTime.Now;
            entity.EditUser = userMessage.UserInfo.UserId;

            instance.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 获取未读消息总数
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetMessageCount(int userId)
        {
            var instance = CreateBizObject<Sys_User_Message>();
            var entity = instance.GetAllList(m => m.UserID == userId && m.Status == 1);

            if (entity == null)
                return 0;
            return entity.Count();
        }
    }
}
