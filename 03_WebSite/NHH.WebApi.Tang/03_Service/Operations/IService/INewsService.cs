using NHH.Models.Operations;

namespace NHH.Service.Operations.IService
{
    public interface INewsService
    {
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        NewsListModel GetNewsList(NewsListQuery queryInfo);

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        NewsInfo GetNews(int newsId, int projectId);
    }
}
