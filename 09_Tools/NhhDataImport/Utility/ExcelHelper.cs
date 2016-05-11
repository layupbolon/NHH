using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport
{
    public class ExcelHelper
    {
        /// <summary>
        /// 获得Excel中的所有sheetname
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ExcelSheetName(string fileName)
        {
            var list = new List<string>();
            string connStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0", fileName);
            OleDbConnection conn = new OleDbConnection(connStr);
            conn.Open();
            DataTable sheetNames = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            conn.Close();
            foreach (DataRow dr in sheetNames.Rows)
            {
                list.Add(dr[2].ToString());
            }
            return list;
        }

        /// <summary>
        /// 读取Excel数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="tableName"></param>
        /// <param name="HeaderRowIndex"></param>
        /// <returns></returns>
        public static DataTable ReadFileData(string fileName, string tableName, int HeaderRowIndex)
        {
            string connStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0", fileName);
            var conn = new System.Data.OleDb.OleDbConnection(connStr);
            var cmd = new System.Data.OleDb.OleDbCommand(string.Format("Select * From [{0}$]", tableName), conn);
            var adapter = new System.Data.OleDb.OleDbDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
