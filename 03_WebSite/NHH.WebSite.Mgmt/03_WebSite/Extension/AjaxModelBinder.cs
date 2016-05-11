using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NHH.WebSite.Management.Extension
{
    public class AjaxModelBinder:DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType.IsEnum)
            {
                var key = bindingContext.ModelName + "[]";
                var valueResult = bindingContext.ValueProvider.GetValue(key);
                if (valueResult != null && !string.IsNullOrEmpty(valueResult.AttemptedValue))
                {
                    bindingContext.ModelName = key;
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}