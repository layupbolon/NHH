using NHH.Models.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Chart.IService
{
    /// <summary>
    /// 报修图表服务接口
    /// </summary>
    public interface IRepairChartService
    {
        /// <summary>
        /// 获取响应时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        BarChartModel<int> GetAcceptTimeBarChart(RepairChartQueryInfo queryInfo);

        /// <summary>
        /// 获取响应时间饼图
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        PieChartModel GetAcceptTimePieChart(RepairChartQueryInfo queryInfo);

        /// <summary>
        /// 获取处置时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        BarChartModel<int> GetRepairTimeBarChart(RepairChartQueryInfo queryInfo);

        /// <summary>
        /// 获取处置时间饼图
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        PieChartModel GetRepairTimePieChart(RepairChartQueryInfo queryInfo);
    }
}
