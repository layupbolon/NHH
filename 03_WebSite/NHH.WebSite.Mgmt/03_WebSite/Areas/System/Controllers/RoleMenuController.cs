using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.System.Controllers
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    public class RoleMenuController : NHHController
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult Edit(int roleId)
        {
            var model = GetService<ISysRoleService>().GetRoleDetail(roleId);
            model.CrumbInfo.AddCrumb("角色列表", Url.Action("List", "Role", new { area = "System" }));
            model.CrumbInfo.AddCrumb("角色菜单");

            return View(model);
        }
    }
}