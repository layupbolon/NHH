using NHH.Framework.Service;
using NHH.Models.Chart;
using NHH.Service.Chart.IService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Chart
{
    /// <summary>
    /// 报修图表服务
    /// </summary>
    public class RepairChartService : NHHService<NHH.Entities.NHHEntities>, IRepairChartService
    {
        /// <summary>
        /// 获取报修响应时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public BarChartModel<int> GetAcceptTimeBarChart(RepairChartQueryInfo queryInfo)
        {
            var model = new BarChartModel<int>();

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@ProjectId", queryInfo.ProjectId),
                new SqlParameter("@StartTime", queryInfo.StartTime),
                new SqlParameter("@EndTime", queryInfo.EndTime)
            };

            var data = ExecCmd<RepairChartDataItem>("Chart.Repair.AcceptMinute.Week", paramList);
            if (data == null || data.Count == 0)
                return model;


            //取得周数
            model.labels = new List<string>();
            int weekNum = data.Max(item => item.WeekNum) + 1;
            for (int w = weekNum; w >= 0; w--)
            {
                model.labels.Add(string.Format("W{0}", w));
            }

            //每个公司
            var companyIds = data.GroupBy(item => item.CompanyId).Select(item => item.Key).ToList();

            model.datasets = new List<BarChartDataSet<int>>();

            var num = 0;
            companyIds.ForEach(companyId =>
            {
                var dataset = new BarChartDataSet<int>()
                {
                    label = data.Find(item => item.CompanyId == companyId).BriefName,
                    data = new List<int>(),
                    fillColor = ColorList.GetColor(num, 0.5f),
                    strokeColor = ColorList.GetColor(num, 0.6f),
                    highlightFill = "",
                    highlightStroke = "",
                };
                //每周
                for (int w = weekNum; w >= 0; w--)
                {
                    var dataItem = data.Find(item => item.CompanyId == companyId && item.WeekNum == w);
                    if (dataItem == null)
                    {
                        dataset.data.Add(0);
                    }
                    else
                    {
                        dataset.data.Add(dataItem.MinuteNum);
                    }
                }

                model.datasets.Add(dataset);

                //步长
                num += 1;
            });

            return model;
        }

        /// <summary>
        /// 获取响应时间饼图
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public PieChartModel GetAcceptTimePieChart(RepairChartQueryInfo queryInfo)
        {
            var model = new PieChartModel();
            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@ProjectId", queryInfo.ProjectId),
                new SqlParameter("@StartTime", queryInfo.StartTime),
                new SqlParameter("@EndTime", queryInfo.EndTime)
            };

            var data = ExecCmd<RepairChartDataItem>("Chart.Repair.AcceptMinute.Week", paramList);
            if (data == null || data.Count == 0)
                return model;

            model.data = new List<PieChartDataItem>();

            var companyList = (from d in data
                               group d by new { d.CompanyId, d.BriefName } into g
                               select new
                               {
                                   label = g.Key.BriefName,
                                   value = g.Sum(item => item.MinuteNum)
                               }).ToList();

            var num = 0;
            companyList.ForEach(company =>
            {
                var dataitem = new PieChartDataItem
                {
                    label = company.label,
                    value = company.value,
                    color = ColorList.GetColor(num, 0.5f),
                    highlight = ColorList.GetColor(num, 0.8f),
                };
                model.data.Add(dataitem);

                //步长
                num += 1;
            });

            return model;
        }

        /// <summary>
        /// 获取报修处置时间柱状图表数据
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public BarChartModel<int> GetRepairTimeBarChart(RepairChartQueryInfo queryInfo)
        {
            var model = new BarChartModel<int>();

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@ProjectId", queryInfo.ProjectId),
                new SqlParameter("@StartTime", queryInfo.StartTime),
                new SqlParameter("@EndTime", queryInfo.EndTime)
            };

            var data = ExecCmd<RepairChartDataItem>("Chart.Repair.RepairMinute.Week", paramList);
            if (data == null || data.Count == 0)
                return model;


            //取得周数
            model.labels = new List<string>();
            int weekNum = data.Max(item => item.WeekNum) + 1;
            for (int w = weekNum; w >= 0; w--)
            {
                model.labels.Add(string.Format("W{0}", w));
            }

            //每个公司
            var companyIds = data.GroupBy(item => item.CompanyId).Select(item => item.Key).ToList();

            model.datasets = new List<BarChartDataSet<int>>();

            var num = 0;
            companyIds.ForEach(companyId =>
            {
                var dataset = new BarChartDataSet<int>()
                {
                    label = data.Find(item => item.CompanyId == companyId).BriefName,
                    data = new List<int>(),
                    fillColor = ColorList.GetColor(num, 0.5f),
                    strokeColor = ColorList.GetColor(num, 0.6f),
                    highlightFill = "",
                    highlightStroke = "",
                };
                //每周
                for (int w = weekNum; w >= 0; w--)
                {
                    var dataItem = data.Find(item => item.CompanyId == companyId && item.WeekNum == w);
                    if (dataItem == null)
                    {
                        dataset.data.Add(0);
                    }
                    else
                    {
                        dataset.data.Add(dataItem.MinuteNum);
                    }
                }

                model.datasets.Add(dataset);

                //步长
                num += 1;
            });

            return model;
        }

        /// <summary>
        /// 获取报修处置时间饼图图表数据
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public PieChartModel GetRepairTimePieChart(RepairChartQueryInfo queryInfo)
        {
            var model = new PieChartModel();
            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@ProjectId", queryInfo.ProjectId),
                new SqlParameter("@StartTime", queryInfo.StartTime),
                new SqlParameter("@EndTime", queryInfo.EndTime)
            };

            var data = ExecCmd<RepairChartDataItem>("Chart.Repair.RepairMinute.Week", paramList);
            if (data == null || data.Count == 0)
                return model;

            model.data = new List<PieChartDataItem>();

            var companyList = (from d in data
                               group d by new { d.CompanyId, d.BriefName } into g
                               select new
                               {
                                   label = g.Key.BriefName,
                                   value = g.Sum(item => item.MinuteNum)
                               }).ToList();

            var num = 0;
            companyList.ForEach(company =>
            {
                var dataitem = new PieChartDataItem
                {
                    label = company.label,
                    value = company.value,
                    color = ColorList.GetColor(num, 0.5f),
                    highlight = ColorList.GetColor(num, 0.8f),
                };
                model.data.Add(dataitem);

                //步长
                num += 1;
            });

            return model;
        }
    }
}
