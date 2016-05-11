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
    /// 报表服务
    /// </summary>
    public class ReportService : NHHService<NHHEntities>, IReportService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ReportListModel GetList(ReportListQueryInfo queryInfo)
        {
            var model = new ReportListModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };
            model.ReportList = new List<ReportInfo>();

            var query = from r in Context.Report
                        where r.Status == 1
                        select new
                        {
                            r.ReportID,
                            r.ReportCode,
                            r.ReportName
                        };

            if (!string.IsNullOrEmpty(queryInfo.ReportName) && queryInfo.ReportName.Length > 0)
            {
                query = query.Where(item => item.ReportName.Contains(queryInfo.ReportName));
            }

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(item => item.ReportID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    model.ReportList.Add(new ReportInfo
                    {
                        ReportID = entity.ReportID,
                        ReportCode = entity.ReportCode,
                        ReportName = entity.ReportName,
                    });
                });
            }

            return model;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public ReportInfoModel GetInfo(int? reportId)
        {
            var model = new ReportInfoModel();
            model.FieldList = new List<ReportFieldInfo>();

            if (!reportId.HasValue)
            {
                model.ReportName = "";
                return model;
            }

            model.ReportID = reportId.Value;

            var info = (from r in Context.Report
                        where r.ReportID == reportId.Value
                        select new
                        {
                            r.ReportCode,
                            r.ReportName
                        }).FirstOrDefault();

            if (info != null)
            {
                model.ReportCode = info.ReportCode;
                model.ReportName = info.ReportName;
            }
            var query = from rf in Context.Report_Field
                        where rf.Status == 1 && rf.ReportID == reportId.Value
                        select new
                        {
                            rf.FieldID,
                            rf.FieldName,
                            rf.FieldTitle,
                            rf.Formatter,
                            rf.FieldClass,
                            rf.SortName,
                            rf.Sortable,
                            rf.Exportable
                        };

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var field = new ReportFieldInfo
                    {
                        FieldID = entity.FieldID,
                        FieldName = entity.FieldName,
                        FieldTitle = entity.FieldTitle,
                        FieldClass = entity.FieldClass,
                        Formatter = entity.Formatter,
                        SortName = entity.SortName,
                        Sortable = entity.Sortable.HasValue ? entity.Sortable.Value : 0,
                        Exportable = entity.Exportable
                    };
                    model.FieldList.Add(field);
                });
            }
            return model;
        }

        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public ReportFieldInfo GetFieldInfo(int? fieldId)
        {
            var info = new ReportFieldInfo();
            if (!fieldId.HasValue)
                return info;

            var entity = (from rf in Context.Report_Field
                          where rf.Status == 1 && rf.FieldID == fieldId.Value
                          select new
                          {
                              rf.FieldID,
                              rf.FieldName,
                              rf.FieldTitle,
                              rf.FieldClass,
                              rf.Formatter,
                              rf.SortName,
                              rf.Sortable,
                              rf.Exportable
                          }).FirstOrDefault();

            if (entity != null)
            {
                info.FieldID = entity.FieldID;
                info.FieldName = entity.FieldName;
                info.FieldTitle = entity.FieldTitle;
                info.FieldClass = entity.FieldClass;
                info.Formatter = entity.Formatter;
                info.SortName = entity.SortName;
                info.Sortable = entity.Sortable.HasValue ? entity.Sortable.Value : 0;
                info.Exportable = entity.Exportable;
            }

            return info;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public MessageBag<int> Save(ReportInfo info)
        {
            var result = new MessageBag<int>();
            result.Code = -1;

            var bll = CreateBizObject<Report>();
            if (info.ReportID <= 0)
            {
                var entity = new Report
                {
                    ReportCode = info.ReportCode,
                    ReportName = info.ReportName,
                    Status = 1,
                    InUser = info.InUser,
                    InDate = DateTime.Now,
                };
                bll.Insert(entity);
                SaveChanges();
                result.Data = entity.ReportID;
            }
            else
            {
                var entity = bll.TopOne(item => item.ReportID == info.ReportID);
                if (entity == null)
                {
                    result.Desc = "未找到相关报表";
                    return result;
                }

                entity.ReportCode = info.ReportCode;
                entity.ReportName = info.ReportName;
                entity.EditUser = info.InUser;
                entity.EditDate = DateTime.Now;

                bll.Update(entity);
                SaveChanges();
                result.Data = entity.ReportID;
            }

            result.Code = 0;
            result.Desc = "保存成功";
            return result;
        }

        /// <summary>
        /// 保存字段
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public MessageBag<bool> SaveFieldInfo(ReportFieldInfo info)
        {
            var result = new MessageBag<bool>();
            result.Code = -1;

            var bll = CreateBizObject<Report_Field>();
            if (info.FieldID <= 0)
            {
                if (info.ReportID <= 0)
                {
                    result.Desc = "请指定报表";
                    return result;
                }

                var entity = new Report_Field
                {
                    ReportID = info.ReportID,
                    FieldName = info.FieldName,
                    FieldTitle = info.FieldTitle,
                    FieldClass = info.FieldClass,
                    Formatter = info.Formatter,
                    SortName = info.SortName,
                    Sortable = info.Sortable,
                    Exportable = info.Exportable,
                    Status = 1,
                    InUser = info.InUser,
                    InDate = DateTime.Now
                };
                bll.Insert(entity);
            }
            else
            {
                var entity = bll.TopOne(item => item.FieldID == info.FieldID);
                if (entity == null)
                {
                    result.Desc = "未发现相关字段";
                    return result;
                }

                entity.FieldName = info.FieldName;
                entity.FieldTitle = info.FieldTitle;
                entity.FieldClass = info.FieldClass;
                entity.SortName = info.SortName;
                entity.Formatter = info.Formatter;
                entity.Sortable = info.Sortable;
                entity.Exportable = info.Exportable;
                entity.EditUser = info.InUser;
                entity.EditDate = DateTime.Now;

                bll.Update(entity);
            }

            SaveChanges();

            result.Code = 0;
            result.Desc = "保存成功";
            return result;
        }
    }
}
