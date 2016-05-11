using NHH.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Report.IService
{
    /// <summary>
    /// 投诉报表服务接口
    /// </summary>
    public interface IComplaintReportService
    {
        /// <summary>
        /// 响应时间报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ComplaintReportModel GetReport1(ComplaintReportQueryInfo queryInfo);

        /// <summary>
        /// 处置时间报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ComplaintReportModel GetReport2(ComplaintReportQueryInfo queryInfo);
    }
}
