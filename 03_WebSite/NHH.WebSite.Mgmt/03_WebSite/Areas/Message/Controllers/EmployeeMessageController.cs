using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Common.Employee;
using NHH.Models.Message;
using NHH.Models.Permission;
using NHH.Service.Common.IService;
using NHH.Service.Estate;
using NHH.Service.Estate.IService;
using NHH.Service.Merchant.IService;
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
    /// 员工消息
    /// </summary>
    public class EmployeeMessageController : NHHController
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            EmployeeMessageDetailModel model = new EmployeeMessageDetailModel();
            model.CrumbInfo.AddCrumb("员工消息", Url.Action("List", "EmployeeMessage", new { area = "Message" }));
            model.CrumbInfo.AddCrumb("发布员工消息");

            //获取当前用户,并且更具当前用户查找对应的项目
            var employeeInfo = GetService<IUserCommonService>().GetEmployeeInfo(Context.UserID);
            model.DepartmentName = employeeInfo.Department;
            model.EmployeeId = employeeInfo.Id;
            model.EmployeeName = employeeInfo.Name;

            return View(model);
        }

        /// <summary>
        /// 提交保存发送给员工的信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(EmployeeMessageInfo message)
        {
            message.UserInfo = new UserInfo
            {
                UserId = Context.UserID,
            };
            GetService<IEmployeeMessageService>().SaveEmployeeMessage(message);
            return RedirectToAction("List");
        }


        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(EmployeeMessageListQueryInfo queryInfo)
        {
            var model = GetService<IEmployeeMessageService>().GetMessageList(queryInfo);
            model.CrumbInfo.AddCrumb("员工消息");

            return View(model);
        }

        /// <summary>
        /// 详细内容
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public ActionResult Detail(int messageId)
        {
            var model = GetService<IEmployeeMessageService>().GetDetail(messageId);
            model.CrumbInfo.AddCrumb("员工消息", Url.Action("List", "EmployeeMessage", new { area = "Message" }));
            model.CrumbInfo.AddCrumb("消息内容");

            return View(model);
        }

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="emploeeId"></param>
        /// <returns></returns>
        public PartialViewResult GetEmployeeList()
        {
            var companyList = GetService<IEmployeeCommonService>().GetCompanyList(Context.UserID);
            ViewData["CompanyList"] = new SelectList(companyList, "Id", "ShortName");

            return PartialView("Employee/_PartialEmployeeList");
        }
    }
}