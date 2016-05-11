using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHH.Models.Estate
{
    public class RepairListQuery
    {
        public int RoleID { get; set; }

        public int StoreID { get; set; }

        public int UserID { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }
        /// <summary>
        /// 报修区间开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 报修区间结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 报修状态：1待受理 2受理中 3已完成
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 排序方式：1-报修时间；2-状态
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
