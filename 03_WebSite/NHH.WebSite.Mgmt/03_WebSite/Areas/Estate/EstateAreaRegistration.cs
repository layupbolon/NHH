﻿using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Estate
{
    public class EstateAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Estate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Estate_default",
                "Estate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Estate.Controllers" }
            );
        }
    }
}