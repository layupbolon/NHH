using System.Collections.Generic;
using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Project;
using NHH.Service.Project.IService;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class ProjectController : NHHController
    {
        #region Service
        private IProjectInfoService m_service;
        public IProjectInfoService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IProjectInfoService>();
                }
                return m_service;
            }
        }
        #endregion

        /// <summary>
        /// 根据项目编号获取楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("project/building/list")]
        //[NHHActionLog]
        public JsonResult GetBuildingList()
        {
            List<BuildingInfo> result = Service.GetBuildingList(NHHAPIContext.Current.ProjectID);
            return Json(result);
        }
        /// <summary>
        /// 根据楼宇编号获取楼层列表
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("project/building/floor/list/{buildingId}")]
        //[NHHActionLog]
        public JsonResult GetFloorList(int buildingId)
        {
            List<FloorInfo> result = Service.GetFloorList(buildingId);
            return Json(result);
        }
    }
}
