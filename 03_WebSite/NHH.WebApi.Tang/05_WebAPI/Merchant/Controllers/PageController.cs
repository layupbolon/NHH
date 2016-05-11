using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Operations;
using NHH.Service.Common.IService;
using NHH.Service.Operations.IService;
using NHH.WebAPI.Merchant.Common;
using NHH.WebAPI.Merchant.Models;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class PageController : NHHController
    {
        #region Service
        private ICommonService commonService;
        public ICommonService CommonService
        {
            get
            {
                if (commonService == null)
                {
                    commonService = this.GetService<ICommonService>();
                }
                return commonService;
            }
        }

        private INewsService newsService;
        public INewsService NewsService
        {
            get
            {
                if (newsService == null)
                {
                    newsService = this.GetService<INewsService>();
                }
                return newsService;
            }
        }
        #endregion

        /// <summary>
        /// 获取首页相关数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("page/index")]
        //[NHHActionLog]
        public JsonResult GetIndexPage()
        {
            int pageId = 1;
            int projectId = NHHAPIContext.Current.ProjectID;
            WebPageModel result = new WebPageModel();
            result.BannerList = CommonService.GetBannerList(projectId, pageId);
            result.NewsList = NewsService.GetNewsList(new NewsListQuery() {Page = 1, ProjectID = projectId, Size = 2});
            return Json(result);
        }
    }
}
