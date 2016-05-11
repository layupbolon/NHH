using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 报表配置
    /// </summary>
    [AllowAnonymous]
    public class ReportConfigController : NHHController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportCode"></param>
        /// <returns></returns>
        public JsonResult GetFieldList(string reportCode)
        {
            int userId = NHHWebContext.Current.UserID;
            var model = GetService<IReportConfigCommonService>().GetConfigInfo(userId, reportCode);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="reportCode"></param>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        public JsonResult Save(string reportCode, string fieldList)
        {
            int userId = NHHWebContext.Current.UserID;
            var messageBag = GetService<IReportConfigService>().Save(userId, reportCode, fieldList);
            var result = new AjaxResult
            {
                Code = messageBag.Code,
                Message = messageBag.Desc,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}