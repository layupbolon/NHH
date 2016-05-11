﻿using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax
{
    public class AjaxAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Ajax";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Ajax_default",
                "Ajax/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Ajax.Controllers" }
            );
        }
    }
}