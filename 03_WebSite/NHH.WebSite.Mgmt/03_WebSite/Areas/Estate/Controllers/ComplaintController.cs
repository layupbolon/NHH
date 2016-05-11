using NHH.Framework.Security.Cryptography;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Common.Employee;
using NHH.Models.Estate;
using NHH.Models.Merchant;
using NHH.Models.Message;
using NHH.Models.Permission;
using NHH.Models.Project;
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
using System.Web.Security;

namespace NHH.WebSite.Management.Areas.Estate.Controllers
{
    /// <summary>
    /// 投诉管理
    /// </summary>
    public class ComplaintController : NHHController
    {
        private IComplaintService complaintService;
        private IComplaintCommentService commentService;
        private ISysUserService iUserService;
        private IEmployeeService iemployService;

        public ComplaintController()
        {
            complaintService = GetService<IComplaintService>();
            commentService = GetService<IComplaintCommentService>();
            iUserService = GetService<ISysUserService>();
            iemployService = GetService<IEmployeeService>();
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ComplaintListQueryInfo queryInfo)
        {
            queryInfo.CurrentUserId = Context.UserID;
            var model = complaintService.GetComplaintList(queryInfo);
            model.CrumbInfo.AddCrumb("投诉管理");

            var commonService = GetService<ICommonService>();
            var companyCommonService = GetService<ICompanyCommonService>();
            //投诉进度
            var complaintStatusList = commonService.GetCommonFieldList("ComplaintStatus");
            ViewData["ComplaintStatusList"] = new SelectList(complaintStatusList, "FieldValue", "FieldName", queryInfo.ComplaintStatus.HasValue ? queryInfo.ComplaintStatus.Value : 0);
            //投诉类型
            var complaintTypeList = commonService.GetCommonFieldList("ComplaintType");
            ViewData["ComplaintTypeList"] = new SelectList(complaintTypeList, "FieldValue", "FieldName", queryInfo.ComplaintType.HasValue ? queryInfo.ComplaintType : 0);

            //客服负责人（运营部所有人）
            var userInfo = new UserInfo() { UserId = Context.UserID };
            var projectConfig = GetService<IProjectBizConfigService>().GetBizConfig(101, userInfo);
            var serviceUserList = complaintService.GetComplaintServiceUserList(projectConfig);
            ViewData["ServiceUserList"] = new SelectList(serviceUserList, "Id", "Name", queryInfo.ServiceUserId.HasValue ? queryInfo.ServiceUserId : 0);

            return View(model);
        }

        /// <summary>
        /// 投诉详情修改
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        public ActionResult Detail(int complaintId)
        {
            var model = complaintService.GetComplaintDetail(complaintId);
            model.CrumbInfo.AddCrumb("投诉管理", Url.Action("List", "Complaint", new { area = "Estate" }));
            model.CrumbInfo.AddCrumb("投诉详情");

            var serviceUserList = GetService<ICompanyCommonService>().GetServiceUserList();
            ViewData["ServiceUserList"] = new SelectList(serviceUserList, "Id", "Name", model.ServiceUserId);

            var complaintStatusList = GetService<ICommonService>().GetCommonFieldList("ComplaintStatus");
            ViewData["ComplaintStatusList"] = new SelectList(complaintStatusList, "FieldValue", "FieldName", model.ComplaintStatus);

            return View(model);
        }

        /// <summary>
        /// 提交详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Detail(ComplaintDetailModel model)
        {
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            bool flag = complaintService.UpdateComplaint(model);
            var result = new AjaxResult();
            if (!flag)
            {
                result.Code = -1;
                result.Message = "更新失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new ComplaintDetailModel();
            model.CrumbInfo.AddCrumb("投诉管理", Url.Action("List", "Complaint", new { area = "Estate" }));
            model.CrumbInfo.AddCrumb("投诉单");

            //所在项目
            var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name");

            //投诉类型
            var complaintTypeList = GetService<ICommonService>().GetCommonFieldList("ComplaintType");
            ViewData["ComplaintTypeList"] = new SelectList(complaintTypeList, "FieldValue", "FieldName");

            return View(model);
        }

        /// <summary>
        /// 提交投诉单申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ComplaintDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("投诉管理", Url.Action("List", "Complaint", new { area = "Estate" }));
                model.CrumbInfo.AddCrumb("投诉单");

                //所在项目
                var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
                ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name");

                //投诉类型
                var complaintTypeList = GetService<ICommonService>().GetCommonFieldList("ComplaintType");
                ViewData["ComplaintTypeList"] = new SelectList(complaintTypeList, "FieldValue", "FieldName");

                return View(model);
            }

            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.ComplaintDetailUrl = Url.Action("Detail", "Complaint", new { area = "Estate" });
            model.ProjectConfig = GetService<IProjectBizConfigService>().GetBizConfig(101, model.ProjectId);
            var complaintId = complaintService.AddComplaint(model);
            return RedirectToAction("Detail", "Complaint", new { area = "Estate", complaintId = complaintId });
        }

        /// <summary>
        /// 获取指派处理人员弹出页面
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult PopAssignDialog(ComplaintAssignUserQueryInfo queryInfo)
        {
            queryInfo.UserInfo = new UserInfo();
            queryInfo.UserInfo.UserId = Context.UserID;
            queryInfo.UserInfo.UserName = Context.UserName;
            var model = GetService<IComplaintService>().GetComplaintAssignUserList(queryInfo);

            var projectConfig = GetService<IProjectBizConfigService>().GetBizConfig(101, queryInfo.ProjectId);
            int companyId = 0;
            int.TryParse(projectConfig.Param1, out companyId);
            var deptList = GetService<ICompanyCommonService>().GetDepartmentList(companyId);
            ViewData["DeptList"] = new SelectList(deptList, "DepartmentID", "DepartmentName", queryInfo.DeptId.HasValue ? queryInfo.DeptId.Value : 0);

            return PartialView("Complaint/_AssignPartial", model);
        }

        /// <summary>
        /// 获取重新指派处理人员弹出页面
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult PopReAssignDialog(ComplaintAssignUserQueryInfo queryInfo)
        {
            queryInfo.UserInfo = new UserInfo();
            queryInfo.UserInfo.UserId = Context.UserID;
            queryInfo.UserInfo.UserName = Context.UserName;
            var model = GetService<IComplaintService>().GetComplaintAssignUserList(queryInfo);

            var reAssignReason = GetService<ICommonService>().GetCommonFieldList("ReAssignReason");
            ViewData["ReAssignReasonList"] = new SelectList(reAssignReason, "FieldValue", "FieldName");

            var projectConfig = GetService<IProjectBizConfigService>().GetBizConfig(101, queryInfo.ProjectId);
            int companyId = 0;
            int.TryParse(projectConfig.Param1, out companyId);
            var deptList = GetService<ICompanyCommonService>().GetDepartmentList(companyId);
            ViewData["DeptList"] = new SelectList(deptList, "DepartmentID", "DepartmentName", queryInfo.DeptId.HasValue ? queryInfo.DeptId.Value : 0);

            return PartialView("Complaint/_ReAssignPartial", model);
        }

        /// <summary>
        /// 搁置弹出框
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PopShelveDialog()
        {
            var shelveReasonList = GetService<ICommonService>().GetCommonFieldList("ShelveReason");
            ViewData["ShelveReasonList"] = new SelectList(shelveReasonList, "FieldValue", "FieldName");
            return PartialView("Complaint/_ShelveReasonPartial");
        }

        /// <summary>
        /// 提交指派/重新指派客服人员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Assign(ComplaintInfo complaintInfo)
        {
            var result = new AjaxResult();
            complaintInfo.UserInfo = new UserInfo();
            complaintInfo.UserInfo.UserId = Context.UserID;
            complaintInfo.UserInfo.UserName = Context.UserName;
            complaintInfo.ComplaintDetailUrl = Url.Action("Detail", "Complaint", new { area = "Estate" });
            var flag = complaintService.AssginServiceUser(complaintInfo);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "指派失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 提交重新指派
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ReAssign(ComplaintInfo info)
        {
            var result = new AjaxResult();
            info.UserInfo = new UserInfo();
            info.UserInfo.UserId = Context.UserID;
            info.UserInfo.UserName = Context.UserName;
            info.ComplaintDetailUrl = Url.Action("Detail", "Complaint", new { area = "Estate" });
            var flag = GetService<IComplaintService>().ReAssginServiceUser(info);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "重新指派";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 提交搁置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Shelve(ComplaintShelveInfo model)
        {
            var result = new AjaxResult();
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.ComplaintDetailUrl = Url.Action("Detail", "Complaint", new { area = "Estate" });
            var flag = complaintService.ComplaintShelve(model);

            if (!flag)
            {
                result.Code = -1;
                result.Message = "搁置失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 根据ID得到客服人员详细信息
        /// </summary>
        /// <param name="repairUserName"></param>
        /// <returns></returns>
        public PartialViewResult GetServiceUserDetail(int serviceUserId)
        {
            var model = complaintService.GetServiceUserById(serviceUserId);
            return PartialView("Complaint/_UserInfoPartial", model);
        }

        /// <summary>
        /// 投诉评价列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult CommentList(ComplaintCommentQueryInfo queryInfo)
        {
            var model = commentService.GetCommentList(queryInfo);
            model.CrumbInfo.AddCrumb("投诉管理", Url.Action("List", "Complaint", new { area = "Estate" }));
            model.CrumbInfo.AddCrumb("评价管理");
            ///客服负责人
            var serviceUserList = GetService<ICompanyCommonService>().GetServiceUserList();
            ViewData["ServiceUserList"] = new SelectList(serviceUserList, "Id", "Name");
            ///投诉类型
            var complaintTypeList = GetService<ICommonService>().GetCommonFieldList("ComplaintType");
            ViewData["ComplaintTypeList"] = new SelectList(complaintTypeList, "FieldValue", "FieldName");

            return View(model);
        }

        /// <summary>
        /// 投诉评价详情弹出框
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult CommentDetail(int commentId)
        {
            var model = commentService.GetCommentDetail(commentId);
            return PartialView("Complaint/_CommentDetailPartial", model);
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        public ActionResult Finish(ComplaintInfo model)
        {
            model.UserInfo = new UserInfo();
            model.UserInfo.UserId = Context.UserID;
            model.UserInfo.UserName = Context.UserName;
            model.ComplaintDetailUrl = Url.Action("Detail", "Complaint", new { area = "Estate" });
            complaintService.Finish(model);
            return RedirectToAction("Detail", new { complaintId = model.ComplaintId });
        }

    }
}