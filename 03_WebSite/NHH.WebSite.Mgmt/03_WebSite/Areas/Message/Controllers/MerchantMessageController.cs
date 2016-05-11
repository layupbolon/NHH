using NHH.Framework.Web;
using NHH.Models.Common.Employee;
using NHH.Models.Merchant;
using NHH.Models.Message;
using NHH.Models.Permission;
using NHH.Models.Project;
using NHH.Service.Common.IService;
using NHH.Service.Estate;
using NHH.Service.Estate.IService;
using NHH.Service.Merchant.IService;
using NHH.Service.Message;
using NHH.Service.Message.IService;
using NHH.Service.Permission.IService;
using NHH.Service.Project.IService;
using NHH.WebSite.Management.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Message.Controllers
{
    /// <summary>
    /// 商户消息
    /// </summary>
    public class MerchantMessageController : NHHController
    {        
        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(MerchantMessageListQueryInfo queryInfo)
        {
            var model = GetService<IMerchantMessageService>().GetList(queryInfo);
            model.CrumbInfo.AddCrumb("商户消息");

            var projectService = GetService<IProjectCommonService>();
            int projectId = queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0;

            var projectList = projectService.GetProjectList(Context.UserID);
            if (projectList != null && projectList.Count > 0 &&
                projectId == 0)
            {
                projectId = projectList[0].Id;
            }
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", projectId);

            int buildingId = queryInfo.BuildingId.HasValue ? queryInfo.BuildingId.Value : 0;
            var buildingList = projectService.GetBuildingList(projectId);
            if (buildingList != null && buildingList.Count > 0 && buildingId ==0)
            {
                buildingId = buildingList[0].Id; 
            }
            ViewData["BuildingList"] = new SelectList(buildingList, "Id", "Name", buildingId);

            var floorList = projectService.GetFloorList(projectId, buildingId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName", queryInfo.FloorId.HasValue ? queryInfo.FloorId.Value : 0);
            
            return View(model);
        }

        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public ActionResult Detail(int messageId)
        {
            var model = GetService<IMerchantMessageService>().GetDetail(messageId);
            model.CrumbInfo.AddCrumb("商户消息", Url.Action("List", "MerchantMessage", new { area = "Message" }));
            model.CrumbInfo.AddCrumb("商户消息详情");

            return View(model);
        }
        
        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Add(MerchantMessageListQueryInfo queryInfo)
        {
            var model = new MerchantMessageDetailModel();
            model.CrumbInfo.AddCrumb("商户消息", Url.Action("List", "MerchantMessage", new { area = "Message" }));
            model.CrumbInfo.AddCrumb("发布商户消息");

            int projectId = 0;
            var projectService = GetService<IProjectCommonService>();
            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");

            if (projectList != null && projectList.Count > 0 &&
               projectId == 0)
            {
                projectId = projectList[0].Id;
            }
            int buildingId = queryInfo.BuildingId.HasValue ? queryInfo.BuildingId.Value : 0;
            var buildingList = projectService.GetBuildingList(projectId);
            ViewData["BuildingList"] = new SelectList(buildingList, "Id", "Name", buildingId);

            var floorList = projectService.GetFloorList(projectId, buildingId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName", queryInfo.FloorId.HasValue ? queryInfo.FloorId.Value : 0);

            var commonService = GetService<ICommonService>();

            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", queryInfo.UnitType.HasValue ? queryInfo.UnitType.Value : 0);

            var bizTypeList = commonService.GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", queryInfo.BizType.HasValue ? queryInfo.BizType.Value : 0);

            var messageLevelList = commonService.GetCommonFieldList("MessageLevel");
            ViewData["MessageLevelList"] = new SelectList(messageLevelList, "FieldValue", "FieldName");

            model.UserID = NHHWebContext.Current.UserID;

            return View(model);
        }

        /// <summary>
        /// 发送提交商户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(MerchantMessageInfo model)
        {
            model.InUser = Context.UserID;
            model.UserID = Context.UserID;
            GetService<IMerchantMessageService>().Add(model);
            return RedirectToAction("List", "MerchantMessage", new { area = "Message" });
        }
    }
}