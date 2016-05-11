using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Merchant;
using NHH.Service.Common.IService;
using NHH.Service.Merchant.IService;
using NHH.WebSite.Management.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    /// <summary>
    /// 商家用户管理
    /// </summary>
    public class MerchantUserController : NHHController
    {
        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(MerchantUserListQueryInfo queryInfo)
        {
            //获取商户用户状态下拉列表
            var model = GetService<IMerchantUserService>().GetUserList(queryInfo);
            model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("商户员工");

            var jobStatusList = GetService<ICommonService>().GetCommonFieldList("MerchantUserStatus");
            ViewData["JobStatusList"] = new SelectList(jobStatusList, "FieldValue", "FieldName", queryInfo.Status.HasValue ? queryInfo.Status.Value : 0);
            return View(model);
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ActionResult Add(int merchantId)
        {
            var model = new MerchantUserDetailModel();
            model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("商户员工", Url.Action("List", "MerchantUser", new { area = "Plan", merchantId = merchantId }));
            model.CrumbInfo.AddCrumb("新增员工");

            var storeList = GetService<IMerchantStoreService>().GetStoreList(merchantId);
            ViewData["StoreList"] = new SelectList(storeList, "StoreId", "StoreName");

            var commonService = GetService<ICommonService>();
            //学历
            var educationList = commonService.GetCommonFieldList("Education");
            ViewData["EducationList"] = new SelectList(educationList, "FieldValue", "FieldName");

            //获取证件下拉列表
            var typeList = commonService.GetCommonFieldList("MechantUserAttach");
            ViewData["MechantUserAttachList"] = new SelectList(typeList, "FieldValue", "FieldName");

            //民族
            var nationlist = commonService.GetCommonFieldList("Nation");
            ViewData["NationList"] = new SelectList(nationlist, "FieldValue", "FieldName");

            //政治面貌
            var politicalStatuslist = commonService.GetCommonFieldList("PoliticalStatus");
            ViewData["PoliticalStatuslist"] = new SelectList(politicalStatuslist, "FieldValue", "FieldName");

            model.MerchantId = merchantId;
            return View(model);
        }

        /// <summary>
        /// 提交新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(MerchantUserDetailModel model)
        {
            if (ModelState.IsValid)
            {
                model.InUser = Context.UserID;
                model.EditUser = Context.UserID;
                var result = GetService<IMerchantUserService>().AddUser(model);
                if (!result.IsSuccess())
                {
                    ModelState.AddModelError(result.Key, result.Desc);
                }
            }

            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
                model.CrumbInfo.AddCrumb("商户员工", Url.Action("List", "MerchantUser", new { area = "Plan", merchantId = model.MerchantId }));
                model.CrumbInfo.AddCrumb("新增员工");

                var storeList = GetService<IMerchantStoreService>().GetStoreList(model.MerchantId);
                ViewData["StoreList"] = new SelectList(storeList, "StoreId", "StoreName");

                var commonService = GetService<ICommonService>();
                //学历
                var educationList = commonService.GetCommonFieldList("Education");
                ViewData["EducationList"] = new SelectList(educationList, "FieldValue", "FieldName");
                //获取证件下拉列表
                var typeList = commonService.GetCommonFieldList("MechantUserAttach");
                typeList = typeList.Where(p => p.FieldValue != 3).ToList();
                ViewData["MechantUserAttachList"] = new SelectList(typeList, "FieldValue", "FieldName");
                //民族
                var nationlist = commonService.GetCommonFieldList("Nation");
                ViewData["NationList"] = new SelectList(nationlist, "FieldValue", "FieldName");
                //政治面貌
                var politicalStatuslist = commonService.GetCommonFieldList("PoliticalStatus");
                ViewData["PoliticalStatuslist"] = new SelectList(politicalStatuslist, "FieldValue", "FieldName");
                model.MerchantId = model.MerchantId;

                return View(model);
            }
            return RedirectToAction("Edit", "MerchantUser", new { area = "Plan", userId = model.UserId });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult Edit(int userId)
        {
            var model = GetService<IMerchantUserService>().GetUserDetail(userId);
            model.CrumbInfo.AddCrumb("商户员工", Url.Action("List", "MerchantUser", new { area = "Plan", merchantId = model.MerchantId }));
            model.CrumbInfo.AddCrumb("编辑员工");

            if (string.IsNullOrEmpty(model.PhotoFile) || model.PhotoFile.Length == 0)
            {
                model.PhotoFile = "default-img.jpg";
            }

            var storeList = GetService<IMerchantStoreService>().GetStoreList(model.MerchantId);
            ViewData["StoreList"] = new SelectList(storeList, "StoreId", "StoreName", model.StoreId.HasValue ? model.StoreId.Value : 0);

            var commonService = GetService<ICommonService>();
            //学历
            var educationList = commonService.GetCommonFieldList("Education");
            ViewData["EducationList"] = new SelectList(educationList, "FieldValue", "FieldName", model.Education);
            //民族
            var nationlist = commonService.GetCommonFieldList("Nation");
            ViewData["NationList"] = new SelectList(nationlist, "FieldValue", "FieldName", model.Nation);
            //政治面貌
            var politicalStatuslist = commonService.GetCommonFieldList("PoliticalStatus");
            ViewData["PoliticalStatuslist"] = new SelectList(politicalStatuslist, "FieldValue", "FieldName", model.PoliticalStatus);
            
            return View(model);
        }

        /// <summary>
        /// 提交编辑信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(MerchantUserDetailModel model)
        {
            model.EditUser = Context.UserID;
            bool flag = GetService<IMerchantUserService>().UpdateUser(model);
            return RedirectToAction("List", "MerchantUser", new { area = "Plan", merchantId = model.MerchantId });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del(int merchantId, int userId)
        {
            var result = new AjaxResult();
            bool flag = GetService<IMerchantUserService>().DeleteUser(merchantId, userId);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "删除失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);

        }

        /// <summary>
        /// 用户证件列表局部页
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult PaperList(int userid)
        {
            var model = GetService<IMerchantUserPaperService>().GetPaperList(userid);
            model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("用户管理", Url.Action("List", "MerchantUser", new { area = "Plan", MerchantId = model.MerchantId }));
            model.CrumbInfo.AddCrumb("证件管理");

            return View(model);
        }

        /// <summary>
        /// 新增用户证件局部页
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public PartialViewResult AddPaper(int userid)
        {
            //获取证件下拉列表
            var typeList = GetService<ICommonService>().GetCommonFieldList("MechantUserAttach");            
            ViewData["UserPaperTypeList"] = new SelectList(typeList, "FieldValue", "FieldName");

            return PartialView("_PartialMerchantUserPaperAdd");
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SavePaper(MerchantUserPaperInfo model)
        {
            AjaxResult result = new AjaxResult();
            result.Code = 1;
            //先获取UserId
            model.InUser = Context.UserID;
            model.EditUser = Context.UserID;
            GetService<IMerchantUserPaperService>().AddPaper(model);
            return Json(result);
        }

        /// <summary>
        /// 删除单张附件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        public JsonResult DelPaper(int userId, int paperId)
        {
            AjaxResult ajaxResult = new AjaxResult();
            //删除证件
            GetService<IMerchantUserPaperService>().DelPaper(userId, paperId);
            ajaxResult.Code = 0;
            return Json(ajaxResult);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ActionResult DelUserId(int userid, int merchantId)
        {
            GetService<IMerchantUserService>().DeleteUser(merchantId, userid);
            return RedirectToAction("List", merchantId);
        }
    }
}