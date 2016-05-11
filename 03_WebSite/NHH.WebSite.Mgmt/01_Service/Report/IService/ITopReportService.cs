using NHH.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Report.IService
{
    /// <summary>
    /// Top报表服务接口
    /// </summary>
    public interface ITopReportService
    {
        /// <summary>
        /// 商户租金Top报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        TopReportModel StoreRent(TopReportQueryInfo queryInfo);

        /// <summary>
        /// 商户租金Top报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        TopReportModel StoreArea(TopReportQueryInfo queryInfo);

        /// <summary>
        /// 商铺租金Top报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        TopReportModel UnitRent(TopReportQueryInfo queryInfo);

        /// <summary>
        /// 商铺面积Top报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        TopReportModel UnitArea(TopReportQueryInfo queryInfo);
    }
}
