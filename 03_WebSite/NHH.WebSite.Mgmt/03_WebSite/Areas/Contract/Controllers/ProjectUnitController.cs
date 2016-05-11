using NHH.Framework.Web;
using NHH.Models.Contract;
using NHH.Service.Common.IService;
using NHH.Service.Contract.IService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Contract.Controllers
{
    /// <summary>
    /// 商铺查询
    /// </summary>
    public class ProjectUnitController : NHHController
    {
        private IProjectUnitService service;

        public ProjectUnitController()
        {
            service = GetService<IProjectUnitService>();
        }

        /// <summary>
        /// 商铺列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ProjectUnitQueryInfo queryInfo)
        {
            var model = service.GetProjectUnitList(queryInfo);
            model.CrumbInfo.AddCrumb("合同管理", Url.Action("List", "Contract", new { area = "Contract" }));
            model.CrumbInfo.AddCrumb("商铺查询");

            var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");

            var unitStatusList = GetService<ICommonService>().GetUnitStatusList();
            ViewData["UnitStatusList"] = new SelectList(unitStatusList, "Id", "Name");

            return View(model);
        }
    }
}