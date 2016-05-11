using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 商铺
    /// </summary>
    [AllowAnonymous]
    public class ProjectUnitController : NHHController
    {
        /// <summary>
        /// 初始化铺位
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Init(ProjectUnitInitInfo info)
        {
            var result = GetService<IProjectUnitInitService>().Init(info);
            var ajaxResult = new AjaxResult
            {
                Message = result.Desc,
                Code = result.Code,
            };
            return Json(ajaxResult, JsonRequestBehavior.DenyGet);
        }
    }
}