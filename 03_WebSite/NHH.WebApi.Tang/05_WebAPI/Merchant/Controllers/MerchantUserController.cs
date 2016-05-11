using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using NHH.Framework.Caching;
using NHH.Framework.Security.Cryptography;
using NHH.Models.Common;
using NHH.Models.Common.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Common.Enum.CommonEnums;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using NHH.Framework.Web;
using System.Web.Mvc;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class MerchantUserController : NHHController
    {

        #region Service
        private IMerchantUserService m_service;
        public IMerchantUserService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IMerchantUserService>();
                }
                return m_service;
            }
        }

        private IMerchantService m_MerchantService;

        public IMerchantService MerchantService
        {
            get
            {
                if (m_MerchantService == null)
                {
                    m_MerchantService = this.GetService<IMerchantService>();
                }
                return m_MerchantService;
            }
        }
        #endregion

        /// <summary>
        /// 微信登陆
        /// </summary>
        /// <param name="wechatOpenId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("merchant/account/wechatlogin/{wechatOpenId}")]
        //[NHHActionLog]
        public JsonResult WechatLogin(string wechatOpenId)
        {
            ApiResult<CommonToken> result = new ApiResult<CommonToken>() { Code = 1001, Desc = "微信号没有绑定！" };
            if (string.IsNullOrEmpty(wechatOpenId)) { return Json(new ApiResult<CommonToken>() { Code = 1000, Desc = "OpenId不能为空！" }); }

            var entity = this.Service.GetMerchantUserDetailByWechatOpenID(wechatOpenId.Trim());
            if (entity != null)
            {
                var userData = new SortedList<string, string>();
                userData.Add("UserID", entity.UserId.ToString());
                userData.Add("UserName", entity.UserName);
                userData.Add("RoleID", entity.RoleId.ToString());
                userData.Add("RoleName", entity.RoleName);
                userData.Add("StoreID", entity.StoreId.ToString());
                userData.Add("MerchantID", entity.MerchantId.ToString());
                int projectId = MerchantService.GetProjectByMerchantId(entity.MerchantId);
                userData.Add("ProjectID", projectId.ToString());
                string token = null;
                NHHWebContext.Current.SignIn(userData, out token);

                entity.ProjectId = projectId;
                entity.Password = null;

                result = new ApiResult<CommonToken>(ApiResultEnum.Success)
                {
                    Data = new CommonToken() { Token = token, UserInfo = entity }
                };
            }
            return Json(result);
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("merchant/account/login")]
        //[NHHActionLog]
        public JsonResult Login(MerchantUserInfo model)
        {
            ApiResult<CommonToken> result = new ApiResult<CommonToken>() { Code = 1001, Desc = "用户名或密码错误！" };
            if (string.IsNullOrEmpty(model.UserName)) { return Json(new ApiResult<CommonToken>() { Code = 1000, Desc = "用户名不能为空！" }); }
            if (string.IsNullOrEmpty(model.Password)) { return Json(new ApiResult<CommonToken>() { Code = 1000, Desc = "密码不能为空！" }); }
            if (!string.IsNullOrEmpty(model.WechatOpenId))
            {
                if (Service.IsExistsOpenID(model.WechatOpenId))
                    return Json(new ApiResult() { Code = 1000, Desc = "微信号已经绑定过其它帐号，不能重复绑定！" });
            }
            var entity = this.Service.GetMerchantUserDetail(model.UserName);
            if (model.WechatOpenId!=null && model.WechatOpenId.Equals(entity.WechatOpenId))
                return Json(new ApiResult() { Code = 1000, Desc = "该帐号已经绑定过其它微信号，请确认您的帐号！" });
            if (entity != null)
            {
                if (entity.Status > 0 && entity.Password.Equals(Cryptographer.SHA1.Encrypt(model.Password)))
                {
                    //如果有传过来OpenID，则在登陆成功后更新数据库绑定
                    if (!string.IsNullOrEmpty(model.WechatOpenId))
                        this.Service.UpdateMerchantUserOpenId(entity.UserId, model.WechatOpenId);
                    var userData = new SortedList<string, string>();
                    userData.Add("UserID", entity.UserId.ToString());
                    userData.Add("UserName", entity.UserName);
                    userData.Add("RoleID", entity.RoleId.ToString());
                    userData.Add("RoleName", entity.RoleName);
                    userData.Add("StoreID", entity.StoreId.ToString());
                    userData.Add("MerchantID", entity.MerchantId.ToString());
                    int projectId = MerchantService.GetProjectByMerchantId(entity.MerchantId);
                    userData.Add("ProjectID", projectId.ToString());
                    string token = null;
                    NHHWebContext.Current.SignIn(userData, out token);

                    entity.ProjectId = projectId;
                    entity.Password = null;

                    result = new ApiResult<CommonToken>(ApiResultEnum.Success)
                    {
                        Data = new CommonToken() { Token = token, UserInfo = entity }
                    };
                }
            }
            return Json(result);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/account/photo/update")]
        //[NHHActionLog]
        public JsonResult UpdateUserPhoto(MerchantUserInfo model)
        {
            ApiResult result = new ApiResult();
            if (this.Service.UpdateUserPhoto(model))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 忘记密码发送验证码到手机或邮箱
        /// </summary>
        /// <param name="recipient"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("merchant/account/password/forget/sendmsg/{recipient}")]
        //[NHHActionLog]
        public JsonResult SendMsgAndGenerateValiCode(string recipient)
        {
            ApiResult result = null;
            #region 验证
            if (string.IsNullOrEmpty(recipient)) { return Json(new ApiResult() { Code = 1000, Desc = "手机号或邮箱地址不能为空！" }); }
            MessageTypeEnum messageType;
            if (Regex.IsMatch(recipient, RegularString.Mobile))
                messageType = MessageTypeEnum.SMS;
            else if (Regex.IsMatch(recipient, RegularString.Email))
                messageType = MessageTypeEnum.Email;
            else
                return Json(new ApiResult() { Code = 1000, Desc = "您输入的不是有效的手机号或邮箱地址！" });
            #endregion 验证
            //发送重置密码验证码，并存入Cache
            if (this.Service.SendForgetValidateCode(messageType, recipient))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("merchant/account/password/forget/reset")]
        //[NHHActionLog]
        public JsonResult ResetPassword(ResetPasswordInfo model)
        {
            ApiResult result = new ApiResult();
            var userEntity = this.Service.GetUserByRecipient(model.Recipient,0);
            #region 验证
            if (string.IsNullOrEmpty(model.Recipient)) { return Json(new ApiResult() { Code = 1000, Desc = "手机号或邮箱地址不能为空！" }); }
            if (string.IsNullOrEmpty(model.VerifyCode)) { return Json(new ApiResult() { Code = 1000, Desc = "验证码不能为空！" }); }
            string verifyCode =
                CacheManager.Default.Get<string>(CommonString.ForgetPasswordCacheKeyPrefix + model.Recipient.Trim());
            if (!model.VerifyCode.Equals(verifyCode))
            {
                return Json(new ApiResult() { Code = 1000, Desc = "验证码输入错误！" });
            }
            if (string.IsNullOrEmpty(model.Password)) { return Json(new ApiResult() { Code = 1000, Desc = "密码不能为空！" }); }
            if (!Regex.IsMatch(model.Password, RegularString.Password)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的密码不是有效的格式！" }); }
            if (userEntity == null) { return Json(new ApiResult() { Code = 1000, Desc = "手机号或邮箱地址未绑定用户！" }); }
            #endregion 验证

            model.UserID = userEntity.UserId;
            if (this.Service.ResetMerchantUserPassword(model))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/account/password/update")]
        //[NHHActionLog]
        public JsonResult UpdatePassword(MerchantUserPassword model)
        {
            model.userID = NHHAPIContext.Current.UserID;
            ApiResult result = null;
            if (!Regex.IsMatch(model.NewPassword, RegularString.Password)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "新密码不是有效的格式！" }); }

            if (this.Service.MerchantUserUpdatePassword(model))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 获取当前用户个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/account/detail")]
        //[NHHActionLog]
        public JsonResult GetUserInfo()
        {
            int userID = NHHAPIContext.Current.UserID;
            var result = this.Service.GetMerchantUserDetail(userID);
            result.Password = null;
            return Json(result);
        }

        /// <summary>
        /// 更新当前用户个人信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/account/update")]
        //[NHHActionLog]
        public JsonResult UpdateUserInfo(MerchantUserInfo model)
        {
            model.UserId = NHHAPIContext.Current.UserID;
            ApiResult<MerchantUserInfo> result = null;
            #region 验证
            if (!string.IsNullOrEmpty(model.Mobile))
            {
                if (!Regex.IsMatch(model.Mobile, RegularString.Mobile))
                {
                    return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的手机号不是有效的格式！" });
                }
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                if (this.Service.GetUserByRecipient(model.Mobile,model.UserId) != null)
                {
                    return Json(new ApiResult() { Code = 1000, Desc = "手机号已绑定用户！" });
                }
                if (!Regex.IsMatch(model.Email, RegularString.Email))
                {
                    return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的邮箱不是有效的格式！" });
                }
            }
            #endregion 验证

            if (this.Service.UpdateMerchantUser(model))
            {
                result = new ApiResult<MerchantUserInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetMerchantUserDetail(model.UserId);
            }
            else
            {
                result = new ApiResult<MerchantUserInfo>(ApiResultEnum.NoUpdateAnyRows);
                result.Data.Password = null;
            }
            return Json(result);
        }

        /// <summary>
        /// 获取当前用户未读的通知消息数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/account/message/noread/qty")]
        //[NHHActionLog]
        public JsonResult GetNoReadMessage()
        {
            int result = Service.GetNoReadMerchantMessageQty(NHHAPIContext.Current.UserID,
                NHHAPIContext.Current.MerchantID);
            return Json(result);
        }

        /// <summary>
        /// 获取当前用户通知消息列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/account/message/list/{page}/{size}")]
        //[NHHActionLog]
        public JsonResult GetMessageList(int page, int size)
        {
            int userId = NHHAPIContext.Current.UserID;
            MerchantMessageListModel result = new MerchantMessageListModel();
            result = this.Service.GetMessageList(userId, page, size);
            return Json(result);
        }

        /// <summary>
        /// 获取指定通知消息信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/account/message/detail/{messageid}")]
        //[NHHActionLog]
        public JsonResult GetMessage(int messageId)
        {
            MerchantMessageInfo result = new MerchantMessageInfo();
            result = this.Service.GetMessageDetail(messageId);
            return Json(result);
        }

        /// <summary>
        /// 更新指定通知消息阅读状态为已读
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/account/message/read/{messageid}")]
        //[NHHActionLog]
        public JsonResult ReadMessage(int messageId)
        {
            ApiResult result = new ApiResult();
            if (this.Service.UpdateMessageReadState(messageId))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 获取商户用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/user/list")]
        //[NHHActionLog]
        public JsonResult GetMerchantUserList()
        {
            int merchantId = NHHAPIContext.Current.MerchantID;
            List<MerchantUserInfo> result = new List<MerchantUserInfo>();
            result = this.Service.GetMerchantUserList(merchantId);
            return Json(result);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/user/create")]
        //[NHHActionLog]
        public JsonResult AddMerchantUser(MerchantUserInfo model)
        {
            model.MerchantId = NHHAPIContext.Current.MerchantID;
            //int storeID = NHHAPIContext.Current.StoreID;
            model.RoleId = 2; //唐小二只能添加操作员
            ApiResult<MerchantUserInfo> result = null;
            #region 验证
            if (model.StoreId<1) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "请选择该用户所属的店铺！" }); }
            if (string.IsNullOrEmpty(model.UserName)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "用户名不能为空！" }); }
            if (this.Service.GetMerchantUserDetail(model.UserName) != null) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的用户名已存在，请重新输入！" }); }
            if (string.IsNullOrEmpty(model.Password)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "密码不能为空！" }); }
            if (!Regex.IsMatch(model.Password, RegularString.Password)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的密码不是有效的格式！" }); }
            if (string.IsNullOrEmpty(model.NickName)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "显示名称不能为空！" }); }
            if (string.IsNullOrEmpty(model.Mobile)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "手机号不能为空！" }); }
            if (!Regex.IsMatch(model.Mobile, RegularString.Mobile)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的手机号不是有效的格式！" }); }
            if (this.Service.GetUserByRecipient(model.Mobile,0) != null) { return Json(new ApiResult() { Code = 1000, Desc = "您输入的手机号已绑定用户！" }); }
            if (string.IsNullOrEmpty(model.Email)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "邮箱不能为空！" }); }
            if (!Regex.IsMatch(model.Email, RegularString.Email)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的邮箱不是有效的格式！" }); }
            if (this.Service.GetUserByRecipient(model.Email,0) != null) { return Json(new ApiResult() { Code = 1000, Desc = "您输入的邮箱已绑定用户！" }); }
            if (model.RoleId != 1 && model.RoleId != 2) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "角色只能为【1】管理员【2】操作员" }); }
            //if (model.StoreId == 0) { model.StoreId = storeID; } //如果店铺不传，则默认当前店铺
            #endregion 验证

            if (this.Service.AddMerchantUser(model))
            {
                result = new ApiResult<MerchantUserInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetMerchantUserDetail(model.UserId);
                result.Data.Password = null;
            }
            else
                result = new ApiResult<MerchantUserInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/user/detail/{userid}")]
        //[NHHActionLog]
        public JsonResult GetUserInfo(int userid)
        {
            MerchantUserInfo result = null;
            result = this.Service.GetMerchantUserDetail(userid);
            result.Password = null;
            return Json(result);
        }

        /// <summary>
        /// 更新指定商户用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/user/update")]
        //[NHHActionLog]
        public JsonResult UpdateMerchantUserInfo(MerchantUserInfo model)
        {
            ApiResult<MerchantUserInfo> result = null;
            #region 验证
            if (model.UserId < 0) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "用户编号不能为空！" }); }
            if (string.IsNullOrEmpty(model.Mobile)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "手机号不能为空！" }); }
            if (!Regex.IsMatch(model.Mobile, RegularString.Mobile)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的手机号不是有效的格式！" }); }
            if (this.Service.GetUserByRecipient(model.Mobile,model.UserId) != null) { return Json(new ApiResult() { Code = 1000, Desc = "您输入的手机号已绑定用户！" }); }
            if (string.IsNullOrEmpty(model.Email)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "邮箱不能为空！" }); }
            if (!Regex.IsMatch(model.Email, RegularString.Email)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的邮箱不是有效的格式！" }); }
            if (this.Service.GetUserByRecipient(model.Email,model.UserId) != null) { return Json(new ApiResult() { Code = 1000, Desc = "您输入的邮箱已绑定用户！" }); }
            //if (model.RoleId != 1 && model.RoleId != 2) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "角色只能为【1】管理员【2】操作员" }); }
            //if (model.StoreId == 0) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "店铺编号不能为空！" }); }
            #endregion 验证

            if (this.Service.UpdateMerchantUser(model))
            {
                result = new ApiResult<MerchantUserInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetMerchantUserDetail(model.UserId);
            }
            else
                result = new ApiResult<MerchantUserInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 删除指定商户用户信息(作废）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/user/delete/{userid}")]
        //[NHHActionLog]
        public JsonResult DeleteMerchantUser(int userId)
        {
            ApiResult result = null;
            if (this.Service.UpdateMerchantUserStatus(userId, -1))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }
    }
}
