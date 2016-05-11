using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 报表公共
    /// </summary>
    public interface IReportConfigCommonService
    {
        /// <summary>
        /// 获取报表配置信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reportCode"></param>
        /// <returns></returns>
        ReportConfigInfo GetConfigInfo(int userId, string reportCode);

        /// <summary>
        /// 获取导出信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ReportExportInfo<T> GetExportInfo<T>(QueryInfo queryInfo);
    }
}
