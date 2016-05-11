using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Service.Message.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Message
{
    /// <summary>
    /// 消息服务
    /// </summary>
    public class MessageService : NHHService<NHHEntities>, IMessageService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="message"></param>
        public void Add(MessageInfo message)
        {
            var entity = new NHH.Entities.Message
            {
                MessageType = message.MessageType,
                Priority = message.Priority,
                Recipient = message.Recipient,
                Subject = message.Subject,
                Content = message.Content,
                Status = 0,
                InDate = DateTime.Now,
                InUser = 1,
                EditDate = DateTime.Now,
                EditUser = 1,
            };

            var bll = CreateBizObject<NHH.Entities.Message>();
            bll.Insert(entity);
            this.SaveChanges();
        }
    }
}
