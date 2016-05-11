using NHH.Models.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure.IService
{
    /// <summary>
    /// 角色菜单服务
    /// </summary>
    public interface ISysRoleMenuService
    {
        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SysMenuInfo> GetRoleMenuList(int roleId);

        /// <summary>
        /// 获取系统菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SysMenuInfo> GetSysMenuList(int roleId);

        /// <summary>
        /// 添加角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        bool AddRoleMenu(int roleId, int menuId);

        /// <summary>
        /// 移除角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        bool RemoveRoleMenu(int roleId, int menuId);
    }
}
