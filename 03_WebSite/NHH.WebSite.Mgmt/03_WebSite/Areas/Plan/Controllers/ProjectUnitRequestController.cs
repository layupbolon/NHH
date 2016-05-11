using NHH.Framework.Utility;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Plan;
using NHH.Service.Common.IService;
using NHH.Service.Plan.IService;
using System;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    /// <summary>
    /// 铺位租决
    /// </summary>
    public class ProjectUnitRequestController : NHHController
    {
        /// <summary>
        /// 铺位租决列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitRequestService>().GetList(queryInfo);
            model.CrumbInfo.AddCrumb("招商租决");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            var commonService = GetService<ICommonService>();
            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", queryInfo.UnitType.HasValue ? queryInfo.UnitType.Value : 0);

            var bizTypeList = commonService.GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", queryInfo.BizType.HasValue ? queryInfo.BizType.Value : 0);

            var requestStatusList = commonService.GetCommonFieldList("UnitRequestStatus");
            ViewData["RequestStatusList"] = new SelectList(requestStatusList, "FieldValue", "FieldName", queryInfo.RequestStatus.HasValue ? queryInfo.RequestStatus.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 招商进度
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Schedule(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitRequestService>().Schedule(queryInfo);
            model.CrumbInfo.AddCrumb("招商租决", Url.Action("List", "ProjectUnitRequest", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("招商进度");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            var commonService = GetService<ICommonService>();
            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", queryInfo.UnitType.HasValue ? queryInfo.UnitType.Value : 0);
            
            return View(model);
        }

        /// <summary>
        /// 招商进度导出PDF
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult ScheduleToPdf(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var result = new AjaxResult();
            result.Code = 0;

            //生成文件名
            string fileName = string.Format("招商进度_{0}_{1}_{2}.pdf", queryInfo.ProjectId, NHHWebContext.Current.UserID, DateTime.Now.ToString("yyyyMMddhhmmss"));
            string filePath = Server.MapPath("/NhhFiles/Pdf/");
            filePath += fileName;

            string url = Url.Action("Schedule", "ProjectUnitRequest", new { area = "Pdf", projectId = queryInfo.ProjectId, buindingId = queryInfo.BuildingId, floorId = queryInfo.FloorId, unitType = queryInfo.UnitType, requestStatus = queryInfo.RequestStatus }, null);

            //生成PDF
            Process p = new Process();
            p.StartInfo.FileName = ParamManager.GetStringValue("PdfProgramPath");
            p.StartInfo.Arguments = string.Format("\"http://{0}{1}\" {2}", HttpContext.Request.Url.Host, url, filePath);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            Thread.Sleep(50000);
            p.Close();

            result.Message = string.Format("/NhhFiles/Pdf/{0}", fileName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 跟踪招商情况
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Track(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitRequestService>().Track(queryInfo);
            model.CrumbInfo.AddCrumb("招商租决", Url.Action("List", "ProjectUnitRequest", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("招商跟踪");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 招商跟踪导出PDF
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult TrackToPdf(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var result = new AjaxResult();
            result.Code = 0;

            //生成文件名
            string fileName = string.Format("招商跟踪_{0}_{1}_{2}.pdf", queryInfo.ProjectId, NHHWebContext.Current.UserID, DateTime.Now.ToString("yyyyMMddhhmmss"));
            string filePath = Server.MapPath("/NhhFiles/Pdf/");
            filePath += fileName;

            string url = Url.Action("Track", "ProjectUnitRequest", new { area = "Pdf", projectId = queryInfo.ProjectId, buindingId = queryInfo.BuildingId, floorId = queryInfo.FloorId, unitType = queryInfo.UnitType, requestStatus = queryInfo.RequestStatus }, null);

            //生成PDF
            Process p = new Process();
            p.StartInfo.FileName = ParamManager.GetStringValue("PdfProgramPath");
            p.StartInfo.Arguments = string.Format("\"http://{0}{1}\" {2}", HttpContext.Request.Url.Host, url, filePath);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            Thread.Sleep(60000);
            p.Close();

            result.Message = string.Format("/NhhFiles/Pdf/{0}", fileName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}