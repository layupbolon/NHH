using System.Collections.Generic;
using NHH.Models.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Estate;
using NHH.Service.Estate.IService;
using NHH.Framework.Web;
using System.Web.Mvc;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class ComplaintController : NHHController
    {
        #region Service
        private IComplaintService m_service;
        public IComplaintService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IComplaintService>();
                }
                return m_service;
            }
        }
        #endregion

        /// <summary>
        /// 获取投诉各状态数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/complaint/status/qty")]
        //[NHHActionLog]
        public JsonResult GetComplaintStatusQty()
        {
            var result = Service.GetComplaintStatusQty(
                NHHAPIContext.Current.RoleID,
                NHHAPIContext.Current.MerchantID,
                NHHAPIContext.Current.StoreID
                );
            return Json(result);
        }

        /// <summary>
        /// 获取商户投诉列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/complaint/list")]
        //[NHHActionLog]
        public JsonResult GetComplaintList(ComplaintListQuery query)
        {
            query.MerchantID = NHHAPIContext.Current.MerchantID;
            query.RoleID = NHHAPIContext.Current.RoleID;
            query.StoreID = NHHAPIContext.Current.StoreID;
            ComplaintListModel result = this.Service.GetComplaintList(query);
            return Json(result);
        }

        /// <summary>
        /// 创建商户投诉信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/complaint/create")]
        //[NHHActionLog]
        public JsonResult AddComplaint(ComplaintInfo model)
        {
            model.MerchantID = NHHAPIContext.Current.MerchantID;
            if (NHHAPIContext.Current.StoreID != 0)
                model.StoreID = NHHAPIContext.Current.StoreID;
            model.RequestUserID = NHHAPIContext.Current.UserID;
            model.InUser = NHHAPIContext.Current.UserID;
            model.ProjectID = NHHAPIContext.Current.ProjectID;
            model.RequestUserName = NHHAPIContext.Current.UserName;

            ApiResult<ComplaintInfo> result = null;
            #region 验证
            if (string.IsNullOrEmpty(model.RequestTarget)) { return Json(new ApiResult<ComplaintInfo>() { Code = 1000, Desc = "投诉对象不能为空！" }); }
            if (string.IsNullOrEmpty(model.RequestDesc)) { return Json(new ApiResult<ComplaintInfo>() { Code = 1000, Desc = "投诉情况描述不能为空！" }); }
            #endregion 验证
            if (this.Service.AddComplaint(model))
            {
                result = new ApiResult<ComplaintInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetComplaintSimple(model.ComplaintID);
            }
            else
                result = new ApiResult<ComplaintInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 更新指定商户投诉信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/complaint/update")]
        //[NHHActionLog]
        public JsonResult UpdateComplaint(ComplaintInfo model)
        {
            model.RequestUserID = NHHAPIContext.Current.UserID;
            model.EditUser = NHHAPIContext.Current.UserID;
            model.RequestUserName = NHHAPIContext.Current.UserName;
            ApiResult<ComplaintInfo> result = null;
            #region 验证
            if (model.ComplaintID < 1) { return Json(new ApiResult<ComplaintInfo>() { Code = 1000, Desc = "投诉编号不能为空！" }); }
            if (string.IsNullOrEmpty(model.RequestTarget)) { return Json(new ApiResult<ComplaintInfo>() { Code = 1000, Desc = "投诉对象不能为空！" }); }
            if (string.IsNullOrEmpty(model.RequestDesc)) { return Json(new ApiResult<ComplaintInfo>() { Code = 1000, Desc = "投诉情况描述不能为空！" }); }
            #endregion 验证

            if (this.Service.UpdateComplaint(model))
            {
                result = new ApiResult<ComplaintInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetComplaintDetail(model.ComplaintID);
            }
            else
                result = new ApiResult<ComplaintInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 作废指定商户投诉信息
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/complaint/cancel/{complaintId}")]
        //[NHHActionLog]
        public JsonResult DeleteComplaint(int complaintId)
        {
            ApiResult result = null;
            if (this.Service.CancelComplaint(complaintId))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 获取指定商户投诉信息
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/complaint/detail/{complaintId}")]
        //[NHHActionLog]
        public JsonResult GetComplaint(int complaintId)
        {
            ComplaintInfo result = this.Service.GetComplaintDetail(complaintId);
            return Json(result);
        }

        /// <summary>
        /// 添加投诉附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/complaint/attch/add")]
        //[NHHActionLog]
        public JsonResult AddComplaintAttachment(ComplaintAttachmentInfo model)
        {
            model.InUser = NHHAPIContext.Current.UserID;
            ApiResult<ComplaintAttachmentInfo> result = null;
            #region 验证
            if (model.ComplaintID < 1) { return Json(new ApiResult<ComplaintAttachmentInfo>() { Code = 1000, Desc = "投诉编号不能为空！" }); }
            if (string.IsNullOrEmpty(model.AttachmentName)) { return Json(new ApiResult<ComplaintAttachmentInfo>() { Code = 1000, Desc = "附件名称不能为空！" }); }
            if (string.IsNullOrEmpty(model.AttachmentPath)) { return Json(new ApiResult<ComplaintAttachmentInfo>() { Code = 1000, Desc = "附件路径不能为空！" }); }
            #endregion 验证

            if (this.Service.AddAttachment(model))
            {
                result = new ApiResult<ComplaintAttachmentInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetAttachment(model.AttachmentID);
            }
            else
                result = new ApiResult<ComplaintAttachmentInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 获取指定投诉附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/complaint/attch/detail/{attachmentId}")]
        //[NHHActionLog]
        public JsonResult GetComplaintAttachment(int attachmentId)
        {
            ComplaintAttachmentInfo result = this.Service.GetAttachment(attachmentId);
            return Json(result);
        }

        /// <summary>
        /// 获取指定投诉单附件列表
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/complaint/attch/list/{complaintId}")]
        //[NHHActionLog]
        public JsonResult GetComplaintAttachmentList(int complaintId)
        {
            List<ComplaintAttachmentInfo> result = this.Service.GetAttachmentList(complaintId);
            return Json(result);
        }

        /// <summary>
        /// 删除指定投诉附件(作废)
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/complaint/attch/delete/{attachmentId}")]
        //[NHHActionLog]
        public JsonResult DeleteAttachment(int attachmentId)
        {
            ApiResult result = null;
            if (this.Service.CancelAttachment(attachmentId))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 提交指定商户投诉评价信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/complaint/comment/add")]
        //[NHHActionLog]
        public JsonResult AddComplaintComment(ComplaintCommentInfo model)
        {
            model.InUser = NHHAPIContext.Current.UserID;
            ApiResult<ComplaintCommentInfo> result = null;
            #region 验证
            if (model.ComplaintID < 1) { return Json(new ApiResult<ComplaintCommentInfo>() { Code = 1000, Desc = "投诉编号不能为空！" }); }
            if (model.Speed <= 0 && model.Speed > 5) { return Json(new ApiResult<ComplaintCommentInfo>() { Code = 1000, Desc = "响应速度不是有效的范围！" }); }
            if (model.Quality <= 0 && model.Quality > 5) { return Json(new ApiResult<ComplaintCommentInfo>() { Code = 1000, Desc = "投诉结果不是有效的范围！" }); }
            if (model.Attitude <= 0 && model.Attitude > 5) { return Json(new ApiResult<ComplaintCommentInfo>() { Code = 1000, Desc = "服务态度不是有效的范围！" }); }
            if (string.IsNullOrEmpty(model.Comment)) { return Json(new ApiResult<ComplaintCommentInfo>() { Code = 1000, Desc = "评价内容不能为空！" }); }
            #endregion 验证
            //总体评价是由三个子评价平均下来的
            model.Overall = (model.Speed + model.Quality + model.Attitude)/3;
            if (this.Service.AddComment(model))
            {
                result = new ApiResult<ComplaintCommentInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetComment(model.CommentID);
            }
            else
            {
                result = new ApiResult<ComplaintCommentInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 提交指定商户投诉追加评论信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/complaint/comment/additional")]
        //[NHHActionLog]
        public JsonResult AddComplaintCommentAdditional(ComplaintCommentInfo model)
        {
            model.EditUser = NHHAPIContext.Current.UserID;
            ApiResult<ComplaintCommentInfo> result = null;
            #region 验证
            if (model.CommentID < 1) { return Json(new ApiResult<ComplaintCommentInfo>() { Code = 1000, Desc = "评论编号不能为空！" }); }
            if (string.IsNullOrEmpty(model.Additional)) { return Json(new ApiResult<ComplaintCommentInfo>() { Code = 1000, Desc = "追加评论不能为空！" }); }
            #endregion 验证

            if (this.Service.UpdateCommentAdditional(model))
            {
                result = new ApiResult<ComplaintCommentInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetComment(model.CommentID);
            }
            else
            {
                result = new ApiResult<ComplaintCommentInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }
    }
}
