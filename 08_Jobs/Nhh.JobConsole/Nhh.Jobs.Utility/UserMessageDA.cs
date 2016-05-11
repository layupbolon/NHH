using Nhh.Jobs.Model;
using NHH.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Utility
{
    /// <summary>
    /// 用户消息
    /// </summary>
    public class UserMessageDA
    {
        /// <summary>
        /// 插入消息到UserMessage表
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int Add(UserMessageInfo message)
        {
            var strCmd = @"INSERT INTO [dbo].[Sys_User_Message]
                            ([UserID]
                            ,[Subject]
                            ,[Content]
                            ,[SourceType]
                            ,[SourceRefID]
                            ,[Status]
                            ,[InDate]
                            ,[InUser]
                            ,[EditDate]
                            ,[EditUser])
                          VALUES
                            (@UserID
                            ,@Subject
                            ,@Content
                            ,@SourceType
                            ,@SourceRefID
                            ,@Status
                            ,@InDate
                            ,@InUser
                            ,@EditDate
                            ,@EditUser)";

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@UserID",message.UserId),  
                new SqlParameter("@Subject",message.Subject), 
                new SqlParameter("@Content",message.Content),
                new SqlParameter("@SourceType",message.SourceType),
                new SqlParameter("@SourceRefID",message.SourceRefId),
                new SqlParameter("@Status",message.Status),
                new SqlParameter("@InDate",message.InDate),
                new SqlParameter("@InUser",message.InUser),
                new SqlParameter("@EditDate",message.EditDate),
                new SqlParameter("@EditUser",message.EditUser),
            };

            return SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }

        /// <summary>
        /// 判断今天是否已发过消息ID
        /// SourceType:
        /// 1:通知
        /// 2:租约
        /// 3:账单
        /// 4:催款
        /// 5:活动
        /// 6:报修
        /// 7:投诉
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="sourceRefID"></param>
        /// <returns></returns>
        public static int GetMessageId(int sourceType, int sourceRefID)
        {
            var strCmd = string.Format(@"SELECT TOP 1 MessageID
                                        FROM [dbo].[Sys_User_Message]
                                        Where SourceType={0} And SourceRefID={1} And DATEDIFF(DAY, InDate, GETDATE())=0", sourceType, sourceRefID);
            var obj = SqlHelper.ExecuteScalar(strCmd);
            return ConvertHelper.ToInt(obj);
        }

        /// <summary>
        /// 判断某一个时间段内是否已发送过消息Id
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="sourceRefID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetMessageId(int sourceType, int sourceRefID, DateTime startTime, DateTime endTime)
        {
            var strCmd = string.Format(@"SELECT TOP 1 MessageID
                                        FROM [dbo].[Sys_User_Message]
                                        Where SourceType={0} And SourceRefID={1} And InDate between '{2}' and '{3}'", sourceType, sourceRefID, startTime, endTime);
            var obj = SqlHelper.ExecuteScalar(strCmd);
            return ConvertHelper.ToInt(obj);
        }
    }
}
