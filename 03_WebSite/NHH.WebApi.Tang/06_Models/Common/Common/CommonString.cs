using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Common
{
    public class CommonString
    {
        /// <summary>
        /// 忘记密码短信、邮件标题
        /// </summary>
        public const string ForgetPasswordSendMessageSubject = "唐小二-忘记密码验证码";

        /// <summary>
        /// 忘记密码短信、邮件内容
        /// </summary>
        public const string ForgetPasswordSendMessageContent = "【唐小二】您的验证码是{0}，请在5分钟内输入有效，谢谢！";

        /// <summary>
        /// 忘记密码验证码Cache前缀
        /// </summary>
        public const string ForgetPasswordCacheKeyPrefix = "ForgetValiCode_";
    }
}
