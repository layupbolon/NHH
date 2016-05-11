using NHH.Framework.Web;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 楼宇
    /// </summary>
    [AllowAnonymous]
    public class BuildingController : NHHController
    {

        #region Service
        private IProjectInfoService m_Service;
        protected IProjectInfoService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<IProjectInfoService>();
                }

                return m_Service;
            }
        }
        #endregion

        /// <summary>
        /// 获取楼宇信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public JsonResult GetBuildingInfo(int buildingId)
        {
            var result = this.Service.GetBuildingDetail(buildingId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}