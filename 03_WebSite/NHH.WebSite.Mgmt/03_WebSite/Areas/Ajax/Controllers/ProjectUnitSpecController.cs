using NHH.Framework.Web;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 商铺交付标准
    /// </summary>
    [AllowAnonymous]
    public class ProjectUnitSpecController : NHHController
    {

        #region Service
        private IProjectPlanService m_Service;
        protected IProjectPlanService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<IProjectPlanService>();
                }

                return m_Service;
            }
        }
        #endregion

        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public JsonResult GetUnitSpecTemplate(int templateId)
        {
            var model = this.Service.GetProjectUnitSpecTemplateInfo(templateId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
