using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NHH.Framework.Logging;

namespace NHH.WebAPI.Merchant.Common
{
    public class NHHActionLogAttribute : ActionFilterAttribute, IExceptionFilter
    {
        APILogingContext log = new APILogingContext();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            LoggerManager.GetAPILogger().OperatorLog(
                filterContext.RequestContext.HttpContext.Request.ServerVariables.Get("URL"), 
                filterContext.RequestContext.HttpContext.Request.ServerVariables.Get("HTTP_REFERER"), 
                filterContext.RequestContext.HttpContext.Request.ServerVariables.Get("REMOTE_ADDR"), 
                filterContext.RequestContext.HttpContext.Request.ServerVariables.Get("HTTP_USER_AGENT"),
                serializer.Serialize(ToDictionary(filterContext.HttpContext.Request.Form)),
                serializer.Serialize(((JsonResult)(filterContext.Result)).Data)
                );

        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public void OnException(ExceptionContext filterContext)
        {
            LoggerManager.GetAPILogger()
                .ExceptionLog(
                    filterContext.RequestContext.HttpContext.Request.ServerVariables.Get("URL"),
                    serializer.Serialize(ToDictionary(filterContext.HttpContext.Request.Form)), 
                    filterContext.Exception.Message, 
                    filterContext.Exception.StackTrace
                    );
        }

        private IDictionary<string, string[]> ToDictionary(NameValueCollection source)
        {
            return source.AllKeys.ToDictionary(k => k, k => source.GetValues(k));
        }
    }
}
