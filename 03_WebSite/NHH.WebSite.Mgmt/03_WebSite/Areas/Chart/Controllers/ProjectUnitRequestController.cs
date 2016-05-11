using NHH.Framework.Web;
using NHH.Models.Plan;
using NHH.Models.Chart;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Chart.Controllers
{
    /// <summary>
    /// 铺位租决
    /// </summary>
    [AllowAnonymous]
    public class ProjectUnitRequestController : NHHController
    {
        /// <summary>
        /// 铺位占比图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult ScheduleChart(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitRequestService>().Schedule(queryInfo);
            var result = new
            {
                UnitNumChart = new PieChartModel
                {
                    data = new List<PieChartDataItem>()
                },
                AreaChart = new PieChartModel
                {
                    data = new List<PieChartDataItem>()
                },
                RentChart = new PieChartModel
                {
                    data = new List<PieChartDataItem>()
                }
            };

            int num = 0;
            model.ScheduleData.ForEach(scheduleItem =>
            {
                result.UnitNumChart.data.Add(new PieChartDataItem
                {
                    label = scheduleItem.RequestStatusName,
                    value = scheduleItem.UnitNum,
                    color = ColorList.GetColor(num, 0.8f),
                    highlight = ColorList.GetColor(num, 0.9f),
                });

                result.AreaChart.data.Add(new PieChartDataItem
                {
                    label = scheduleItem.RequestStatusName,
                    value = scheduleItem.Area,
                    color = ColorList.GetColor(num, 0.8f),
                    highlight = ColorList.GetColor(num, 0.9f),
                });

                result.RentChart.data.Add(new PieChartDataItem
                {
                    label = scheduleItem.RequestStatusName,
                    value = scheduleItem.Rent,
                    color = ColorList.GetColor(num, 0.8f),
                    highlight = ColorList.GetColor(num, 0.9f),
                });
                num += 1;
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 招商跟踪
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult TrackChart(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitRequestService>().Track(queryInfo);
            var result = new
            {
                TotalUnit = new BarChartModel<decimal>
                {
                    labels = new List<string> { "铺位完成情况", },
                    datasets = new List<BarChartDataSet<decimal>>(),
                },
                TotalArea = new BarChartModel<decimal>
                {
                    labels = new List<string> { "面积完成情况" },
                    datasets = new List<BarChartDataSet<decimal>>(),
                },
                TotalRent = new BarChartModel<decimal>
                {
                    labels = new List<string> { "租金完成情况" },
                    datasets = new List<BarChartDataSet<decimal>>(),
                },
                UnitType1 = new BarChartModel<decimal>
                {
                    labels = new List<string>(),
                    datasets = new List<BarChartDataSet<decimal>>(),
                },
                UnitType2 = new BarChartModel<decimal>
                {
                    labels = new List<string>(),
                    datasets = new List<BarChartDataSet<decimal>>(),
                },
                UnitType3 = new BarChartModel<decimal>
                {
                    labels = new List<string>(),
                    datasets = new List<BarChartDataSet<decimal>>(),
                },
                BizType1 = new BarChartModel<decimal>
                {
                    labels = new List<string>(),
                    datasets = new List<BarChartDataSet<decimal>>(),
                },
                BizType2 = new BarChartModel<decimal>
                {
                    labels = new List<string>(),
                    datasets = new List<BarChartDataSet<decimal>>(),
                },
                BizType3 = new BarChartModel<decimal>
                {
                    labels = new List<string>(),
                    datasets = new List<BarChartDataSet<decimal>>(),
                }
            };
            #region 总览图表
            result.TotalUnit.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal> { (decimal)model.TotalUnit },
            });
            result.TotalUnit.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal> { (decimal)model.TotalRequestUnit },
            });
            result.TotalArea.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal> { (decimal)model.TotalArea },
            });
            result.TotalArea.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal> { (decimal)model.TotalRequestArea },
            });
            result.TotalRent.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal> { (decimal)model.TotalRent },
            });
            result.TotalRent.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal> { (decimal)model.TotalRequestRent },
            });
            #endregion


            #region 铺位类型
            result.UnitType1.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.UnitType1.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.UnitType2.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.UnitType2.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.UnitType3.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.UnitType3.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });

            model.UnitTypeList.ForEach(unitType =>
            {
                result.UnitType1.labels.Add(unitType.TrackTypeName);
                result.UnitType2.labels.Add(unitType.TrackTypeName);
                result.UnitType3.labels.Add(unitType.TrackTypeName);

                result.UnitType1.datasets[0].data.Add(unitType.UnitNum);
                result.UnitType1.datasets[1].data.Add(unitType.RequestUnitNum);

                result.UnitType2.datasets[0].data.Add(unitType.Area);
                result.UnitType2.datasets[1].data.Add(unitType.RequestArea);

                result.UnitType3.datasets[0].data.Add(unitType.Rent);
                result.UnitType3.datasets[1].data.Add(unitType.RequestRent);
            });
            #endregion

            #region 业态
            result.BizType1.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.BizType1.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.BizType2.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.BizType2.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.BizType3.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "计划",
                fillColor = "rgba(24,113,182,0.8)",
                strokeColor = "rgba(24,113,182,0.1)",
                highlightFill = "rgba(24,113,182,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });
            result.BizType3.datasets.Add(new BarChartDataSet<decimal>
            {
                label = "实际",
                fillColor = "rgba(255,102,0,0.8)",
                strokeColor = "rgba(255,102,0,0.1)",
                highlightFill = "rgba(255,102,0,0.9)",
                highlightStroke = "",
                data = new List<decimal>(),
            });

            model.BizTypeList.ForEach(bizType =>
            {
                result.BizType1.labels.Add(bizType.TrackTypeName);
                result.BizType2.labels.Add(bizType.TrackTypeName);
                result.BizType3.labels.Add(bizType.TrackTypeName);

                result.BizType1.datasets[0].data.Add(bizType.UnitNum);
                result.BizType1.datasets[1].data.Add(bizType.RequestUnitNum);

                result.BizType2.datasets[0].data.Add(bizType.Area);
                result.BizType2.datasets[1].data.Add(bizType.RequestArea);

                result.BizType3.datasets[0].data.Add(bizType.Rent);
                result.BizType3.datasets[1].data.Add(bizType.RequestRent);
            });
            #endregion
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}