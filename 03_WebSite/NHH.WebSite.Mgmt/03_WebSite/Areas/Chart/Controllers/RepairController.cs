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
    /// 报修图表
    /// </summary>
    public class RepairController : NHHController
    {
        private IRepairChartService service;

        public RepairController()
        {
            service = GetService<IRepairChartService>();
        }

        /// <summary>
        /// 获取响应时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetAcceptTimeBarChart(RepairChartQueryInfo queryInfo)
        {
            var model = service.GetAcceptTimeBarChart(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取响应饼状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetAcceptTimePieChart(RepairChartQueryInfo queryInfo)
        {
            var model = service.GetAcceptTimePieChart(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取处置时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetRepairTimeBarChart(RepairChartQueryInfo queryInfo)
        {
            var model = service.GetRepairTimeBarChart(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取处置时间饼状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetRepairTimePieChart(RepairChartQueryInfo queryInfo)
        {
            var model = service.GetRepairTimePieChart(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
