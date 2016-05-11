using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 维修搁置信息
    /// </summary>
    public class RepairShelveInfo
    {
        /// <summary>
        /// 维修Id
        /// </summary>
        public int RepairId { get; set; }

        /// <summary>
        /// 搁置原因名称
        /// </summary>
        public string ShelveReason { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 维修人员Id
        /// </summary>
        public int RepairUserId { get; set; }

        /// <summary>
        /// 维修来源
        /// </summary>
        public int RequestSrcType { get; set; }

        /// <summary>
        /// 申请人Id
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
        /// 提报时间
        /// </summary>
        public DateTime? RequestTime { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedFinishTime { get; set; }

        /// <summary>
        /// 维修人员名称
        /// </summary>
        public string RepairUserName { get; set; }

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
