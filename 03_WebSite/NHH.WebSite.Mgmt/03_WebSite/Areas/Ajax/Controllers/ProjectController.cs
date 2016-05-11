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
    /// 项目管理
    /// </summary>
    [AllowAnonymous]
    public class ProjectController : NHHController
    {
        #region Service
        private IProjectCommonService m_Service;
        protected IProjectCommonService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<IProjectCommonService>();
                }

                return m_Service;
            }
        }
        #endregion


        /// <summary>
        /// 获取项目列表（权限控制）
        /// </summary>
        /// <returns></returns>
        public JsonResult GetProjectList()
        {
            var list = this.Service.GetProjectList(Context.UserID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取项目列表（无权限控制）
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllProject()
        {
            var list = this.Service.GetProjectList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public JsonResult GetBuildingList(int projectId)
        {
            var list = this.Service.GetBuildingList(projectId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取楼宇组
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public JsonResult GetBuildingGroup(int projectId)
        {
            var list = this.Service.GetBuildingGroup(projectId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取楼层列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public JsonResult GetAllFloor(int projectId)
        {
            var list = this.Service.GetAllFloor(projectId);
            return Json(list, JsonRequestBehavior.AllowGet); 
        }

        /// <summary>
        /// 获取楼层列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public JsonResult GetFloorList(int projectId, int buildingId)
        {
            var list = this.Service.GetFloorList(projectId, buildingId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}