using NHH.Framework.Service;
using NHH.Models.Project.ProjectUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 铺位初始化服务接口
    /// </summary>
    public interface IProjectUnitInitService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initInfo"></param>
        MessageBag<bool> Init(ProjectUnitInitInfo initInfo);
    }
}
