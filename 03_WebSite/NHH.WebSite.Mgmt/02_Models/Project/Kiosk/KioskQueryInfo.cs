using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.Kiosk
{
    public class KioskQueryInfo: QueryInfo
    {

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 多经点位号
        /// </summary>
        public string KioskNumber { get; set; } 
    }
}
