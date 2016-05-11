using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    [AllowAnonymous]
    public class RoleMenuController : NHHController
    {
        #region Service
        private ISysRoleMenuService m_Service;
        protected ISysRoleMenuService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<ISysRoleMenuService>();
                }

                return m_Service;
            }
        }
        #endregion

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetRoleMenuList(int roleId)
        {
            var list = this.Service.GetRoleMenuList(roleId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取系统菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetSystemMenuList(int roleId)
        {
            var list = this.Service.GetSysMenuList(roleId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddMenu(int roleId, int menuId)
        {
            var result = new AjaxResult();
            result.Code = 0;
            result.Message = "添加成功";
            this.Service.AddRoleMenu(roleId, menuId);
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 移除菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveMenu(int roleId, int menuId)
        {
            var result = new AjaxResult();
            result.Code = 0;
            result.Message = "移除成功";
            this.Service.RemoveRoleMenu(roleId, menuId);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}