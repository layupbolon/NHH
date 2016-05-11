using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Campaign
{
    public class CampaignListModel
    {
        public CampaignListModel()
        {
        }

        public CampaignListModel(int pageIndex,int pageSize)
        {
            this.PagingInfo=new PagingInfo();
            this.PagingInfo.PageIndex = pageIndex;
            this.PagingInfo.PageSize = pageSize;
        }
        /// <summary>
        /// 活动列表
        /// </summary>
        public List<CampaignInfo> CampaignList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
