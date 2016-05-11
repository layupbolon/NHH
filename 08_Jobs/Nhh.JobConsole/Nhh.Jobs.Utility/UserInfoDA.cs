using Nhh.Jobs.Model;
using NHH.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Utility
{
    /// <summary>
    /// 用户DA
    /// </summary>
    public class UserInfoDA
    {
        /// <summary>
        /// 获取工程主管系统用户信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="bizConfigType"></param>
        /// <returns></returns>
        public static UserInfo GetGovernorUserInfo(int projectId, int bizConfigType)
        {
            var userInfo = new UserInfo();

            var strCmd = string.Format(@"SELECT TOP 1 
                                         su.UserID AS GovernorUserId
			                             FROM dbo.Project_BizConfig  pb with (Nolock)
			                             INNER JOIN dbo.Sys_User su on pb.Param3=CONVERT(varchar(500),su.EmployeeID) 
				                         WHERE pb.ProjectID={0} AND pb.BizConfigType={1}", projectId, bizConfigType);

            var table = SqlHelper.ExecuteDataTable(strCmd);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return userInfo;

            //负责人
            userInfo.GovernorUserId = ConvertHelper.ToInt(table.Rows[0]["GovernorUserId"]);

            return userInfo;
        }

        /// <summary>
        /// 获取工程经理或者项目经理系统用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetManagerUserId(int userId)
        {
            if (userId <= 0)
                return 0;

            var strCmd = string.Format(@"SELECT TOP 1
                                         ssu.UserID AS ManagerUserId
                                         FROM  [dbo].[Sys_User](Nolock) su
                                         INNER JOIN [dbo].[Employee](Nolock) em on su.EmployeeID=em.EmployeeID
                                         INNER JOIN [dbo].[Sys_User](Nolock) ssu on em.SupervisorID=ssu.EmployeeID
                                         WHERE su.UserID={0}", userId);
            var table = SqlHelper.ExecuteDataTable(strCmd);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return 0;

            return ConvertHelper.ToInt(table.Rows[0]["ManagerUserId"]);
        }
    }
}
