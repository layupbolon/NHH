using NHH.Entities;
using NHH.Framework.Service;
using NHH.Service.Permission.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Permission;

namespace NHH.Service.Permission
{
    public class MenuService : NHHService<NHHEntities>, IMenuService
    {
        /// <summary>
        /// 按用户分组加载用户功能菜单列表
        /// </summary>
        /// <returns></returns>
        public SortedList<int, List<MenuInfo>> LoadUserMenus()
        {
            var rlt = new SortedList<int, List<MenuInfo>>();

            //获取超级管理员默认全菜单列表
            var bizAllMenu = this.CreateCacheBizObject<Sys_Menu>();
            var listAllMenu = bizAllMenu.TryCacheAllList(x => x.Status == 1);
            rlt.Add(0, listAllMenu.Select(x => new MenuInfo() { MenuId = x.MenuID, MenuIcon=x.MenuIcon, ParentId = x.ParentID, SeqNo = x.SeqNo, MenuName = x.MenuName, MenuUrl = x.MenuUrl }).ToList());

            //获取普通用户菜单列表
            var bizUserMenu = this.CreateCacheBizObject<View_User_Menu>();
            var listUserMenu = bizUserMenu.TryCacheAllList();
            var users = listUserMenu.Select(x => x.UserID).Distinct().ToArray();

            foreach (var uid in users)
            {
                rlt.Add(uid, listUserMenu.Where(x => x.UserID == uid).Select(x => new MenuInfo() { MenuId = x.MenuID, MenuIcon = x.MenuIcon, ParentId = x.ParentID, SeqNo = x.SeqNo, MenuName = x.MenuName, MenuUrl = x.MenuUrl }).ToList());
            }

            return rlt;
        }
        /// <summary>
        /// 加载指定用户功能菜单列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<MenuInfo> LoadUserMenus(int user)
        {
            var rlt = new List<MenuInfo>();
            if (user == -1)
            {
                var bizAllMenu = this.CreateCacheBizObject<Sys_Menu>();
                var listAllMenu = bizAllMenu.TryCacheAllList(x => x.Status == 1);
                rlt.AddRange(listAllMenu.Select(x => new MenuInfo() { MenuId = x.MenuID, MenuIcon = x.MenuIcon, ParentId = x.ParentID, SeqNo = x.SeqNo, MenuName = x.MenuName, MenuUrl = x.MenuUrl }).ToArray());
            }
            else
            {
                var bizUserMenu = this.CreateCacheBizObject<View_User_Menu>();
                var listUserMenu = bizUserMenu.TryCacheAllList(x=>x.UserID==user);
                rlt.AddRange(listUserMenu.Select(x => new MenuInfo() { MenuId = x.MenuID, MenuIcon = x.MenuIcon, ParentId = x.ParentID, SeqNo = x.SeqNo, MenuName = x.MenuName, MenuUrl = x.MenuUrl }).ToArray());
            }
            return rlt;
        }


        /// <summary>
        /// 按角色分组加载角色功能菜单列表
        /// </summary>
        /// <returns></returns>
        public SortedList<int, List<MenuInfo>> LoadRoleMenus()
        {
            var rlt = new SortedList<int, List<MenuInfo>>();

            //获取超级管理员默认全菜单列表
            var bizAllMenu = this.CreateCacheBizObject<Sys_Menu>();
            var listAllMenu = bizAllMenu.TryCacheAllList(x => x.Status == 1);
            rlt.Add(0, listAllMenu.Select(x => new MenuInfo() { MenuId = x.MenuID, MenuIcon = x.MenuIcon, ParentId = x.ParentID, SeqNo = x.SeqNo, MenuName = x.MenuName, MenuUrl = x.MenuUrl }).ToList());

            //获取普通角色菜单列表
            var bizRoleMenu = this.CreateCacheBizObject<View_Role_Menu>();
            var listRoleMenu = bizRoleMenu.TryCacheAllList();
            var roles = listRoleMenu.Select(x => x.RoleID).Distinct().ToArray();

            foreach (var rid in roles)
            {
                rlt.Add(rid, listRoleMenu.Where(x => x.RoleID == rid).Select(x => new MenuInfo() { MenuId = x.MenuID, MenuIcon = x.MenuIcon, ParentId = x.ParentID, SeqNo = x.SeqNo, MenuName = x.MenuName, MenuUrl = x.MenuUrl }).ToList());
            }

            return rlt;
        }


        /// <summary>
        /// 加载指定角色功能菜单列表
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public List<MenuInfo> LoadRoleMenus(int role)
        {
            var rlt = new List<MenuInfo>();
            if (role == -1)
            {
                var bizAllMenu = this.CreateCacheBizObject<Sys_Menu>();
                var listAllMenu = bizAllMenu.TryCacheAllList(x => x.Status == 1);
                rlt.AddRange(listAllMenu.Select(x => new MenuInfo() { MenuId = x.MenuID, MenuIcon = x.MenuIcon, ParentId = x.ParentID, SeqNo = x.SeqNo, MenuName = x.MenuName, MenuUrl = x.MenuUrl }).ToArray());
            }
            else
            {
                var bizRoleMenu = this.CreateCacheBizObject<View_Role_Menu>();
                var listRoleMenu = bizRoleMenu.TryCacheAllList(x => x.RoleID == role);
                rlt.AddRange(listRoleMenu.Select(x => new MenuInfo() { MenuId = x.MenuID, MenuIcon = x.MenuIcon, ParentId = x.ParentID, SeqNo = x.SeqNo, MenuName = x.MenuName, MenuUrl = x.MenuUrl }).ToArray());
            }
            return rlt;
        }

    }
}
