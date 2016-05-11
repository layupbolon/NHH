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
    /// 合同
    /// </summary>
    [AllowAnonymous]
    public class ContractController : NHHController
    {

        #region Service
        private IContractCommonService m_Service;
        protected IContractCommonService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<IContractCommonService>();
                }

                return m_Service;
            }
        }
        #endregion

        /// <summary>
        /// 获取铺位列表
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public JsonResult GetProjectUnitList(int contractId)
        {
            var list = this.Service.GetProjectUnitList(contractId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}