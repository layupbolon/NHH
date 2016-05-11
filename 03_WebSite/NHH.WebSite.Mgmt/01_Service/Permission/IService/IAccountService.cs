using NHH.Framework.Service;
using NHH.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Permission.IService
{
    public interface IAccountService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="req">登录请求信息</param>
        /// <returns>登录结果</returns>
        MessageBag<UserLoginResult> Login(UserLoginRequest req);

        /// <summary>
        /// 验证该用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool SearchExsitName(string userName);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        bool UpdatePassword(string userName, string password);        
    }
}
