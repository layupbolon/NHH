using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.System.Controllers
{
    /// <summary>
    /// 报表
    /// </summary>
    [AllowAnonymous]
    public class ReportController : NHHController
    {

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ReportListQueryInfo queryInfo)
        {
            var model = GetService<IReportService>().GetList(queryInfo);
            model.CrumbInfo.AddCrumb("报表管理");
            return View(model);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public ActionResult Info(int? reportId)
        {
            var model = GetService<IReportService>().GetInfo(reportId);
            model.CrumbInfo.AddCrumb("报表管理", Url.Action("List", "Report", new { area = "System" }));

            return View(model);
        }

        /// <summary>
        /// 字段信息
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public PartialViewResult FieldInfo(int reportId, int? fieldId)
        {
            var info = GetService<IReportService>().GetFieldInfo(fieldId);
            return PartialView("Report/_PartialFieldInfo", info);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(ReportInfo info)
        {
            info.InUser = NHHWebContext.Current.UserID;
            var message = GetService<IReportService>().Save(info);

            var result = new AjaxResult<int>(message.Data);
            result.Code = message.Code;
            result.Message = message.Desc;

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 保存字段信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveFieldInfo(ReportFieldInfo info)
        {
            info.InUser = NHHWebContext.Current.UserID;
            var message = GetService<IReportService>().SaveFieldInfo(info);

            var result = new AjaxResult
            {
                Code = message.Code,
                Message = message.Desc
            };
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}