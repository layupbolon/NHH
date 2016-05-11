using NHH.Framework.Configuration;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Logging;
using NHH.Service.Logging.IService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Logging
{
    public class LoggingService : NHHService<NHH.Entities.NHHEntities>, ILoggingService
    {

        #region ActionNames
        private Dictionary<string, string> m_ActionNames;
        /// <summary>
        /// 操作方式
        /// </summary>
        protected Dictionary<string, string> ActionNames
        {
            get
            {
                if (m_ActionNames == null)
                {
                    m_ActionNames = new Dictionary<string, string>();
                    m_ActionNames.Add("Added", "新增");
                    m_ActionNames.Add("Modified", "修改");
                    m_ActionNames.Add("Deleted", "删除");
                }
                return m_ActionNames;
            }
        }
        #endregion 

        public Dictionary<string, string> GetActionNames()
        {
            return this.ActionNames;
        }

        #region EntityNames
        private Dictionary<string, string> m_EntityNames;
        /// <summary>
        /// 实体名称
        /// </summary>
        protected Dictionary<string, string> EntityNames
        {
            get
            {
                if (m_EntityNames == null)
                {
                    var config = ConfigManager.GetConfig<OptLogConfig>();
                    m_EntityNames = new Dictionary<string, string>();
                    foreach (var entity in config.Entities)
                    {
                        m_EntityNames.Add(entity.Name, entity.Alias);
                    }
                }
                return m_EntityNames;
            }
        }
        #endregion

        public Dictionary<string, string>  GetEntityNames()
        {
            return this.EntityNames;
        }

        private const string c_QuerySQL = @"SELECT @TotalCount=COUNT(1) FROM 
(SELECT l.[OptEventLogID]
FROM [NHH_LOG].[dbo].[OptEventLogs] l 
LEFT JOIN [NHH].[dbo].[Sys_User] u ON l.EventUser=u.UserID 
LEFT JOIN [NHH].[dbo].[Employee] e ON e.EmployeeID = u.EmployeeID
{filter}
) T

;WITH T AS(
SELECT ROW_NUMBER() Over(ORDER BY l.[OptEventLogID] DESC) AS [RowNumber],
	   l.[OptEventLogID]
      ,l.[EntityName]
      ,l.[EntityID]
      ,l.[Action]
      ,l.[Detail]
      ,l.[ClientIP]
      ,EventUser=ISNULL(u.UserName,l.[EventUser])+'/'+ISNULL(e.EmployeeName,'')
	  ,l.[EventTime]
FROM [NHH_LOG].[dbo].[OptEventLogs] l 
LEFT JOIN [NHH].[dbo].[Sys_User] u ON l.EventUser=u.UserID 
LEFT JOIN [NHH].[dbo].[Employee] e ON e.EmployeeID = u.EmployeeID
{filter}
)

SELECT * FROM T
WHERE t.RowNumber BETWEEN (@PageIndex-1)*@PageSize AND (@PageSize*@PageIndex)";

        public OptLogListModel QueryOptLogs(OptLogQueryInfo queryInfo)
        {
            var model = new OptLogListModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };
            
            
            var prams = new List<SqlParameter>();
            prams.Add(new SqlParameter("@TotalCount", model.PagingInfo.TotalCount) { Direction = System.Data.ParameterDirection.Output });
            prams.Add(new SqlParameter("@PageIndex", model.PagingInfo.PageIndex));
            prams.Add(new SqlParameter("@PageSize", model.PagingInfo.PageSize));
           

            var filter = " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(queryInfo.EntityName))
            {
                filter += " AND EntityName=@EntityName ";
                prams.Add(new SqlParameter("@EntityName", queryInfo.EntityName));
            }

            if (!string.IsNullOrEmpty(queryInfo.EntityID))
            {
                filter += " AND EntityID=@EntityID ";
                prams.Add(new SqlParameter("@EntityID", queryInfo.EntityID));
            }

            if (!string.IsNullOrEmpty(queryInfo.ActionType))
            {
                filter += " AND Action=@Action ";
                prams.Add(new SqlParameter("@Action", queryInfo.ActionType));
            }

            if (!string.IsNullOrEmpty(queryInfo.UserName))
            {
                filter += " AND (EmployeeName LIKE @UserName+'%' OR UserName LIKE @UserName+'%') ";
                prams.Add(new SqlParameter("@UserName", queryInfo.UserName));
            }

            if (!string.IsNullOrEmpty(queryInfo.Company))
            {
                filter += " AND CompanyID = @CompanyID ";
                prams.Add(new SqlParameter("@CompanyID", queryInfo.Company));
            }

            if (queryInfo.StartTime.HasValue)
            {
                filter += " AND EventTime >= @StartTime ";
                prams.Add(new SqlParameter("@StartTime", queryInfo.StartTime));
            }

            if (queryInfo.EndTime.HasValue)
            {
                filter += " AND EventTime <= @EndTime ";
                prams.Add(new SqlParameter("@EndTime", queryInfo.EndTime));
            }

            var sql = c_QuerySQL.Replace("{filter}", filter);
            model.OptLogList = this.ExecSQL<OptLogModel>(sql,prams.ToArray());
            model.PagingInfo.TotalCount =(int)prams[0].Value;
            if (model.OptLogList != null)
            {
                model.OptLogList.ForEach((item) =>
                {
                    item.EntityAlias = this.EntityNames.ContainsKey(item.EntityName) ? this.EntityNames[item.EntityName] : item.EntityName;
                    item.ActionAlias = this.ActionNames.ContainsKey(item.Action) ? this.ActionNames[item.Action] : item.Action;
                });
            }
            return model;
        }
    }
}
