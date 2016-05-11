using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Report;
using NHH.Service.Report.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Report
{
    /// <summary>
    /// Top报表服务
    /// </summary>
    public class TopReportService : NHHService<NHHEntities>, ITopReportService
    {
        /// <summary>
        /// 商户租金TOP报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public TopReportModel StoreRent(TopReportQueryInfo queryInfo)
        {
            var model = new TopReportModel();
            model.QueryInfo = queryInfo;
            model.Data = new List<TopReportItem>();

            return model;
        }

        /// <summary>
        /// 商户面积TOP报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public TopReportModel StoreArea(TopReportQueryInfo queryInfo)
        {
            var model = new TopReportModel();
            model.QueryInfo = queryInfo;
            model.Data = new List<TopReportItem>();

            return model;
        }

        /// <summary>
        /// 商铺租金TOP报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public TopReportModel UnitRent(TopReportQueryInfo queryInfo)
        {
            var model = new TopReportModel();
            model.QueryInfo = queryInfo;
            model.Data = new List<TopReportItem>();

            return model;
        }

        /// <summary>
        /// 商铺面积TOP报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public TopReportModel UnitArea(TopReportQueryInfo queryInfo)
        {
            var model = new TopReportModel();
            model.QueryInfo = queryInfo;
            model.Data = new List<TopReportItem>();

            return model;
        }
    }
}
