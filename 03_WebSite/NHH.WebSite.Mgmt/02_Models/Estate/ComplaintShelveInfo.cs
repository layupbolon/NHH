using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉搁置信息
    /// </summary>
    public class ComplaintShelveInfo
    {

        /// <summary>
        /// 投诉Id
        /// </summary>
        public int ComplaintId { get; set; }

        /// <summary>
        /// 搁置原因名称
        /// </summary>
        public string ShelveReason { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 投诉来源
        /// 1 商户
        /// 2 
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
        /// 投诉详情url
        /// </summary>
        public string ComplaintDetailUrl { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }
}
