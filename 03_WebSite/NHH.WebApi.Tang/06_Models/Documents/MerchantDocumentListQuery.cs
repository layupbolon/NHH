using System;

namespace NHH.Models.Documents
{
    public class MerchantDocumentListQuery
    {
        /// <summary>
        /// 商户编号
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 店铺编号
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// 角色 1 Admin 2 Operator
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 状态 -1已作废 1未审核 2审核中 3已驳回 4已审核
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 申请单提交时间起
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 申请单提交时间止
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 单据类型 1出门单 2装修单 3延时经营单
        /// </summary>
        public int DocumentType { get; set; }
    }
}
