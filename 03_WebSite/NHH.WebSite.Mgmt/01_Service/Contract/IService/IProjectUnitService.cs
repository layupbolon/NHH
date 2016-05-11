using NHH.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract.IService
{
    /// <summary>
    /// 商铺服务接口
    /// </summary>
    public interface IProjectUnitService
    {
        /// <summary>
        /// 获取商铺列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectUnitListModel GetProjectUnitList(ProjectUnitQueryInfo queryInfo);
    }
}
