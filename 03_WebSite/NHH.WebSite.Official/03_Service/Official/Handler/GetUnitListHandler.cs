using System.Collections.Generic;
using System.Web;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.Service.Official.Handler
{
    public class GetUnitListHandler : Models.Official.Handler
    {
        public GetUnitListHandler(HttpContext context)
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

            var queryInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UnitQueryInfo>(queryStr);

            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();

            List<UnitInfo> list;
            int unitID;
            if (Request["uid"] != null && int.TryParse(Request["uid"], out unitID))
                list = service.GetUnitInfos(queryInfo, unitID);
            else
                list = service.GetUnitInfos(queryInfo);

            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }
    }
}
