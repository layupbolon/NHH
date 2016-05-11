using NHH.Framework.Web;
using NHH.Models.Contract;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Contract.Controllers
{
    /// <summary>
    /// 意向管理
    /// </summary>
    public class RequestMgmtController : NHHController
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new RequestDetailModel();

            var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");

            return View(model);
        }
    }
}