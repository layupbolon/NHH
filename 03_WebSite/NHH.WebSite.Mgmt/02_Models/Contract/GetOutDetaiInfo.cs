using System;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 出门申请单详细
    /// </summary>
    public class GetOutDetaiInfo : DocumentsInfo
    {
        /// <summary>
        /// 出门时间
        /// </summary>
        public DateTime GetOutTime { get; set; }

        /// <summary>
        /// 物品数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 出门原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 物品描述
        /// </summary>
        public string Remark { get; set; }
    }
}
