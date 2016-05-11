using NHH.Models.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate.IService
{
    /// <summary>
    /// 投诉评价服务接口
    /// </summary>
    public interface IComplaintCommentService
    {
        /// <summary>
        /// 获取投诉评价列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ComplaintCommentListModel GetCommentList(ComplaintCommentQueryInfo queryInfo);

        /// <summary>
        /// 投诉评价详情
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        ComplaintCommentDetailModel GetCommentDetail(int commentId);
    }
}
