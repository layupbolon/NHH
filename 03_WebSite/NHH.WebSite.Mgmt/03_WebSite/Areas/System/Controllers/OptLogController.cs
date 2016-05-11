using NHH.Framework.Web;
using NHH.Models.Logging;
using NHH.Service.Common.IService;
using NHH.Service.Logging.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.System.Controllers
{
    public class OptLogController : NHHController
    {
        #region Service
        private ILoggingService m_LoggingService;
        protected ILoggingService LoggingService
        {
            get
            {
                if (m_LoggingService == null)
                {
                    m_LoggingService = this.GetService<ILoggingService>();
                }

                return m_LoggingService;
            }
        }
        #endregion

        #region CompanyService
        private ICompanyCommonService m_CompanyService;
        public ICompanyCommonService CompanyService
        {
            get
            {
                if (m_CompanyService == null)
                {
                    m_CompanyService = this.GetService<ICompanyCommonService>();
                }

                return m_CompanyService;
            }
        } 
        #endregion

        public ActionResult List(OptLogQueryInfo queryInfo)
        {
            var model = this.LoggingService.QueryOptLogs(queryInfo);
            model.CrumbInfo.AddCrumb("业务操作日志");
            var actions = this.LoggingService.GetActionNames();
            var entities = this.LoggingService.GetEntityNames();
            var companies = this.CompanyService.GetCompanyList();
            ViewData["ActionList"] = new SelectList(actions, "Key", "Value");
            ViewData["EntityList"] = new SelectList(entities, "Key", "Value");
            ViewData["CompanyList"] = new SelectList(companies, "Id", "ShortName");
            return View(model);
        }
    }
}