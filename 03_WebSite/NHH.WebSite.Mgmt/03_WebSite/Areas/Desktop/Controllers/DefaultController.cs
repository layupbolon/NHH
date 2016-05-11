using NHH.Framework.Web;
using NHH.Models.Report;
using NHH.Service.Common.IService;
using NHH.Service.Report.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Desktop.Controllers
{
    /// <summary>
    /// 默认桌面
    /// </summary>
    public class DefaultController : NHHController
    {
        /// <summary>
        /// 默认首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new DefaultPageModel();
            model.CrumbInfo.AddCrumb("项目总览");

            model.CountInfoList = new List<KpiReportCountInfo>();
            var projectList = GetService<IProjectCommonService>().GetProjectList();
            if (projectList != null)
            {
                IKPIReportService service = GetService<IKPIReportService>();
                projectList.ForEach(project =>
                {
                    var countInfo = service.GetCountInfo(new KpiReportQueryInfo { ProjectId = project.Id });
                    countInfo.ProjectName = project.Name;
                    model.CountInfoList.Add(countInfo);
                });
            }

            return View(model);
        }
    }
}