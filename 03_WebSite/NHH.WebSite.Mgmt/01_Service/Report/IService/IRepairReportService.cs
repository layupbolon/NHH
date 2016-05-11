using NHH.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Report.IService
{
    /// <summary>
    /// 维修报表服务接口
    /// </summary>
    public interface IRepairReportService
    {
        /// <summary>
        /// 响应时间报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        RepairReportModel GetAcceptMinuteReport(RepairReportQueryInfo queryInfo);

        /// <summary>
        /// 处置时间报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        RepairReportModel GetRepairMinuteReport(RepairReportQueryInfo queryInfo);
    }
}
