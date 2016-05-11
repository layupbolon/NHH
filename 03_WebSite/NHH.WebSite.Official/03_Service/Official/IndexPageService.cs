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
    public class IndexPageService : NHHService<NHHEntities>, IIndexPageService
    {
        /// <summary>
        /// 获取官网首页顶部banner
        /// </summary>
        /// <returns></returns>
        public string GetTopBanner()
        {
            var query = from b in Context.Banner
                        join bi in Context.BannerInfo on b.BannerID equals bi.BannerID
                        where b.BannerTarget == 2 && b.LocationID == (int)LocationEnum.官网首页顶部Banner && b.Status == 1
                        //目标：官网 ；位置：官网首页顶部；状态：有效
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
                int i = 1;
                list.ForEach(info =>
                {
                    sb.AppendLine("<li>");
                    sb.AppendFormat("<a href=\"{0}\">", info.Link);
                    switch (i)
                    {
                        case 1:
                        case 2:
                            sb.AppendLine("<div class=\"top-text top-text3\">");
                            break;
                        case 3:
                            sb.AppendLine("<div class=\"top-text top-text2\">");
                            break;
                        case 4:
                            sb.AppendLine("<div class=\"top-text top-text\">");
                            break;
                    }
                    sb.AppendFormat("<h3>{0}</h3>", info.Title);
                    sb.AppendFormat("<p>{0}</p>", info.Content);
                    sb.AppendLine("</div>");
                    sb.AppendFormat("<div class=\"bgImg\" style=\"background-image: url({0});\">", string.IsNullOrEmpty(info.BackgroundImageUrl) ? "#" :
                        Framework.Utility.UrlHelper.GetImageUrl(info.BackgroundImageUrl));
                    sb.AppendLine("<!--[if lt IE 9]><img src=\"img/banner3.jpg\"><![endif]--></div></a>");
                    sb.AppendLine("</li>");

                    i++;
                });

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 通过locationID获取banner
        /// </summary>
        /// <param name="location">4：官网首页第二屏  6：官网首页第三屏</param>
        /// <returns></returns>
        public string GetBannerByLocationID(LocationEnum location)
        {
            var query = from b in Context.Banner
                        join bi in Context.BannerInfo on b.BannerID equals bi.BannerID
                        where b.BannerTarget == 2 && b.LocationID == (int)location && b.Status == 1
                        //目标：官网 ；状态：有效
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
                int i = 1;
                list.ForEach(info =>
                {
                    sb.AppendLine(i == 1
                        ? "<div class=\"two-column u-half fl\">"
                        : "<div class=\"two-column u-half u-half-h fl\">");
                    sb.AppendFormat("<a href=\"{0}\" target=\"_blank\" class=\"hoverArea\">", info.Link);
                    sb.AppendFormat("<div class=\"adbg\" style=\"background-image: url({0});\"></div>", string.IsNullOrEmpty(info.BackgroundImageUrl) ? "#" :
                        Framework.Utility.UrlHelper.GetImageUrl(info.BackgroundImageUrl));
                    sb.AppendLine("<div class=\"panel-content\">");
                    sb.AppendFormat("<h3>{0}</h3>", info.Title);
                    sb.AppendFormat("<blockquote class=\"mt10\">{0}</blockquote>", info.Content);
                    sb.AppendLine(
                        "<span class=\"arrow\"></span></div><div class=\"panel-mask panel-mask-small\"></div></a></div>");

                    i++;
                });

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取底部banner
        /// </summary>
        /// <returns></returns>
        public string GetLastBanner()
        {
            var query = from b in Context.Banner
                        join bi in Context.BannerInfo on b.BannerID equals bi.BannerID
                        where b.BannerTarget == 2 && b.LocationID == (int)LocationEnum.官网首页底部Banner && b.Status == 1
                        //目标：官网 ；状态：有效
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
                var list = query.OrderBy(a => a.Seq).Take(2).ToList();
                var sb = new StringBuilder();
                int i = 1;
                list.ForEach(info =>
                {
                    sb.AppendLine(i == 1
                        ? "<div class=\"two-column u-half fl\">"
                        : "<div class=\"two-column u-half u-half-h fl\">");
                    sb.AppendFormat("<a href=\"{0}\" target=\"_blank\" class=\"hoverArea\">", info.Link);
                    sb.AppendFormat("<div class=\"adbg\" style=\"background-image: url({0});\"></div>", string.IsNullOrEmpty(info.BackgroundImageUrl) ? "#" :
                        Framework.Utility.UrlHelper.GetImageUrl(info.BackgroundImageUrl));
                    sb.AppendLine("<div class=\"panel-content\">");
                    sb.AppendFormat("<h3>{0}</h3>", info.Title);
                    sb.AppendFormat("<blockquote class=\"mt10\">{0}</blockquote>", info.Content);
                    sb.AppendLine(
                        "<span class=\"arrow\"></span></div><div class=\"panel-mask panel-mask-small\"></div></a></div>");

                    i++;
                });

                sb.AppendLine("<div class=\"two-column u-half u-half-h fl\">");
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\" class=\"hoverArea\">", "NewsList.aspx");
                sb.AppendFormat("<div class=\"adbg\" style=\"background-image: url(img/temp/default_img.jpg);\"></div>");
                sb.AppendLine("<div class=\"panel-content default-pic-text\">");
                sb.AppendLine("<h3>即刻前往，发现唐人中心所有即时资讯</h3>");
                sb.AppendLine("</div></a></div>");

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取官网首页顶部banner数量
        /// </summary>
        /// <returns></returns>
        public int GetTopBannerCount()
        {
            var query = from b in Context.Banner
                        join bi in Context.BannerInfo on b.BannerID equals bi.BannerID
                        where b.BannerTarget == 2 && b.LocationID == (int)LocationEnum.官网首页顶部Banner && b.Status == 1
                        //目标：官网 ；位置：官网首页顶部；状态：有效
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
