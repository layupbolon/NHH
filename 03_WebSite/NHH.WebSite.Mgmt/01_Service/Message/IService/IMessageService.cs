using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Message.IService
{
    /// <summary>
    /// 消息服务
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="message"></param>
        void Add(MessageInfo message);
    }
}
