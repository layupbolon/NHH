using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.Adpoint
{
    public class AdPointQueryInfo : QueryInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int? FloorId { get; set; }


        public int? Type { get; set; }

        /// <summary>
        /// 广告位号
        /// </summary>
        public string AdPointNumber { get; set; }
    }
}
