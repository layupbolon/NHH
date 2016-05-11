using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Documents
{
    public class DecorateRequestInfo
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
        /// 装修施工公司名称
        /// </summary>
        public string DecorationCompanyName { get; set; }
        /// <summary>
        /// 装修施工公司地址
        /// </summary>
        public string DecorationCompanyAddress { get; set; }
        /// <summary>
        /// 现场负责人
        /// </summary>
        public string PersonInCharge { get; set; }
        /// <summary>
        /// 联络电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 装修开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 装修结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 装修项目描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 驳回理由
        /// </summary>
        public string RejectReason { get; set; }
        /// <summary>
        /// 用电量（千瓦）
        /// </summary>
        public int ElectricityConsumption { get; set; }

    }
}
