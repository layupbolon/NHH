using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Campaign;

namespace NHH.Service.Campaign.IService
{
    public interface ICampaignInfoService
    {
        /// <summary>
        /// 获取当前项目中的所有活动列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        CampaignListModel GetCampaignList(CampaignListQuery queryInfo);

        /// <summary>
        /// 获取指定的活动内容
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        CampaignInfo GetCampaignInfo(int campaignId);

        /// <summary>
        /// 添加企划活动点评
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddCampaignComment(CampaignCommentInfo model);
    }
}
