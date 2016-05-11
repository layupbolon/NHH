using NHH.Framework.Web;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.System.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : NHHController
    {
        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var model = GetService<ISysRoleService>().GetRoleList();
            model.CrumbInfo.AddCrumb("角色列表");
            return View(model);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new SysRoleDetailModel();
            model.CrumbInfo.AddCrumb("角色列表", Url.Action("List", "Role", new { area = "System" }, null));
            model.CrumbInfo.AddCrumb("添加角色");

            return View(model);
        }

        /// <summary>
        /// 提交新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(SysRoleDetailModel model)
        {
            var flag = GetService<ISysRoleService>().AddRole(model);
            if (flag)
            {
                return RedirectToAction("Edit", "Role", new { area = "System", roleId = model.RoleId });
            }
            return View(model);
        }

        /// <summary>
        /// 进入编辑页
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult Edit(int roleId)
        {
            var model = GetService<ISysRoleService>().GetRoleDetail(roleId);
            model.CrumbInfo.AddCrumb("角色列表", Url.Action("List", "Role", new { area = "System" }, null));
            model.CrumbInfo.AddCrumb("角色功能");
            return View(model);
        }

        /// <summary>
        /// 提交编辑页
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(SysRoleDetailModel model)
        {
            model.InUser = Context.UserID;
            var flag = GetService<ISysRoleService>().UpdateRole(model);
            return RedirectToAction("Edit", "Role", new { area = "System", roleId = model.RoleId });
        }
    }
}