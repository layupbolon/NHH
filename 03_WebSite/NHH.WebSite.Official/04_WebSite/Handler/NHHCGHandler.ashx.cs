using System.Web;
using NHH.Service.Official.Handler;

namespace NHH.WebSite.Official.Handler
{
    /// <summary>
    /// NHHCGHandler 的摘要说明
    /// </summary>
    public class NHHCGHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            Models.Official.Handler action = null;
            switch (context.Request["action"])
            {
                case "getProject":
                    action = new GetProjectHandler(context);
                    break;
                case "getBizType":
                    action = new GetBizTypeHandler(context);
                    break;
                case "getFloor":
                    action = new GetFloorHandler(context);
                    break;
                case "getArea":
                    action = new GetAreaHandler(context);
                    break;
                case "getUnitList":
                    action = new GetUnitListHandler(context);
                    break;
                case "getRegion":
                    action = new GetRegionHandler(context);
                    break;
                case "getBusinessScope":
                    action = new GetBusinessScopeHandler(context);
                    break;
                case "getKioskList":
                    action = new GetKioskListHandler(context);
                    break;
                case "getADType":
                    action = new GetADTypeHandler(context);
                    break;
                case "getElectricityType":
                    action = new GetElectricityTypeHandler(context);
                    break;
                case "getADPositionList":
                    action = new GetADPositionListHandler(context);
                    break;
                case "getBuilding":
                    action = new GetBuildingHandler(context);
                    break;
            }

            if (action != null)
                action.Process();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}