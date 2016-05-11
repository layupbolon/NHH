using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Contract;
using NHH.Service.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract
{
    /// <summary>
    /// 意向信息服务
    /// </summary>
    public class RequestInfoService : NHHService<NHHEntities>, IRequestInfoService
    {

        /// <summary>
        /// 获取意向列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public RequestListModel GetRequestList(RequestListQueryInfo queryInfo)
        {
            var model = new RequestListModel();
            model.PagingInfo = new Models.Common.PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;

            model.RequestList = new List<RequestInfo>();

            var query = from mr in Context.Merchant_Request
                        select new 
                        {
                            mr.RequestID,
                            mr.ProjectID,
                            mr.RentLength,
                            mr.RentMethod,
                            mr.SpecialRequest,
                            mr.AreaRequest,

                        };

            return model;
        }
    }
}
