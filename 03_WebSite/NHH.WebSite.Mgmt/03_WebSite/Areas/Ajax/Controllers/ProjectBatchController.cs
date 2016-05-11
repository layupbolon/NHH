using NHH.Framework.Web;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 项目招商批次
    /// </summary>
    [AllowAnonymous]
    public class ProjectBatchController : NHHController
    {

        /// <summary>
        /// 获取项目招商批次
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public JsonResult GetBatchList(int projectId)
        {
            var list = GetService<IProjectBatchService>().GetBatchList(projectId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}