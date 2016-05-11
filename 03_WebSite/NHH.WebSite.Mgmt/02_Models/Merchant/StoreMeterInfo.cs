using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商铺计量表
    /// </summary>
    public class StoreMeterInfo
    {
        /// <summary>
        /// 计量表ID
        /// </summary>
        public int MeterID { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// 商铺ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContractID { get; set; }

        /// <summary>
        /// 铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 计量表类型
        /// </summary>
        [Required(ErrorMessage = "请选择计量表类型")]
        public int MeterType { get; set; }

        /// <summary>
        /// 计量表类型名称
        /// </summary>
        public string MeterTypeName { get; set; }

        /// <summary>
        /// 计量表编号
        /// </summary>
        [Required(ErrorMessage = "请输入计量表编号")]
        public string MeterCode { get; set; }

        /// <summary>
        /// 计量表属性
        /// </summary>
        [Required(ErrorMessage = "请输入计表量属性")]
        public string MeterAttr { get; set; }

        /// <summary>
        /// 最后一次抄表日期
        /// </summary>
        public DateTime? LastReading { get; set; }

        /// <summary>
        /// 最后度数
        /// </summary>
        public decimal LastNumber { get; set; }

        public int Status { get; set; }

        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime EditDate { get; set; }

        public int EditUser { get; set; }
    }
}
