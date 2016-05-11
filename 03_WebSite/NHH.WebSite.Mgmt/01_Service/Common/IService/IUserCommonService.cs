using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 用户公共服务接口
    /// </summary>
    public interface IUserCommonService
    {
        /// <summary>
        /// 获取用户的默认项目ID列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<int> GetProjectIdList(int userId);

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        EmployeeCommonInfo GetEmployeeInfo(int userId);
    }
}
