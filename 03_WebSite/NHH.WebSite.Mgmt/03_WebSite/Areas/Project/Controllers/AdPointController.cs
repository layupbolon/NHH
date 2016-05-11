using NHH.Framework.Web;
using NHH.Models.Project.Adpoint;
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
    /// 广告位
    /// </summary>
    public class AdPointController : NHHController
    {
        private IAdPointService iService;
        private IProjectCommonService projectService;
        public AdPointController()
        {
            this.iService = this.GetService<IAdPointService>();
            projectService = GetService<IProjectCommonService>();
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult List(AdPointQueryInfo queryInfo)
        {
            var model = iService.GetAdPointList(queryInfo);
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("广告位");

            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            int projectId = projectList.Count > 0 ? projectId = projectList[0].Id : 0;
            projectId = queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : projectId;

            var floorList = GetService<IProjectCommonService>().GetAllFloor(projectId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName");

            var adPointTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointType");
            ViewData["AdPointTypeList"] = new SelectList(adPointTypeList, "FieldValue", "FieldName", queryInfo.Type.HasValue ? queryInfo.Type.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 广告位创建
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new AdPointDetailModel();
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("广告位管理", Url.Action("List", "AdPoint", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("新增广告位");

            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");

            int projectId = projectList.Count > 0 ? projectId = projectList[0].Id : 0;
            var floorList = GetService<IProjectCommonService>().GetAllFloor(projectId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName");


            var adPointTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointType");
            ViewData["AdPointTypeList"] = new SelectList(adPointTypeList, "FieldValue", "FieldName");

            //广告媒介
            var adPointMediaList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
            ViewData["AdPointMediaList"] = new SelectList(adPointMediaList, "FieldValue", "FieldName");

            return View(model);
        }

        /// <summary>
        /// 保存广告位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(AdPointDetailModel model)
        {
            if (ModelState.IsValid)
            {
                iService.SaveAdPoint(model);
                return RedirectToAction("List", "Adpoint", new { area = "Project" });
            }

            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("广告位管理", Url.Action("List", "AdPoint", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("新增广告位");

            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", model.ProjectID);

            int projectId = projectList.Count > 0 ? projectId = projectList[0].Id : 0;
            var floorList = GetService<IProjectCommonService>().GetAllFloor(projectId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName", model.FloorId);


            var adPointTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointType");
            ViewData["AdPointTypeList"] = new SelectList(adPointTypeList, "FieldValue", "FieldName", model.AdPointType);

            //广告媒介
            var adPointMediaList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
            ViewData["AdPointMediaList"] = new SelectList(adPointMediaList, "FieldValue", "FieldName", model.AdPointMedia);

            return View(model);

        }

        /// <summary>
        /// 编辑广告位信息
        /// </summary>
        /// <param name="adPointId"></param>
        /// <returns></returns>
        public ActionResult Edit(int adPointId)
        {
            var model = iService.GetAdPointDetail(adPointId);
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("广告位管理", Url.Action("List", "AdPoint", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("编辑广告位");

          //  var projectList = projectService.GetProjectList();
          //  ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", model.ProjectID);

           // var floorList = GetService<IProjectCommonService>().GetAllFloor(model.ProjectID);
          //  ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName");


            var adPointTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointType");
            ViewData["AdPointTypeList"] = new SelectList(adPointTypeList, "FieldValue", "FieldName", model.AdPointType);

            //广告媒介
            var adPointMediaList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
            ViewData["AdPointMediaList"] = new SelectList(adPointMediaList, "FieldValue", "FieldName", model.AdPointMedia);

            return View(model);
        }

        /// <summary>
        /// 保存广告位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AdPointDetailModel model)
        {
            if (ModelState.IsValid)
            {
                iService.SaveAdPoint(model);
                return RedirectToAction("List", "Adpoint", new { area = "Project" });
            }

            #region 验证失败则继续填写

            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("广告位管理", Url.Action("List", "AdPoint", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("编辑广告位");

            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", model.ProjectID);

            var floorList = GetService<IProjectCommonService>().GetAllFloor(model.ProjectID);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName",model.FloorId);


            var adPointTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointType");
            ViewData["AdPointTypeList"] = new SelectList(adPointTypeList, "FieldValue", "FieldName", model.AdPointType);

            //广告媒介
            var adPointMediaList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
            ViewData["AdPointMediaList"] = new SelectList(adPointMediaList, "FieldValue", "FieldName", model.AdPointMedia);

            return View(model);

            #endregion
        }
    }
}