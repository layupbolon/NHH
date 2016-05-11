using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 修修指派信息
    /// </summary>
    public class RepairAssignInfo
    {
        /// <summary>
        /// 维修任务ID
        /// </summary>
        public int RepairId { get; set; }

        /// <summary>
        /// 指派维修人员的姓名
        /// </summary>
        public string RepairUserName { get; set; }

        /// <summary>
        /// 指派维修人员的ID
        /// </summary>
        public int RepairUserId { get; set; }

        /// <summary>
        /// 维修人的联系方式
        /// </summary>
        public string RepairMobile { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public bool Important { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public double? EstimatedHour { get; set; }

        /// <summary>
        /// 提报时间
        /// </summary>
        public DateTime? RequestTime { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedFinishTime { get; set; }

        /// <summary>
        /// 重新指派原因
        /// </summary>
        public string ReAssignReason { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 维修来源
        /// </summary>
        public int RequestSrcType { get; set; }

        /// <summary>
        /// 提报人Id
        /// </summary>
        public int RequestUserId { get; set; }

        /// <summary>
        /// 提报人名称
        /// </summary>
        public string RequestUserName { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 维修详情Url
        /// </summary>
        public string RepairDetailUrl { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }

    }
}
