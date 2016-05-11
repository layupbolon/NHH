using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{

    /// <summary>
    /// 投诉查询信息
    /// </summary>
    public class ComplaintListQuery
    {
        /// <summary>
        /// 商户编号
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 商铺ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// 角色 1 Admin 2 Operator
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 投诉区间开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 投诉区间结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 投诉状态 1待处理 2处理中 3已完成
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 排序方式：1-投诉时间；2-投诉状态
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int Size { get; set; }

    }
}
