using NHH.Framework.Exceptions;
using NHH.Framework.Web;
using NHH.Models.Common;
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
    /// 商铺管理
    /// </summary>
    public class ProjectUnitController : NHHController
    {
        /// <summary>
        /// 商铺列表查询
        /// </summary>
        /// <param name="queryInfo">查询信息</param>
        /// <returns></returns>
        public ActionResult List(ProjectUnitListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitService>().GetProjectUnitList(queryInfo);
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("铺位管理");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);
            
            var unitTypeList = GetService<ICommonService>().GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", queryInfo.UnitType.HasValue ? queryInfo.UnitType.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 商铺信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ActionResult Info(int unitId)
        {
            var model = GetService<IProjectUnitService>().GetProjectUnitInfo(unitId);

            model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("铺位信息");

            return View(model);
        }

        /// <summary>
        /// 添加商铺
        /// </summary>
        /// <param name="floorId"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public ActionResult Add(int floorId, int? unitType)
        {
            var model = new ProjectUnitInfoModel
            {
                FloorId = floorId,
                ContractStatus = 1,
            };

            model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project", floorId = floorId }));
            model.CrumbInfo.AddCrumb("新增铺位");

            var floorInfo = GetService<IProjectUnitService>().GetFloorInfo(floorId);
            model.ProjectId = floorInfo.ProjectId;
            model.BuildingId = floorInfo.BuildingId;
            model.BuildingName = floorInfo.BuildingName;
            model.FloorId = floorInfo.FloorId;
            model.FloorName = floorInfo.FloorName;
            model.FloorMapFileName = floorInfo.FloorMapFileName;

            var commonService = GetService<ICommonService>();

            var typeId = unitType.HasValue ? unitType.Value : 1;
            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", typeId);

            var contractStatusList = commonService.GetContractStatusList();
            ViewData["ContractStatusList"] = new SelectList(contractStatusList, "Id", "Name");
            
            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProjectUnitInfoModel model)
        {
            model.UserId = Context.UserID;
            if (ModelState.IsValid)
            {
                var result = GetService<IProjectUnitService>().AddProjectUnit(model);
                if (!result.IsSuccess())
                {
                    ModelState.AddModelError(result.Key, result.Desc);
                }
            }
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project", floorId = model.FloorId }));
                model.CrumbInfo.AddCrumb("新增铺位");

                var floorInfo = GetService<IProjectUnitService>().GetFloorInfo(model.FloorId);
                model.ProjectId = floorInfo.ProjectId;
                model.BuildingId = floorInfo.BuildingId;
                model.BuildingName = floorInfo.BuildingName;
                model.FloorId = floorInfo.FloorId;
                model.FloorName = floorInfo.FloorName;
                model.FloorMapFileName = floorInfo.FloorMapFileName;

                var commonService = GetService<ICommonService>();

                var typeId = model.UnitTypeId;
                var unitTypeList = commonService.GetUnitTypeList();
                ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", typeId);

                var contractStatusList = commonService.GetContractStatusList();
                ViewData["ContractStatusList"] = new SelectList(contractStatusList, "Id", "Name");

                return View(model);

            }
            return RedirectToAction("List", new { area = "Project", projectId = model.ProjectId });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ActionResult Edit(int unitId)
        {
            var model = GetService<IProjectUnitService>().GetProjectUnitInfo(unitId);

            model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("铺位信息");

            var commonService = GetService<ICommonService>();

            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", model.UnitTypeId);

            var contractStatusList = commonService.GetContractStatusList();
            ViewData["ContractStatusList"] = new SelectList(contractStatusList, "Id", "Name", model.ContractStatus);

            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ProjectUnitInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project" }));
                model.CrumbInfo.AddCrumb("铺位信息");

                var commonService = GetService<ICommonService>();

                var unitTypeList = commonService.GetUnitTypeList();
                ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", model.UnitTypeId);

                var contractStatusList = commonService.GetContractStatusList();
                ViewData["ContractStatusList"] = new SelectList(contractStatusList, "Id", "Name", model.ContractStatus);

                return View(model);

            }
            model.UserId = Context.UserID;
            GetService<IProjectUnitService>().UpdateProjectUnit(model);
            return RedirectToAction("List", new { area = "Project", projectId = model.ProjectId });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del(int unitId, int projectId)
        {
            var result = new AjaxResult();
            var message = GetService<IProjectUnitService>().DelProjectUnit(unitId);

            if (!message.IsSuccess())
            {
                result.Message = message.Desc;
                result.Code = message.Code;
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 铺位初始化信息
        /// </summary>
        /// <param name="initInfo"></param>
        /// <returns></returns>
        public ActionResult Init(ProjectUnitInitInfo initInfo)
        {
            var model = new ProjectUnitInitModel();
            model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("铺位初始化");

            model.UnitCodeLength = 2;

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", initInfo.ProjectId.HasValue ? initInfo.ProjectId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult UploadFile()
        {
            HttpPostedFileBase file = Request.Files["file"];
            if (file != null)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                string fileName = string.Format("ProjectUnit-{0}-{1}.xls", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9));
                string filePath = Path.Combine(HttpContext.Server.MapPath("../../UploadFiles/ProjectUnits/"), fileName);
                file.SaveAs(filePath);
            }

            return Content("Ok");
        }
    }
}