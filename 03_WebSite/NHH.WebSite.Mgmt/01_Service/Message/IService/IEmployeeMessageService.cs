using NHH.Models.Common;
using NHH.Models.Common.Employee;
using NHH.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Message.IService
{
    /// <summary>
    /// 员工消息服务接口
    /// </summary>
    public interface IEmployeeMessageService
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool SaveEmployeeMessage(EmployeeMessageInfo message);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        EmployeeMessageListModel GetMessageList(EmployeeMessageListQueryInfo queryInfo);

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        EmployeeMessageDetailModel GetDetail(int messageId);
    }
}
