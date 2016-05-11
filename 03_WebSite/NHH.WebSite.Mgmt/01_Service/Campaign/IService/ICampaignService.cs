using NHH.Models.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Campaign.IService
{
    public interface ICampaignService
    {
        /// <summary>
        /// 获取企划活动列表信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        CampaignListModel GetCampaignList(CampaignQueryInfo queryInfo);

        /// <summary>
        /// 增加企划活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddCampaign(CampaignPlanDetailModel model);

        /// <summary>
        /// 更新企划活动
        /// </summary>
        /// <param name="model"></param>
        void UpdateCampaign(CampaignPlanDetailModel model);

        /// <summary>
        /// 企划详情
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        CampaignPlanDetailModel GetCampaignPlanDetail(int campaignId);

    }
}
