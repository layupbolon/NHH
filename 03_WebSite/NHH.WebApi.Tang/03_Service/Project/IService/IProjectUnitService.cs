using NHH.Entities;
using NHH.Models.Common;
using NHH.Models.Project.ProjectUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 铺位管理服务接口
    /// </summary>
    public interface IProjectUnitService
    {
        /// <summary>
        /// 根据铺位号取铺位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectUnitInfo GetUnitInfo(int unitId);
    }
}
