using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHH.WebSite.Management.Models
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}