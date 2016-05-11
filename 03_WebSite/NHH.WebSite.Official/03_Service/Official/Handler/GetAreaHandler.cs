using System.Web;
using NHH.Framework.Service;
using NHH.Service.Official.IService;

namespace NHH.Service.Official.Handler
{
    public class GetAreaHandler : Models.Official.Handler
    {
        public GetAreaHandler(HttpContext context)
            : base(context)
        {
        }

        public override void Process()
        {
            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();
            var list = service.GetNHHCGAreaDropDownList();
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }
    }
}
