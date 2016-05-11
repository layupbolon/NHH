using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NHH.Framework.Web;
using System.Web.Mvc;
using NHH.Models.Common;
using NHH.Models.Common.Enum.CommonEnums;
using NHH.Service.Common.IService;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class CommonController : NHHController
    {
        #region Service
        private ICommonService m_service;
        public ICommonService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<ICommonService>();
                }
                return m_service;
            }
        }
        #endregion

        /// <summary>
        /// 获取维修类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("common/repairtype/list")]
        //[NHHActionLog]
        public JsonResult GetRepairType()
        {
            List<RepairTypeInfo> result = Service.GetRepairType();
            return Json(result);
        }

        /// <summary>
        /// 获取商家证照类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("common/merchantattachtype/list")]
        //[NHHActionLog]
        public JsonResult GetMerchantAttachType()
        {
            List<SelectListItemInfo> result = Service.GetCommonSelectList("MerchantAttachType");
            return Json(result);
        }
        /// <summary>
        /// 获取BannerLocationType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("common/bannerlocationtype")]
        [AllowAnonymous]
        //[NHHActionLog]
        public JsonResult GetBannerLocationType()
        {
            var result = Service.GetBannerLocationType();
            return Json(result);
        }

        /// <summary>
        /// 获取Banner
        /// </summary>
        /// <param name="locationType"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("common/banner/{locationtype}/{pageid}")]
        //[NHHActionLog]
        public JsonResult GetBanner(int locationType, int pageId)
        {
            int projectId = NHHAPIContext.Current.ProjectID;
            var result = Service.GetBanner(projectId, locationType, pageId);
            if (result != null)
                return Json(result);
            else
                return Json(new ApiResult() {Code = 1000, Desc = "没有找到当前的Banner，请确认参数是否正确！"});
        }
        /// <summary>
        /// 投诉评论内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("common/complaint/comment/get")]
        public JsonResult GetComplaintComment()
        {
            var result = Service.GetComplaintCommentList();
            return Json(result);
        }
        /// <summary>
        /// 维修评论内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("common/repair/comment/get")]
        public JsonResult GetRepairComment()
        {
            var result = Service.GetRepairCommentList();
            return Json(result);
        }

        /// <summary>
        /// 吐槽标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("common/complaint/tag/get")]
        public JsonResult GetComplaintTag()
        {
            var result = Service.GetComplaintTagList();
            return Json(result);
        }
    }
}
