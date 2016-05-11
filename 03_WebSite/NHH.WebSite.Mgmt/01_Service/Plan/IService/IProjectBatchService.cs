using NHH.Framework.Service;
using NHH.Models.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Plan.IService
{
    /// <summary>
    /// 基目批次服务接口
    /// </summary>
    public interface IProjectBatchService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectBatchListModel GetList(ProjectBatchListQueryInfo queryInfo);
        
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        ProjectBatchDetailModel GetDetail(int batchId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        MessageBag<bool> Add(ProjectBatchInfo info);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="info"></param>
        MessageBag<bool> Edit(ProjectBatchInfo info);

        /// <summary>
        /// 获取项目招商批次列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        List<ProjectBatchInfo> GetBatchList(int projectId);
    }
}
