using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Campaign
{
    /// <summary>
    /// 活动列表信息
    /// </summary>
    public class CampaignListModel : BaseModel
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        public List<CampaignInfo> CampaignInfoList { get; set; }

        /// <summary>
        /// 翻页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// 活动查询条件
        /// </summary>
        public CampaignQueryInfo QueryInfo { get; set; }

    }
}
