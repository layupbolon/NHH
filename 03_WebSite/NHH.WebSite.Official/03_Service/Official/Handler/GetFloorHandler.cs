using System.Web;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.Service.Official.Handler
{
    public class GetFloorHandler : Models.Official.Handler
    {
        public GetFloorHandler(HttpContext context)
            : base(context)
        {
        }

        public override void Process()
        {
            int NHHCGType = 0;
            if (Request.QueryString["type"] == null || !int.TryParse(Request.QueryString["type"], out NHHCGType))
            {
                Response.Write("error");
                Response.End();
            }

            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();
            var list = service.GetNHHCGFloorDropDownList((NHHCGTypeEnum)NHHCGType);

            Response.ContentType = "text/plain";
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }
    }
}
