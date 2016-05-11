using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 合约附件
    /// </summary>
    public class ContractAppendixInfo
    {
        /// <summary>
        /// 合约附件ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AppendixID { get; set; }

        /// <summary>
        /// 租约ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ContractID { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AppendixType { get; set; }

        /// <summary>
        /// 附件类型描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AppendixTypeDescription {
            get
            {
                switch (AppendixType)
                {
                        //TODO:待确认
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 附件模板
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AppendixTemplate { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AppendixName { get; set; }

        /// <summary>
        /// 附件扫描副本文件URL（小图）
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AppendixPath { get; set; }

        /// <summary>
        /// 附件扫描副本文件URL(原图)
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AppendixOriginalPath { get; set; }
    }
}
