using NHH.Framework.Web;
using NHH.Models.Plan;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Pdf.Controllers
{
    /// <summary>
    /// 铺位租决
    /// </summary>
    [AllowAnonymous]
    public class ProjectUnitRequestController : NHHController
    {
        /// <summary>
        /// 招商进度
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Schedule(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitRequestService>().Schedule(queryInfo);
            return View(model);
        }

        /// <summary>
        /// 招商跟踪
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Track(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitRequestService>().Track(queryInfo);
            return View(model);
        }
    }
}