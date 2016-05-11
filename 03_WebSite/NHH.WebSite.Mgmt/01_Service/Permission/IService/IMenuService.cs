using NHH.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Permission.IService
{
    public interface IMenuService
    {
        /// <summary>
        /// 加载指定用户功能菜单列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<MenuInfo> LoadUserMenus(int user);

        /// <summary>
        /// 加载指定角色功能菜单列表
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        List<MenuInfo> LoadRoleMenus(int role);

        /// <summary>
        /// 按用户分组加载用户功能菜单列表
        /// </summary>
        /// <returns></returns>
        SortedList<int, List<MenuInfo>> LoadUserMenus();

        /// <summary>
        /// 按角色分组加载角色功能菜单列表
        /// </summary>
        /// <returns></returns>
        SortedList<int, List<MenuInfo>> LoadRoleMenus();
    }
}
