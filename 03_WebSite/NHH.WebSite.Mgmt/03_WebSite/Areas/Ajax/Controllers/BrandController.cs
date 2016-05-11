using NHH.Framework.Web;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 品牌
    /// </summary>
    [AllowAnonymous]
    public class BrandController : NHHController
    {
        #region Service
        private IBrandService m_Service;
        protected IBrandService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<IBrandService>();
                }

                return m_Service;
            }
        } 
        #endregion

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public JsonResult GetInfo(int brandId)
        {
            var brand = this.Service.GetBrandDetail(brandId);
            return Json(brand, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult GetList(string name)
        {
            var list = this.Service.GetBrandList(name);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}