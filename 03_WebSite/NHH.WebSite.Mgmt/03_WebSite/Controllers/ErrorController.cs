using NHH.Framework.Web;
using NHH.WebSite.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Controllers
{
    [AllowAnonymous]
    public class ErrorController : NHHController
    {
        
        public ActionResult Detail(int id)
        {
            var error = new ErrorModel();
            switch (id)
            {
                case 400:
                    {
                        error.Code = 400;
                        error.Message = "Bad request";
                        break;
                    }
                case 401:
                    {
                        error.Code = 401;
                        error.Message = "Unauthorized";
                        break;
                    }
                case 403:
                    {
                        error.Code = 403;
                        error.Message = "Forbidden";
                        break;
                    }
                case 404:
                    {
                        error.Code = 404;
                        error.Message = "Not Found";
                        break;
                    }
                case 500:
                    {
                        error.Code = 500;
                        error.Message = "Internal Server Error";
                        var data = this.ViewData.Model as HandleErrorInfo;
                        error.Detail = data == null ? "" : data.Exception.Message;
                        break;
                    }
                default:
                    {
                        error.Code = 500;
                        error.Message = "General Error";

                        var data = this.ViewData["Error"] as HandleErrorInfo;
                        error.Detail = data == null ? "" : data.Exception.Message;
                        break;
                    }
            }
            return View(error);
        }
    }
}