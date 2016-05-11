using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 水电煤气费抄表信息
    /// </summary>
    public class UtilityBillInfo
    {
        public int BillID { get; set; }
        
        /// <summary>
        /// 计量表ID
        /// </summary>
        public int MeterID { get; set; }

        /// <summary>
        /// 计量表编号
        /// </summary>
        public string MeterCode { get; set; }

        /// <summary>
        /// 计量表参数
        /// </summary>
        public string MeterAttr { get; set; }

        /// <summary>
        /// 计量表类型
        /// </summary>
        public int MeterType { get; set; }

        /// <summary>
        /// 计量表类型
        /// </summary>
        public string MeterTypeName { get; set; }

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
                
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public decimal PrevNumber { get; set; }
        
        public decimal CurNumber { get; set; }

        public decimal UseNumber { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal BillAmount { get; set; }
        
        public int? OperatorID { get; set; }
        
        public string OperatorName { get; set; }
        
        public string Remark { get; set; }
        
        public int Status { get; set; }
        
        public DateTime InDate { get; set; }
        
        public DateTime EditDate { get; set; }
        
        public int InUser { get; set; }
        
        public int? EditUser { get; set; }
    }
}
