using System;
using NHH.Models.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using NHH.Framework.Web;
using System.Web.Mvc;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class MerchantAttachmentController : NHHController
    {
        #region Service
        private IAttachmentService m_service;
        public IAttachmentService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IAttachmentService>();
                }
                return m_service;
            }
        }
        #endregion


        #region 证照
        /// <summary>
        /// 获取商户证照列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/attch/list")]
        //[NHHActionLog]
        public JsonResult GetMerchantAttchmentList()
        {
            int merchantId = NHHAPIContext.Current.MerchantID;
            var result = Service.GetMerchantAttachmentList(merchantId);
            return Json(result);
        }

        /// <summary>
        /// 添加证照
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/attch/add")]
        //[NHHActionLog]
        public JsonResult UploadAttachment(MerchantAttachmentInfo model)
        {
            model.MerchantID = NHHAPIContext.Current.MerchantID;
            model.InUser = NHHAPIContext.Current.UserID;
            model.ProjectID = NHHAPIContext.Current.ProjectID;
            ApiResult<MerchantAttachmentInfo> result = null;
            #region 有效性验证
            if (model.AttachmentType < 1 || model.AttachmentType > 8)
            {
                result = new ApiResult<MerchantAttachmentInfo>() { Code = 1000, Desc = "不是有效的证照类型！" };
                return Json(result);
            }
            if (string.IsNullOrEmpty(model.AttachmentName))
            {
                result = new ApiResult<MerchantAttachmentInfo>() { Code = 1000, Desc = "证照名称不能为空！" };
                return Json(result);
            }
            if (string.IsNullOrEmpty(model.AttachmentCode))
            {
                result = new ApiResult<MerchantAttachmentInfo>() { Code = 1000, Desc = "证照编号不能为空！" };
                return Json(result);
            }
            if (model.ExpiredDate.HasValue && model.ExpiredDate < DateTime.Now)
            {
                result = new ApiResult<MerchantAttachmentInfo>() { Code = 1000, Desc = "证照过期日期不能小于当前日期！" };
                return Json(result);
            }
            if (string.IsNullOrEmpty(model.AttachmentPath))
            {
                result = new ApiResult<MerchantAttachmentInfo>() { Code = 1000, Desc = "证照图片URL不能为空！" };
                return Json(result);
            }
            #endregion 有效性验证

            int attachmentId = this.Service.AddMerchantAttachment(model);
            if (attachmentId > 0)
            {
                result = new ApiResult<MerchantAttachmentInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetAttachmentInfo(attachmentId,model.MerchantID);
            }
            else
                result = new ApiResult<MerchantAttachmentInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 更新证照
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/attch/update")]
        //[NHHActionLog]
        public JsonResult UpdateAttachment(MerchantAttachmentInfo model)
        {
            model.EditUser = NHHAPIContext.Current.UserID;
            model.MerchantID = NHHAPIContext.Current.MerchantID;
            model.ProjectID = NHHAPIContext.Current.ProjectID;
            ApiResult<MerchantAttachmentInfo> result = null;
            #region 有效性验证
            if (string.IsNullOrEmpty(model.AttachmentCode))
            {
                result = new ApiResult<MerchantAttachmentInfo>() { Code = 1000, Desc = "证照编号不能为空！" };
                return Json(result);
            }
            if (model.ExpiredDate.HasValue && model.ExpiredDate < DateTime.Now)
            {
                result = new ApiResult<MerchantAttachmentInfo>() { Code = 1000, Desc = "证照过期日期不能小于当前日期！" };
                return Json(result);
            }
            #endregion 有效性验证

            if (this.Service.UpdateMerchantAttachment(model))
            {
                result = new ApiResult<MerchantAttachmentInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetAttachmentInfo(model.AttachmentID,model.MerchantID);
            }
            else
                result = new ApiResult<MerchantAttachmentInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 获取指定商户证照
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/attch/detail/{attachmentId}")]
        //[NHHActionLog]
        public JsonResult GetAttachmentInfo(int attachmentId)
        {
            MerchantAttachmentInfo result = new MerchantAttachmentInfo();
            result = this.Service.GetAttachmentInfo(attachmentId, NHHAPIContext.Current.MerchantID);
            return Json(result);
        }

        /// <summary>
        /// 删除指定商户证照(作废）
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/attch/delete/{attachmentId}")]
        //[NHHActionLog]
        public JsonResult CancelAttachment(int attachmentId)
        {
            ApiResult result = null;
            if (this.Service.CancelAttachment(attachmentId, NHHAPIContext.Current.MerchantID))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }
        #endregion 证照
    }
}
