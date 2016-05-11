using System;
using System.Collections.Generic;
using NHH.Models.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Store;
using NHH.Service.Store.IService;
using NHH.Framework.Web;
using System.Web.Mvc;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class MerchantStoreController : NHHController
    {
        #region Service
        private IMerchantStoreService m_service;
        public IMerchantStoreService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IMerchantStoreService>();
                }
                return m_service;
            }
        }
        #endregion


        /// <summary>
        /// 获取当前商户店铺列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/store/list")]
        //[NHHActionLog]
        public ActionResult GetMerchantStoreList()
        {
            int merchantId = NHHAPIContext.Current.MerchantID;
            List<MerchantStoreView> result = null;
            result = this.Service.GetMerchantStoreList(merchantId);
            return Json(result);
        }

        /// <summary>
        /// 获取指定店铺信息
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/store/detail/{storeId}")]
        //[NHHActionLog]
        public ActionResult GetMerchantStoreDetail(int storeId)
        {
            MerchantStoreView result = null;
            result = this.Service.GetMerchantStoreDetail(storeId);
            return Json(result);
        }

        /// <summary>
        /// 添加店铺图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/store/image/add")]
        //[NHHActionLog]
        public ActionResult AddMerchantStoreImage(MerchantStoreImageInfo model)
        {
            ApiResult<MerchantStoreImageInfo> result = null;
            model.InUser = NHHAPIContext.Current.UserID;
            model.EditUser = NHHAPIContext.Current.UserID;
            model.MerchantID = NHHAPIContext.Current.MerchantID;

            #region 验证
            if (model.StoreID <= 0) { return Json(new ApiResult<MerchantStoreImageInfo>() { Code = 1000, Desc = "店铺编号不能为空！" }); }
            if (string.IsNullOrEmpty(model.ImageName)) { return Json(new ApiResult<MerchantStoreImageInfo>() { Code = 1000, Desc = "图片名不能为空！" }); }
            if (string.IsNullOrEmpty(model.ImagePath)) { return Json(new ApiResult<MerchantStoreImageInfo>() { Code = 1000, Desc = "图片路径不能为空！" }); }
            #endregion 验证

            var entity = Service.AddMerchantStoreImage(model);
            if (entity != null)
            {
                result = new ApiResult<MerchantStoreImageInfo>(ApiResultEnum.Success);
                entity.ImagePath = NHH.Framework.Utility.UrlHelper.GetImageUrl(entity.ImagePath, 100);
                entity.ImageBigPath = NHH.Framework.Utility.UrlHelper.GetImageUrl(entity.ImagePath);
                result.Data = entity;
            }
            else
            {
                result = new ApiResult<MerchantStoreImageInfo>(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 删除店铺图片
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/store/image/delete/{imageId}")]
        //[NHHActionLog]
        public ActionResult DeleteMerchantStoreImage(int imageId)
        {
            ApiResult result = new ApiResult();
            if (this.Service.DeleteMerchantStoreImage(imageId))
                result = new ApiResult(ApiResultEnum.Success);
            else
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 更新指定商户店铺信息（仅修改名称）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/storename/update")]
        //[NHHActionLog]
        public ActionResult UpdateMerchantStoreName(MerchantStoreView model)
        {
            ApiResult<MerchantStoreView> result = null;
            if (this.Service.UpdateMerchantStoreName(model.StoreID, model.StoreName))
            {
                result = new ApiResult<MerchantStoreView>(ApiResultEnum.Success);
                result.Data = this.Service.GetMerchantStoreDetail(model.StoreID);
            }
            else
                result = new ApiResult<MerchantStoreView>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }

        /// <summary>
        /// 获取商户店铺营收列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/revenue/list")]
        //[NHHActionLog]
        public ActionResult GetMerchantRevenueList(MerchantRevenueListQuery query)
        {
            query.MerchantID = NHHAPIContext.Current.MerchantID;
            MerchantRevenueListModel result = Service.GetMerchantRevenuelList(query);
            return Json(result);
        }

        /// <summary>
        /// 新增商户店铺营收数据（一周以前的数据不能添加）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/revenue/add")]
        //[NHHActionLog]
        public ActionResult AddMerchantRevenue(MerchantRevenueInfo model)
        {
            model.MerchantID = NHHAPIContext.Current.MerchantID;
            model.InUser = NHHAPIContext.Current.UserID;
            model.EditUser = NHHAPIContext.Current.UserID;
            if (NHHAPIContext.Current.StoreID != 0)
                model.StoreID = NHHAPIContext.Current.StoreID;
            model.EndDate = model.StartDate; //帐期开始与结束为同一天
            ApiResult result = null;
            #region 验证
            if (model.Revenue < 0) { return Json(new ApiResult<MerchantRevenueInfo>() { Code = 1000, Desc = "营业额不能为负数！" }); }
            if (model.StoreID <= 0) { return Json(new ApiResult<MerchantRevenueInfo>() { Code = 1000, Desc = "店铺ID不能为空！" }); }
            if (model.StartDate > DateTime.Now || model.StartDate < DateTime.Now.AddDays(-7)) { return Json(new ApiResult<MerchantRevenueInfo>() { Code = 1000, Desc = "销售日期只能添加一周之内的营业额！" }); }
            #endregion 验证

            if (Service.AddMerchantRevenue(model))
            {
                result = new ApiResult(ApiResultEnum.Success);
            }
            else
            {
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

        /// <summary>
        /// 更新商户店铺营收数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/revenue/update")]
        //[NHHActionLog]
        public ActionResult UpdateMerchantRevenue(MerchantRevenueInfo model)
        {
            model.EditUser = NHHAPIContext.Current.UserID;

            ApiResult result = null;
            #region 验证
            if (model.Revenue < 0) { return Json(new ApiResult<MerchantRevenueInfo>() { Code = 1000, Desc = "营业额不能为负数！" }); }
            #endregion 验证

            if (Service.UpdateMerchantRevenue(model))
            {
                result = new ApiResult(ApiResultEnum.Success);
            }
            else
            {
                result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
            }
            return Json(result);
        }

    }
}
