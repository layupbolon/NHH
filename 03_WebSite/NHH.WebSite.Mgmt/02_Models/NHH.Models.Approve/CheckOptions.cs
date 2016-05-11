using Newtonsoft.Json;

namespace NHH.Models.Approve
{
    public class CheckOptions
    {
        /// <summary>
        /// check项ID
        /// </summary>
        public int Option { get; set; }

        /// <summary>
        /// check项名称
        /// </summary>
        [JsonIgnore]
        public string OptionName { get; set; }

        /// <summary>
        /// 是否必要在审批时勾选
        /// </summary>
        public int Required { get; set; }

        /// <summary>
        /// 是否勾选
        /// </summary>
        public int Checked { get; set; }
    }
}
