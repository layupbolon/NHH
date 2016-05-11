using System;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 装修申请单详细
    /// </summary>
    public class DecorateDetailInfo : DocumentsInfo
    {
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
    }
}
