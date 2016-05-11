using NHH.Framework.Web;
using NHH.Models.Common;
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
    /// 功能
    /// </summary>
    public class FunctionController : NHHController
    {
        /// <summary>
        /// 功能模块列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(SysFunctionListQueryInfo queryInfo)
        {
            var model = GetService<ISysFunctionService>().GetFunctionList(queryInfo);
            model.CrumbInfo.AddCrumb("功能列表");

            return View(model);
        }

        /// <summary>
        /// 进入新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new SysFunctionDetailModel();
            model.CrumbInfo.AddCrumb("功能列表", Url.Action("List", "Function", new { area = "System" }, null));
            model.CrumbInfo.AddCrumb("新增功能");

            return View(model);
        }

        /// <summary>
        /// 提交新增页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(SysFunctionDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("功能列表", Url.Action("List", "Function", new { area = "System" }, null));
                model.CrumbInfo.AddCrumb("新增功能");
                return View(model);
            }

            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            var flag = GetService<ISysFunctionService>().AddFunction(model);
            return RedirectToAction("List", "Function", new { area = "System" });
        }

        /// <summary>
        /// 进入编辑页面
        /// </summary>
        /// <param name="FunctionId"></param>
        /// <returns></returns>
        public ActionResult Edit(int functionId)
        {
            var model = GetService<ISysFunctionService>().GetFunctionDetail(functionId);
            model.CrumbInfo.AddCrumb("功能列表", Url.Action("List", "Function", new { area = "System" }, null));
            model.CrumbInfo.AddCrumb("编辑功能");
            return View(model);
        }

        /// <summary>
        /// 提交编辑信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(SysFunctionDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("功能列表", Url.Action("List", "Function", new { area = "System" }, null));
                model.CrumbInfo.AddCrumb("编辑功能");
                return View(model);
            }
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            var flag = GetService<ISysFunctionService>().UpdateFunction(model);
            return RedirectToAction("List", "Function", new { area = "System" });
        }
    }
}