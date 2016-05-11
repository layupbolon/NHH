using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.Service.Official
{
    public class ProjectService : NHHService<NHHEntities>, IProjectService
    {
        /// <summary>
        /// 获取项目详细页顶部banner
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public string GetProjectBanner(int projectID)
        {
            var query = from b in Context.Banner
                        join bi in Context.BannerInfo on b.BannerID equals bi.BannerID
                        where
                            b.BannerTarget == 2 && b.ProjectID == projectID &&
                            b.LocationID == (int)LocationEnum.官网项目明细页顶部banner && b.Status == 1
                        select new Models.Official.BannerInfo()
                        {
                            Title = bi.Title,
                            Content = bi.Content,
                            BackgroundImageUrl = bi.ResourcePath,
                            Link = bi.Link,
                            Seq = bi.Seq
                        };

            if (query.Any())
            {
                var list = query.OrderBy(a => a.Seq).ToList();
                var sb = new StringBuilder();
                list.ForEach(info =>
                {
                    sb.AppendFormat("<li><a href=\"#\"><img src=\"{0}\" alt=\"\" /></a></li>",
                        Framework.Utility.UrlHelper.GetImageUrl(info.BackgroundImageUrl));
                });

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectInfo GetProjectInfo(int projectID)
        {
            var model = from p in Context.NHHCG_Project
                join dc in Context.Dictionary.Where(a => a.FieldType == "NHHCG_BusinessType") on p.BusinessType equals
                    dc.FieldValue
                where p.ProjectID == projectID
                select new ProjectInfo()
                {
                    ProjectName = p.ProjectName,
                    ProjectBriefName = p.BriefName,
                    Feature = p.Feature,
                    OperatingArea = p.OperatingArea,
                    GrossArea = p.GrossArea,
                    OpeningDate = p.OpeningDate,
                    Population = p.Population,
                    BusinessType = dc.FieldName,
                    MonthlyHumanTraffic = p.MonthlyHumanTraffic,
                    Position = p.Position,
                    PublicTransport = p.PublicTransport,
                    Tel = p.Tel,
                    Introduce = p.Introduce,
                    Longitude = p.Longitude.HasValue ? p.Longitude.Value : 0,
                    Latitude = p.Latitude.HasValue ? p.Latitude.Value : 0,
                    SEO_Title = p.SEO_Title,
                    Keywords = p.KeyWords,
                    Description = p.Description
                };

            return model.FirstOrDefault();
        }

        /// <summary>
        /// 获取项目图片
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public string GetProjectPic(int projectID)
        {
            var query = from p in Context.NHHCG_Pic
                        where p.RefID == projectID && p.Type == 101
                        select new
                        {
                            p.Path,
                            p.Seq
                        };

            if (query.Any())
            {
                var list = query.OrderBy(a => a.Seq).ToList();
                var sb = new StringBuilder();

                list.ForEach(info =>
                {
                    sb.AppendFormat(
                        "<li><a href=\"{0}\" class=\"fancybox\" rel=\"gallery1\" ><img src=\"{0}\" alt=\"\" /></a></li>",
                        Framework.Utility.UrlHelper.GetImageUrl(info.Path));
                });

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取项目活动
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public string GetProjectCampaign(int projectID)
        {
            var query = from c in Context.Campaign
                        join cp in Context.Campaign_Page on c.CampaignID equals cp.CampaignID
                        where cp.PageType == 2 && c.ProjectID == projectID && cp.PublishStatus == 2
                        select new
                        {
                            cp.PageID,
                            c.CampaignID,
                            cp.PageTitle,
                            cp.PageCover,
                            cp.PageContent
                        };

            if (query.Any())
            {
                var list = query.ToList();
                var sb = new StringBuilder();

                list.ForEach(info =>
                {
                    sb.AppendLine("<li class=\"th3-column fl htSection\">");
                    sb.AppendFormat("<a href=\"{0}\" target=\"_blank\">", string.Format("CampaignDetail.aspx?pageID={0}", info.PageID));
                    sb.AppendFormat("<img src=\"{0}\">", Framework.Utility.UrlHelper.GetImageUrl(info.PageCover));
                    sb.AppendLine("<div class=\"txt-ad mask-content mask-content-small\">");
                    sb.AppendFormat("<h3 class=\"tit-30\">{0}</h3>", info.PageTitle);
                    sb.AppendFormat("<p>{0}</p>", info.PageContent);
                    sb.AppendFormat("<span class=\"cmnBtn btnB mt30\" href=\"{0}\">查看详情</span>", string.Format("CampaignDetail.aspx?pageID={0}", info.PageID));
                    sb.AppendLine("</div><i class=\"mask-bg\"></i></a></li>");
                });

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取官网项目列表
        /// </summary>
        /// <returns></returns>
        public string GetProjectList(int projectID)
        {
            var query = Context.NHHCG_Project.Where(a => a.Status == 1).Select(a => new { a.ProjectName, a.ProjectID });

            if (!query.Any()) return string.Empty;

            var list = query.ToList();
            var sb = new StringBuilder();
            int i = 1;
            list.ForEach(info =>
            {
                sb.AppendFormat(
                    "<option value=\"{0}\" {1}>{0}</option>", info.ProjectName,
                    projectID == info.ProjectID ? "selected=\"selected\"" : string.Empty);
                i++;
            });

            return sb.ToString();
        }

        /// <summary>
        /// 获取项目详细页顶部banner数量
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int GetProjectBannerCount(int projectID)
        {
            var query = from b in Context.Banner
                        join bi in Context.BannerInfo on b.BannerID equals bi.BannerID
                        where
                            b.BannerTarget == 2 && b.ProjectID == projectID &&
                            b.LocationID == (int)LocationEnum.官网项目明细页顶部banner && b.Status == 1
                        select new Models.Official.BannerInfo()
                        {
                            Title = bi.Title,
                            Content = bi.Content,
                            BackgroundImageUrl = bi.ResourcePath,
                            Link = bi.Link,
                            Seq = bi.Seq
                        };

            return query.Count();
        }
    }
}
