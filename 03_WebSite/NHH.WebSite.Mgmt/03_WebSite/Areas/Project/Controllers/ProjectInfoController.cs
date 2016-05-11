using NHH.Entities;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Project;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Common.IService;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Project.Controllers
{
    /// <summary>
    /// 项目信息
    /// </summary>
    public class ProjectInfoController : NHHController
    {
        IProjectInfoService service { set; get; }
        IProjectUnitService puService { set; get; }
        public ProjectInfoController()
        {
            this.service = this.GetService<IProjectInfoService>();
            this.puService = this.GetService<IProjectUnitService>();
        }
        
        /// <summary>
        /// 项目列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ProjectListQueryInfo queryInfo)
        {
            var model = service.GetProjectList(queryInfo);
            model.CrumbInfo.AddCrumb("项目管理");

            var commonService = GetService<ICommonService>();
            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            var regionList = commonService.GetRegionList();
            ViewData["RegionList"] = new SelectList(regionList, "Id", "Name", queryInfo.RegionId.HasValue ? queryInfo.RegionId.Value : 0);

            var provinceList = commonService.GetProvinceList();
            ViewData["ProvinceList"] = new SelectList(provinceList, "Id", "Name", queryInfo.ProvinceId.HasValue ? queryInfo.ProvinceId.Value : 0);

            var cityList = commonService.GetCityList(0);
            ViewData["CityList"] = new SelectList(cityList, "Id", "Name", queryInfo.CityId.HasValue ? queryInfo.CityId.Value : 0);

            var projectStageList = commonService.GetCommonFieldList("ProjectStage");
            ViewData["ProjectStageList"] = new SelectList(projectStageList, "FieldValue", "FieldName", queryInfo.Stage.HasValue ? queryInfo.Stage.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 显示完成页面的具体信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult Detail(int projectId)
        {
            var model = service.GetProjectDetail(projectId);
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("项目详情");

            model.ProjectId = projectId;            
            return View(model);
        }

        #region 项目详情分页

        /// <summary>
        /// 基础信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PartialViewResult BasicInfo(int projectId)
        {
            var model = new NHH.Models.Project.ProjectBasicInfoModel();
            model=service.GetProjectBasicInfo(projectId);
            return PartialView("_PartialProjectBasicInfo", model);
        }

        /// <summary>
        /// 详细信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PartialViewResult DetailInfo(int projectId)
        {
            var model = new NHH.Models.Project.ProjectDetailInfoModel();
            model = service.GetProjectDetailInfo(projectId);
            return PartialView("_PartialProjectDetailInfo", model);
        }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PartialViewResult BuildingList(int projectId)
        {
            var model = service.GetBuildingList(projectId);
            return PartialView("_PartialProjectBuildingList", model);
        }

        #endregion
    }
}
