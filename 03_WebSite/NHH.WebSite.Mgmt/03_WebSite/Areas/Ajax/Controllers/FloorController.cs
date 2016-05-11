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
    /// 楼层
    /// </summary>
    [AllowAnonymous]
    public class FloorController : NHHController
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
        /// 获取楼层信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public JsonResult GetFloorInfo(int floorId)
        {
            var result = this.Service.GetFloorInfo(floorId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取楼层平面图信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public JsonResult GetFloorMapFile(int floorId)
        {
            var floor = this.Service.GetFloorInfo(floorId);
            var result = new
            {
                smallMapFile = NHH.Framework.Utility.UrlHelper.GetImageUrl(floor.FloorMapFileName, 100),
                bigMapFile = NHH.Framework.Utility.UrlHelper.GetImageUrl(floor.FloorMapFileName, 800)
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}