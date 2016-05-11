using NHH.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Report.IService
{
    /// <summary>
    /// KPI报表服务接口
    /// </summary>
    public interface IKPIReportService
    {
        /// <summary>
        /// KPI报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        KpiReportModel Search(KpiReportQueryInfo queryInfo);

        /// <summary>
        /// 统计信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        KpiReportCountInfo GetCountInfo(KpiReportQueryInfo queryInfo);
    }
}
