using System.Web;
using NHH.Framework.Service;
using NHH.Service.Official.IService;

namespace NHH.Service.Official.Handler
{
    public class GetElectricityTypeHandler:Models.Official.Handler
    {
        public GetElectricityTypeHandler(HttpContext context) : base(context)
        {
        }

        public override void Process()
        {
            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();
            var list = service.GetNHHCGElectricityTypeDropDownList();
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }
    }
}
