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
    /// 商户消息
    /// </summary>
    public class MerchantMessageDA
    {
        /// <summary>
        /// 插入商户消息
        /// </summary>
        /// <param name="message"></param>
        public static void Add(MerchantMessageInfo message)
        {
            var strCmd = string.Format(@"INSERT INTO [dbo].[Merchant_Message]
                                        ([MerchantID]
                                        ,[StoreID]
                                        ,[UserID]
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
                                        (@MerchantID
                                        ,@StoreID
                                        ,@UserID
                                        ,@Subject
                                        ,@Content
                                        ,@SourceType
                                        ,@SourceRefID
                                        ,@Status
                                        ,@InDate
                                        ,@InUser
                                        ,@EditDate
                                        ,@EditUser)");

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@MerchantID",message.MerchantId),  
                new SqlParameter("@StoreID",message.StoreId),  
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

            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
