using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhMessageService
{
    /// <summary>
    /// 任务
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        void SendMessage();
    }
}
