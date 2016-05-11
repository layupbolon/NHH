using NHH.Framework.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NHH.Framework.Logging
{
    /// <summary>
    /// 操作日志记录器
    /// </summary>
    public class NHHOptLogger : IOptLogger
    {
        public NHHOptLogger()
        {
            this.Category = "General";

        }

        public NHHOptLogger(string category)
        {
            this.Category = category;
        }

        /// <summary>
        /// 日志分类
        /// </summary>
        public string Category { get; set; }


        #region Context
        private OptLoggingContext m_Context;
        /// <summary>
        /// 日志上下文对象
        /// </summary>
        protected OptLoggingContext Context
        {
            get
            {
                if (m_Context == null)
                {
                    //日志数据库链接
                    var dbname = ParamManager.GetStringValue("logging:database");
                    m_Context = string.IsNullOrEmpty(dbname) ? new OptLoggingContext() : new OptLoggingContext(dbname);
                }
                return m_Context;
            }
        }
        #endregion

        /// <summary>
        /// 记录业务操作日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="detail"></param>
        /// <param name="user"></param>
        public void Log(object sender, string entity, string key, string action, string detail,string user,DateTime? time=null)
        {
            var log = new OptEventLog();
            log.Category = this.Category;
            log.Sender = sender.GetType().FullName;
            log.EntityName = entity;
            log.EntityID = key;
            log.Action = action;
            log.Detail = detail;
            log.EventUser = user;
            log.EventTime = time??DateTime.Now;
            if (HttpContext.Current != null)
            {
                log.HostName = HttpContext.Current.Server.MachineName;
                log.Url = HttpContext.Current.Request.Url.ToString();
                log.ClientIP = HttpContext.Current.Request.UserHostAddress;
            }
            this.Context.BizEventLogs.Add(log);
            this.Context.SaveChanges();
        }

    }

    #region OptLoggingContext
    /// <summary>
    /// 业务日志记录数据库上下文
    /// </summary>
    public class OptLoggingContext : DbContext
    {
        public OptLoggingContext()
            : base()
        {

        }
        public OptLoggingContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public DbSet<OptEventLog> BizEventLogs { get; set; }
    }
    #endregion

    #region OptEventLogs
    /// <summary>
    /// 业务日志记录实体对象
    /// </summary>
    public class OptEventLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OptEventLogID { get; set; }
        [StringLength(200)]
        public string Category { get; set; }
        [StringLength(200)]
        public string Sender { get; set; }
        [StringLength(200)]
        public string EntityName { get; set; }
        [StringLength(200)]
        public string EntityID { get; set; }
        [StringLength(200)]
        public string Action { get; set; }
        public string Detail { get; set; }
        [StringLength(200)]
        public string HostName { get; set; }
        [StringLength(500)]
        public string Url { get; set; }
        [StringLength(200)]
        public string ClientIP { get; set; }

        public DateTime EventTime { get; set; }
        [StringLength(200)]
        public string EventUser { get; set; }
    }
    #endregion
}
