using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Campaign;
using NHH.Models.Common;
using NHH.Service.Campaign.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace NHH.Service.Campaign
{
    /// <summary>
    /// 企划活动服务
    /// </summary>
    public class CampaignService : NHHService<NHHEntities>, ICampaignService
    {
        /// <summary>
        /// 获取企划活动列表信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public CampaignListModel GetCampaignList(CampaignQueryInfo queryInfo)
        {
            var model = new CampaignListModel();
            model.CampaignInfoList = new List<CampaignInfo>();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo { PageIndex = queryInfo.Page ?? 1 };

            var query = from cm in Context.Campaign
                        join p in Context.Project on cm.ProjectID equals p.ProjectID into queryResult
                        from p in queryResult.DefaultIfEmpty()
                        join nhhp in Context.NHHCG_Project on cm.ProjectID equals nhhp.ProjectID into queryResult2
                        from nhhp in queryResult2.DefaultIfEmpty()
                        join dtype in Context.Dictionary on cm.CampaignType equals dtype.FieldValue
                        join dstatus in Context.Dictionary on cm.CampaignStatus equals dstatus.FieldValue
                        join cp in Context.Campaign_Page on cm.CampaignID equals cp.CampaignID
                        join dc in Context.Dictionary on cp.PageType equals dc.FieldValue
                        where cm.Status == 1 && dtype.FieldType == "CampaignType" && dstatus.FieldType == "CampaignStatus" && dc.FieldType == "NewsTarget"
                        select new
                        {
                            cm.CampaignID,
                            cm.CampaignCode,
                            cm.CampaignName,
                            cm.CampaignType,
                            cm.CampaignStatus,
                            CampaignTypeName = dtype.FieldName,
                            CampaignStatusName = dstatus.FieldName,
                            cm.StartDate,
                            cm.EndDate,
                            //cm.Project,
                            cm.ProjectID,
                            projectName = p.ProjectName,
                            nhhProjectName = nhhp.ProjectName,
                            cp.PageType,
                            PageTypeName = dc.FieldName,
                            cp.PublishStatus,
                            cp.PageID
                        };

            #region 查询信息

            if (queryInfo.CampaignId.HasValue)
            {
                query = query.Where(m => m.CampaignID == queryInfo.CampaignId);
            }
            if (!string.IsNullOrEmpty(queryInfo.CampaignName))
            {
                query = query.Where(m => m.CampaignName.Contains(queryInfo.CampaignName));
            }
            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(m => m.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.CampaignType.HasValue)
            {
                query = query.Where(m => m.CampaignType == queryInfo.CampaignType);
            }
            if (queryInfo.CampaignStatus.HasValue)
            {
                query = query.Where(m => m.CampaignStatus == queryInfo.CampaignStatus);
            }
            if (queryInfo.StartDate.HasValue)
            {
                query = query.Where(m => m.StartDate <= queryInfo.StartDate.Value);
            }
            if (queryInfo.EndDate.HasValue)
            {
                query = query.Where(m => m.EndDate >= queryInfo.EndDate.Value);
            }
            if (queryInfo.PageType.HasValue)
            {
                query = query.Where(m => m.PageType == queryInfo.PageType.Value);
            }

            #endregion

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList.Any())
            {
                foreach (var entity in entityList)
                {
                    var info = new CampaignInfo
                    {
                        CampaignCode = entity.CampaignCode,
                        CampaignId = entity.CampaignID,
                        CampaignName = entity.CampaignName,
                        ProjectName =
                            entity.PageType == 1
                                ? entity.projectName
                                : entity.PageType == 2 ? entity.nhhProjectName : string.Empty,
                        CampaignTypeName = entity.CampaignTypeName,
                        CampaignStatusName = entity.CampaignStatusName,
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                        PublishStatus = entity.PublishStatus,
                        PageTypeName = entity.PageTypeName,
                        PageID = entity.PageID
                    };

                    model.CampaignInfoList.Add(info);
                }
            }
            return model;
        }

        /// <summary>
        /// 增加企划活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddCampaign(CampaignPlanDetailModel model)
        {
            var instance = CreateBizObject<Entities.Campaign>();
            var entity = new Entities.Campaign();
            entity.ProjectID = model.ProjectId;
            entity.Location = model.Location;
            entity.CampaignName = model.CampaignName;
            entity.StartDate = model.StartDate.Value;
            entity.EndDate = model.EndDate.Value;
            entity.CampaignType = model.CampaignType;
            entity.CampaignStatus = 1;//策划中
            entity.CampaignBrief = model.CampaignBrief;
            entity.Status = 1;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;
            entity.InDate = DateTime.Now;
            entity.InUser = model.UserInfo.UserId;
            entity.SEO_Title = model.SEO_Title;
            entity.KeyWords = model.KeyWords;
            entity.Description = model.Description;

            instance.Insert(entity);
            this.SaveChanges();

            #region 增加活动页面
            model.CampaignId = entity.CampaignID;
            AddOrUpdateCampaignPage(model);
            #endregion
            return entity.CampaignID;
        }

        /// <summary>
        /// 更新企划活动
        /// </summary>
        /// <param name="model"></param>
        public void UpdateCampaign(CampaignPlanDetailModel model)
        {
            var instance = CreateBizObject<Entities.Campaign>();
            var entity = instance.GetBySysNo(model.CampaignId);
            entity.Location = model.Location;
            entity.CampaignName = model.CampaignName;
            entity.StartDate = model.StartDate.Value;
            entity.EndDate = model.EndDate.Value;
            entity.CampaignType = model.CampaignType;
            entity.CampaignBrief = model.CampaignBrief;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;
            entity.SEO_Title = model.SEO_Title;
            entity.KeyWords = model.KeyWords;
            entity.Description = model.Description;
            instance.Update(entity);
            SaveChanges();

            #region 增加活动页面
            AddOrUpdateCampaignPage(model);
            #endregion
        }

        /// <summary>
        /// 增加或者更新企划活动页面
        /// </summary>
        /// <param name="model"></param>
        public void AddOrUpdateCampaignPage(CampaignPlanDetailModel model)
        {
            var instance = CreateBizObject<Campaign_Page>();
            var exist = instance.Exists(m => m.CampaignID == model.CampaignId);

            var publishTime = (from d in Context.Dictionary
                               where d.FieldType == "PublishTime" && d.FieldValue == model.PublishTime
                               select d.FieldName).FirstOrDefault();

            if (exist)
            {
                var entity = instance.TopOne(m => m.CampaignID == model.CampaignId);
                entity.PageTitle = model.PageTitle;
                entity.PageCover = model.PageCover;
                entity.PageContent = model.PageContent;
                entity.PublishTime = DateTime.Parse(model.PublishDate + " " + publishTime);
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.UserInfo.UserId;

                instance.Update(entity);
                SaveChanges();
            }
            else
            {
                var entity = new Campaign_Page
                {
                    CampaignID = model.CampaignId,
                    PageType = model.PageType,
                    PageTitle = model.PageTitle,
                    PageCover = model.PageCover,
                    PageContent = model.PageContent,
                    PublishStatus = 1,
                    PublishTime = DateTime.Parse(model.PublishDate + " " + publishTime),
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = model.UserInfo.UserId,
                    EditDate = DateTime.Now,
                    EditUser = model.UserInfo.UserId
                };
                //未发布状态
                instance.Insert(entity);
                SaveChanges();
            }
        }

        /// <summary>
        /// 企划详情页面
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public CampaignPlanDetailModel GetCampaignPlanDetail(int pageID)
        {
            var model = new CampaignPlanDetailModel();
            var query = from cm in Context.Campaign
                        join cp in Context.Campaign_Page on cm.CampaignID equals cp.CampaignID
                        join p in Context.Project on cm.ProjectID equals p.ProjectID into queryResult
                        from p in queryResult.DefaultIfEmpty()
                        join nhhp in Context.NHHCG_Project on cm.ProjectID equals nhhp.ProjectID into queryResult2
                        from nhhp in queryResult2.DefaultIfEmpty()
                        join ds in Context.Dictionary on cm.CampaignStatus equals ds.FieldValue
                        where cm.Status == 1 && ds.FieldType == "CampaignStatus" && cp.PageID == pageID
                        select new
                        {
                            cm.CampaignID,
                            cm.CampaignCode,
                            cm.CampaignName,
                            cm.Location,
                            cm.CampaignType,
                            CampaignStatusName = ds.FieldName,
                            cm.StartDate,
                            cm.EndDate,
                            cm.CampaignBrief,
                            cm.ProjectID,
                            p.ProjectName,
                            NHHProjectName = nhhp.ProjectName,
                            cp.PageTitle,
                            cp.PublishTime,
                            cp.PublishStatus,
                            cp.PageContent,
                            cp.PageCover,
                            cp.PageType,
                            cm.SEO_Title,
                            cm.KeyWords,
                            cm.Description
                        };

            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                model.CampaignId = entity.CampaignID;
                model.CampaignCode = entity.CampaignCode;
                model.CampaignName = entity.CampaignName;
                model.Location = entity.Location;
                model.CampaignType = entity.CampaignType;
                model.CampaignStatusName = entity.CampaignStatusName;
                model.StartDate = entity.StartDate;
                model.EndDate = entity.EndDate;
                model.CampaignBrief = entity.CampaignBrief;
                model.ProjectName = entity.PageType == 1
                    ? entity.ProjectName
                    : entity.PageType == 2 ? entity.NHHProjectName : string.Empty;
                model.ProjectId = entity.ProjectID;
                model.PageTitle = entity.PageTitle;
                model.PublishDate = entity.PublishTime.Value.ToShortDateString();

                var time = entity.PublishTime.Value.ToShortTimeString();
                var publishTime = (from d in Context.Dictionary
                                   where d.FieldType == "PublishTime" && d.FieldName == time
                                   select d.FieldValue).FirstOrDefault();

                model.PublishTime = publishTime;

                model.PublishStatus = entity.PublishStatus;
                model.PublishStatusName = (from dc in Context.Dictionary
                                           where dc.FieldType == "PublishStatus" && dc.FieldValue == entity.PublishStatus
                                           select dc.FieldName
                                           ).FirstOrDefault();
                model.PageCover = entity.PageCover;
                model.PageContent = entity.PageContent;
                model.PageType = entity.PageType;
                model.SEO_Title = entity.SEO_Title;
                model.KeyWords = entity.KeyWords;
                model.Description = entity.Description;
            }

            return model;
        }

    }
}