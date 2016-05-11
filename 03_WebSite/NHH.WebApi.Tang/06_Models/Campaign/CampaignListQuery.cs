using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Campaign
{
    public class CampaignListQuery
    {
        /// <summary>
        /// 商家编号
        /// </summary>
        public int MerchantID { get; set; }
        ///// <summary>
        ///// 项目编号
        ///// </summary>
        //public int StoreID { get; set; }
        /// <summary>
        /// 活动状态 1策划中 2进行中 3待小结 4已完成
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 排序方式 1活动开始时间 2状态
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
