namespace NHH.Service.Official.IService
{
    public interface ICampaignService
    {
        /// <summary>
        /// 获取顶部三个活动
        /// </summary>
        /// <returns></returns>
        string GetTopCampaign();

        /// <summary>
        /// 获取剩下的活动
        /// </summary>
        /// <returns></returns>
        string GetOtherCampaign();

        /// <summary>
        /// 获取活动内容
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        string GetCampaignContent(int pageID);
    }
}
