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
    /// 商铺
    /// </summary>
    [AllowAnonymous]
    public class MerchantStoreController : NHHController
    {

        #region Service
        private IMerchantCommonService m_Service;
        protected IMerchantCommonService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<IMerchantCommonService>();
                }

                return m_Service;
            }
        }
        #endregion

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult GetList(string name)
        {
            var list = this.Service.GetStoreList(name);
            return Json(list, JsonRequestBehavior.AllowGet);
        }        
    }
}