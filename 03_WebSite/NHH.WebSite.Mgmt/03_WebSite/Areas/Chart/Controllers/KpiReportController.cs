using NHH.Framework.Web;
using NHH.Models.Chart;
using NHH.Models.Report;
using NHH.Service.Report.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Chart.Controllers
{
    /// <summary>
    /// KPI报表
    /// </summary>
    public class KpiReportController : NHHController
    {
        /// <summary>
        /// 获取饼图列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPieList(KpiReportQueryInfo queryInfo)
        {
            var result = new Dictionary<string, PieChartModel>();
            var model = GetService<IKPIReportService>().Search(queryInfo);
            if (model != null && model.CountInfo!=null)
            {
                var countInfo = model.CountInfo;

                result.Add("SignedTotalUnit", new PieChartModel
                {
                    data = new List<PieChartDataItem>(),
                });
                result["SignedTotalUnit"].data.Add(new PieChartDataItem
                {
                    label = "已签约铺位",
                    value = countInfo.CountSignedTotalUnit,
                    color = ColorList.GetColor(1, 0.5F),
                    highlight = ColorList.GetColor(1, 0.8F),
                });
                result["SignedTotalUnit"].data.Add(new PieChartDataItem
                {
                    label = "未签约铺位",
                    value = countInfo.CountTotalUnit - countInfo.CountSignedTotalUnit,
                    color = ColorList.GetColor(2, 0.5F),
                    highlight = ColorList.GetColor(2, 0.8F),
                });

                result.Add("SignedTotalArea", new PieChartModel
                {
                    data = new List<PieChartDataItem>(),
                });
                result["SignedTotalArea"].data.Add(new PieChartDataItem
                {
                    label = "已签约面积",
                    value = countInfo.CountSignedTotalArea,
                    color = ColorList.GetColor(1, 0.5F),
                    highlight = ColorList.GetColor(1, 0.8F),
                });
                result["SignedTotalArea"].data.Add(new PieChartDataItem
                {
                    label = "未签约面积",
                    value = countInfo.CountTotalArea - countInfo.CountSignedTotalArea,
                    color = ColorList.GetColor(2, 0.5F),
                    highlight = ColorList.GetColor(2, 0.8F),
                });

                result.Add("SignedTotalRent", new PieChartModel
                {
                    data = new List<PieChartDataItem>(),
                });
                result["SignedTotalRent"].data.Add(new PieChartDataItem
                {
                    label = "已签约租金",
                    value = countInfo.CountSignedTotalRent,
                    color = ColorList.GetColor(1, 0.5F),
                    highlight = ColorList.GetColor(1, 0.8F),
                });
                result["SignedTotalRent"].data.Add(new PieChartDataItem
                {
                    label = "未签约租金",
                    value = countInfo.CountTotalRent - countInfo.CountSignedTotalRent,
                    color = ColorList.GetColor(2, 0.5F),
                    highlight = ColorList.GetColor(2, 0.8F),
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}