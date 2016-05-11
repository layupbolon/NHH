using System.Collections.Generic;
using System.Web;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.Service.Official.Handler
{
    public class GetKioskListHandler : Models.Official.Handler
    {
        public GetKioskListHandler(HttpContext context)
            : base(context)
        {
        }

        public override void Process()
        {
            string queryStr = Request["query"];
            if (string.IsNullOrEmpty(queryStr))
            {
                Response.Write("error");
                Response.End();
            }

            var queryInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<KioskQueryInfo>(queryStr);

            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();

            List<KioskInfo> list;
            int kioskID;
            if (Request["kid"] != null && int.TryParse(Request["kid"], out kioskID))
                list = service.GetKioskInfos(queryInfo, kioskID);
            else
                list = service.GetKioskInfos(queryInfo);

            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }
    }
}
