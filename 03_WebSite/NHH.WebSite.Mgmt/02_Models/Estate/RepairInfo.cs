using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    public class RepairInfo
    {
        /// <summary>
        /// 维修编号
        /// </summary>
        public int RepairID { get; set; }

        /// <summary>
        /// 维修类型
        /// </summary>
        public string RepairTypeName { get; set; }

        /// <summary>
        /// 维修类型ID
        /// </summary>
        [Required(ErrorMessage = "请选择报修类型")]
        public int? RepairType { get; set; }

        /// <summary>
        /// 报修开始时间
        /// </summary>
        public DateTime? RepairStartTime { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string RepairUser { get; set; }

        /// <summary>
        /// 维修人员ID
        /// </summary>
        public int? RepairUserId { get; set; }

        /// <summary>
        /// 维修状态ID
        /// </summary>
        public int? RepairStatus { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public string RepairStatusName { get; set; }

        /// <summary>
        /// 维修人电话
        /// </summary>
        public string RepairContact { get; set; }

        /// <summary>
        /// 维修完成时间
        /// </summary>
        public DateTime? RepairFinishTime { get; set; }

        /// <summary>
        /// 报修时间
        /// </summary>
        public DateTime? RequestTime { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string CertificationId { get; set; }

        /// <summary>
        /// 维修人员标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 维修是否紧急
        /// </summary>
        public int Important { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedFinishTime { get; set; }
    }
}
