using NHH.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Common
{
    /// <summary>
    /// 用户权限验证
    /// </summary>
    public class NHHWebAuthorizeAttribute: NHHAuthorizeAttribute
    {

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var rlt = base.AuthorizeCore(httpContext);

            if (rlt)
            {
                var nhhUser = httpContext.User as NHHPrincipal;
                //不具有指定功能权限
                if (nhhUser==null || !nhhUser.IsInPermission(this.FunctionPoint))
                {
                    httpContext.Response.StatusCode = 401;
                    rlt = false;
                }
            }

            return rlt;
            
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                filterContext.Result = new RedirectResult("/error/detail/401");
            }
        }
    }
}
