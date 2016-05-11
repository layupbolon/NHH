using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉列表基本信息
    /// </summary>
    public class ComplaintInfo
    {
        public int ComplaintId { get; set; }

        [Required(ErrorMessage = "请选择投诉类别")]
        public int ComplaintType { get; set; }

        public string ComplaintTypeName { get; set; }

        public DateTime? RequestTime { get; set; }

        public int ComplaintStatus { get; set; }

        public string ComplaintStatusName { get; set; }

        public int? ServiceUserId { get; set; }

        public string ServiceUserName { get; set; }

        /// <summary>
        /// 重新指派原因
        /// </summary>
        public string ReAssignReason { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedFinishTime { get; set; }

        /// <summary>
        /// 投诉来源
        /// 1 商户
        /// </summary>
        public int RequestSrcType { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public int RequestUserId { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 投诉详情url
        /// </summary>
        public string ComplaintDetailUrl { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }

        public string RequestTarget { get; set; }

        public string RequestDesc { get; set; }

    }
}
