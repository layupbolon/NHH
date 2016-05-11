using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport
{
    public class SqlHelper
    {
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="strCmd"></param>
        /// <returns></returns>
        public static int ExecuteScalar(string strCmd)
        {
            object obj = null;
            using (var conn = GetConnection())
            {
                var cmd = new SqlCommand(strCmd, conn);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                obj = cmd.ExecuteScalar();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            if (obj == null || obj == DBNull.Value)
                return -1;
            return (int)obj;
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="strCmd"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string strCmd)
        {
            var table = new DataTable();
            using (var conn = GetConnection())
            {
                var cmd = new SqlCommand(strCmd, conn);
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            return table;
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="strCmd"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string strCmd)
        {
            var result = -1;
            using (var conn = GetConnection())
            {
                var cmd = new SqlCommand(strCmd, conn);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 带参执行
        /// </summary>
        /// <param name="strCmd"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string strCmd, params SqlParameter[] paramList)
        {
            var result = -1;
            using (var conn = GetConnection())
            {
                var cmd = new SqlCommand(strCmd, conn);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SetParameterList(cmd, paramList);
                result = cmd.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 带参执行
        /// </summary>
        /// <param name="strCmd"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static int ExecuteScalar(string strCmd, params SqlParameter[] paramList)
        {
            var result = -1;
            using (var conn = GetConnection())
            {
                var cmd = new SqlCommand(strCmd, conn);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SetParameterList(cmd, paramList);
                int.TryParse(cmd.ExecuteScalar().ToString(), out result);
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行SQL
        /// 带事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="strCmd"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction trans, string strCmd, params SqlParameter[] paramList)
        {
            var cmd = new SqlCommand(strCmd, trans.Connection, trans);
            SetParameterList(cmd, paramList);
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行SQL
        /// 带事务
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="strCmd"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static int ExecuteScalar(SqlTransaction trans, string strCmd, params SqlParameter[] paramList)
        {
            var cmd = new SqlCommand(strCmd, trans.Connection, trans);
            SetParameterList(cmd, paramList);
            var id = 0;
            int.TryParse(cmd.ExecuteScalar().ToString(), out id);
            return id;
        }

        /// <summary>
        /// 设置参数列表
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paramList"></param>
        private static void SetParameterList(SqlCommand cmd, params SqlParameter[] paramList)
        {
            if (paramList == null)
                return;
            foreach (var param in paramList)
            {
                if ((param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput) &&
                           param.Value == null)
                {
                    param.Value = DBNull.Value;
                }
                cmd.Parameters.Add(param); 
            }
        }

        /// <summary>
        /// 获取数据链接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["NHHConn"].ToString();
            return new SqlConnection(connStr);
        }
    }
}
