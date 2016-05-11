using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure
{
    /// <summary>
    /// 角色菜单服务
    /// </summary>
    public class SysRoleMenuService : NHHService<NHHEntities>, ISysRoleMenuService
    {
        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysMenuInfo> GetRoleMenuList(int roleId)
        {
            var list = new List<SysMenuInfo>();

            var query = from rm in Context.Sys_Role_Menu
                        join m in Context.Sys_Menu on rm.MenuID equals m.MenuID
                        where rm.RoleID == roleId
                        select new
                        {
                            m.ParentID,
                            m.SeqNo,
                            m.MenuID,
                            m.MenuName
                        };

            var entityList = query.OrderBy(item => new { item.ParentID, item.SeqNo }).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new SysMenuInfo
                    {
                        MenuID = entity.MenuID,
                        MenuName = entity.MenuName,
                        SeqNo = entity.SeqNo,
                        ParentID = entity.ParentID,
                    });
                });
            }

            return list;
        }

        /// <summary>
        /// 获取系统菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysMenuInfo> GetSysMenuList(int roleId)
        {
            var list = new List<SysMenuInfo>();

            var query = from m in Context.Sys_Menu
                        where !(from rm in Context.Sys_Role_Menu
                                where rm.RoleID == roleId && rm.MenuID == m.MenuID
                                select rm.MenuID).Any()
                        select new
                        {
                            m.ParentID,
                            m.SeqNo,
                            m.MenuID,
                            m.MenuName
                        };

            var entityList = query.OrderBy(item => new { item.ParentID, item.SeqNo }).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new SysMenuInfo
                    {
                        MenuID = entity.MenuID,
                        MenuName = entity.MenuName,
                        SeqNo = entity.SeqNo,
                        ParentID = entity.ParentID,
                    });
                });
            }

            return list;
        }

        /// <summary>
        /// 添加角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool AddRoleMenu(int roleId, int menuId)
        {
            var bll = CreateBizObject<Sys_Role_Menu>();
            var entity = bll.TopOne(item => item.RoleID == roleId && item.MenuID == menuId);
            if (entity == null)
            {
                entity = new Sys_Role_Menu
                {
                    RoleID = roleId,
                    MenuID = menuId,
                    InDate = DateTime.Now,
                    InUser = 0,
                    EditDate = DateTime.Now,
                    EditUser = 0,
                };
                bll.Insert(entity);
                this.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// 移除角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool RemoveRoleMenu(int roleId, int menuId)
        {
            var bll = CreateBizObject<Sys_Role_Menu>();
            var entity = bll.TopOne(item=>item.RoleID == roleId && item.MenuID == menuId);
            if (entity != null)
            {
                bll.Delete(entity);
                this.SaveChanges();
            }
            return true;
        }
    }
}
