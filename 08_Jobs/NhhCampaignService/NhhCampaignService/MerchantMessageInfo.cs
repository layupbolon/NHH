using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhCampaignService
{
    public class MerchantMessageInfo : BaseInfo
    {
        /// <summary>
        /// 商户消息Id
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 商户店铺Id
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 商户用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int SourceType { get; set; }

        /// <summary>
        /// 消息单据
        /// </summary>
        public int SourceRefId { get; set; }

    }
}
