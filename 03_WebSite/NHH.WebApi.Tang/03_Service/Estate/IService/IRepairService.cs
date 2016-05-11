using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Estate;

namespace NHH.Service.Estate.IService
{
    /// <summary>
    /// 维修服务接口
    /// </summary>
    public interface IRepairService
    {
        /// <summary>
        /// 获取维修单各状态数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="merchantId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        RepairStatusInfo GetRepairStatusQty(int roleId, int merchantId, int userId);
        /// <summary>
        /// 获取维修列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        RepairListModel GetRepairList(RepairListQuery queryInfo);

        /// <summary>
        /// 添加报修
        /// </summary>
        /// <param name="model"></param>
        bool AddRepair(RepairInfo model);

        /// <summary>
        /// 更新指定的报修信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateRepair(RepairInfo model);

        /// <summary>
        /// 作废报修
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        bool CancelRepair(int repairId);

        /// <summary>
        /// 获取维修详情(精简)
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        RepairInfo GetRepairSimple(int repairId);

        /// <summary>
        /// 获取维修详情(详细)
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        RepairInfo GetRepairDetail(int repairId);
        /// <summary>
        /// 添加维修附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddRepairAttachment(RepairAttachmentInfo model);

        /// <summary>
        /// 获取指定的报修附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        RepairAttachmentInfo GetRepairAttachment(int attachmentId);

        /// <summary>
        /// 作废指定报修附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        bool CancelRepairAttachment(int attachmentId);

        /// <summary>
        /// 添加评价
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddComment(RepairCommentInfo model);

        /// <summary>
        /// 获取点评详情
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        RepairCommentInfo GetCommentDetail(int commentId);

        /// <summary>
        /// 追加评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddAdditional(RepairCommentInfo model);
    }
}
