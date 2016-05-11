using System.Text.RegularExpressions;
using NHH.Models.Common;
using NHH.Models.Common.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using NHH.Framework.Web;
using System.Web.Mvc;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class MerchantController : NHHController
    {

        #region Service
        private IMerchantService m_service;
        public IMerchantService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IMerchantService>();
                }
                return m_service;
            }
        }
        #endregion
        /// <summary>
        /// 根据店铺编号获取店铺所在的铺位列表
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/store/unit/list/{storeid}")]
        //[NHHActionLog]
        public JsonResult GetUnitListByStoreId(int storeid)
        {
            var result = Service.GetUnitListByStoreId(storeid);
            return Json(result);
        }

        #region 商户详情
        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <remarks>{host}/api/merchant/detail</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/detail")]
        //[NHHActionLog]
        public JsonResult Detail()
        {
            int merchantId = NHHAPIContext.Current.MerchantID;
            var result = this.Service.GetMerchantDetail(merchantId);
            return Json(result);
        }

        /// <summary>
        /// 更新商户信息
        /// </summary>
        /// <param name="param"></param>
        /// <remarks>{host}/api/merchant/update</remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/update")]
        //[NHHActionLog]
        public JsonResult Update(MerchantDetailModel model)
        {
            model.EditUser = NHHAPIContext.Current.UserID;
            ApiResult<MerchantDetailModel> result = null;
            #region 有效性验证
            if (model.MerchantType != 1 && model.MerchantType != 2) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "商户类型只能为【1】法人【2】自然人" }); }
            if (string.IsNullOrEmpty(model.MerchantName)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "商户名称不能为空！" }); }
            if (!string.IsNullOrEmpty(model.Zipcode))
                if (!Regex.IsMatch(model.Zipcode, RegularString.ZipCode)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的邮编不是有效的格式！" }); }
            if (!string.IsNullOrEmpty(model.ContactEmail))
                if (!Regex.IsMatch(model.ContactEmail, RegularString.Email)) { return Json(new ApiResult<MerchantUserInfo>() { Code = 1000, Desc = "您输入的联系人邮箱不是有效的格式！" }); }
            #endregion 有效性验证
            if (this.Service.UpdateMerchant(model, model.FinanceInfo))
            {
                result = new ApiResult<MerchantDetailModel>(ApiResultEnum.Success);
                result.Data = this.Service.GetMerchantDetail(model.MerchantID);
            }
            else
                result = new ApiResult<MerchantDetailModel>(ApiResultEnum.NoUpdateAnyRows);
            return Json(result);
        }
        #endregion 商户详情



        #region 私有方法

        #endregion 私有方法


    }
}
