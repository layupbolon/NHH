using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 商铺公共服务接口
    /// </summary>
    public interface IProjectUnitCommonService
    {
        /// <summary>
        /// 获取商铺公共基础信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectUnitCommonInfo GetProjectUnitCommonInfo(int unitId);
    }
}
