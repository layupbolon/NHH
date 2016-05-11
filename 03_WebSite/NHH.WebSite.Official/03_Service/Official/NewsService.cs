using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.Service.Official
{
    public class NewsService : NHHService<NHHEntities>, INewsService
    {
        /// <summary>
        /// 获取所有新闻
        /// </summary>
        /// <returns></returns>
        private List<NewsInfo> GetNews()
        {
            var query =
                Context.News.Where(a => a.NewsTarget == 2 && a.Status == 3 && a.PublishTime <= DateTime.Now)
                    .Select(a => new NewsInfo()
                    {
                        NewsID = a.NewsID,
                        NewsTitle = a.NewsTitle,
                        NewsContent = a.NewsContent,
                        NewsBrief = a.NewsBrief,
                        NewsCover = a.NewsCover,
                        PublishTime = a.PublishTime,
                        Stick = a.Stick
                    });
            return !query.Any() ? new List<NewsInfo>() : query.ToList();
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetResult(List<NewsInfo> list)
        {
            if (!list.Any()) return string.Empty;

            var sb = new StringBuilder();
            list.ForEach(info =>
            {
                sb.AppendLine("<dl class=\"listwrap ml20 mt20\">");
                sb.AppendFormat("<dt class=\"fl\"><img src=\"{0}\"></dt>",
                    Framework.Utility.UrlHelper.GetImageUrl(info.NewsCover));
                sb.AppendFormat("<dd><a href=\"{0}\">", string.Format("News.aspx?newsID={0}", info.NewsID));
                sb.AppendFormat("<h4 title=\"{0}\">{0}</h4>", info.NewsTitle);
                sb.AppendFormat("<p>{0}</p>", info.NewsBrief);
                sb.AppendFormat("<em>{0}</em>", info.PublishTime.ToString("yyyy-MM-dd"));
                sb.AppendLine("</a></dd></dl>");
            });

            return sb.ToString();
        }

        /// <summary>
        /// 获取所有新闻
        /// </summary>
        /// <returns></returns>
        public string GetAllNews()
        {
            var list = GetNews();
            if (!list.Any()) return string.Empty;

            return GetResult(list.OrderByDescending(a => a.Stick).ThenByDescending(a => a.PublishTime).ToList());
        }

        /// <summary>
        /// 获取置顶新闻
        /// </summary>
        /// <returns></returns>
        public string GetStickNews()
        {
            var list = GetNews();
            if (!list.Any()) return string.Empty;

            return GetResult(list.Where(a => a.Stick == 1).OrderByDescending(a => a.PublishTime).ToList());
        }

        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public NewsInfo GetNews(int newsID)
        {
            var query =
                Context.News.Where(a => a.NewsID == newsID)
                    .Select(a => new NewsInfo()
                    {
                        NewsID = a.NewsID,
                        NewsTitle = a.NewsTitle,
                        NewsContent = a.NewsContent,
                        NewsBrief = a.NewsBrief,
                        NewsCover = a.NewsCover,
                        PublishTime = a.PublishTime,
                        Stick = a.Stick
                    });
            return !query.Any() ? new NewsInfo() : query.FirstOrDefault();
        }

        /// <summary>
        /// 获取下一条新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public NewsInfo GetNextNews(int newsID)
        {
            var list = GetNews();
            if (!list.Any()) return null;

            list = list.OrderByDescending(a => a.Stick).ThenByDescending(a => a.PublishTime).ToList();

            //return list.SkipWhile(a => a.NewsID == newsID).Take(1).FirstOrDefault();

            NewsInfo model;
            int index = list.IndexOf(list.FirstOrDefault(a => a.NewsID == newsID)) + 1;
            try
            {
                model = list[index];
            }
            catch (Exception)
            {
                model = null;
            }

            return model;
        }

        /// <summary>
        /// 获取上一则新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public NewsInfo GetPreviousNews(int newsID)
        {
            var list = GetNews();
            if (!list.Any()) return null;

            list = list.OrderBy(a => a.Stick).ThenBy(a => a.PublishTime).ToList();

            //return list.SkipWhile(a => a.NewsID == newsID).Take(1).FirstOrDefault();

            NewsInfo model;
            int index = list.IndexOf(list.FirstOrDefault(a => a.NewsID == newsID)) + 1;
            try
            {
                model = list[index];
            }
            catch (Exception)
            {
                model = null;
            }

            return model;
        }
    }
}
