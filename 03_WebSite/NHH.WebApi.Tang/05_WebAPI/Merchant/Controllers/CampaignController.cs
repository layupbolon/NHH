using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Campaign;
using NHH.Models.Common;
using NHH.Models.Common.Enum;
using NHH.Service.Campaign.IService;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class CampaignController : NHHController
    {
        #region Service
        private ICampaignInfoService m_service;
        public ICampaignInfoService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<ICampaignInfoService>();
                }
                return m_service;
            }
        }
        #endregion

        /// <summary>
        /// 获取活动信息列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/campaign/list")]
        //[NHHActionLog]
        public JsonResult GetCampaignList(CampaignListQuery query)
        {
            query.MerchantID = NHHAPIContext.Current.MerchantID;
            CampaignListModel result = this.Service.GetCampaignList(query);
            return Json(result);
        }

        /// <summary>
        /// 获取指定的活动信息详情
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/campaign/detail/{campaignId}")]
        //[NHHActionLog]
        public JsonResult GetCampaign(int campaignId)
        {
            CampaignInfo result = this.Service.GetCampaignInfo(campaignId);
            return Json(result);
        }

        /// <summary>
        /// 添加活动评价
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/campaign/comment/add")]
        //[NHHActionLog]
        public JsonResult AddCampaignComment(CampaignCommentInfo model)
        {
            model.CommentUserID = NHHAPIContext.Current.UserID;
            model.InUser = NHHAPIContext.Current.UserID;
            model.EditUser = NHHAPIContext.Current.UserID;
            model.CommentUserName = NHHAPIContext.Current.UserName;

            ApiResult result = null;

            #region 验证
            if (model.CampaignID < 1) { return Json(new ApiResult() { Code = 1000, Desc = "活动编号不能为空！" }); }
            if (model.Overall <= 0 && model.Overall > 5) { return Json(new ApiResult() { Code = 1000, Desc = "评价值只能在1-5之间！" }); }
            if (string.IsNullOrEmpty(model.CommentContent)) { return Json(new ApiResult() { Code = 1000, Desc = "评价内容不能为空！" }); }
            #endregion 验证

            if (Service.AddCampaignComment(model))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

    }
}
