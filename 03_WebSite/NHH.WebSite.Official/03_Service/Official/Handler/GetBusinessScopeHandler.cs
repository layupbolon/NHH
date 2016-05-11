using System.Web;
using NHH.Framework.Service;
using NHH.Service.Official.IService;

namespace NHH.Service.Official.Handler
{
    public  class GetBusinessScopeHandler:Models.Official.Handler
    {
        public GetBusinessScopeHandler(HttpContext context)
            : base(context)
        {
        }

        public override void Process()
        {
            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();
            var list = service.GetNHHCGBusinessScopeDropDownList();
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }
    }
}
