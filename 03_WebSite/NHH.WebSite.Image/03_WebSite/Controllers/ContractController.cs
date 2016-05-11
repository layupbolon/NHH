using NHH.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Image.Controllers
{
    /// <summary>
    /// 合同附件上传
    /// </summary>
    [AllowAnonymous]
    public class ContractController : NHHController
    {
        /// <summary>
        /// 合同附件上传
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(int contractId)
        {
            return View();
        }
    }
}