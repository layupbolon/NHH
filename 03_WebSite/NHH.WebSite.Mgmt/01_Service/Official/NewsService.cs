using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Web;
using NHH.Models.Approve;
using NHH.Models.Common;
using NHH.Models.Official;
using NHH.Service.Approve;
using NHH.Service.Official.IService;

namespace NHH.Service.Official
{
    public class NewsService : NHHService<NHHEntities>, INewsService
    {
        /// <summary>
        /// 提测 智能平台消息模板
        /// </summary>
        private const string AuditNotify_MgmtMessage = "News_AuditNotify_MgmtMessage";

        /// <summary>
        /// 审核通过 智能平台消息模板
        /// </summary>
        private const string PassNotify_MgmtMessage = "News_PassNotify_MgmtMessage";

        /// <summary>
        /// 审核驳回 智能平台消息模板
        /// </summary>
        private const string FailNotify_MgmtMessage = "News_FailNotify_MgmtMessage";

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="newsQueryInfo"></param>
        /// <returns></returns>
        public NewsListModel GetNewsListModel(NewsQueryInfo newsQueryInfo)
        {
            var model = new NewsListModel();
            model.QueryInfo = newsQueryInfo;
            model.NewsModelList = new List<NewsModel>();

            var query = from n in Context.News
                        join dc in Context.Dictionary.Where(d => d.FieldType == "NewsType") on n.NewsType equals dc.FieldValue
                        join dc2 in Context.Dictionary.Where(d => d.FieldType == "NewsTarget") on n.NewsTarget equals
                            dc2.FieldValue
                        join c in Context.Company on n.CompanyID equals c.CompanyID
                        join p in Context.Project on n.ProjectID equals p.ProjectID into queryResult
                        from p in queryResult.DefaultIfEmpty()
                        join nhhp in Context.NHHCG_Project on n.ProjectID equals nhhp.ProjectID into queryResult2
                        from nhhp in queryResult2.DefaultIfEmpty()
                        join dc3 in Context.Dictionary.Where(d => d.FieldType == "NewsStatus") on n.Status equals dc3.FieldValue
                        select new
                        {
                            n.NewsID,
                            n.NewsType,
                            NewsTypeName = dc.FieldName,
                            n.NewsTarget,
                            NewsTargetName = dc2.FieldName,
                            n.NewsTitle,
                            n.CompanyID,
                            c.CompanyName,
                            n.ProjectID,
                            p.ProjectName,
                            NHHCGProjectName = nhhp.ProjectName,
                            n.Status,
                            NewsStatus = dc3.FieldName,
                            n.Stick,
                            n.PublishTime,
                            n.InUser,
                            n.InDate
                        };

            #region 查询条件

            if (newsQueryInfo.NewsType.HasValue)
            {
                query = query.Where(q => q.NewsType == newsQueryInfo.NewsType.Value);
            }
            if (newsQueryInfo.NewsTarget.HasValue)
            {
                query = query.Where(q => q.NewsTarget == newsQueryInfo.NewsTarget.Value);
            }
            if (!string.IsNullOrEmpty(newsQueryInfo.NewsTitle))
            {
                query = query.Where(q => q.NewsTitle.Contains(newsQueryInfo.NewsTitle));
            }
            if (newsQueryInfo.ProjectID.HasValue)
            {
                query = query.Where(q => q.ProjectID == newsQueryInfo.ProjectID.Value);
            }
            if (newsQueryInfo.NewsStatus.HasValue)
            {
                query = query.Where(q => q.Status == newsQueryInfo.NewsStatus.Value);
            }
            if (newsQueryInfo.PubStartTime.HasValue)
            {
                query = query.Where(q => q.PublishTime >= newsQueryInfo.PubStartTime.Value);
            }
            if (newsQueryInfo.PubEndTime.HasValue)
            {
                query = query.Where(q => q.PublishTime <= newsQueryInfo.PubEndTime.Value);
            }
            if (newsQueryInfo.InStartTime.HasValue)
            {
                query = query.Where(q => q.InDate >= newsQueryInfo.InStartTime.Value);
            }
            if (newsQueryInfo.InEndTime.HasValue)
            {
                query = query.Where(q => q.InDate <= newsQueryInfo.InEndTime.Value);
            }

            #endregion

            //分页信息
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = newsQueryInfo.Page ?? 1;
            model.PagingInfo.TotalCount = query.Count();

            query =
                query.OrderByDescending(q => q.Stick)
                    .ThenByDescending(q => q.NewsID)
                    .Skip(model.PagingInfo.SkipNum)
                    .Take(model.PagingInfo.TakeNum);
            var entityList = query.ToList();
            if (!entityList.Any())
            {
                return model;
            }

            entityList.ForEach(entity =>
            {
                model.NewsModelList.Add(new NewsModel()
                {
                    NewsID = entity.NewsID,
                    NewsType = entity.NewsType,
                    NewsTypeName = entity.NewsTypeName,
                    NewsTarget = entity.NewsTarget,
                    NewsTargetName = entity.NewsTargetName,
                    NewsTitle = entity.NewsTitle,
                    CompanyID = entity.CompanyID,
                    CompanyName = entity.CompanyName,
                    ProjectID = entity.ProjectID,
                    ProjectName = entity.NewsTarget == 1 ? entity.ProjectName : entity.NewsTarget == 2 ? entity.NHHCGProjectName : string.Empty,
                    Status = entity.Status,
                    NewsStatus = entity.NewsStatus,
                    Stick = entity.Stick,
                    PublishTime = entity.PublishTime,
                    InUser = entity.InUser,
                    InDate = entity.InDate
                });
            });

            return model;
        }

        /// <summary>
        /// 置顶新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool StickNews(int? newsID, int userID)
        {
            return StickOrNot(newsID, true, userID);
        }

        /// <summary>
        /// 取消置顶
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool UnStickNews(int? newsID, int userID)
        {
            return StickOrNot(newsID, false, userID);
        }

        /// <summary>
        /// 发布新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool PublishNews(int? newsID, int userID)
        {
            if (newsID.HasValue)
            {
                var instance = CreateBizObject<News>();
                var news = instance.TopOne(a => a.NewsID == newsID);
                news.PublishTime = DateTime.Now;
                news.EditUser = userID;
                news.EditDate = DateTime.Now;
                instance.Update(news);
                SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 作废新闻
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public bool CancelNews(int? newsID, int currentUserID)
        {
            if (newsID.HasValue)
            {
                var instance = CreateBizObject<News>();
                var news = instance.TopOne(a => a.NewsID == newsID);
                news.Status = -1;
                news.EditUser = currentUserID;
                news.EditDate = DateTime.Now;
                instance.Update(news);
                SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="newsModel"></param>
        /// <returns></returns>
        public bool AddNews(NewsModel newsModel)
        {
            var publishTime = (from d in Context.Dictionary
                               where d.FieldType == "PublishTime" && d.FieldValue == newsModel.PublishDateTime
                               select d.FieldName).FirstOrDefault();

            using (var trans = Context.Database.BeginTransaction())
            {
                var instance = CreateBizObject<News>();
                var entity = new News()
                {
                    NewsType = newsModel.NewsType,
                    NewsTarget = newsModel.NewsTarget,
                    NewsTitle = newsModel.NewsTitle,
                    NewsContent = newsModel.NewsContent,
                    NewsBrief = newsModel.NewsBrief,
                    NewsCover = newsModel.NewsCover,
                    CompanyID = newsModel.CompanyID,
                    ProjectID = newsModel.ProjectID,
                    PublishTime = DateTime.Parse(newsModel.PublishDate + " " + publishTime),
                    Status = 1,
                    Stick = newsModel.Stick,
                    InUser = newsModel.UserID,
                    InDate = DateTime.Now
                };

                instance.Insert(entity);
                SaveChanges();

                SubmitAudit(entity.NewsID, newsModel.UserID);

                trans.Commit();
            }

            return true;
        }

        /// <summary>
        /// 获取新闻详细
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        public NewsModel GetNewsDetail(int newsID, int configType = (int)ConfigTypeEnum.新闻)
        {
            var query = from n in Context.News
                        join dc in Context.Dictionary.Where(d => d.FieldType == "NewsType") on n.NewsType equals dc.FieldValue
                        join dc2 in Context.Dictionary.Where(d => d.FieldType == "NewsTarget") on n.NewsTarget equals
                            dc2.FieldValue
                        join c in Context.Company on n.CompanyID equals c.CompanyID
                        join p in Context.Project on n.ProjectID equals p.ProjectID into queryResult
                        from p in queryResult.DefaultIfEmpty()
                        join nhhp in Context.NHHCG_Project on n.ProjectID equals nhhp.ProjectID into queryResult2
                        from nhhp in queryResult2.DefaultIfEmpty()
                        where n.NewsID == newsID
                        select new
                        {
                            n.NewsID,
                            n.NewsType,
                            NewsTypeName = dc.FieldName,
                            n.NewsTarget,
                            NewsTargetName = dc2.FieldName,
                            n.NewsTitle,
                            n.NewsContent,
                            n.NewsBrief,
                            n.NewsCover,
                            n.CompanyID,
                            c.CompanyName,
                            n.ProjectID,
                            p.ProjectName,
                            NHHProjectName = nhhp.ProjectName,
                            n.Status,
                            n.Stick,
                            n.PublishTime,
                            n.InUser,
                            n.InDate
                        };

            var entity = query.FirstOrDefault();

            var time = entity.PublishTime.ToShortTimeString();
            var publishTime = (from d in Context.Dictionary
                               where d.FieldType == "PublishTime" && d.FieldName == time
                               select d.FieldValue).FirstOrDefault();

            var model = new NewsModel
            {
                NewsID = entity.NewsID,
                NewsType = entity.NewsType,
                NewsTypeName = entity.NewsTypeName,
                NewsTarget = entity.NewsTarget,
                NewsTargetName = entity.NewsTargetName,
                NewsTitle = entity.NewsTitle,
                NewsContent = entity.NewsContent,
                NewsBrief = entity.NewsBrief,
                NewsCover = entity.NewsCover,
                CompanyID = entity.CompanyID,
                CompanyName = entity.CompanyName,
                ProjectID = entity.ProjectID,
                ProjectName = entity.NewsTarget == 1 ? entity.ProjectName : entity.NewsTarget == 2 ? entity.NHHProjectName : string.Empty,
                Status = entity.Status,
                Stick = entity.Stick,
                PublishTime = entity.PublishTime,
                PublishDate = entity.PublishTime.ToShortDateString(),
                PublishDateTime = publishTime,
                InUser = entity.InUser,
                InDate = entity.InDate,
                ApproveRecordInfos = new ApproveRecordListInfo
                {
                    ApproveRecordInfos = new ApproveService().GetDocumentApproveProcess(newsID, configType)
                },
                CurrentApprover = GetCurrentApprover(newsID)
            };

            return model;
        }

        /// <summary>
        /// 编辑新闻
        /// </summary>
        /// <param name="newsModel"></param>
        /// <returns></returns>
        public bool EditNews(NewsModel newsModel)
        {
            var publishTime = (from d in Context.Dictionary
                               where d.FieldType == "PublishTime" && d.FieldValue == newsModel.PublishDateTime
                               select d.FieldName).FirstOrDefault();

            using (var trans = Context.Database.BeginTransaction())
            {
                var instance = CreateBizObject<News>();
                var model = instance.TopOne(a => a.NewsID == newsModel.NewsID);

                model.NewsType = newsModel.NewsType;
                model.NewsTarget = newsModel.NewsTarget;
                model.NewsTitle = newsModel.NewsTitle;
                model.NewsContent = newsModel.NewsContent;
                model.NewsBrief = newsModel.NewsBrief;
                model.NewsCover = newsModel.NewsCover;
                model.CompanyID = newsModel.CompanyID;
                model.ProjectID = newsModel.ProjectID;
                model.Stick = newsModel.Stick;
                model.PublishTime = DateTime.Parse(newsModel.PublishDate + " " + publishTime);
                model.EditUser = newsModel.UserID;
                model.EditDate = DateTime.Now;

                instance.Update(model);
                SaveChanges();

                SubmitAudit(model.NewsID, newsModel.UserID);

                UpdateNewsStatus(newsModel.NewsID, 1, newsModel.UserID);

                trans.Commit();
            }

            return true;
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="stickOrNot"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        private bool StickOrNot(int? newsID, bool stickOrNot, int userID)
        {
            if (newsID.HasValue)
            {
                var instance = CreateBizObject<News>();
                var news = instance.TopOne(a => a.NewsID == newsID);
                news.Stick = stickOrNot ? 1 : 0;
                news.EditUser = userID;
                news.EditDate = DateTime.Now;
                instance.Update(news);
                SaveChanges();
                return true;
            }
            return false;
        }

        public bool Approve(ApproveModel approve)
        {
            ApproveService approveService = new ApproveService();

            using (var trans = Context.Database.BeginTransaction())
            {
                if (!approveService.UpdateApprove(approve))
                    return false;

                var userId = Context.News.Where(a => a.NewsID == approve.RefID).Select(a => a.InUser).FirstOrDefault();

                if (approve.Result == 1)
                {
                    if (approveService.IsApprovePass(approve))
                    {
                        //若审批全部通过，则给提交人发送智能平台提醒
                        AddSysMessage(userId, approve.RefID, PassNotify_MgmtMessage, approve.UserID);
                        //更改业务状态为已审批
                        UpdateNewsStatus(approve.RefID, 3, approve.UserID);
                    }
                }
                else
                {
                    //若驳回，则给提交人发送智能平台提醒
                    AddSysMessage(userId, approve.RefID, FailNotify_MgmtMessage, approve.UserID);
                    //更改业务状态为已驳回
                    UpdateNewsStatus(approve.RefID, 2, approve.UserID);
                }

                trans.Commit();
            }

            return true;
        }

        #region 私有方法

        /// <summary>
        /// 获取单据当前审核人
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        private List<int> GetCurrentApprover(int newsID, int configType = 103)
        {
            try
            {
                var status = Context.News.Where(a => a.NewsID == newsID).Select(a => a.Status).FirstOrDefault();
                if (status != 1)
                    return new List<int>();

                var resultQuery = from ap in Context.Approve_Process
                                  join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                                  where ap.RefID == newsID && ac.ConfigType == configType
                                  select ap.GroupNum;

                if (!resultQuery.Any())
                {
                    return (from config in Context.Approve_Config.Where(a => a.ConfigType == configType)
                            join role in Context.Sys_User_Role on config.RoleID equals role.RoleID
                            select role.UserID).ToList();
                }

                int groupNum = resultQuery.Distinct().Max();

                var query = from ac in Context.Approve_Config
                            join ap in Context.Approve_Process on ac.ConfigID equals ap.ConfigID into queryResult
                            from ap in queryResult.DefaultIfEmpty()
                            where
                                ac.ConfigType == configType && ap.RefID == newsID && ap.GroupNum == groupNum
                            select new
                            {
                                ac.ConfigID,
                                ac.Step,
                                ap.ProcessID,
                                ap.Approver,
                                ap.Result
                            };

                if (!query.Any())
                    return new List<int>();

                int configID = query.Where(a => !a.Approver.HasValue).Min(a => a.ConfigID);
                int roleID = (from con in Context.Approve_Config
                              where con.ConfigID == configID
                              select con.RoleID).FirstOrDefault();

                var result = from ur in Context.Sys_User_Role
                             where ur.RoleID == roleID
                             select ur.UserID;

                return result.ToList();
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }

        /// <summary>
        /// 更新新闻状态
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="status"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        private bool UpdateNewsStatus(int newsID, int status, int userID)
        {
            var instance = CreateBizObject<News>();
            var entity = instance.TopOne(a => a.NewsID == newsID);
            entity.Status = status;
            entity.EditUser = userID;
            entity.EditDate = DateTime.Now;
            instance.Update(entity);
            SaveChanges();

            return true;
        }

        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="userID">收到消息的用户ID</param>
        /// <param name="newsId"></param>
        /// <param name="messageKey"></param>
        /// <param name="currentUserID"></param>
        /// <param name="needSaveChanges"></param>
        /// <returns></returns>
        private bool AddSysMessage(int userID, int newsId, string messageKey, int currentUserID, bool needSaveChanges = true)
        {
            var MessageTemplate =
                Context.Message_Template.Where(a => a.TemplateKey == messageKey)
                    .Select(a => a)
                    .FirstOrDefault();

            var result = (from n in Context.News
                          join u in Context.Sys_User on n.InUser equals u.UserID
                          where n.NewsID == newsId
                          select new { u.UserName, n.InDate }).FirstOrDefault();

            var instance = CreateBizObject<Sys_User_Message>();
            instance.Insert(new Sys_User_Message()
            {
                UserID = userID,
                Subject = MessageTemplate.Title.Replace("#USERNAME#", result.UserName),
                Content =
                    MessageTemplate.Content.Replace("#USERNAME#", result.UserName)
                        .Replace("#INDATE#", result.InDate.ToString())
                        .Replace("#URL#", string.Format("/Official/News/Detail?newsID={0}", newsId)),
                SourceType = 9, //新闻
                SourceRefID = newsId,
                Status = 1,
                InDate = DateTime.Now,
                InUser = currentUserID,
                EditDate = DateTime.Now,
                EditUser = currentUserID
            });
            if (needSaveChanges)
                SaveChanges();

            return true;
        }

        private bool SubmitAudit(int newsId, int currentUserID, int configType = 103)
        {
            var query = from ap in Context.Approve_Process
                        join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                        where ap.RefID == newsId && ac.ConfigType == configType
                        select ap.GroupNum;

            var maxGroupNum = !query.Any() ? 1 : query.Distinct().Max() + 1;

            var instance = CreateBizObject<Approve_Process>();

            Context.Approve_Config.Where(a => a.ConfigType == configType).ToList().ForEach(ac =>
            {
                var entity = new Approve_Process()
                {
                    ConfigID = ac.ConfigID,
                    GroupNum = maxGroupNum,
                    RefID = newsId
                };
                instance.Insert(entity);
            });

            List<int> currentUserIds = GetCurrentApprover(newsId);
            if (currentUserIds.Any())
            {
                currentUserIds.ForEach(userId =>
                {
                    AddSysMessage(userId, newsId, AuditNotify_MgmtMessage, currentUserID, false);
                });
            }

            SaveChanges();

            return true;
        }

        #endregion
    }
}
