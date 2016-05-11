using NHH.Framework.Web;
using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebAPI.Merchant.Controllers
{
    [AllowAnonymous]
    [Route("error/detail/{id}")]
    public class ErrorController : NHHController
    {

        public ActionResult Detail(int id)
        {
            var error = new ApiResult<Object>();
            switch (id)
            {
                case 400:
                    {
                        error.Code = 400;
                        error.Desc = "Bad request";
                        break;
                    }
                case 401:
                    {
                        error.Code = 401;
                        error.Desc = "Unauthorized";
                        break;
                    }
                case 403:
                    {
                        error.Code = 403;
                        error.Desc = "Forbidden";
                        break;
                    }
                case 404:
                    {
                        error.Code = 404;
                        error.Desc = "Not Found";
                        break;
                    }
                case 500:
                    {
                        error.Code = 500;
                        error.Desc = "Internal Server Error";
                        var data = this.ViewData.Model as HandleErrorInfo;
                        error.Data = data == null ? "" : data.Exception.Message;
                        break;
                    }
                default:
                    {
                        error.Code = 500;
                        error.Desc = "General Error";

                        var data = this.ViewData["Error"] as HandleErrorInfo;
                        error.Data = data == null ? "" : data.Exception.Message;
                        break;
                    }
            }
            return Json(error);
        }
    }
}