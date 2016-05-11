using NHH.Framework.Web;
using NHH.Models.Common;
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
    /// 铺位管理
    /// </summary>
    public class ProjectUnitAdjustController : NHHController
    {
        /// <summary>
        /// 合并
        /// </summary>
        /// <returns></returns>
        public ActionResult Merge()
        {
            var model = new BaseModel();
            model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("合并铺位");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");
            
            return View(model);
        }

        /// <summary>
        /// 拆分
        /// </summary>
        /// <returns></returns>
        public ActionResult Split()
        {
            var model = new BaseModel();
            model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("拆分铺位");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");
            
            return View(model);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ProjectUnitAdjustListQueryInfo queryInfo)
        {
            var model = GetService<IProjectUnitAdjustService>().GetAdjustList(queryInfo);
            model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("铺位调整记录");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            var adjustTypeList = GetService<ICommonService>().GetCommonFieldList("UnitAdjustType");
            ViewData["AdjustTypeList"] = new SelectList(adjustTypeList, "FieldValue", "FieldName", queryInfo.AdjustType.HasValue ? queryInfo.AdjustType.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="adjustId"></param>
        /// <returns></returns>
        public ActionResult Info(int adjustId)
        {
            var model = GetService<IProjectUnitAdjustService>().GetAdjustInfo(adjustId);

            model.CrumbInfo.AddCrumb("铺位管理", Url.Action("List", "ProjectUnit", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("铺位调整记录", Url.Action("List", "ProjectUnitAdjust", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("铺位调整详情");

            return View(model);
        }
    }
}