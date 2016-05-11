using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Service.Common.IService;
using NHH.Service.Configure.IService;
using NHH.Service.Estate.IService;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Estate.Controllers
{
    /// <summary>
    /// 维修任务管理
    /// </summary>
    public class RepairController : NHHController
    {
        private IRepairService repairService;
        private IRepairCommentService commentService;


        public RepairController()
        {
            repairService = GetService<IRepairService>();
            commentService = GetService<IRepairCommentService>();
        }

        /// <summary>
        /// 维修列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(RepairListQueryInfo queryInfo)
        {
            queryInfo.CurrentUserId = Context.UserID;
            var model = repairService.GetRepairList(queryInfo);
            model.CrumbInfo.AddCrumb("维修管理");

            var repairTypeList = GetService<ICommonService>().GetCommonFieldList("RepairType");
            ViewData["RepairTypeList"] = new SelectList(repairTypeList, "FieldValue", "FieldName", queryInfo.Type.HasValue ? queryInfo.Type.Value : 0);

            var repairStatusList = GetService<ICommonService>().GetCommonFieldList("RepairStatus");
            ViewData["RepairStatusList"] = new SelectList(repairStatusList, "FieldValue", "FieldName", queryInfo.Status.HasValue ? queryInfo.Status.Value : 0);

            var userInfo = new UserInfo() { UserId = Context.UserID };
            var projectConfig = GetService<IProjectBizConfigService>().GetBizConfig(102, userInfo);
            var employeeList = GetService<IRepairService>().GetRepairAssignUserList(projectConfig);
            ViewData["RepairUserList"] = new SelectList(employeeList, "Id", "Name", queryInfo.RepairUserId.HasValue ? queryInfo.RepairUserId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 创建报修单
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new RepairDetailModel();
            model.CrumbInfo.AddCrumb("维修管理", Url.Action("List", "Repair", new { area = "Estate" }));
            model.CrumbInfo.AddCrumb("创建维修");

            var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name");

            int projectId = projectList.Count > 0 ? projectId = projectList[0].Id : 0;
            var floorList = GetService<IProjectCommonService>().GetAllFloor(projectId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName");

            return View(model);
        }

        /// <summary>
        /// 提交维修信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(RepairDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("维修管理", Url.Action("List", "Repair", new { area = "Estate" }));
                model.CrumbInfo.AddCrumb("创建维修");

                var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
                ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name");

                int projectId = projectList.Count > 0 ? projectId = projectList[0].Id : 0;
                var floorList = GetService<IProjectCommonService>().GetAllFloor(projectId);
                ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName");

                return View(model);
            }
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.RepairDetailUrl = Url.Action("Detail", "Repair", new { area = "Estate" });
            model.ProjectConfig = GetService<IProjectBizConfigService>().GetBizConfig(102, model.ProjectId);
            int repairId = repairService.AddRepair(model);
            return RedirectToAction("Detail", "Repair", new { area = "Estate", repairId = repairId });
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ContentResult AcceptImgUpload()
        {
            HttpPostedFileBase file = Request.Files["RepairPicture"];
            var result = "";
            if (file != null)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                string fileName = string.Format("Project-{0}-{1}.jpg", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9));
                string filePath = Path.Combine(HttpContext.Server.MapPath(Url.Content("../../UploadFiles/RepairPictures/")), fileName);
                file.SaveAs(filePath);
                result = fileName;
            }
            return Content(result);
        }

        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        public ActionResult Detail(int repairId)
        {
            var model = repairService.GetRepairDetail(repairId);
            model.CrumbInfo.AddCrumb("维修管理", Url.Action("List", "Repair", new { area = "Estate" }));
            model.CrumbInfo.AddCrumb("维修单详情");

            return View(model);
        }

        /// <summary>
        /// 根据ID得到维修人员相关信息
        /// </summary>
        /// <param name="repairUserName"></param>
        /// <returns></returns>
        public PartialViewResult GetRepairUserDetail(int repairUserId)
        {
            var model = repairService.GetRepairUserById(repairUserId);
            return PartialView("Repair/_UserInfoPartial", model);
        }

        /// <summary>
        /// 点评列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult CommentList(RepairCommentQueryInfo queryInfo)
        {
            var model = commentService.GetCommentList(queryInfo);
            model.CrumbInfo.AddCrumb("维修管理", Url.Action("List", "Repair", new { area = "Estate" }));
            model.CrumbInfo.AddCrumb("评价管理");

            var employeeList = GetService<ICompanyCommonService>().GetServiceUserList();
            ViewData["RepairUserList"] = new SelectList(employeeList, "Id", "Name", queryInfo.RepairUserId.HasValue ? queryInfo.RepairUserId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 获取点评弹出框信息
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult PopComment(int commentId)
        {
            var model = commentService.GetCommentDetail(commentId);
            return PartialView("Repair/_CommentDetailPartial", model);
        }

        /// <summary>
        /// 弹出维修人员信息--指派
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="repairUserId"></param>
        /// <returns></returns>
        public PartialViewResult PopAssignDialog(int projectId, int? repairUserId)
        {
            var projectConfig = GetService<IProjectBizConfigService>().GetBizConfig(102, projectId);
            var employeeList = GetService<IRepairService>().GetRepairAssignUserList(projectConfig);
            ViewData["RepairUserList"] = new SelectList(employeeList, "Id", "Name", repairUserId.HasValue ? repairUserId.Value : 0);

            return PartialView("Repair/_AssignPartial");
        }

        /// <summary>
        /// 重新指派弹出框
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="repairUserId"></param>
        /// <returns></returns>
        public PartialViewResult PopReAssignDialog(int projectId, int? repairUserId)
        {
            var projectConfig = GetService<IProjectBizConfigService>().GetBizConfig(102, projectId);
            var employeeList = GetService<IRepairService>().GetRepairAssignUserList(projectConfig);
            ViewData["RepairUserList"] = new SelectList(employeeList, "Id", "Name", repairUserId.HasValue ? repairUserId.Value : 0);

            var reAssignReason = GetService<ICommonService>().GetCommonFieldList("ReAssignReason");
            ViewData["ReAssignReasonList"] = new SelectList(reAssignReason, "FieldValue", "FieldName");
            return PartialView("Repair/_ReAssignPartial");
        }

        /// <summary>
        /// 搁置弹出框
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PopShelveDialog()
        {
            var shelveReasonList = GetService<ICommonService>().GetCommonFieldList("ShelveReason");
            ViewData["ShelveReasonList"] = new SelectList(shelveReasonList, "FieldValue", "FieldName");
            return PartialView("Repair/_ShelveReasonPartial");
        }

        /// <summary>
        /// 延迟弹出框
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PopDelayDialog()
        {
            var repairDelayReasonList = GetService<ICommonService>().GetCommonFieldList("RepairDelayReason");
            ViewData["RepairDelayReasonList"] = new SelectList(repairDelayReasonList, "FieldValue", "FieldName");
            return PartialView("Repair/_DelayReasonPartial");

        }

        /// <summary>
        /// 提交指派信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Assign(RepairAssignInfo model)
        {
            var result = new AjaxResult();
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.RepairDetailUrl = Url.Action("Detail", "Repair", new { area = "Estate" });
            var flag = repairService.RepairAssign(model);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "指派失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 提交重新指派信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ReAssign(RepairAssignInfo model)
        {
            var result = new AjaxResult();
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.RepairDetailUrl = Url.Action("Detail", "Repair", new { area = "Estate" });
            if (!model.EstimatedHour.HasValue)
            {
                model.EstimatedHour = model.Important == true ? 8 : 20;//当维修为紧急时默认为8个小时，非紧急默认为20个小时---当页面没有填写estmateHour时做此赋值
            }

            var flag = repairService.RepairReAssign(model);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "重新指派失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 结束报修
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Finish(RepairFinishInfo model)
        {
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.RepairDetailUrl = Url.Action("Detail", "Repair", new { area = "Estate" });
            repairService.RepairFinish(model);
            return Json("Ok", JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 提交搁置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Shelve(RepairShelveInfo model)
        {
            var result = new AjaxResult();
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.RepairDetailUrl = Url.Action("Detail", "Repair", new { area = "Estate" });
            var flag = repairService.RepairShelve(model);

            if (!flag)
            {
                result.Code = -1;
                result.Message = "搁置失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 提交延迟信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delay(RepairDelayInfo model)
        {
            var result = new AjaxResult();
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.RepairDetailUrl = Url.Action("Detail", "Repair", new { area = "Estate" });
            var flag = repairService.RepairDelay(model);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "延迟失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑维修报价,以后要增加编辑字段可扩展
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(RepairDetailEditInfo info)
        {
            var result = new AjaxResult();
            info.UserInfo = new UserInfo();
            info.UserInfo.UserId = Context.UserID;
            info.UserInfo.UserName = Context.UserName;
            var flag = repairService.UpdateQuoteAmount(info);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "保存失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}