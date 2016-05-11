using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhRepairService
{
    /// <summary>
    /// 任务接口
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// 发送消息到智能平台
        /// </summary>
        void SendMessage();
    }
}
