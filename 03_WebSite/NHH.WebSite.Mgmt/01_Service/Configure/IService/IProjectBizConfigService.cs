using NHH.Models.Common;
using NHH.Models.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure.IService
{
    /// <summary>
    /// 项目业务配置服务接口
    /// </summary>
    public interface IProjectBizConfigService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectBizConfigListModel GetBizConfigList(ProjectBizConfigListQueryInfo queryInfo);

        /// <summary>
        /// 获取业务配置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        ProjectBizConfigInfo GetBizConfig(int type, int projectId);

        /// <summary>
        /// 获取业务配置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        ProjectBizConfigInfo GetBizConfig(int type, int projectId, int buildingId);

        /// <summary>
        /// 根据当前系统用户信息获取业务配置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        ProjectBizConfigInfo GetBizConfig(int type, UserInfo userInfo);

        /// <summary>
        /// 获取当前系统用户所在公司所管的所有项目
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userInfo"></param>
        List<int> GetProjectList(int type, UserInfo userInfo);
    }
}
