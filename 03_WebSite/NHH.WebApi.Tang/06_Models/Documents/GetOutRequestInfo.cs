using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Documents
{
    public class GetOutRequestInfo
    {
        /// <summary>
        /// PK
        /// </summary>
        public int ID { get; set; }
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
        /// 铺位号
        /// </summary>
        public string UnitNumber { get; set; }
        /// <summary>
        /// 店铺名
        /// </summary>
        public string MerchantStoreName { get; set; }
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
        /// 状态名
        /// </summary>
        public string StatusStr { get; set; }
        /// <summary>
        /// 申请人编号
        /// </summary>
        public int InUser { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime InDate { get; set; }
        /// <summary>
        /// 出门时间
        /// </summary>
        public DateTime GetOutTime { get; set; }
        /// <summary>
        /// 物品数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 出门理由
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 概述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 驳回理由
        /// </summary>
        public string RejectReason { get; set; }
        /// <summary>
        /// 是否临时货品出门
        /// </summary>
        public int IsTemporary { get; set; }
        /// <summary>
        /// 还回时间（只有临时货品才使用）
        /// </summary>
        public DateTime? ReturnTime { get; set; }
    }
}
