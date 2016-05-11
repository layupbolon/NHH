using NhhDataImport.Entity;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 实体处理
    /// </summary>
    public class BaseEntityProcess<T> : INhhDataProcess
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
        protected virtual List<T> GetProcessEntity(string fileName)
        {
            if (string.IsNullOrEmpty(SheetName))
            {
                _worker.ReportProgress(10, string.Format("未找到 {0} 相关表", SheetName));
                return null;
            }

            var list = new List<T>();

            IWorkbook workbook;
            fileName = fileName.ToLower();

            using (var stream = System.IO.File.OpenRead(fileName))
            {
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(stream);
                }
                else
                {
                    workbook = new HSSFWorkbook(stream);
                }
                var sheet = workbook.GetSheet(SheetName);

                for (int i = (HeaderRowNum); i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    try
                    {
                        var entity = ToEntity(row);
                        list.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _worker.ReportProgress(10, string.Format("第{0}行数据转换为实体失败,{1}", i, ex.ToString()));
                    }
                }

                stream.Close();
                workbook = null;
                sheet = null;
            }
            return list;
        }

        /// <summary>
        /// 检验数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="worker"></param>
        public virtual void CheckData(string fileName, BackgroundWorker worker)
        {
            _worker = worker;

            var list = GetProcessEntity(fileName);
            if (list == null)
            {
                _worker.ReportProgress(10, string.Format("未找到 {0} 相关数据", SheetName));
                return;
            }

            _worker.ReportProgress(1, string.Format("{0} 共有 {1} 条数据需要检验", SheetName, list.Count));

            int index = 0;
            foreach (T entity in list)
            {
                index += 1;
                _worker.ReportProgress(1, string.Format("开始检验{0}第{1}条数据", SheetName, index));

                try
                {
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
            var list = GetProcessEntity(fileName);
            if (list == null)
            {
                _worker.ReportProgress(10, string.Format("未找到 {0} 相关数据", SheetName));
                return;
            }

            _worker.ReportProgress(1, string.Format("{0} 共有 {1} 条数据需要处理", SheetName, list.Count));

            int index = 0;
            foreach (var entity in list)
            {
                index += 1;
                _worker.ReportProgress(1, string.Format("开始处理{0}第{1}条数据", SheetName, index));

                try
                {
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
        protected virtual T ToEntity(IRow row)
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
        /// <typeparam name="T1"></typeparam>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected T1 GetValue<T1>(IRow row, int index)
        {
            object obj;
            var type = typeof(T1);
            ICell cell = row.GetCell(index);
            try
            {
                if (type == typeof(string))
                    obj = cell.StringCellValue;
                else if (type == typeof(int))
                    obj = cell.NumericCellValue;
                else if (type == typeof(decimal))
                    obj = cell.NumericCellValue;
                else if (type == typeof(DateTime))
                    obj = cell.DateCellValue;
                else if (type == typeof(bool))
                    obj = cell.BooleanCellValue;
                else
                    obj = cell.ToString();
            }
            catch (Exception)
            {
                obj = GetValue(cell);
            }
            if (obj == null)
                return default(T1);
            return (T1)Convert.ChangeType(obj, type);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private object GetValue(ICell cell)
        {
            try { return cell.StringCellValue; }
            catch { }
            try { return cell.NumericCellValue; }
            catch { }
            try { return cell.DateCellValue; }
            catch { }
            try { return cell.BooleanCellValue; }
            catch { }
            try { return cell.RichStringCellValue; }
            catch { }
            return cell.ToString();
        }


    }
}
