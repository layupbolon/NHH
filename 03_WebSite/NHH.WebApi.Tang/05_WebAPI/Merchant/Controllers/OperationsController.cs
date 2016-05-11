using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Operations;
using NHH.Service.Operations.IService;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class OperationsController : NHHController
    {
        #region Service
        private IIntentionService intentionService;
        public IIntentionService IntentionService
        {
            get
            {
                if (intentionService == null)
                {
                    intentionService = this.GetService<IIntentionService>();
                }
                return intentionService;
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

        private IComplaintService complaintService;
        public IComplaintService ComplaintService
        {
            get { return complaintService ?? (complaintService = GetService<IComplaintService>()); }
        }

        #endregion
        #region 入驻意向
        /// <summary>
        /// 添加入驻意向
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("operations/intention/add")]
        public JsonResult AddIntention(IntentionInfo model)
        {
            ApiResult result = null;
            #region 验证
            if (string.IsNullOrEmpty(model.ContactName)) { return Json(new ApiResult() { Code = 1000, Desc = "联系人姓名不能为空！" }); }
            if (string.IsNullOrEmpty(model.ContactPhone)) { return Json(new ApiResult() { Code = 1000, Desc = "联系人姓名不能为空！" }); }
            if (string.IsNullOrEmpty(model.ProjectName)) { return Json(new ApiResult() { Code = 1000, Desc = "请选择您有意向的项目！" }); }
            #endregion 验证

            if (IntentionService.AddIntention(model))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }
        #endregion 入驻意向

        #region 新闻
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("operations/news/list")]
        //[NHHActionLog]
        public JsonResult GetNewsList(NewsListQuery query)
        {
            query.ProjectID = NHHAPIContext.Current.ProjectID;
            var result = NewsService.GetNewsList(query);
            return Json(result);
        }

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("operations/news/detail/{newsid}")]
        //[NHHActionLog]
        public JsonResult GetNews(int newsId)
        {
            int projectID = NHHAPIContext.Current.ProjectID;
            var result = NewsService.GetNews(newsId, projectID);
            return Json(result);
        }
        #endregion 新闻

        #region 投诉建议

        /// <summary>
        /// （官网）添加投诉建议
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("NHHCG/Complaint/add")]
        public JsonResult AddNHHCGComplain(NHHCGComplain model)
        {
            #region 验证
            if (string.IsNullOrEmpty(model.Name)) { return Json(new ApiResult() { Code = 1000, Desc = "联系人姓名不能为空！" }); }
            if (string.IsNullOrEmpty(model.Tel)) { return Json(new ApiResult() { Code = 1000, Desc = "联系人姓名不能为空！" }); }
            #endregion 验证

            var result = ComplaintService.AddComplain(model) ? new ApiResult(ApiResultEnum.Success) : new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }
        #endregion
    }
}
