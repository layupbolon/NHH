using NHH.Models.Official;

namespace NHH.Service.Official.IService
{
    public interface IIndexPageService
    {
        /// <summary>
        /// 获取官网首页顶部banner
        /// </summary>
        /// <returns></returns>
        string GetTopBanner();

        /// <summary>
        /// 获取官网首页顶部banner数量
        /// </summary>
        /// <returns></returns>
        int GetTopBannerCount();

        /// <summary>
        /// 通过locationID获取banner
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        string GetBannerByLocationID(LocationEnum location);

        /// <summary>
        /// 获取底部Banner
        /// </summary>
        /// <returns></returns>
        string GetLastBanner();
    }
}
