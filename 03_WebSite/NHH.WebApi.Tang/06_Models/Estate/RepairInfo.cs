using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHH.Models.Common.Converter;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 维修单信息
    /// </summary>
    public class RepairInfo
    {
        /// <summary>
        /// 维修单ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RepairID { get; set; }

        /// <summary>
        /// 维修类型
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RepairType { get; set; }

        /// <summary>
        /// 维修类型描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RepairTypeDescription
        {
            get
            {
                switch (RepairType)
                {
                    case 1:
                        return "排水";
                    case 2:
                        return "强电";
                    case 3:
                        return "弱电";
                    case 4:
                        return "暖通";
                    case 5:
                        return "装饰";
                    case 6:
                        return "消防设备";
                    case 7:
                        return "安防设备";
                    case 8:
                        return "其他";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ProjectID { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StoreID { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int FloorID { get; set; }

        /// <summary>
        /// 铺位ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int UnitID { get; set; }

        /// <summary>
        /// 是否公共区域
        /// </summary>
        public int IsCommon { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Location { get; set; }

        /// <summary>
        /// 报修情况描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RequestDesc { get; set; }

        /// <summary>
        /// 报修人ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RequestUserID { get; set; }

        /// <summary>
        /// 报修人姓名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RequestUserName { get; set; }

        /// <summary>
        /// 报修时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? RequestTime { get; set; }

        /// <summary>
        /// 受理人ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AcceptUserID { get; set; }

        /// <summary>
        /// 受理人姓名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AcceptUserName { get; set; }

        /// <summary>
        /// 受理时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? AcceptTime { get; set; }

        /// <summary>
        /// 维修结果描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RepairDesc { get; set; }

        /// <summary>
        /// 维修人ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RepairUserID { get; set; }

        /// <summary>
        /// 维修人姓名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RepairUserName { get; set; }

        /// <summary>
        /// 维修开始时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? RepairStartTime { get; set; }

        /// <summary>
        /// 维修完成时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? RepairFinishTime { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RepairStatus { get; set; }

        /// <summary>
        /// 维修状态描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RepairStatusDescription {
            get
            {
                switch (RepairStatus)
                {
                    case 1:
                        return "待受理";
                    case 2:
                        return "受理中";
                    case 3:
                        return "已完成";
                    case 4:
                        return "未点评";
                    default:
                        return null;
                }        
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Status { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public System.DateTime InDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int InUser { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? EditDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int EditUser { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<RepairAttachmentInfo> RepairAttachmentList { get; set; }

        /// <summary>
        /// 评论信息
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<RepairCommentInfo> RepairCommentList { get; set; }

        /// <summary>
        /// 日志列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<RepairLogInfo> RepairLogList { get; set; }
    }
}
