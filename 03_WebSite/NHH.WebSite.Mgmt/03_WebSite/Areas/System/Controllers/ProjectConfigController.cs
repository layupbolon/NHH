using NHH.Framework.Web;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.System.Controllers
{
    /// <summary>
    /// 项目配置
    /// </summary>
    public class ProjectConfigController : NHHController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ProjectBizConfigListQueryInfo queryInfo)
        {
            var model = GetService<IProjectBizConfigService>().GetBizConfigList(queryInfo);
            model.CrumbInfo.AddCrumb("项目业务配置列表");

            return View(model);
        }
    }
}