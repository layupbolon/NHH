using NHH.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract.IService
{
    /// <summary>
    /// 意向信息服务接口
    /// </summary>
    public interface IRequestInfoService
    {
        /// <summary>
        /// 获取意向列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        RequestListModel GetRequestList(RequestListQueryInfo queryInfo);
    }
}
