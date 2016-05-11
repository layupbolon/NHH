using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Campaign;
using NHH.Models.Common;
using NHH.Service.Campaign.IService;

namespace NHH.Service.Campaign
{
    public class CampaignInfoService : NHHService<NHHEntities>, ICampaignInfoService
    {
        /// <summary>
        /// 获取当前项目中的所有活动列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public CampaignListModel GetCampaignList(CampaignListQuery queryInfo)
        {
            var model = new CampaignListModel(queryInfo.Page, queryInfo.Size);
            model.CampaignList = new List<CampaignInfo>();

            var query = from c in Context.Campaign
                join cpe in Context.Campaign_Page on c.CampaignID equals cpe.CampaignID
                //join pj in Context.Project on c.ProjectID equals pj.ProjectID
                //join cp in Context.Company on pj.ManageCompanyID equals cp.CompanyID
                where c.Status == 1 && cpe.PublishStatus == 2 && cpe.PageType == 1 
                //&& (from ct in Context.Contract
                //    where ct.Status == 1 && ct.MerchantID == queryInfo.MerchantID
                //    select ct.ProjectID).Contains(c.ProjectID)
                select new
                {
                    c.CampaignID,
                    c.CampaignName,
                    c.CampaignType,
                    c.CampaignStatus,
                    c.ProjectID,
                    c.StartDate,
                    c.EndDate,
                    c.InDate,
                    c.Status,
                    cpe.PageCover,
                    //cpe.PageContent,
                    cpe.PageTitle
                    //TitlePic = (from ca in Context.Campaign_Attachment
                    //    where ca.AttachmentType == 5 && ca.Status == 1
                    //    orderby ca.AttachmentID descending
                    //    select ca.AttachmentPath).FirstOrDefault(),
                    //IsMobileHTML =
                    //    (Context.Campaign_Attachment.Count(
                    //        item => item.CampaignID == c.CampaignID && item.Status == 1 && item.AttachmentType == 4))
                };
            #region 查询条件
            //只取有上传手机HTML附件的
            //query = query.Where(item => item.IsMobileHTML > 0);
            //if (queryInfo.StoreID != 0) //如果当前用户有StoreID，
            //{
            //    query = query.Where(item => (from ms in Context.Merchant_Store
            //        join ct in Context.Contract on ms.RentContractID equals ct.ContractID
            //        where ct.Status == 1 && ms.Status == 1 && ms.StoreID == queryInfo.StoreID
            //        select ct.ProjectID).Contains(item.ProjectID));
            //}
            //else
            //{
            //    query = query.Where(item => (from ms in Context.Merchant_Store
            //        join ct in Context.Contract on ms.RentContractID equals ct.ContractID
            //        where ct.Status == 1 && ms.Status == 1 && ms.MerchantID == queryInfo.MerchantID
            //        select ct.ProjectID).Contains(item.ProjectID));
            //}
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(item => item.CampaignStatus == queryInfo.Status.Value);
            }
            if (queryInfo.BeginTime.HasValue)
            {
                query = query.Where(item => item.InDate >= queryInfo.BeginTime.Value);
            }
            if (queryInfo.EndTime.HasValue)
            {
                query = query.Where(item => item.InDate <= queryInfo.EndTime.Value);
            }
            #endregion 查询条件

            model.PagingInfo.TotalCount = query.Count();
            #region 排序

            switch (queryInfo.Sort)
            {
                case 1:
                    query = query.OrderByDescending(item => item.InDate).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
                case 2:
                    query = query.OrderBy(item => item.Status).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
                default:
                    query = query.OrderByDescending(item => item.InDate).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
            }
            #endregion 排序

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new CampaignInfo();
                    contract.CampaignID = entity.CampaignID;
                    contract.CampaignName = entity.CampaignName;
                    contract.CampaignType = entity.CampaignType;
                    contract.CampaignStatus = entity.CampaignStatus;
                    contract.InDate = entity.InDate;
                    //contract.PageContent = entity.PageContent;
                    contract.PageCover = NHH.Framework.Utility.UrlHelper.GetImageUrl(entity.PageCover);
                    contract.PageTitle = entity.PageTitle;
                    model.CampaignList.Add(contract);
                });
            }

            return model;
        }

        /// <summary>
        /// 获取指定的活动内容
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public CampaignInfo GetCampaignInfo(int campaignId)
        {
            CampaignInfo model = null;
            var query=from c in Context.Campaign
                      join cpe in Context.Campaign_Page on c.CampaignID equals cpe.CampaignID
                      join su in Context.Sys_User on c.InUser equals su.UserID
                      join e in Context.Employee on su.EmployeeID equals e.EmployeeID
                      join cp in Context.Company on e.CompanyID equals cp.CompanyID
                      //join pj in Context.Project on c.ProjectID equals pj.ProjectID
                      //join cp in Context.Company on pj.ManageCompanyID equals cp.CompanyID
                      where c.Status == 1 && c.CampaignID == campaignId && cpe.PublishStatus == 2 && cpe.PageType == 1
                      select new
                      {
                          c.CampaignID,
                          c.CampaignName,
                          c.CampaignType,
                          c.CampaignStatus,
                          c.ProjectID,
                          c.StartDate,
                          c.EndDate,
                          c.InDate,
                          c.Status,
                          ManageCompanyName = cp.BriefName,
                          cpe.PageCover,
                          cpe.PageContent,
                          cpe.PageTitle
                          //MobileContentHtmlPath = (from ca in Context.Campaign_Attachment where ca.AttachmentType == 4 && ca.Status == 1 orderby ca.AttachmentID descending select ca.AttachmentPath).FirstOrDefault()
                      };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model = new CampaignInfo();
                model.CampaignID = entity.CampaignID;
                model.CampaignName = entity.CampaignName;
                model.CampaignType = entity.CampaignType;
                model.CampaignStatus = entity.CampaignStatus;
                model.InDate = entity.InDate;
                model.ManageCompanyName = entity.ManageCompanyName;
                model.PageTitle = entity.PageTitle;
                model.PageCover = NHH.Framework.Utility.UrlHelper.GetImageUrl(entity.PageCover);
                model.PageContent = entity.PageContent;
                model.StartDate = entity.StartDate;
                model.EndDate = entity.EndDate;
            }
            return model;
        }

        /// <summary>
        /// 添加企划活动点评
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCampaignComment(CampaignCommentInfo model)
        {
            var instance = CreateBizObject<Campaign_Comment>();
            var entity = new Campaign_Comment()
            {
                CampaignID = model.CampaignID,
                Overall = model.Overall,
                CommentContent = model.CommentContent,
                CommentUserID = model.CommentUserID,
                CommentUserName = model.CommentUserName,
                InUser = model.InUser,
                InDate = DateTime.Now,
                EditUser = model.EditUser,
                EditDate = DateTime.Now
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.CommentID = entity.CommentID;
            return model.CommentID > 0;
        }
    }
}
