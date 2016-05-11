using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    public  class RepairCommentListInfo
    {
        /// <summary>
        /// 维修人
        /// </summary>
        public string RepairName { get; set; }

        /// <summary>
        /// 平均分
        /// </summary>
        public decimal? Average { get; set; }

        /// <summary>
        /// 评价
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///报修编号
        /// </summary>
        public int? RepairId { get; set; }
    }
}
