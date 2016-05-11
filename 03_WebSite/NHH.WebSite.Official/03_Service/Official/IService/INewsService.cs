using NHH.Models.Official;

namespace NHH.Service.Official.IService
{
    public interface INewsService
    {
        /// <summary>
        /// 获取所有新闻
        /// </summary>
        /// <returns></returns>
        string GetAllNews();

        /// <summary>
        /// 获取置顶新闻
        /// </summary>
        /// <returns></returns>
        string GetStickNews();

        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        NewsInfo GetNews(int newsID);

        /// <summary>
        /// 获取下一则新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        NewsInfo GetNextNews(int newsID);

        /// <summary>
        /// 获取上一则新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        NewsInfo GetPreviousNews(int newsID);
    }
}
