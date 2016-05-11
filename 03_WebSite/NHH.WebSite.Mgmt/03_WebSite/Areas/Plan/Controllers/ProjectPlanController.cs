using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Plan;
using NHH.Service.Common.IService;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    /// <summary>
    /// 项目招商计划
    /// </summary>
    public class ProjectPlanController : NHHController
    {
        private IProjectPlanService iService { set; get; }

        public ProjectPlanController()
        {
            this.iService = this.GetService<IProjectPlanService>();

        }

        /// <summary>
        /// 招商计划列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ProjectPlanListQueryInfo queryInfo)
        {
            var model = iService.GetProjectPlanList(queryInfo);
            model.QueryInfo = queryInfo;

            model.CrumbInfo.AddCrumb("招商筹划");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            var commonService = GetService<ICommonService>();
            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", queryInfo.UnitType.HasValue ? queryInfo.UnitType.Value : 0);

            var bizTypeList = commonService.GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", queryInfo.BizType.HasValue ? queryInfo.BizType.Value : 0);
            
            var unitStatusList = commonService.GetUnitStatusList();
            ViewData["UnitStatusList"] = new SelectList(unitStatusList, "Id", "Name", queryInfo.UnitStatus.HasValue ? queryInfo.UnitStatus.Value : 0);

            return View(model);
        }
    }
}