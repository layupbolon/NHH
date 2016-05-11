using NHH.Framework.Caching;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Service.Permission.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.WebSite.Management
{
    public class UserPermissionHelper
    {
        #region PermissionService
        protected static IPermissionService m_UserPermissionService;
        public static IPermissionService UserPermissionService
        {
            get
            {
                if (m_UserPermissionService == null)
                {
                    m_UserPermissionService = NHHServiceFactory.Instance.CreateService<IPermissionService>(); ;
                }

                return m_UserPermissionService;
            }
        }
        #endregion

        public const string c_UserPermissionsKey = "NHH_Permissions_User_{0}";

        /// <summary>
        /// 加载用户功能权限列表
        /// </summary>
        public static void LoadUserPermissions(int user)
        {
            var u = user;
            //是否启用权限验证
            if (false == ParamManager.GetBoolValue("auth:enabled", false))
            {
                u = -1;
            }
            //是否超级管理员
            if (user == ParamManager.GetIntValue("auth:admin", 0))
            {
                u = -1;
            }
            var key = string.Format(c_UserPermissionsKey, user);
            var permissions = UserPermissionService.LoadUserFuncPermissions(u)??new List<string>();
            CacheManager.Default.Add(key, permissions);
        }

        /// <summary>
        /// 获取指定ID用户功能权限列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<string> GetUserPermissions(int user)
        {
            var key = string.Format(c_UserPermissionsKey, user);
            if (!CacheManager.Default.Contains(key))
            {
                LoadUserPermissions(user);
            }
            return CacheManager.Default.Get<List<string>>(key) ?? new List<string>();
        }

        /// <summary>
        /// 清理所有的用户功能权限列表缓存
        /// </summary>
        public static void ClearCache()
        {
            CacheManager.Default.Clear(string.Format(c_UserPermissionsKey, string.Empty));
        }

        public static void ClearCache(int user)
        {
            var key = string.Format(c_UserPermissionsKey, user);
            if (CacheManager.Default.Contains(key))
            {
                CacheManager.Default.Remove(key);
            }

        }
    }
}
