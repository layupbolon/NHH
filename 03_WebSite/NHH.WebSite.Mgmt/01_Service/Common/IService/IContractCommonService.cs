using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 合同公共服务接口
    /// </summary>
    public interface IContractCommonService
    {
        /// <summary>
        /// 获取铺位列表
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        List<ProjectUnitCommonInfo> GetProjectUnitList(int contractId);
    }
}
