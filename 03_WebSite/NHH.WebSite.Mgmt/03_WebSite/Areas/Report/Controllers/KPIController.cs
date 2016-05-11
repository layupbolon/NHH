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
    /// KPI报表管理
    /// </summary>
    public class KPIController : NHHController
    {
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Search(KpiReportQueryInfo queryInfo)
        {
            var model = GetService<IKPIReportService>().Search(queryInfo);
            model.CrumbInfo.AddCrumb("KPI报表");

            var projectService = GetService<IProjectCommonService>();
            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId);

            int projectId = queryInfo.ProjectId;
            if (projectId < 1)
            {
                projectId = projectList.Count > 0 ? projectList[0].Id : 0;
            }
            var buildingList = projectService.GetBuildingList(projectId);
            int buildingId = queryInfo.BuildingId.HasValue ? queryInfo.BuildingId.Value : 0;
            ViewData["BuildingList"] = new SelectList(buildingList, "Id", "Name", buildingId);

            if (buildingId < 1)
            {
                buildingId = buildingList.Count > 0 ? buildingList[0].Id : 0;
            }
            var floorList = projectService.GetFloorList(projectId, buildingId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName", buildingId);

            var commonService = GetService<ICommonService>();
            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", queryInfo.UnitType.HasValue ? queryInfo.UnitType.Value : 0);

            var bizTypeList = commonService.GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", queryInfo.BizTypeId.HasValue ? queryInfo.BizTypeId.Value : 0);

            return View(model);
        }
    }
}