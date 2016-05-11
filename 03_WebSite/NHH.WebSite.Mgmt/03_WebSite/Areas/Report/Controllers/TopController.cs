using NHH.Framework.Web;
using NHH.Models.Report;
using NHH.Service.Common.IService;
using NHH.Service.Report.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Report.Controllers
{
    /// <summary>
    /// TOP10报表
    /// </summary>
    public class TopController : NHHController
    {
        private ITopReportService service;
        private IProjectCommonService projectService;

        public TopController()
        {
            service = GetService<ITopReportService>();
            projectService = GetService<IProjectCommonService>();
        }

        /// <summary>
        /// 商户租金
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult StoreRent(TopReportQueryInfo queryInfo)
        {
            var model = service.StoreRent(queryInfo);
            model.CrumbInfo.AddCrumb("商户租金Top10");

            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId);

            return View(model);
        }

        /// <summary>
        /// 商户面积
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult StoreArea(TopReportQueryInfo queryInfo)
        {
            var model = service.StoreRent(queryInfo);
            model.CrumbInfo.AddCrumb("商户面积Top10");

            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId);

            return View(model);
        }

        /// <summary>
        /// 商铺租金
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult UnitRent(TopReportQueryInfo queryInfo)
        {
            var model = service.StoreRent(queryInfo);
            model.CrumbInfo.AddCrumb("商铺租金Top10");

            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId);

            return View(model);
        }

        /// <summary>
        /// 商铺面积
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult UnitArea(TopReportQueryInfo queryInfo)
        {
            var model = service.StoreRent(queryInfo);
            model.CrumbInfo.AddCrumb("商铺面积Top10");

            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId);

            return View(model);
        }
    }
}
