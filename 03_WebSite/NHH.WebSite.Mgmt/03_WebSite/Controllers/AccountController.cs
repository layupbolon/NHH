using NHH.Framework.Caching;
using NHH.Framework.Utility;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Permission;
using NHH.Service.Common.IService;
using NHH.Service.Message.IService;
using NHH.Service.Permission.IService;
using NHH.WebSite.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NHH.WebSite.Management.Controllers
{

    [AllowAnonymous]
    public class AccountController : NHHController
    {
        #region AccountService
        private IAccountService m_AccountService;
        public IAccountService AccountService
        {
            get
            {
                if (m_AccountService == null)
                    m_AccountService = this.GetService<IAccountService>();
                return m_AccountService;
            }
        }
        #endregion

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginRequest model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                model.ClientIP = this.Request.UserHostAddress;
                var rlt = this.AccountService.Login(model);
                if (rlt.IsSuccess())
                {
                    var userData = new SortedList<string, string>();
                    userData.Add("UserID", rlt.Data.UserID.ToString());
                    userData.Add("UserName", rlt.Data.UserName);
                    userData.Add("RoleID", rlt.Data.RoleIDs == null ? "" : string.Join(",", rlt.Data.RoleIDs));
                    userData.Add("RoleName", rlt.Data.RoleNames == null ? "" : string.Join(",", rlt.Data.RoleNames));
                    if (rlt.Data.EmployeeID != null)
                    {
                        userData.Add("EmployeeID", rlt.Data.EmployeeID.ToString());
                        userData.Add("EmployeeName", rlt.Data.EmployeeName);
                    }
                    if (rlt.Data.DepartmentID != null)
                    {
                        userData.Add("DepartmentID", rlt.Data.DepartmentID.ToString());
                        userData.Add("DepartmentName", rlt.Data.DepartmentName);
                    }
                    if (rlt.Data.CompanyID != null)
                    {
                        userData.Add("CompanyID", rlt.Data.CompanyID.ToString());
                        userData.Add("CompanyName", rlt.Data.CompanyName);
                    }
                    NHHWebContext.Current.SignIn(userData);
                    string retUrl = string.IsNullOrEmpty(this.HttpContext.Request["ReturnUrl"]) ? FormsAuthentication.DefaultUrl : this.HttpContext.Request["ReturnUrl"];
                    return Redirect(retUrl);
                }
                else
                {
                    this.ModelState.AddModelError("Password", rlt.Desc ?? "用户名或密码错误");
                }
            }

            return View(model);

        }




        public ActionResult Logout()
        {
            NHHWebContext.Current.SignOut();
            return Redirect("~/");
        }

        /// <summary>
        /// 弹出密码修改层
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public PartialViewResult ShowChangePasswordPage()
        {
            return PartialView("_PartialPasswordOption");
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ContentResult JudgeUserName(string UserName)
        {
            string userName = UserName;
            bool flag = AccountService.SearchExsitName(userName);
            if (flag)
            {
                return Content("success");
            }
            else
            {
                return Content("no exist");
            }
        }

        /// <summary>
        /// 跳转到忘记密码的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FotGetPassword()
        {
            return View();
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SendEmail(string email)
        {
            var result = new AjaxResult();
            //先验证该邮箱是否为已经存在的用户邮箱
            var isExisst = AccountService.SearchExsitName(email);

            if (!isExisst)
            {
                result.Message = "该邮件不存在，请验证后再试";
                result.Code = -1;
                return Json(result, JsonRequestBehavior.DenyGet);
            }

            //验证码
            var code = "";
            var random = new Random((int)DateTime.Now.Ticks);
            const string textArray = "23456789ABCDEFGHGKLMNPQRSTUVWXYZ";
            for (var i = 0; i < 7; i++)
            {
                code += textArray.Substring(random.Next() % textArray.Length, 1);
            }
            CacheManager.Default.SlidingExpiration = new TimeSpan(0, 10, 0);
            CacheManager.Default.Add(string.Format("{0}_Code", email), code);

            //邮件
            var message = new MessageInfo
            {
                MessageType = 1,
                Recipient = email,
                Subject = "立天唐人智能商业管理平台-密码找回验证码",
                Content = string.Format("请将后面的验证码填写回忘记密码的页面：{0}", code),
            };

            GetService<IMessageService>().Add(message);

            result.Message = "邮件已发送，请查收";
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="passWord"></param>
        /// <param name="veriCode"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetNewPwd(string passWord, string veriCode, string email)
        {
            var result = new AjaxResult();

            //先验证缓存的验证码
            string code = CacheManager.Default.Get<string>(string.Format("{0}_Code", email));
            if (veriCode != code)
            {
                result.Code = -1;
                result.Message = "验证码输入错误，请验证后再试";
                return Json(result, JsonRequestBehavior.DenyGet);
            }

            //进行密码修改
            AccountService.UpdatePassword(email, passWord);
            result.Message = "密码修改成功";

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}
