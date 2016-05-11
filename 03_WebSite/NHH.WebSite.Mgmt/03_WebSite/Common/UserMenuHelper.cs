using NHH.Framework.Caching;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Models.Permission;
using NHH.Service.Permission.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.WebSite.Management
{
    public class UserMenuHelper
    {
        #region UserMenuService
        protected static IMenuService m_UserMenuService;      
        public static IMenuService UserMenuService
        {
            get
            {
                if (m_UserMenuService == null)
                {
                    m_UserMenuService = NHHServiceFactory.Instance.CreateService<IMenuService>(); ;
                }

                return m_UserMenuService;
            }
        }
        #endregion

        public const string c_UserMemusKey = "NHH_Memus_User_{0}";

        /// <summary>
        /// 加载用户功能菜单列表
        /// </summary>
        public static void LoadUserMenus(int user)
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

            var key = string.Format(c_UserMemusKey, user);
            var menus = UserMenuService.LoadUserMenus(u) ?? new List<MenuInfo>();
            CacheManager.Default.Add(key, menus);
        }

        /// <summary>
        /// 获取指定ID用户功能菜单列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<MenuInfo> GetUserMenus(int user)
        {
            var key = string.Format(c_UserMemusKey, user);
            if (!CacheManager.Default.Contains(key))
            {
                LoadUserMenus(user);
            }
            return CacheManager.Default.Get<List<MenuInfo>>(key) ?? new List<MenuInfo>();
        }

        /// <summary>
        /// 清理所有的用户功能菜单列表缓存
        /// </summary>
        public static void ClearCache()
        {
            CacheManager.Default.Clear(string.Format(c_UserMemusKey, string.Empty));
        }

        public static void ClearCache(int user)
        {
            var key = string.Format(c_UserMemusKey, user);
            if (CacheManager.Default.Contains(key))
            {
                CacheManager.Default.Remove(key);
            }
        }
    }
}
