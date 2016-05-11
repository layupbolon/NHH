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
    /// 消息
    /// </summary>
    public class MessageDA
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="message"></param>
        public static void Add(MessageInfo message)
        {
            string strCmd = @"INSERT INTO [dbo].[Message]
                                   ([MessageType]
                                   ,[Priority]
                                   ,[Recipient]
                                   ,[Subject]
                                   ,[Content]
                                   ,[Link]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser])
                             VALUES
                                   (@MessageType
                                   ,@Priority
                                   ,@Recipient
                                   ,@Subject
                                   ,@Content
                                   ,@Link
                                   ,@Status
                                   ,@InDate
                                   ,@InUser)";
            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@MessageType",message.MessageType),  
                new SqlParameter("@Priority",message.Priority), 
                new SqlParameter("@Recipient",message.Recipient),
                new SqlParameter("@Subject",message.Subject),
                new SqlParameter("@Content",message.Content),
                new SqlParameter("@Status",message.Status),
                new SqlParameter("@Link",message.Link),
                new SqlParameter("@InDate",message.InDate),
                new SqlParameter("@InUser",message.InUser),
            };

            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
