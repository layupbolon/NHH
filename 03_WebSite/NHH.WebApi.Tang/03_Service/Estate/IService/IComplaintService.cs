using NHH.Models.Common;
using NHH.Models.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate.IService
{
    /// <summary>
    /// 投诉服务接口
    /// </summary>
    public interface IComplaintService
    {

        /// <summary>
        /// 获取投诉各状态数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="merchantId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        ComplaintStatusInfo GetComplaintStatusQty(int roleId, int merchantId, int storeId);

        /// <summary>
        /// 投诉任务列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ComplaintListModel GetComplaintList(ComplaintListQuery queryInfo);

        ///// <summary>
        ///// 获取投诉详情
        ///// </summary>
        ///// <param name="complaintId"></param>
        ///// <returns></returns>
        //ComplaintDetailModel GetComplaintDetail(int complaintId);

      

        ///// <summary>
        ///// 受理投诉请求
        ///// </summary>
        ///// <param name="complaintId"></param>
        ///// <returns></returns>
        //bool AcceptedRequest(int complaintId);


        /// <summary>
        /// 更新投诉信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateComplaint(ComplaintInfo model);

        /// <summary>
        /// 作废投诉单
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        bool CancelComplaint(int complaintId);

        /// <summary>
        /// 增加投诉单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddComplaint(ComplaintInfo model);

        /// <summary>
        /// 获取指定的投诉信息（精简）
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        ComplaintInfo GetComplaintSimple(int complaintId);

        /// <summary>
        /// 获取指定的投诉信息（详细）
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        ComplaintInfo GetComplaintDetail(int complaintId);

        /// <summary>
        /// 添加投诉附件
        /// </summary>
        bool AddAttachment(ComplaintAttachmentInfo model);

        /// <summary>
        /// 获取指定的投诉附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        ComplaintAttachmentInfo GetAttachment(int attachmentId);

        /// <summary>
        /// 获取指定投诉单附件列表
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        List<ComplaintAttachmentInfo> GetAttachmentList(int complaintId);

        /// <summary>
        /// 作废投诉附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        bool CancelAttachment(int attachmentId);

        /// <summary>
        /// 添加投诉单评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddComment(ComplaintCommentInfo model);

        /// <summary>
        /// 获取指定的评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        ComplaintCommentInfo GetComment(int commentId);

        /// <summary>
        /// 更新追加评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCommentAdditional(ComplaintCommentInfo model);
    }
}
