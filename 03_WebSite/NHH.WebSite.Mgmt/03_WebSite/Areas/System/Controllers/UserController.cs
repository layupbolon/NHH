using NHH.Framework.Security.Cryptography;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Configure;
using NHH.Service.Common.IService;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NHH.WebSite.Management.Areas.System.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController : NHHController
    {
        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(SysUserListQueryInfo queryInfo)
        {
            var model = GetService<ISysUserService>().GetUserList(queryInfo);
            model.CrumbInfo.AddCrumb("用户列表");

            int companyId = queryInfo.CompanyId.HasValue ? queryInfo.CompanyId.Value : 0;
            var companyService = GetService<ICompanyCommonService>();
            var companyList = companyService.GetCompanyList();
            ViewData["CompanyList"] = new SelectList(companyList, "Id", "ShortName", queryInfo.CompanyId.HasValue ? queryInfo.CompanyId.Value : companyId);

            var departmentList = companyService.GetDepartmentList(companyId);
            ViewData["DepartmentList"] = new SelectList(departmentList, "DepartmentId", "DepartmentName", queryInfo.DepartmentId.HasValue ? queryInfo.DepartmentId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 进入用户编辑页面
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult Edit(int userId)
        {
            var model = GetService<ISysUserService>().GetUserDetail(userId);
            model.CrumbInfo.AddCrumb("用户列表", Url.Action("List", "User", new { area = "System" }, null));
            model.CrumbInfo.AddCrumb("编辑用户");
            return View(model);
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PartialViewResult GetRoleList(int userId)
        {
            var model = GetService<ISysUserService>().GetUserRoleList(userId);
            return PartialView("_PartialGetRoleList", model);
        }

        /// <summary>
        /// 提交用户编辑信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(SysUserDetailModel model)
        {
            model.InUser = Context.UserID;
            var flag = GetService<ISysUserService>().UpdateUser(model);
            if (flag)
            {
                //清除用户菜单、权限缓存
                UserMenuHelper.ClearCache(model.UserId);
                UserPermissionHelper.ClearCache(model.UserId);

                return RedirectToAction("Edit", "User", new { area = "System", userId = model.UserId });
            }
            return View(model);
        }

        /// <summary>
        /// 个人信息中心
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            var model = GetService<ISysUserService>().GetSysUserDetail(Context.UserID);
            model.CrumbInfo.AddCrumb("个人信息");
            return View(model);
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Detail(SysUserDetailModel model)
        {
            model.UserId = Context.UserID;
            if (!ModelState.IsValid)
            {
                model = GetService<ISysUserService>().GetSysUserDetail(model.UserId);
                model.CrumbInfo.AddCrumb("个人信息");
                return View(model);
            }
            var flag = GetService<ISysUserService>().UpdateSysUserInfo(model);
            return RedirectToAction("Detail", "User", new { area = "System" });
        }

        /// <summary>
        /// 进入修改密码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Password(string statusText)
        {
            var model = new SysUserPasswordModel();
            model.StatusText = statusText;
            model.CrumbInfo.AddCrumb("修改密码");
            return View(model);
        }

        /// <summary>
        /// 提交密码信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Password(SysUserPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("修改密码");
                return View(model);
            }
            model.UserId = Context.UserID;
            var message = GetService<ISysUserService>().UpdatePassword(model);
            return RedirectToAction("Password", "User", new { area = "System", statusText = message.Desc });
        }

    }
}