using NHH.Models.Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace NHH.WebSite.Management.Common
{
    /// <summary>
    /// Excel助手
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="exportInfo"></param>
        public static void Export<T>(ReportExportInfo<T> exportInfo)
        {
            exportInfo.ReportName = string.Format("{0}_{1}.xls", exportInfo.ReportName, DateTime.Now.ToString("yyyyMMdd"));

            var book = new HSSFWorkbook();
            var sheet1 = book.CreateSheet(DateTime.Now.ToString("yyyy-MM-dd"));

            int column = 0;
            //页头
            IRow row1 = sheet1.CreateRow(0);
            exportInfo.FieldList.ForEach(field =>
            {
                row1.CreateCell(column).SetCellValue(field.FieldTitle);
                column += 1;
            });

            Type type = typeof(T);

            //主体
            int rowNum = 0;

            exportInfo.Body.ForEach(t =>
            {
                column = 0;
                rowNum += 1;

                IRow row = sheet1.CreateRow(rowNum);
                exportInfo.FieldList.ForEach(field =>
                {
                    var property = type.GetProperty(field.FieldName);
                    if (property != null)
                    {
                        object obj = property.GetValue(t, null);
                        if (obj == null)
                        {
                            row.CreateCell(column).SetCellValue("");
                        }
                        else
                        {
                            Type objType = obj.GetType();
                            if (typeof(string) == objType)
                            {
                                row.CreateCell(column).SetCellValue(obj.ToString());
                            }
                            else if (typeof(int) == objType)
                            {
                                row.CreateCell(column).SetCellValue((int)obj);
                            }
                            else if (typeof(decimal) == objType)
                            {
                                row.CreateCell(column).SetCellValue((double)obj);
                            }
                            else if (typeof(DateTime) == objType)
                            {
                                row.CreateCell(column).SetCellValue(((DateTime)obj).ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                row.CreateCell(column).SetCellValue(obj.ToString());
                            }
                        }
                    }
                    else
                    {
                        row.CreateCell(column).SetCellValue("");
                    }
                    column += 1;
                });

            });
            // 输出Excel
            HttpContext.Current.Response.Clear();
            using (MemoryStream file = new MemoryStream())
            {
                book.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
            }
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(exportInfo.ReportName));
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}