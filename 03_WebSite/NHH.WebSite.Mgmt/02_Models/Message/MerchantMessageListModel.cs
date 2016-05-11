using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Message
{
    /// <summary>
    /// 商户消息列表
    /// </summary>
    public class MerchantMessageListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public MerchantMessageListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 消息列表
        /// </summary>
        public List<MerchantMessageInfo> MessageList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
