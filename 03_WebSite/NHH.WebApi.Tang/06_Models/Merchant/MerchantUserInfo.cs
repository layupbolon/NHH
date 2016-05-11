using System.Web.Script.Serialization;
using Newtonsoft.Json;
using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户用户信息
    /// </summary>
    public class MerchantUserInfo
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int UserId { get; set; }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MerchantId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ProjectId { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PhotoFile { get; set; }

        /// <summary>
        /// 商户信息
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MerchantName { get; set; }

        /// <summary>
        /// 所属商铺ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StoreId { get; set; }

        /// <summary>
        /// 所属商铺名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string StoreName { get; set; }
        
        /// <summary>
        /// 铺位号
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UnitNumber { get; set; }

        /// <summary>
        /// 所属角色 1管理员 2操作员
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RoleId { get; set; }

        /// <summary>
        /// 所属角色名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RoleName { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UserName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Mobile { get; set; }

        /// <summary>
        /// 微信OpenID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string WechatOpenId { get; set; }

        /// <summary>
        /// 状态 1有效 -1无效
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Status { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string IDNumber { get; set; }
    }

    public class MerchantUserPassword
    {
        public int userID { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }

    public class ResetPasswordInfo
    {
        public int UserID { get; set; }
        /// <summary>
        /// 容器，手机号或邮箱地址
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// 验证码（手机或邮箱）
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string Password { get; set; }
    }
}
