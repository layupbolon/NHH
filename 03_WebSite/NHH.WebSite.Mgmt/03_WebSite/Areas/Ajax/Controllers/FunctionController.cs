using NHH.Framework.Web;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    [AllowAnonymous]
    public class FunctionController : NHHController
    {
        // GET: Ajax/Function
        public ActionResult Index()
        {
            return View();
        }

        #region Service
        private ISysFunctionService m_Service;
        protected ISysFunctionService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<ISysFunctionService>();
                }

                return m_Service;
            }
        }
        #endregion

        /// <summary>
        /// 获取系统功能模块
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetFunctionByKeyword(string keyword)
        {
            var functionList =this.Service.GetFunctionList(keyword);

            return Json(functionList, JsonRequestBehavior.AllowGet);
        }
    }
}