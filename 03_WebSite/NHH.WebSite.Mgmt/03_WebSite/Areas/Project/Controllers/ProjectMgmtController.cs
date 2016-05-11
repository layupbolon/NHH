using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Project;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Common.IService;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Project.Controllers
{
    /// <summary>
    /// 项目管理
    /// </summary>
    public class ProjectMgmtController : NHHController
    {
        IProjectMgmtService service;
        IProjectInfoService projectInfoService;
        IProjectCommonService projectCommonService;
        ICommonService commonService;
        ICompanyCommonService companyService;

        public ProjectMgmtController()
        {
            this.service = this.GetService<IProjectMgmtService>();
            projectInfoService = GetService<IProjectInfoService>();
            projectCommonService = GetService<IProjectCommonService>();
            commonService = GetService<ICommonService>();
            companyService = GetService<ICompanyCommonService>();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new ProjectInfoModel();
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("新增项目");
            //if (!ModelState.IsValid)
            //{ }
            var regionList = commonService.GetRegionList();
            ViewData["RegionList"] = new SelectList(regionList, "Id", "Name");

            var provinceList = commonService.GetProvinceList();
            ViewData["ProvinceList"] = new SelectList(provinceList, "Id", "Name");

            var cityList = commonService.GetCityList(0);
            ViewData["CityList"] = new SelectList(cityList, "Id", "Name");

            //初始化投资公司 
            var investCompanyIDList = companyService.GetCompanyList(1);
            ViewData["InvestCompanyList"] = new SelectList(investCompanyIDList, "Id", "ShortName");

            //初始业主公司
            var ownerCompanyList = companyService.GetCompanyList(2);
            ViewData["OwnerCompanyList"] = new SelectList(ownerCompanyList, "Id", "ShortName");

            //初始化管理公司
            var manageCompanyList = companyService.GetCompanyList(3);
            ViewData["ManageCompanyList"] = new SelectList(manageCompanyList, "Id", "ShortName");

            return View(model);
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(ProjectInfoModel model)
        {
            var projectId = 0;
            if (!ModelState.IsValid)
            {
                model.InUser = Context.UserID;
                model.EditUser = Context.UserID;
                //model.UserInfo.UserName = Context.UserName;
                projectId = service.Create(model);
                return RedirectToAction("Detail", "ProjectInfo", new { area = "Project", projectId = projectId });
            }
            return RedirectToAction("Create");
        }

        /// <summary>
        /// 编辑项目基本信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PartialViewResult EditBasicInfo(int projectId)
        {
            var model = projectInfoService.GetProjectBasicInfo(projectId);
            var regionList = commonService.GetRegionList();
            ViewData["RegionList"] = new SelectList(regionList, "Id", "Name", model.RegionID);

            var provinceList = commonService.GetProvinceList();
            ViewData["ProvinceList"] = new SelectList(provinceList, "Id", "Name", model.ProvinceID);

            var cityList = commonService.GetCityList(model.ProvinceID);
            ViewData["CityList"] = new SelectList(cityList, "Id", "Name", model.CityID);

            //初始化投资公司
            var investCompanyIDList = companyService.GetCompanyList(1);
            ViewData["InvestCompanyList"] = new SelectList(investCompanyIDList, "Id", "ShortName", model.InvestCompanyId);
            
            //初始化管理公司
            var manageCompanyList = companyService.GetCompanyList(3);
            ViewData["ManageCompanyList"] = new SelectList(manageCompanyList, "Id", "ShortName", model.ManageCompanyId);
            return PartialView("_PartialEditProjectBasicInfo", model);
        }

        /// <summary>
        /// 保证项目基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult EditBasicInfo(ProjectBasicInfoModel model)
        {
            model.EditUser = Context.UserID;
            service.EditProjectInfoBasic(model);
            return Content(model.ProjectID.ToString());
        }

        /// <summary>
        /// 编辑项目详情页
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PartialViewResult EditDetailInfo(int projectId)
        {
            var model = projectInfoService.GetProjectDetailInfo(projectId);
            var regionList = commonService.GetRegionList();
            ViewData["RegionList"] = new SelectList(regionList, "Id", "Name", model.RegionID);

            var provinceList = commonService.GetProvinceList();
            ViewData["ProvinceList"] = new SelectList(provinceList, "Id", "Name", model.ProvinceID);

            var cityList = commonService.GetCityList(model.ProvinceID);
            ViewData["CityList"] = new SelectList(cityList, "Id", "Name", model.CityID);

            //初始化投资公司
            var investCompanyIDList = companyService.GetCompanyList(1);
            ViewData["InvestCompanyList"] = new SelectList(investCompanyIDList, "Id", "ShortName", model.InvestCompanyId);
            
            //初始化管理公司
            var manageCompanyList = companyService.GetCompanyList(3);
            ViewData["ManageCompanyList"] = new SelectList(manageCompanyList, "Id", "ShortName", model.ManageCompanyId);

            var projectStageList = commonService.GetCommonFieldList("ProjectStage");
            ViewData["ProjectStageList"] = new SelectList(projectStageList, "FieldValue", "FieldName");

            return PartialView("_PartialEditProjectDetailInfo", model);
        }

        /// <summary>
        /// 保存项目详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult EditDetailInfo(ProjectInfoModel model)
        {
            if (!ModelState.IsValid)
            {

            }
            model.EditUser = Context.UserID;
            service.SaveDetailInfo(model);
            return Content(model.ProjectID.ToString());
        }

        /// <summary>
        /// 添加楼宇
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PartialViewResult AddBuilding(int projectId)
        {
            var model = projectInfoService.GetBuildingDetail(0);
            model.ProjectID = projectId;
            return PartialView("_PartialProjectBuildingInfo", model);
        }

        /// <summary>
        /// 新增楼宇
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddBuilding(BuildingDetailInfoModel model)
        {
            var result = new AjaxResult();
            model.InUser = Context.UserID;
            model.EditUser = Context.UserID;
            service.AddBuilding(model);
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// 编辑楼宇信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public PartialViewResult EditBuilding(int buildingId)
        {
            var model = projectInfoService.GetBuildingDetail(buildingId);

            return PartialView("_PartialEditProjectBuildingInfo", model);
        }

        /// <summary>
        /// 更新楼宇信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditBuilding(BuildingDetailInfoModel model)
        {
            var result = new AjaxResult();
            model.EditUser = Context.UserID;
            service.UpdateBuilding(model);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        
        /// <summary>
        /// 编辑楼层
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public PartialViewResult EditFloor(int floorId)
        {
            var model = projectInfoService.GetFloorDetail(floorId);
            return PartialView("_PartialEditProjectFloorInfo", model);
        }

        /// <summary>
        /// 保存楼层
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditFloor(FloorDetailModel model)
        {
            var result = new AjaxResult();
            model.EditUser = Context.UserID;
            service.UpdateFloor(model);
            return Json(result, JsonRequestBehavior.DenyGet);
        }

    }
}
