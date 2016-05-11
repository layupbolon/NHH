using NHH.Models.Common;
using NHH.Models.Message;

namespace NHH.Service.Message.IService
{
    /// <summary>
    /// 用户消息
    /// </summary>
    public interface IUserMessageService
    {
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="messageStatus"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        UserMessageListModel GetUserMessageList(UserMessageQueryInfo queryInfo);

        /// <summary>
        /// 获取具体用户消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        UserMessageDetailModel GetUserMessageDetail(int messageId);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userMessage"></param>
        /// <returns></returns>
        void UpdateUserMessageStatus(UserMessage userMessage);

        /// <summary>
        /// 获取当前用户的消息数
        /// </summary>
        /// <returns></returns>
        int GetMessageCount(int userId);
    }
}
