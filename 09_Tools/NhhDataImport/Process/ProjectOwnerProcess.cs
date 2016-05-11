using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 项目业主处理
    /// </summary>
    public class ProjectOwnerProcess
    {
        /// <summary>
        /// 保存项目业主公司
        /// </summary>
        /// <param name="project"></param>
        public static void SaveProjectOwner(ProjectEntity project)
        {
            string strCmd = string.Format(@"UPDATE [dbo].[Project_Owner]
                                               SET [Status] = -1
                                             WHERE [ProjectID]={0}", project.ProjectID);

            SqlHelper.ExecuteNonQuery(strCmd);
            
            strCmd = @"INSERT INTO [dbo].[Project_Owner]
                                ([ProjectID]
                                ,[CompanyID]
                                ,[CompanyName]
                                ,[Status]
                                ,[InDate]
                                ,[InUser]
                                ,[EditDate]
                                ,[EditUser])
                            VALUES
                                (@ProjectID
                                ,@CompanyID
                                ,@CompanyName
                                ,@Status
                                ,@InDate
                                ,@InUser
                                ,@EditDate
                                ,@EditUser)";

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@ProjectID", project.ProjectID),
                new SqlParameter("@CompanyID", project.OwnerCompanyID),
                new SqlParameter("@CompanyName", project.OwnerCompany),
                new SqlParameter("@Status", 1),
                new SqlParameter("@InDate", DateTime.Now),
                new SqlParameter("@InUser", 0),
                new SqlParameter("@EditDate", DateTime.Now),
                new SqlParameter("@EditUser", 0),
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
