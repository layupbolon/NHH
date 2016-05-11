using System.Web;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.Service.Official.Handler
{
    public class GetBuildingHandler:Models.Official.Handler
    {
        public GetBuildingHandler(HttpContext context) : base(context)
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
            var list = service.GetNGGCGBuildingDropDownList((NHHCGTypeEnum)NHHCGType);

            Response.ContentType = "text/plain";
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }
    }
}
