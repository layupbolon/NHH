using NHH.Framework.Web;
using NHH.Models.Chart;
using NHH.Service.Chart.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Chart.Controllers
{
    /// <summary>
    /// 投诉图表
    /// </summary>
    public class ComplaintController : NHHController
    {
        private IComplaintChartService service;

        public ComplaintController()
        {
            service = GetService<IComplaintChartService>();
        }

        /// <summary>
        /// 获取响应时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetAcceptTimeBarChart(ComplaintChartQueryInfo queryInfo)
        {
            var model = service.GetAcceptTimeBarChart(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取响应时间饼状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetAcceptTimePieChart(ComplaintChartQueryInfo queryInfo)
        {
            var model = service.GetAcceptTimePieChart(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取处置时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetServiceTimeBarChart(ComplaintChartQueryInfo queryInfo)
        {
            var model = service.GetServiceTimeBarChart(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取处置时间饼状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetServiceTimePieChart(ComplaintChartQueryInfo queryInfo)
        {
            var model = service.GetServiceTimePieChart(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}