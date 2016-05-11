using NHH.Framework.Web;
using NHH.Models.Contract;
using NHH.Service.Common.IService;
using NHH.Service.Contract.IService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Contract.Controllers
{
    /// <summary>
    /// 意向管理
    /// </summary>
    public class RequestInfoController : NHHController
    {
        private IRequestInfoService service;
        public RequestInfoController()
        {
            service = GetService<IRequestInfoService>();
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(RequestListQueryInfo queryInfo)
        {
            var model = service.GetRequestList(queryInfo);

            model.CrumbInfo.AddCrumb("意向管理");

            return View(model);
        }
    }
}