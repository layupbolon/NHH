using System;
using NHH.Models.Approve;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 特殊表单信息
    /// </summary>
    public class DocumentsInfo : ApproveBaseModel
    {
        private int _documentID;

        /// <summary>
        /// ID
        /// </summary>
        public int DocumentID
        {
            get { return _documentID; }
            set { _documentID = value; }
        }

        /// <summary>
        /// 单据类型ID
        /// </summary>
        public int DocumentType { get; set; }
        
        /// <summary>
        /// 单据类型名称
        /// </summary>
        public string DocumentTypeName { get; set; }

        /// <summary>
        /// 店铺编号
        /// </summary>
        public int MerchantStoreID { get; set; }

        /// <summary>
        /// 店铺信息
        /// </summary>
        public ProjectUnitInfo ProjectUnitInfo { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string MerchantStoreName { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string RequestUserName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public int InUser { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime InDate { get; set; }

        /// <summary>
        /// 当前审核人
        /// </summary>
        public string ApproverName { get; set; }

        public override int RefID
        {
            get { return _documentID; }
            set { _documentID = value; }
        }

        public override string ApproveUrl
        {
            get { return "/Contract/Documents/Approve"; }
        }

        public override string RedirectUrl
        {
            get { return "/Contract/Documents/List"; }
        }
    }
}
