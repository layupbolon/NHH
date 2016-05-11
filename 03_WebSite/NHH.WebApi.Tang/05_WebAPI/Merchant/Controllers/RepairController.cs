using NHH.Models.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Estate;
using NHH.Service.Estate.IService;
using NHH.Service.Project.IService;
using NHH.Framework.Web;
using System.Web.Mvc;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class RepairController : NHHController
    {
        #region RepairService
        private IRepairService m_service;
        public IRepairService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IRepairService>();
                }
                return m_service;
            }
        }
        #endregion

        #region ProjectUnitService
        private IProjectUnitService m_projectUnitService;
        public IProjectUnitService ProjectUnitService
        {
            get
            {
                if (m_projectUnitService == null)
                {
                    m_projectUnitService = this.GetService<IProjectUnitService>();
                }
                return m_projectUnitService;
            }
        }

        #endregion
        /// <summary>
        /// 获取维修单各状态数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/repair/status/qty")]
        //[NHHActionLog]
        public JsonResult GetRepairStatusQty()
        {
            var result = Service.GetRepairStatusQty(
                NHHAPIContext.Current.RoleID,
                NHHAPIContext.Current.MerchantID,
                NHHAPIContext.Current.UserID
                );
            return Json(result);
        }

        /// <summary>
        /// 获取商户报修列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/repair/list")]
        //[NHHActionLog]
        public JsonResult GetRepairList(RepairListQuery query)
        {
            query.MerchantID = NHHAPIContext.Current.MerchantID;
            query.RoleID = NHHAPIContext.Current.RoleID;
            query.StoreID = NHHAPIContext.Current.StoreID;
            query.UserID = NHHAPIContext.Current.UserID;
            RepairListModel result = this.Service.GetRepairList(query);
            return Json(result);
        }

        /// <summary>
        /// 创建商户报修信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/repair/create")]
        //[NHHActionLog]
        public JsonResult AddRepair(RepairInfo model)
        {
            if (model.StoreID == 0)
                model.StoreID = NHHAPIContext.Current.StoreID;
            model.ProjectID = NHHAPIContext.Current.ProjectID;
            model.RequestUserID = NHHAPIContext.Current.UserID;
            model.RequestUserName = NHHAPIContext.Current.UserName;
            model.InUser = NHHAPIContext.Current.UserID;

            ApiResult<RepairInfo> result = null;
            #region 验证
            if (model.RepairType < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "维修类型不能为空！" }); }
            if (model.ProjectID < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "项目编号不能为空！" }); }
            if (model.IsCommon != 1 && model.IsCommon != 2) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "是否公共区域不是有效的格式！" }); }
            if (model.IsCommon == 1) //我的铺内
            {
                if (model.StoreID < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "店铺编号不能为空！" }); }
                if (model.UnitID < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "铺位编号不能为空！" }); }
                //model.UnitID = ProjectUnitService.GetUnitIdByStoreId(model.StoreID);
            }
            if (model.IsCommon == 2) //公共区域
            {
                if (model.FloorID < 1)
                {
                    return Json(new ApiResult<RepairInfo>() {Code = 1000, Desc = "楼层编号不能为空！"});
                }
                if (string.IsNullOrEmpty(model.Location))
                {
                    return Json(new ApiResult<RepairInfo>() {Code = 1000, Desc = "位置描述不能为空！"});
                }
            }
            if (string.IsNullOrEmpty(model.RequestDesc)) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "报修情况描述不能为空！" }); }
            #endregion 验证
            if (this.Service.AddRepair(model))
            {
                result = new ApiResult<RepairInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetRepairSimple(model.RepairID);
            }
            else
                result = new ApiResult<RepairInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 更新指定商户报修信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/repair/update")]
        //[NHHActionLog]
        public JsonResult UpdateRepair(RepairInfo model)
        {
            model.EditUser = NHHAPIContext.Current.UserID;
            ApiResult<RepairInfo> result = null;
            #region 验证
            if (model.RepairID < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "报修编号不能为空！" }); }
            if (model.RepairType < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "维修类型不能为空！" }); }
            if (model.ProjectID < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "项目编号不能为空！" }); }
            if (model.StoreID < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "店铺编号不能为空！" }); }
            if (model.FloorID < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "楼层编号不能为空！" }); }
            if (model.UnitID < 1) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "铺位编号不能为空！" }); }
            if (model.IsCommon != 1 && model.IsCommon != 2) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "是否公共区域不是有效的格式！" }); }
            if (string.IsNullOrEmpty(model.Location)) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "位置描述不能为空！" }); }
            if (string.IsNullOrEmpty(model.RequestDesc)) { return Json(new ApiResult<RepairInfo>() { Code = 1000, Desc = "报修情况描述不能为空！" }); }
            #endregion 验证
            if (this.Service.UpdateRepair(model))
            {
                result = new ApiResult<RepairInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetRepairDetail(model.RepairID);
            }
            else
                result = new ApiResult<RepairInfo>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 获取指定商户报修信息
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/repair/detail/{repairId}")]
        //[NHHActionLog]
        public JsonResult GetRepair(int repairId)
        {
            RepairInfo result = this.Service.GetRepairDetail(repairId);
            return Json(result);
        }

        /// <summary>
        /// 作废指定商户报修信息
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/repair/cancel/{repairId}")]
        //[NHHActionLog]
        public JsonResult DeleteRepair(int repairId)
        {
            ApiResult result = null;
            if (this.Service.CancelRepair(repairId))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 添加报修附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/repair/attch/add")]
        //[NHHActionLog]
        public JsonResult AddAttachment(RepairAttachmentInfo model)
        {
            model.InUser = NHHAPIContext.Current.UserID;
            ApiResult<RepairAttachmentInfo> result = null;
            #region 验证

            if (model.RepairID < 1)
            {
                return Json(new ApiResult<RepairAttachmentInfo>() { Code = 1000, Desc = "报修编号不能为空！" });
            }
            if (string.IsNullOrEmpty(model.AttachmentName))
            {
                return Json(new ApiResult<RepairAttachmentInfo>() { Code = 1000, Desc = "附件图片名称不能为空！" });
            }
            if (string.IsNullOrEmpty(model.AttachmentPath))
            {
                return Json(new ApiResult<RepairAttachmentInfo>() { Code = 1000, Desc = "附件图片路径不能为空！" });
            }

            #endregion 验证

            if (this.Service.AddRepairAttachment(model))
            {
                result = new ApiResult<RepairAttachmentInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetRepairAttachment(model.AttachmentID);
            }
            else
            {
                result = new ApiResult<RepairAttachmentInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 获取指定报修附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/repair/attch/detail/{attachmentId}")]
        //[NHHActionLog]
        public JsonResult GetAttachment(int attachmentId)
        {
            RepairAttachmentInfo result = this.Service.GetRepairAttachment(attachmentId);
            return Json(result);
        }

        /// <summary>
        /// 删除指定报修附件（作废）
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/repair/attch/delete/{attachmentId}")]
        //[NHHActionLog]
        public JsonResult DeleteRepairAttachment(int attachmentId)
        {
            ApiResult result = null;
            if (this.Service.CancelRepairAttachment(attachmentId))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 提交指定修评价信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/repair/comment/add")]
        ////[NHHActionLog]
        public JsonResult AddRepairComment(RepairCommentInfo model)
        {
            ApiResult<RepairCommentInfo> result = null;
            #region 验证
            if (model.RepairId < 1) { return Json(new ApiResult<RepairCommentInfo>() { Code = 1000, Desc = "报修编号不能为空！" }); }
            if (model.Speed <= 0 && model.Speed > 5) { return Json(new ApiResult<RepairCommentInfo>() { Code = 1000, Desc = "响应速度不是有效的范围！" }); }
            if (model.Quality <= 0 && model.Quality > 5) { return Json(new ApiResult<RepairCommentInfo>() { Code = 1000, Desc = "维修结果不是有效的范围！" }); }
            if (model.Attitude <= 0 && model.Attitude > 5) { return Json(new ApiResult<RepairCommentInfo>() { Code = 1000, Desc = "服务态度不是有效的范围！" }); }
            if (string.IsNullOrEmpty(model.Comment)) { return Json(new ApiResult<RepairCommentInfo>() { Code = 1000, Desc = "评价内容不能为空！" }); }
            #endregion 验证
            //总体评价是由三个子评价平均下来的
            model.Overall = (model.Speed + model.Quality + model.Attitude)/3;
            if (this.Service.AddComment(model))
            {
                result = new ApiResult<RepairCommentInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetCommentDetail(model.CommentId);
            }
            else
            {
                result = new ApiResult<RepairCommentInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 提交追加评论信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/repair/comment/additional/add")]
        //[NHHActionLog]
        public JsonResult AddAdditional(RepairCommentInfo model)
        {
            ApiResult<RepairCommentInfo> result = null;
            if (model.CommentId < 1) { return Json(new ApiResult<RepairCommentInfo>() { Code = 1000, Desc = "评价编号不能为空！" }); }
            if (string.IsNullOrEmpty(model.Additional)) { return Json(new ApiResult<RepairCommentInfo>() { Code = 1000, Desc = "追加评论不能为空！" }); }

            if (this.Service.AddAdditional(model))
            {
                result = new ApiResult<RepairCommentInfo>(ApiResultEnum.Success);
                result.Data = this.Service.GetCommentDetail(model.CommentId);
            }
            else
            {
                result = new ApiResult<RepairCommentInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }
    }
}
