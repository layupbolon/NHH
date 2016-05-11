using NHH.Framework.Service;
using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 报表服务接口
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ReportListModel GetList(ReportListQueryInfo queryInfo);

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        ReportInfoModel GetInfo(int? reportId);

        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        ReportFieldInfo GetFieldInfo(int? fieldId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        MessageBag<int> Save(ReportInfo info);

        /// <summary>
        /// 保存字段
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        MessageBag<bool> SaveFieldInfo(ReportFieldInfo info);
    }
}
