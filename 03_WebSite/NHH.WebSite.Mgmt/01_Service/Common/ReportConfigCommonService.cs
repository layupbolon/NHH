using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 报表配置公共服务
    /// </summary>
    public class ReportConfigCommonService : NHHService<NHHEntities>, IReportConfigCommonService
    {
        /// <summary>
        /// 获取用户报表配置信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reportCode"></param>
        /// <returns></returns>
        public ReportConfigInfo GetConfigInfo(int userId, string reportCode)
        {
            var result = new ReportConfigInfo();
            result.UserId = userId;
            result.ReportCode = reportCode;
            result.FieldList = new List<ReportFieldInfo>();
            #region 报表全部字段列表
            var allFieldList = (from rf in Context.Report_Field
                                join r in Context.Report on rf.ReportID equals r.ReportID
                                where rf.Status == 1 && r.ReportCode == reportCode
                                select new
                                {
                                    rf.FieldID,
                                    rf.FieldName,
                                    rf.FieldTitle,
                                    rf.FieldClass,
                                    rf.SortName,
                                    rf.Sortable,
                                    rf.Exportable,
                                    rf.Formatter
                                }).ToList();

            if (allFieldList != null)
            {
                allFieldList.ForEach(entity =>
                {
                    var field = new ReportFieldInfo
                    {
                        FieldID = entity.FieldID,
                        FieldName = entity.FieldName,
                        FieldTitle = entity.FieldTitle,
                        FieldClass = entity.FieldClass,
                        SortName = entity.SortName,
                        Sortable = entity.Sortable.HasValue ? entity.Sortable.Value : 0,
                        Exportable = entity.Exportable,
                        Formatter = entity.Formatter
                    };
                    result.FieldList.Add(field);
                });
            }
            #endregion
            var fieldList = (from rc in Context.Report_Config
                             join r in Context.Report on rc.ReportID equals r.ReportID
                             where rc.Status == 1 && rc.UserID == userId && r.ReportCode == reportCode
                             select rc.FieldList).FirstOrDefault();

            if (string.IsNullOrEmpty(fieldList) || fieldList.Length == 0)
                return result;

            var fieldArrList = fieldList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            result.FieldList.ForEach(field =>
            {
                if (!fieldArrList.Exists(item => field.FieldID.ToString() == item))
                {
                    field.Display = false;
                }
            });

            return result;
        }

        /// <summary>
        /// 获取导出信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ReportExportInfo<T> GetExportInfo<T>(QueryInfo queryInfo)
        {
            var result = new ReportExportInfo<T>();
            result.UserId = queryInfo.CurrentUserId;
            result.ReportCode = queryInfo.ReportCode;
            result.ReportName = (from r in Context.Report
                                 where r.ReportCode == result.ReportCode
                                 select r.ReportName).FirstOrDefault();
            result.FieldList = new List<ReportFieldInfo>();

            #region 报表全部字段列表
            var allFieldList = (from rf in Context.Report_Field
                                join r in Context.Report on rf.ReportID equals r.ReportID
                                where rf.Status == 1 && rf.Exportable == 1 && r.ReportCode == result.ReportCode
                                select new
                                {
                                    rf.FieldID,
                                    rf.FieldName,
                                    rf.FieldTitle,
                                }).ToList();

            if (allFieldList != null)
            {
                allFieldList.ForEach(entity =>
                {
                    var field = new ReportFieldInfo
                    {
                        FieldID = entity.FieldID,
                        FieldName = entity.FieldName,
                        FieldTitle = entity.FieldTitle,
                    };
                    result.FieldList.Add(field);
                });
            }
            #endregion

            var fieldList = (from rc in Context.Report_Config
                             join r in Context.Report on rc.ReportID equals r.ReportID
                             where rc.Status == 1 && rc.UserID == queryInfo.CurrentUserId && r.ReportCode == result.ReportCode
                             select rc.FieldList).FirstOrDefault();

            if (string.IsNullOrEmpty(fieldList) || fieldList.Length == 0)
                return result;

            var fieldArrList = fieldList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //移除
            result.FieldList.RemoveAll(field =>
            {
                return !fieldArrList.Exists(item => field.FieldID.ToString() == item);
            });

            return result;
        }
    }
}
