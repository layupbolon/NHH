using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Report;
using NHH.Service.Report.IService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Report
{
    /// <summary>
    /// 投诉报表服务
    /// </summary>
    public class ComplaintReportService : NHHService<NHHEntities>, IComplaintReportService
    {
        /// <summary>
        /// 响应时间报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ComplaintReportModel GetReport1(ComplaintReportQueryInfo queryInfo)
        {
            var model = new ComplaintReportModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var paramList = new SqlParameter[] 
            {
                new SqlParameter("@Count", System.Data.SqlDbType.Int),
                new SqlParameter("@ProjectId", queryInfo.ProjectId),
                new SqlParameter("@StartTime", queryInfo.StartTime),
                new SqlParameter("@EndTime", queryInfo.EndTime),
                new SqlParameter("@PageIndex", model.PagingInfo.PageIndex),
                new SqlParameter("@PageSize", model.PagingInfo.PageSize),
            };
            paramList[0].Direction = System.Data.ParameterDirection.Output;


            model.Data = ExecCmd<ComplaintReportItem>("Report.Complaint.AcceptMinute", paramList);
            model.PagingInfo.TotalCount = (int)paramList[0].Value;

            return model;
        }

        /// <summary>
        /// 处置时间报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ComplaintReportModel GetReport2(ComplaintReportQueryInfo queryInfo)
        {
            var model = new ComplaintReportModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var paramList = new SqlParameter[] 
            {
                new SqlParameter("@Count", System.Data.SqlDbType.Int),
                new SqlParameter("@ProjectId", queryInfo.ProjectId),
                new SqlParameter("@StartTime", queryInfo.StartTime),
                new SqlParameter("@EndTime", queryInfo.EndTime),
                new SqlParameter("@PageIndex", model.PagingInfo.PageIndex),
                new SqlParameter("@PageSize", model.PagingInfo.PageSize),
            };
            paramList[0].Direction = System.Data.ParameterDirection.Output;


            model.Data = ExecCmd<ComplaintReportItem>("Report.Complaint.ServiceMinute", paramList);
            model.PagingInfo.TotalCount = (int)paramList[0].Value;

            return model;
        }
    }
}
