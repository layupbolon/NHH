using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 处理基类
    /// </summary>
    public abstract class BaseDataProcess<T> : INhhDataProcess
        where T : BaseEntity
    {
        protected BackgroundWorker _worker;
        protected string SheetName = string.Empty;
        protected int HeaderRowNum = 0;

        /// <summary>
        /// 预处理数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected virtual DataTable GetProcessData(string fileName)
        {
            if (string.IsNullOrEmpty(SheetName))
            {
                _worker.ReportProgress(10, string.Format("未找到 {0} 相关表", SheetName));
                return null;
            }
            var table = ExcelHelper.ReadFileData(fileName, SheetName, HeaderRowNum);
            return table;
        }

        /// <summary>
        /// 检验数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="worker"></param>
        public virtual void CheckData(string fileName, BackgroundWorker worker)
        {
            _worker = worker;

            var table = GetProcessData(fileName);
            if (table == null)
            {
                _worker.ReportProgress(10, string.Format("未找到 {0} 相关数据", SheetName));
                return;
            }

            _worker.ReportProgress(1, string.Format("{0} 共有 {1} 条数据需要检验", SheetName, table.Rows.Count));

            T entity;
            int index = 0;
            foreach (DataRow row in table.Rows)
            {
                index += 1;
                _worker.ReportProgress(1, string.Format("开始检验{0}第{1}条数据", SheetName, index));

                #region 跳过页头
                if (index < HeaderRowNum)
                    continue;
                #endregion

                try
                {
                    entity = ToEntity(row);
                    //校验数据
                    ValidEntity(entity);
                }
                catch (NhhException ex)
                {
                    _worker.ReportProgress(10, ex.Message);
                    continue;
                }
            }
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public virtual void ProcessData(string fileName, BackgroundWorker worker)
        {
            _worker = worker;

            _worker.ReportProgress(2, string.Format("{0} 数据开始处理", SheetName));
            var table = GetProcessData(fileName);
            if (table == null)
            {
                _worker.ReportProgress(10, string.Format("未找到 {0} 相关数据", SheetName));
                return;
            }

            _worker.ReportProgress(1, string.Format("{0} 共有 {1} 条数据需要处理", SheetName, table.Rows.Count));

            T entity;
            int index = 0;
            foreach (DataRow row in table.Rows)
            {
                index += 1;
                _worker.ReportProgress(1, string.Format("开始处理{0}第{1}条数据", SheetName, index));

                #region 跳过页头
                if (index < HeaderRowNum)
                    continue;
                #endregion

                try
                {
                    entity = ToEntity(row);
                    //校验数据
                    ValidEntity(entity);
                }
                catch (NhhException ex)
                {
                    _worker.ReportProgress(10, ex.Message);
                    continue;
                }
                try
                {
                    //数据是否存在
                    if (!IsExists(entity))
                    {
                        SaveData(entity);
                    }
                    else
                    {
                        UpdateData(entity);
                    }
                }
                catch (Exception ex)
                {
                    _worker.ReportProgress(10, ex.ToString());
                }
                _worker.ReportProgress(1, string.Format("{0}第{1}条数据处理完成", SheetName, index));
            }
            _worker.ReportProgress(100, string.Format("{0} 数据处理完成\r\n", SheetName));
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected virtual T ToEntity(DataRow row)
        {
            return default(T);
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual void ValidEntity(T entity)
        {
        }

        /// <summary>
        /// 数据是否已存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool IsExists(T entity)
        {
            return false;
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void SaveData(T entity)
        { }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void UpdateData(T entity)
        { }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual T1 GetValue<T1>(DataRow row, string name)
        {
            if (!row.Table.Columns.Contains(name))
                return GetDefaultValue<T1>();
            var obj = row[name];
            if (obj == null || obj == DBNull.Value)
                return GetDefaultValue<T1>();
            var str = obj.ToString();
            if (string.IsNullOrEmpty(str) || str.Length == 0)
                return GetDefaultValue<T1>();

            return (T1)Convert.ChangeType(obj, typeof(T1));
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual T1 GetValue<T1>(DataRow row, int index)
        {
            var obj = row[index];
            if (obj == null || obj == DBNull.Value)
                return GetDefaultValue<T1>();
            var str = obj.ToString();
            var type = typeof(T1);
            if (type == typeof(decimal))
            {
                str = str.Replace("¥", "").Replace(",", "");
                obj = Convert.ToDecimal(str.Replace("%", "")) * 0.01M;
            }
            return (T1)Convert.ChangeType(obj, type);
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        private T1 GetDefaultValue<T1>()
        {
            T1 defaultValue= default(T1);
            var type = typeof(T1);
            
            if (type == typeof(string))
                defaultValue = (T1)Convert.ChangeType("", type);
            else if (type == typeof(DateTime))
                defaultValue = (T1)Convert.ChangeType("1900-1-1", type);

            return defaultValue;
        }
    }
}
