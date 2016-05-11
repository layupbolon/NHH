using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户用户证件信息
    /// </summary>
   public  class MerchantUserPaperInfo
   {
       /// <summary>
       /// 证件ID
       /// </summary>
       public int PaperID { get; set; }

       public int UserID { get; set; }
       
       /// <summary>
       /// 证件类型
       /// </summary>
       public int PaperType { get; set; }

       /// <summary>
       /// 证件类型名称
       /// </summary>
       public string PaperTypeName { get; set; }

       /// <summary>
       /// 证件编号
       /// </summary>
       public string PaperNumber { get; set; }

       /// <summary>
       /// 证件名称
       /// </summary>
       public string PaperName { get; set; }

       /// <summary>
       /// 证件路径
       /// </summary>
       public string PaperPath { get; set; }

       /// <summary>
       /// 审核状态
       /// </summary>
       public int AuditStatus { get; set; }

       /// <summary>
       /// 审核状态
       /// </summary>
       public string AuditStatusName { get; set; }

       /// <summary>
       /// 有效期
       /// </summary>
       public DateTime? ExpiredDate { get; set; }

       /// <summary>
       /// 有效天数
       /// </summary>
       public int ExpiredDays
       {
           get
           {
               if (!ExpiredDate.HasValue)
                   return 0;

               return (int)(ExpiredDate.Value - DateTime.Now).TotalDays;
           }
       }

       public int Status { get; set; }

       public DateTime InDate { get; set; }

       public int InUser { get; set; }

       public DateTime EditDate { get; set; }

       public int EditUser { get; set; }
   }
}
