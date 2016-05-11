using NHH.Models.Common.Employee;
using NHH.Models.Merchant;
using NHH.Models.Message;
using NHH.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Message.IService
{
    /// <summary>
    /// 商户消息服务接口
    /// </summary>
    public interface IMerchantMessageService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        MerchantMessageListModel GetList(MerchantMessageListQueryInfo queryInfo);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        void Add(MerchantMessageInfo info);

        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        MerchantMessageDetailModel GetDetail(int messageId);
    }
}
