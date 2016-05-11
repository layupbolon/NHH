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
    /// 投诉报表管理
    /// </summary>
    public class ComplaintController : NHHController
    {
        private IComplaintReportService service;
        private IProjectCommonService projectService;

        public ComplaintController()
        {
            service = GetService<IComplaintReportService>();
            projectService = GetService<IProjectCommonService>();
        }

        /// <summary>
        /// 响应时间报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Report1(ComplaintReportQueryInfo queryInfo)
        {
            var model = service.GetReport1(queryInfo);

            model.CrumbInfo.AddCrumb("响应时间报表");

            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId);

            return View(model);
        }

        /// <summary>
        /// 处置时间报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Report2(ComplaintReportQueryInfo queryInfo)
        {
            var model = service.GetReport2(queryInfo);

            model.CrumbInfo.AddCrumb("处置时间报表");

            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId);

            return View(model);
        }
    }
}