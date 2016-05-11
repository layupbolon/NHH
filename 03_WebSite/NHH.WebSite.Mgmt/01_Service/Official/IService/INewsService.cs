using NHH.Models.Approve;
using NHH.Models.Official;
using NHH.Service.Approve.IService;

namespace NHH.Service.Official.IService
{
    public interface INewsService : IApprove
    {
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="newsQueryInfo"></param>
        /// <returns></returns>
        NewsListModel GetNewsListModel(NewsQueryInfo newsQueryInfo);

        /// <summary>
        /// 置顶新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        bool StickNews(int? newsID, int userID);

        /// <summary>
        /// 取消置顶
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        bool UnStickNews(int? newsID, int userID);

        /// <summary>
        /// 发布新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        bool PublishNews(int? newsID, int userID);

        /// <summary>
        /// 作废新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        bool CancelNews(int? newsID, int currentUserID);

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="newsModel"></param>
        /// <returns></returns>
        bool AddNews(NewsModel newsModel);

        /// <summary>
        /// 获取新闻详细
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        NewsModel GetNewsDetail(int newsID,int configType = (int)ConfigTypeEnum.新闻);

        /// <summary>
        /// 编辑新闻
        /// </summary>
        /// <param name="newsModel"></param>
        /// <returns></returns>
        bool EditNews(NewsModel newsModel);
    }
}
