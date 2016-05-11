using NHH.Framework.Web;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using NHH.Service.Permission.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.System.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController : NHHController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(SysMenuListQueryInfo queryInfo)
        {
            var iService = GetService<ISysMenuService>();
            var model = iService.GetMenuList(queryInfo);
            model.CrumbInfo.AddCrumb("菜单列表");

            var menuList = iService.GetMenuList(0);
            ViewData["MenuList"] = new SelectList(menuList, "MenuID", "MenuName", queryInfo.ParentId.HasValue ? queryInfo.ParentId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new SysMenuDeatilModel();
            model.CrumbInfo.AddCrumb("菜单列表", Url.Action("List", "Menu", new { area = "System" }));
            model.CrumbInfo.AddCrumb("新增菜单");

            var iService = GetService<ISysMenuService>();
            var menuList = iService.GetMenuList(0);
            ViewData["MenuList"] = new SelectList(menuList, "MenuID", "MenuName");

            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(SysMenuDeatilModel model)
        {
            var iService = GetService<ISysMenuService>();
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("菜单列表", Url.Action("List", "Menu", new { area = "System" }, null));
                model.CrumbInfo.AddCrumb("新增菜单");

                var menuList = iService.GetMenuList(0);
                ViewData["MenuList"] = new SelectList(menuList, "MenuID", "MenuName");

                return View(model);
            }
            model.EditUser = Context.UserID;
            var flag = iService.AddMenu(model);
            return RedirectToAction("List", "Menu", new { area = "System" });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public ActionResult Edit(int menuId)
        {
            var iService = GetService<ISysMenuService>();
            var model = iService.GetMenuDetail(menuId);
            model.CrumbInfo.AddCrumb("菜单列表", Url.Action("List", "Menu", new { area = "System" }));
            model.CrumbInfo.AddCrumb("编辑菜单");

            var menuList = iService.GetMenuList(0);
            ViewData["MenuList"] = new SelectList(menuList, "MenuID", "MenuName");

            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(SysMenuDeatilModel model)
        {
            var iService = GetService<ISysMenuService>();
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("菜单列表", Url.Action("List", "Menu", new { area = "System" }, null));
                model.CrumbInfo.AddCrumb("编辑菜单");

                var menuList = iService.GetMenuList(0);
                ViewData["MenuList"] = new SelectList(menuList, "MenuID", "MenuName");

                return View(model);
            }
            model.EditUser = Context.UserID;
            var flag = iService.UpdateMenu(model);
            return RedirectToAction("List", "Menu", new { area = "System" });
        }
    }
}