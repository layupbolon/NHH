using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 维修微信内容信息
    /// </summary>
    public class RepairWechatContentInfo
    {
        /// <summary>
        /// 维修标题称呼：尊敬的xxx先生/女士，您的报修有新的进展。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 报修房号：深圳红树西岸5栋11A
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 阳台玻璃门破裂
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 2014/8/28 10:30
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 已指派给维修人员xxx，将在3个工作日内上门更换。
        /// </summary>
        public string RepairStatus { get; set; }

        /// <summary>
        /// 2014/8/31
        /// </summary>
        public DateTime EstimateTime { get; set; }

        /// <summary>
        /// 上门前工作人员将提前与您预约，请保持电话畅通，谢谢
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// #173177
        /// </summary>
        string color = "#173177";
        public string Color { get { return color; } }

    }
}
