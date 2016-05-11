using NHH.Models.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Chart.IService
{
    /// <summary>
    /// 投诉图表服务接口
    /// </summary>
    public interface IComplaintChartService
    {
        /// <summary>
        /// 获取响应时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        BarChartModel<int> GetAcceptTimeBarChart(ComplaintChartQueryInfo queryInfo);

        /// <summary>
        /// 获取响应时间饼图
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        PieChartModel GetAcceptTimePieChart(ComplaintChartQueryInfo queryInfo);

        /// <summary>
        /// 获取处置时间柱状图表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        BarChartModel<int> GetServiceTimeBarChart(ComplaintChartQueryInfo queryInfo);

        /// <summary>
        /// 获取处置时间饼图
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        PieChartModel GetServiceTimePieChart(ComplaintChartQueryInfo queryInfo);
    }
}
