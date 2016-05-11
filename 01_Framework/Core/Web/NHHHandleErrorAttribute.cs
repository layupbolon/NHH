using NHH.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NHH.Framework.Web
{
    public class NHHHandleErrorAttribute : HandleErrorAttribute
    {

        public override void OnException(ExceptionContext filterContext)
        {
            //记录日志信息
            var area = filterContext.GetAreaName();
            var loger = LoggerManager.GetAPPLogger(string.IsNullOrEmpty(area)? filterContext.GetControllerName():string.Format("{0}Area",area));
            loger.Error(filterContext.Controller,filterContext.Exception);

            base.OnException(filterContext);
        }
    }
}
