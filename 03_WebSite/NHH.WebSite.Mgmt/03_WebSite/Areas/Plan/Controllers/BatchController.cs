using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Plan;
using NHH.Service.Common.IService;
using NHH.Service.Plan.IService;
using NHH.WebSite.Management.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    /// <summary>
    /// 招商批次
    /// </summary>
    public class BatchController : NHHController
    {
        /// <summary>
        /// 批次列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ProjectBatchListQueryInfo queryInfo)
        {
            queryInfo.CurrentUserId = NHHWebContext.Current.UserID;
            var model = new ProjectBatchListModel();
            model.QueryInfo = queryInfo;
            model.CrumbInfo.AddCrumb("招商批次");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 获取批次列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult GetBatchList(ProjectBatchListQueryInfo queryInfo)
        {
            queryInfo.CurrentUserId = NHHWebContext.Current.UserID;
            var model = GetService<IProjectBatchService>().GetList(queryInfo);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Export(ProjectBatchListQueryInfo queryInfo)
        {
            queryInfo.CurrentUserId = NHHWebContext.Current.UserID;
            var exportInfo = GetService<IReportConfigCommonService>().GetExportInfo<ProjectBatchInfo>(queryInfo);
            exportInfo.Body = GetService<IProjectBatchService>().GetBatchList(queryInfo.ProjectId.Value);
            ExcelHelper.Export<ProjectBatchInfo>(exportInfo);

            return new EmptyResult();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult Add(int projectId)
        {
            var model = new ProjectBatchDetailModel();
            model.CrumbInfo.AddCrumb("招商批次", Url.Action("List", "Batch", new { area = "Plan", projectId = projectId }));
            model.CrumbInfo.AddCrumb("新增招商批次");

            var projectInfo = GetService<IProjectCommonService>().GetProjectInfo(projectId);
            if (projectInfo == null)
            {
                return RedirectToAction("List");
            }

            model.ProjectID = projectInfo.Id;
            model.ProjectName = projectInfo.Name;

            return View(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(ProjectBatchInfo info)
        {
            if (!ModelState.IsValid)
            {
                var model = new ProjectBatchDetailModel();
                model.CrumbInfo.AddCrumb("招商批次", Url.Action("List", "Batch", new { area = "Plan", projectId = info.ProjectID }));
                model.CrumbInfo.AddCrumb("新增招商批次");

                var projectInfo = GetService<IProjectCommonService>().GetProjectInfo(info.ProjectID);
                if (projectInfo == null)
                {
                    return RedirectToAction("List");
                }

                model.ProjectID = projectInfo.Id;
                model.ProjectName = projectInfo.Name;
                return View(model);
            }

            info.InUser = Context.UserID;
            GetService<IProjectBatchService>().Add(info);
            return RedirectToAction("List", "Batch", new { area = "Plan", projectId = info.ProjectID });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public ActionResult Edit(int batchId)
        {
            var model = GetService<IProjectBatchService>().GetDetail(batchId);
            model.CrumbInfo.AddCrumb("招商批次", Url.Action("List", "Batch", new { area = "Plan", projectId = model.ProjectID }));
            model.CrumbInfo.AddCrumb("编辑招商批次");

            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ProjectBatchInfo info)
        {
            if (!ModelState.IsValid)
            {
                var model = new ProjectBatchDetailModel();
                model.CrumbInfo.AddCrumb("招商批次", Url.Action("List", "Batch", new { area = "Plan", projectId = info.ProjectID }));
                model.CrumbInfo.AddCrumb("编辑招商批次");

                return View(model);
            }

            info.InUser = Context.UserID;
            GetService<IProjectBatchService>().Edit(info);
            return RedirectToAction("List", "Batch", new { area = "Plan", projectId = info.ProjectID });
        }

        /// <summary>
        /// 铺位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult UnitList(ProjectBatchUnitListQueryInfo queryInfo)
        {
            var model = GetService<IProjectBatchUnitService>().GetUnitList(queryInfo);
            model.CrumbInfo.AddCrumb("招商批次", Url.Action("List", "Batch", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("铺位列表");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            return View(model);
        }
    }
}