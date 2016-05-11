using NHH.Framework.Web;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 员工
    /// </summary>
    [AllowAnonymous]
    public class EmployeeController : NHHController
    {
        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public JsonResult GetEmployeeList(int companyId, int? departmentId)
        {
            var list = GetService<IEmployeeCommonService>().GetEmployeeList(companyId, departmentId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}