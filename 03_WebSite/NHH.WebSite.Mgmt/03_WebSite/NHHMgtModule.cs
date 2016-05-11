using NHH.Framework.Caching;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Framework.Web;
using NHH.Models.Permission;
using NHH.Service.Permission.IService;
using System;
using System.Collections.Generic;
using System.Web;

namespace NHH.WebSite.Management
{
    public class NHHMgtModule : NHHWebModule
    {
        /// <summary>
        /// 加载用户相关配置信息
        /// </summary>
        /// <param name="user"></param>
        protected override void LoadUserConfig(int user)
        {

            UserPermissionHelper.LoadUserPermissions(user);
            UserMenuHelper.LoadUserMenus(user);
        }

        /// <summary>
        /// 获取指定ID用户功能权限列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected override List<string> GetUserPermissions(int user)
        {
            return UserPermissionHelper.GetUserPermissions(user);
        }
    }
}
