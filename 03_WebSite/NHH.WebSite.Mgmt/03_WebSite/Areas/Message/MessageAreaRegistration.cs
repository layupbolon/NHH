﻿using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Message
{
    public class MessageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Message";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Message_default",
                "Message/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Message.Controllers" }
            );
        }
    }
}