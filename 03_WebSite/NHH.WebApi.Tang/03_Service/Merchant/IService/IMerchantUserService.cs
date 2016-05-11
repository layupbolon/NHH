using NHH.Models.Common;
using NHH.Models.Common.Enum.CommonEnums;
using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商家用户服务接口
    /// </summary>
    public interface IMerchantUserService
    {
        /// <summary>
        /// 根据WechatOpenID获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        MerchantUserInfo GetMerchantUserDetailByWechatOpenID(string wechatOpenId);

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        MerchantUserInfo GetMerchantUserDetail(string userName);

        /// <summary>
        /// 根据手机号或邮箱取用户
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        MerchantUserInfo GetUserByRecipient(string recipient, int userId);

        /// <summary>
        /// 获取商户用户详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        MerchantUserInfo GetMerchantUserDetail(int userId);

        /// <summary>
        /// 根据商户ID获取该商户的所有用户列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        List<MerchantUserInfo> GetMerchantUserList(int merchantId);

        /// <summary>
        ///新增
        /// </summary>
        /// <param name="MerchantId"></param>
        /// <returns></returns>
        bool AddMerchantUser(MerchantUserInfo detailModel);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        bool UpdateMerchantUser(MerchantUserInfo detailModel);

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateUserPhoto(MerchantUserInfo model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteMerchantUser(int userId);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool MerchantUserUpdatePassword(MerchantUserPassword model);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ResetMerchantUserPassword(ResetPasswordInfo model);

        /// <summary>
        /// 获取用户未读的消息数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        int GetNoReadMerchantMessageQty(int userId, int merchantId);

        /// <summary>
        /// 获取用户的消息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        MerchantMessageListModel GetMessageList(int userId, int page, int size);

        /// <summary>
        /// 获取单个通知消息信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        MerchantMessageInfo GetMessageDetail(int messageId);

        /// <summary>
        /// 更新通知消息为已读
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        bool UpdateMessageReadState(int messageId);

        /// <summary>
        /// 更新商户用户状态
        /// </summary>
        /// <param name="userId">商户用户编号</param>
        /// <param name="status">状态 -1作废 1有效</param>
        /// <returns></returns>
        bool UpdateMerchantUserStatus(int userId, int status);

        /// <summary>
        /// 用户是否绑定过微信 
        /// </summary>
        /// <param name="wechatOpenId"></param>
        /// <returns></returns>
        bool IsExistsOpenID(string wechatOpenId);

        /// <summary>
        /// 更新商户用户的微信OpenID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="wechatOpenId"></param>
        /// <returns></returns>
        bool UpdateMerchantUserOpenId(int userId, string wechatOpenId);

        /// <summary>
        /// 发送重置密码验证码，并存入Cache
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="recipient"></param>
        /// <returns></returns>
        bool SendForgetValidateCode(MessageTypeEnum messageType, string recipient);

    }

}
