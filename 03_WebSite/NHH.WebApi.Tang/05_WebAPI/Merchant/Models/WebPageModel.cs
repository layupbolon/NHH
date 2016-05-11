using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHH.Models.Common;
using NHH.Models.Operations;

namespace NHH.WebAPI.Merchant.Models
{
    public class WebPageModel
    {
        /// <summary>
        /// Banner列表
        /// </summary>
        public List<BannerMaster> BannerList { get; set; }
        /// <summary>
        /// 新闻列表
        /// </summary>
        public NewsListModel NewsList { get; set; }
    }
}