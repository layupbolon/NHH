using System;
using System.Collections.Generic;
using System.Linq;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Models.Operations;
using NHH.Service.Operations.IService;

namespace NHH.Service.Operations
{
    public class NewsService : NHHService<NHHEntities>, INewsService
    {
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public NewsListModel GetNewsList(NewsListQuery queryInfo)
        {
            var model = new NewsListModel(queryInfo.Page, queryInfo.Size);
            model.NewsList = new List<NewsInfo>();
            var query = from n in Context.News
                where n.Status == 3 && n.PublishTime <= DateTime.Now && (n.ProjectID == queryInfo.ProjectID || n.ProjectID == 0) && n.NewsTarget == 1
                select n;
            if (queryInfo.BeginTime.HasValue)
            {
                query = query.Where(item => item.PublishTime >= queryInfo.BeginTime.Value);
            }
            if (queryInfo.EndTime.HasValue)
            {
                query = query.Where(item => item.PublishTime <= queryInfo.EndTime.Value);
            }
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderByDescending(item => item.Stick).ThenByDescending(item=>item.PublishTime).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new NewsInfo();
                    contract.NewsID = entity.NewsID;
                    contract.NewsTitle = entity.NewsTitle;
                    //contract.NewsContent = entity.NewsContent;
                    contract.NewsBrief = entity.NewsBrief;
                    contract.NewsCover = UrlHelper.GetImageUrl(entity.NewsCover);
                    contract.PublishTime = entity.PublishTime;
                    //contract.Status = entity.Status;
                    contract.Stick = entity.Stick;
                    //contract.InUser = entity.InUser;
                    //contract.InDate = entity.InDate;
                    model.NewsList.Add(contract);
                });
            }

            return model;
        }

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public NewsInfo GetNews(int newsId, int projectId)
        {
            var model = new NewsInfo();
            var query = from n in Context.News
                join su in Context.Sys_User on n.InUser equals su.UserID
                join e in Context.Employee on su.EmployeeID equals e.EmployeeID
                where
                    n.NewsID == newsId && n.Status == 3 && n.PublishTime <= DateTime.Now &&
                    (n.ProjectID == projectId || n.ProjectID == 0) && n.NewsTarget == 1
                select new
                {
                    n.NewsID,
                    n.NewsTitle,
                    n.NewsContent,
                    n.NewsBrief,
                    n.NewsCover,
                    n.PublishTime,
                    n.Status,
                    n.InUser,
                    n.InDate,
                    n.Stick,
                    e.EmployeeName,
                    LastNews = (from ln in Context.News
                                where ln.NewsID < newsId && ln.Status == 3 && ln.PublishTime <= DateTime.Now && (ln.ProjectID == projectId || ln.ProjectID == 0) && ln.NewsTarget == 1
                                select new {ln.NewsID, ln.NewsTitle}).FirstOrDefault(),
                    NextNews = (from ln in Context.News
                                where ln.NewsID > newsId && ln.Status == 3 && ln.PublishTime <= DateTime.Now && (ln.ProjectID == projectId || ln.ProjectID == 0) && ln.NewsTarget == 1
                                select new {ln.NewsID, ln.NewsTitle}).FirstOrDefault()
                };
            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.NewsID = entity.NewsID;
                model.NewsTitle = entity.NewsTitle;
                model.NewsContent = entity.NewsContent;
                model.NewsBrief = entity.NewsBrief;
                model.NewsCover = entity.NewsCover;
                model.PublishTime = entity.PublishTime;
                //model.Status = entity.Status;
                model.Stick = entity.Stick;
                //model.InUser = entity.InUser;
                //model.InDate = entity.InDate;
                model.EmployeeName = entity.EmployeeName;
                if (entity.LastNews != null)
                {
                    model.LastNewsID = entity.LastNews.NewsID;
                    model.LastNewsTitle = entity.LastNews.NewsTitle;
                }
                if (entity.NextNews != null)
                {
                    model.NextNewsID = entity.NextNews.NewsID;
                    model.NextNewsTitle = entity.NextNews.NewsTitle;
                }
            }
            return model;
        }
    }
}
