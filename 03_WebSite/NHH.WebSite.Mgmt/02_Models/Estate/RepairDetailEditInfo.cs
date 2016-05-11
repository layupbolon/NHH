using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 维修详情编辑字段
    /// </summary>
    public class RepairDetailEditInfo
    {
        /// <summary>
        /// 维修单Id
        /// </summary>
        public int RepairId { get; set; }

        /// <summary>
        /// 维修报价
        /// </summary>
        public decimal? QuoteAmount { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }
}
