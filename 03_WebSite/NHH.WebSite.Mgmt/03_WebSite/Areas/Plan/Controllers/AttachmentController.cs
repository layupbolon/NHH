using NHH.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHH.Models.Merchant;
using NHH.Models.Common;
using NHH.Service.Merchant.IService;
using NHH.Service.Common.IService;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    /// <summary>
    /// 附件管理
    /// </summary>
    public class AttachmentController : NHHController
    {

        // GET: Merchant/Attachment
        public ActionResult List(int merchantId)
        {
            var model = GetService<IAttachmentService>().GetAttachmentList(merchantId);
            model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("证照信息");
            
            return View(model);
        }

        /// <summary>
        /// 打开新增页面
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ActionResult Add(int merchantId)
        {
            var model = new MerchantAttachmentDetailModel();
            model.CrumbInfo.AddCrumb("证照信息", Url.Action("List", "Attachment", new { area = "Plan", merchantId = merchantId }));
            model.CrumbInfo.AddCrumb("新增证照");

            var merchantType = GetService<IMerchantService>().GetMerchantType(merchantId);

            model.MerchantId = merchantId;
            var typeList = GetService<ICommonService>().GetCommonFieldList("MerchantAttachType");
            if (merchantType == 1)//当商户是公司时
            {
                ViewData["AttachmentTypeList"] = new SelectList(typeList, "FieldValue", "FieldName");
                return View(model);
            }
            typeList = typeList.Where(m => m.FieldValue == 4 || m.FieldValue == 6).ToList();
            ViewData["AttachmentTypeList"] = new SelectList(typeList, "FieldValue", "FieldName");

            return View(model);
        }

        /// <summary>
        /// 添加附件信息到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(MerchantAttachmentDetailModel model)
        {
            var result = new AjaxResult();
            //数据库记录附件信息
            model.UserId = Context.UserID;
            var message = GetService<IAttachmentService>().AddAttachment(model);
            if (!message.IsSuccess())
            {
                result.Code = message.Code;
                result.Message = message.Desc;
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// 删除附件信息
        /// </summary>
        /// <param name="AttachmentId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del(int AttachmentId)
        {
            var result = new AjaxResult();
            var message = GetService<IAttachmentService>().DelAttachment(AttachmentId); ;
            if (!message.IsSuccess())
            {
                result.Code = message.Code;
                result.Message = message.Desc;
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}