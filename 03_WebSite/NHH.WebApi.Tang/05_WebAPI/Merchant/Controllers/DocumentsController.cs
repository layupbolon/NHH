using System;
using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Common.Enum.CommonEnums;
using NHH.Models.Documents;
using NHH.Service.Documents.IService;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class DocumentsController : NHHController
    {
        #region Service
        private IDocumentService m_service;
        public IDocumentService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IDocumentService>();
                }
                return m_service;
            }
        }
        #endregion

        /// <summary>
        /// 获取商户特殊单据列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("documents/merchant/list")]
        //[NHHActionLog]
        public JsonResult GetMerchantDocumentList(MerchantDocumentListQuery query)
        {
            query.MerchantID = NHHAPIContext.Current.MerchantID;
            query.RoleID = NHHAPIContext.Current.RoleID;
            if (query.RoleID == 2)
                query.StoreID = NHHAPIContext.Current.StoreID;
            var result = Service.GetMerchantDocumentList(query);
            return Json(result);
        }

        /// <summary>
        /// 获取装修申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("documents/merchant/decoraterequest/detail/{documentid}")]
        //[NHHActionLog]
        public JsonResult GetDecorateRequest(int documentId)
        {
            var result = Service.GetDecorateRequest(documentId);
            return Json(result);
        }

        /// <summary>
        /// 获取出门申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("document/merchant/getoutrequest/detail/{documentid}")]
        //[NHHActionLog]
        public JsonResult GetGetOutRequest(int documentId)
        {
            var result = Service.GetGetOutRequest(documentId);
            return Json(result);
        }

        /// <summary>
        /// 获取延时运营申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("document/merchant/delayrequest/detail/{documentid}")]
        //[NHHActionLog]
        public JsonResult GetDelayOperateRequest(int documentId)
        {
            var result = Service.GetDelayOperateRequest(documentId);
            return Json(result);
        }

        /// <summary>
        /// 创建装修申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("documents/merchant/decoraterequest/add")]
        //[NHHActionLog]
        public JsonResult AddDecorateRequest(DecorateRequestInfo model)
        {
            ApiResult<DecorateRequestInfo> result = null;
            model.InUser = NHHAPIContext.Current.UserID;
            model.DocumentType = (int)MerchantDocumentTypeEnum.DecorateRequest;
            #region 验证
            if (string.IsNullOrEmpty(model.RequestUserName)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "申请人不能为空！" }); }
            if (string.IsNullOrEmpty(model.ContactPhone)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "申请人联系电话不能为空！" }); }
            if (model.MerchantStoreID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请选择店铺！" }); }
            if (string.IsNullOrEmpty(model.DecorationCompanyName)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修施工公司名称不能为空！" }); }
            if (string.IsNullOrEmpty(model.DecorationCompanyAddress)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修施工公司地址不能为空！" }); }
            if (string.IsNullOrEmpty(model.PersonInCharge)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "现场负责人不能为空！" }); }
            if (string.IsNullOrEmpty(model.Phone)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "负责人联络电话不能为空！" }); }
            if (model.StartDate<DateTime.Now.AddDays(-1)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修开始时间不能小于今天！" }); }
            if (model.EndDate <= model.StartDate) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修结束时间不能小于开始时间！" }); }
            if (string.IsNullOrEmpty(model.Remark)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修项目描述不能为空！" }); }
            if (model.ElectricityConsumption < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请输入用电量！" }); }
            #endregion 验证

            if (Service.AddDecorateRequest(model))
            {
                result = new ApiResult<DecorateRequestInfo>(ApiResultEnum.Success);
                result.Data = Service.GetDecorateRequest(model.DocumentID);
            }
            else
            {
                result = new ApiResult<DecorateRequestInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 创建出门申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("document/merchant/getoutrequest/add")]
        //[NHHActionLog]
        public JsonResult AddGetOutRequest(GetOutRequestInfo model)
        {
            ApiResult<GetOutRequestInfo> result = null;
            model.InUser = NHHAPIContext.Current.UserID;
            model.DocumentType = (int)MerchantDocumentTypeEnum.GetOutRequest;
            #region 验证
            if (string.IsNullOrEmpty(model.RequestUserName)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "申请人不能为空！" }); }
            if (string.IsNullOrEmpty(model.ContactPhone)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "申请人联系电话不能为空！" }); }
            if (model.MerchantStoreID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请选择店铺！" }); }
            if (model.Qty < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "物品数量不能为空！" }); }
            if (model.GetOutTime <= DateTime.Now) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "出门时间不能小于当前时间！" }); }
            if (string.IsNullOrEmpty(model.Reason)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "出门理由不能为空！" }); }
            if (string.IsNullOrEmpty(model.Remark)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "概述不能为空！" }); }
            if (model.IsTemporary == 1 && model.ReturnTime == null) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "临时货品出门需要填写返回时间！" }); }
            if (model.ReturnTime != null)
            {
                if (model.ReturnTime <= model.GetOutTime) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "返回时间不能小于出门时间！" }); }
            }
            #endregion 验证

            if (Service.AddGetOutRequest(model))
            {
                result = new ApiResult<GetOutRequestInfo>(ApiResultEnum.Success);
                result.Data = Service.GetGetOutRequest(model.DocumentID);
            }
            else
            {
                result = new ApiResult<GetOutRequestInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }
        /// <summary>
        /// 创建延时运营申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("document/merchant/delayrequest/add")]
        //[NHHActionLog]
        public JsonResult AddDelayOperateRequest(DelayOperateRequestInfo model)
        {
            ApiResult<DelayOperateRequestInfo> result = null;
            model.InUser = NHHAPIContext.Current.UserID;
            model.DocumentType = (int) MerchantDocumentTypeEnum.DelayOperateRequest;
            #region 验证
            if (string.IsNullOrEmpty(model.RequestUserName)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "申请人不能为空！" }); }
            if (string.IsNullOrEmpty(model.ContactPhone)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "申请人联系电话不能为空！" }); }
            if (model.MerchantStoreID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请选择店铺！" }); }
            if (model.StartTime <= DateTime.Now.AddDays(-1)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "开始日期不能小于今天！" }); }
            if (model.EndTime < model.StartTime) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "结束日期不能小于开始日期！" }); }
            if (string.IsNullOrEmpty(model.Reason)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "申请内容不能为空！" }); }
            if (model.MoreHour < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请输入延长时间，以小时为单位！" }); }
            #endregion 验证

            if (Service.AddDelayOperateRequest(model))
            {
                result = new ApiResult<DelayOperateRequestInfo>(ApiResultEnum.Success);
                result.Data = Service.GetDelayOperateRequest(model.DocumentID);
            }
            else
            {
                result = new ApiResult<DelayOperateRequestInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 更新装修申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("documents/merchant/decoraterequest/update")]
        //[NHHActionLog]
        public JsonResult UpdateDecorateRequest(DecorateRequestInfo model)
        {
            ApiResult<DecorateRequestInfo> result = null;
            model.DocumentType = (int)MerchantDocumentTypeEnum.DecorateRequest;
            #region 验证
            if (model.DocumentID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "单据编号不能为空！" }); }
            if (model.MerchantStoreID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请选择店铺！" }); }
            if (string.IsNullOrEmpty(model.DecorationCompanyName)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修施工公司名称不能为空！" }); }
            if (string.IsNullOrEmpty(model.DecorationCompanyAddress)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修施工公司地址不能为空！" }); }
            if (string.IsNullOrEmpty(model.PersonInCharge)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "现场负责人不能为空！" }); }
            if (string.IsNullOrEmpty(model.Phone)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "联络电话不能为空！" }); }
            if (model.StartDate < DateTime.Now.AddDays(-1)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修开始时间不能小于今天！" }); }
            if (model.EndDate <= model.StartDate) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修结束时间不能小于开始时间！" }); }
            if (string.IsNullOrEmpty(model.Remark)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "装修项目描述不能为空！" }); }
            if (model.ElectricityConsumption < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请输入用电量！" }); }
            #endregion 验证

            if (Service.UpdateDecorateRequest(model))
            {
                result = new ApiResult<DecorateRequestInfo>(ApiResultEnum.Success);
                result.Data = Service.GetDecorateRequest(model.DocumentID);
            }
            else
            {
                result = new ApiResult<DecorateRequestInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 更新出门申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("document/merchant/getoutrequest/update")]
        //[NHHActionLog]
        public JsonResult UpdateGetOutRequest(GetOutRequestInfo model)
        {
            ApiResult<GetOutRequestInfo> result = null;
            model.DocumentType = (int)MerchantDocumentTypeEnum.GetOutRequest;
            #region 验证
            if (model.DocumentID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "单据编号不能为空！" }); }
            if (model.MerchantStoreID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请选择店铺！" }); }
            if (model.Qty < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "物品数量不能为空！" }); }
            if (model.GetOutTime <= DateTime.Now) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "出门时间不能小于当前时间！" }); }
            if (string.IsNullOrEmpty(model.Reason)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "出门理由不能为空！" }); }
            if (string.IsNullOrEmpty(model.Remark)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "概述不能为空！" }); }
            if (model.IsTemporary == 1 && model.ReturnTime == null) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "临时货品出门需要填写返回时间！" }); }
            if (model.ReturnTime != null)
            {
                if (model.ReturnTime <= model.GetOutTime) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "返回时间不能小于出门时间！" }); }
            }
            #endregion 验证

            if (Service.UpdateGetOutRequest(model))
            {
                result = new ApiResult<GetOutRequestInfo>(ApiResultEnum.Success);
                result.Data = Service.GetGetOutRequest(model.DocumentID);
            }
            else
            {
                result = new ApiResult<GetOutRequestInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 更新延时运营申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("document/merchant/delayrequest/update")]
        //[NHHActionLog]
        public JsonResult UpdateDelayOperateRequest(DelayOperateRequestInfo model)
        {
            ApiResult<DelayOperateRequestInfo> result = null;
            model.DocumentType = (int) MerchantDocumentTypeEnum.DelayOperateRequest;
            #region 验证
            if (model.DocumentID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "单据编号不能为空！" }); }
            if (model.MerchantStoreID < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请选择店铺！" }); }
            if (model.StartTime <= DateTime.Now.AddDays(-1)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "开始日期不能小于今天！" }); }
            if (model.EndTime < model.StartTime) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "结束日期不能小于开始日期！" }); }
            if (string.IsNullOrEmpty(model.Reason)) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "申请内容不能为空！" }); }
            if (model.MoreHour < 1) { return Json(new ApiResult<DecorateRequestInfo>() { Code = 1000, Desc = "请输入延长时间，以小时为单位！" }); }
            #endregion 验证

            if (Service.UpdateDelayOperateRequest(model))
            {
                result = new ApiResult<DelayOperateRequestInfo>(ApiResultEnum.Success);
                result.Data = Service.GetDelayOperateRequest(model.DocumentID);
            }
            else
            {
                result = new ApiResult<DelayOperateRequestInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 作废申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("document/merchant/cancel/{documentid}")]
        //[NHHActionLog]
        public JsonResult CancelDocumentRequest(int documentId)
        {
            if (Service.CancelDocumentRequest(documentId))
                return Json(new ApiResult(ApiResultEnum.Success));
            else
                return Json(new ApiResult(ApiResultEnum.Error));
        }
    }
}
