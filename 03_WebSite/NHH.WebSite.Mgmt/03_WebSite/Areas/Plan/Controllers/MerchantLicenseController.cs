using System;
using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Approve;
using NHH.Models.Plan.MerchantLicense;
using NHH.Service.Common.IService;
using NHH.Service.Plan.IService;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    public class MerchantLicenseController : NHHController
    {
        private readonly IMerchantLicenseService _service;

        public MerchantLicenseController()
        {
            _service = GetService<IMerchantLicenseService>();
        }

        /// <summary>
        /// 证照列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(MerchantLicenseQueryModel queryInfo)
        {
            var model = _service.GetMerchantLicenseList(queryInfo);
            model.CrumbInfo.AddCrumb("证照管理");

            var documentTypeList = GetService<ICommonService>().GetCommonFieldList("MerchantAttachStatus");
            ViewData["StatusList"] = new SelectList(documentTypeList, "FieldValue", "FieldName", queryInfo.Status ?? 0);

            return View(model);
        }

        /// <summary>
        /// 证照详情
        /// </summary>
        /// <param name="AttachmentID"></param>
        /// <returns></returns>
        public ActionResult Detail(int AttachmentID)
        {
            var model = _service.GetMerchantLicense(AttachmentID);
            model.CrumbInfo.AddCrumb("证照管理", Url.Action("List", "MerchantLicense", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("证照详情");
            return View(model);
        }

        /// <summary>
        /// 添加审批意见
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Approve(ApproveModel approve)
        {
            approve.ApproveTime = DateTime.Now;
            approve.UserID = Context.UserID;
            var result = _service.Approve(approve);
            return Json(result);
        }
    }
}