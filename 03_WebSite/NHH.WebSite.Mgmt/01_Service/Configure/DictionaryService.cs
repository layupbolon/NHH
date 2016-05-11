using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure
{
    /// <summary>
    /// 数据字典服务
    /// </summary>
    public class DictionaryService : NHHService<NHHEntities>, IDictionaryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public DictionaryListModel GetList(DictionaryListQueryInfo queryInfo)
        {
            var model = new DictionaryListModel();
            model.QueryInfo = queryInfo;
            model.DictionaryList = new List<DictionaryInfo>();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from d in Context.Dictionary
                        select d;

            if (!string.IsNullOrEmpty(queryInfo.Type) && queryInfo.Type.Length > 0)
            {
                query = query.Where(item => item.FieldType == queryInfo.Type);
            }

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(item => item.FieldType).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new DictionaryInfo
                    {
                        FieldID = entity.FieldID,
                        FieldType = entity.FieldType,
                        FieldName = entity.FieldName,
                        FieldValue = entity.FieldValue,
                        FieldDesc = entity.FieldDesc,
                    };
                    model.DictionaryList.Add(info);
                });
            }
            return model;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public DictionaryDetailModel GetDetail(int fieldId)
        {
            var model = new DictionaryDetailModel();

            var query = from d in Context.Dictionary
                        where d.FieldID == fieldId
                        select d;
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.FieldID = entity.FieldID;
                model.FieldType = entity.FieldType;
                model.FieldName = entity.FieldName;
                model.FieldValue = entity.FieldValue;
                model.FieldDesc = entity.FieldDesc;
            }

            return model;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        public void Add(DictionaryInfo info)
        {
            var bll = CreateBizObject<Dictionary>();

            var entity = new Dictionary
            {
                FieldName = info.FieldName,
                FieldValue = info.FieldValue,
                FieldType = info.FieldType,
                FieldDesc = info.FieldDesc,
            };
            bll.Insert(entity);
            this.SaveChanges();
            info.FieldID = entity.FieldID;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="info"></param>
        public void Edit(DictionaryInfo info)
        {
            var bll = CreateBizObject<Dictionary>();

            var entity = bll.GetBySysNo(info.FieldID);
            if (entity != null)
            {
                entity.FieldName = info.FieldName;
                entity.FieldValue = info.FieldValue;
                entity.FieldType = info.FieldType;
                entity.FieldDesc = info.FieldDesc;
            }
            bll.Update(entity);
            this.SaveChanges();
            info.FieldID = entity.FieldID;
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public string GetFieldName(string fieldType, int fieldValue)
        {
            return (from d in Context.Dictionary
                    where d.FieldType == fieldType && d.FieldValue == fieldValue
                    select d.FieldName).FirstOrDefault();
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public int GetFieldValue(string fieldType, string fieldName)
        {
            return (from d in Context.Dictionary
                    where d.FieldType == fieldType && d.FieldName == fieldName
                    select d.FieldValue).FirstOrDefault();
        }
    }
}
