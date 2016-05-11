using System.Collections.Generic;
using System.Web;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.Service.Official.Handler
{
    public class GetADPositionListHandler:Models.Official.Handler
    {
        public GetADPositionListHandler(HttpContext context) : base(context)
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

            var queryInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ADPositionQueryInfo>(queryStr);

            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();

            List<ADPositionInfo> list;
            int ADPositionID;
            if (Request["adid"] != null && int.TryParse(Request["adid"], out ADPositionID))
                list = service.GetADPositionInfos(queryInfo, ADPositionID);
            else
                list = service.GetADPositionInfos(queryInfo);

            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }
    }
}
