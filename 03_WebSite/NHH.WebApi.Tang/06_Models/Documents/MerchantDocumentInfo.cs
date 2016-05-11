using System;

namespace NHH.Models.Documents
{
    public class MerchantDocumentInfo
    {
        /// <summary>
        /// 单据编号
        /// </summary>
        public int DocumentID { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public int DocumentType { get; set; }
        /// <summary>
        /// 单据类型名
        /// </summary>
        public string DocumentTypeName { get; set; }
        /// <summary>
        /// 店铺编号
        /// </summary>
        public int MerchantStoreID { get; set; }
        /// <summary>
        /// 申请人名字
        /// </summary>
        public string RequestUserName { get; set; }
        /// <summary>
        /// 申请人联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 状态字符
        /// </summary>
        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case -1:
                        return "已作废";
                    case 1:
                        return "待审批";
                    case 2:
                        return "审批中";
                    case 3:
                        return "已驳回";
                    case 4:
                        return "已审批";
                    default:
                        return null;
                }
            }
        }
        /// <summary>
        /// 申请人编号
        /// </summary>
        public int InUser { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime InDate { get; set; }

        #region 特殊显示字段
        /*由于列表上要列出的内容不统一，要根据不同类型的单据拼出要显示的字段名与字段内容*/

        /// <summary>
        /// 
        /// </summary>
        public string RemarkFieldName
        {
            get
            {
                switch (DocumentType)
                {
                    case 1:
                        return "物品概述";
                    case 2:
                        return "装修内容";
                    case 3:
                        return "申请内容";
                    default:
                        return null;
                }
            }
        }
        public string Remark { get; set; }
        #endregion 特殊显示字段
    }
}
