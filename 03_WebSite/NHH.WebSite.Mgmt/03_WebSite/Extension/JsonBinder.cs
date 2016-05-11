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
    public class JsonBinder<T>: IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //从请求中获取提交的参数数据 
            var json = controllerContext.HttpContext.Request.Form[bindingContext.ModelName] as string;
            //提交参数是对象 
            if (json.StartsWith("{") && json.EndsWith("}"))
            {
                JObject jsonBody = JObject.Parse(json);
                JsonSerializer js = new JsonSerializer();
                object obj = js.Deserialize(jsonBody.CreateReader(), typeof(T));
                return obj;
            }
            //提交参数是数组 
            if (json.StartsWith("[") && json.EndsWith("]"))
            {
                IList<T> list = new List<T>();
                JArray jsonRsp = JArray.Parse(json);
                if (jsonRsp != null)
                {
                    for (int i = 0; i < jsonRsp.Count; i++)
                    {
                        JsonSerializer js = new JsonSerializer();
                        try
                        {
                            object obj = js.Deserialize(jsonRsp[i].CreateReader(), typeof(T));
                            list.Add((T)obj);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
                return list;
            }
            return null;
        }

    }
}