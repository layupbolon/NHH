using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Model
{
    /// <summary>
    /// 维修信息
    /// </summary>
    public class RepairInfo
    {
        /// <summary>
        /// 维修Id
        /// </summary>
        public int RepairId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 提报时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedFinishTime { get; set; }

        /// <summary>
        /// 维修状态
        /// 1 待处理
        /// 2 处理中
        /// </summary>
        public int RepairStatus { get; set; }
    }
}
