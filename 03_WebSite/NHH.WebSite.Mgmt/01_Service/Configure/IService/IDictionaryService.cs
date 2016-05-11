using NHH.Models.Common;
using NHH.Models.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure.IService
{
    /// <summary>
    /// 数据字典服务接口
    /// </summary>
    public interface IDictionaryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        DictionaryListModel GetList(DictionaryListQueryInfo queryInfo);

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="filedId"></param>
        /// <returns></returns>
        DictionaryDetailModel GetDetail(int filedId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        void Add(DictionaryInfo info);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="info"></param>
        void Edit(DictionaryInfo info);

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        string GetFieldName(string fieldType, int fieldValue);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        int GetFieldValue(string fieldType, string fieldName);
    }
}
