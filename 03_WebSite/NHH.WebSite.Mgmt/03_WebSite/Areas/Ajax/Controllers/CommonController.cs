using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Service.Common.IService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 公共
    /// </summary>
    [AllowAnonymous]
    public class CommonController : NHHController
    {
        #region Service
        private ICommonService m_Service;
        protected ICommonService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<ICommonService>();
                }

                return m_Service;
            }
        }
        #endregion


        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllProvince()
        {
            var provinceList = this.Service.GetProvinceList();
            return Json(provinceList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public JsonResult GetProvinceList(int regionId)
        {
            var provinceList = this.Service.GetProvinceList(regionId);
            return Json(provinceList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public JsonResult GetCityList(int provinceId)
        {
            var cityList = this.Service.GetCityList(provinceId);
            return Json(cityList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取经营品类列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBizCategoryList(int bizTypeId)
        {
            var bizCategoryList = this.Service.GetBizCategoryList(bizTypeId);
            return Json(bizCategoryList, JsonRequestBehavior.AllowGet);
        }

    }
}