using NHH.Models.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate.IService
{
    /// <summary>
    /// 报修评价服务接口
    /// </summary>
    public interface IRepairCommentService
    {
        /// <summary>
        /// 获取评价列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        RepairCommentListModel GetCommentList(RepairCommentQueryInfo queryInfo);

        /// <summary>
        /// 获取点评详情弹出框
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        RepairCommentDetailModel GetCommentDetail(int commentId);

    }
}
