using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉评价详细信息
    /// </summary>
    public class ComplaintCommentDetailModel : ComplaintCommentInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo { };

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get
            {
                return _crumbInfo;
            }
        }

        public decimal? Quality { get; set; }


        public decimal? Overall { get; set; }

        public string comment { get; set; }

        public string Additional { get; set; }

        public int Status { get; set; }

        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime EditDate { get; set; }

        public int EditUser { get; set; }

        public DateTime? ServiceFinishTime { get; set; }
    }
}
