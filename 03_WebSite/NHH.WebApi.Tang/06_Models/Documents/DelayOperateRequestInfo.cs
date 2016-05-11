using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Documents
{
    public class DelayOperateRequestInfo
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
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 申请理由
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 申请人编号
        /// </summary>
        public int InUser { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime InDate { get; set; }
        /// <summary>
        /// 驳回理由
        /// </summary>
        public string RejectReason { get; set; }
        /// <summary>
        /// 是否需要空调
        /// </summary>
        public int NeedAC { get; set; }
        /// <summary>
        /// 是否需要通道及照明
        /// </summary>
        public int NeedLighting { get; set; }
        /// <summary>
        /// 延长时间
        /// </summary>
        public int MoreHour { get; set; }
    }
}
