using NHH.Framework.Web;
using NHH.Models.Campaign;
using NHH.Service.Campaign.IService;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Management.Areas.Campaign.Controllers
{
    /// <summary>
    /// 企划管理
    /// </summary>
    public class CampaignController : NHHController
    {
        private ICampaignService campaignService;
        public CampaignController()
        {
            this.campaignService = GetService<ICampaignService>();

        }

        /// <summary>
        /// 企划活动列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List(CampaignQueryInfo queryInfo)
        {
            var model = campaignService.GetCampaignList(queryInfo);
            model.CrumbInfo.AddCrumb("企划管理");

            //唐小二
            var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name", model.QueryInfo.ProjectId ?? 0);

            //官网
            var nhhProjectList = GetService<INHHCGService>().GetProjectList();
            ViewData["NhhProjectList"] = new SelectList(nhhProjectList, "ProjectID", "ProjectName", model.QueryInfo.ProjectId ?? 0);

            var campaignTypeList = GetService<ICommonService>().GetCommonFieldList("CampaignType");
            ViewData["CampaignTypeList"] = new SelectList(campaignTypeList, "FieldValue", "FieldName",
                model.QueryInfo.CampaignType ?? 0);

            var campaignStatusList = GetService<ICommonService>().GetCommonFieldList("CampaignStatus");
            ViewData["CampaignStatusList"] = new SelectList(campaignStatusList, "FieldValue", "FieldName",
                model.QueryInfo.CampaignStatus ?? 0);

            ICommonService commonService = GetService<ICommonService>();
            var pageTypeList = commonService.GetCommonFieldList("BannerTarget");
            ViewData["PageTypeList"] = new SelectList(pageTypeList, "FieldValue", "FieldName",
                queryInfo.PageType ?? 0);

            return View(model);
        }

        /// <summary>
        /// 新增活动页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new CampaignPlanDetailModel();
            model.CrumbInfo.AddCrumb("企划管理", Url.Action("List", "Campaign", new { area = "Campaign" }));
            model.CrumbInfo.AddCrumb("创建企划活动");

            var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name");

            var campaignTypeList = GetService<ICommonService>().GetCommonFieldList("CampaignType");
            ViewData["CampaignTypeList"] = new SelectList(campaignTypeList, "FieldValue", "FieldName");

            var publishTimeList = GetService<ICommonService>().GetCommonFieldList("PublishTime");
            ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName");

            return View(model);
        }

        /// <summary>
        /// 获取企划活动详情
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public ActionResult Detail(int pageID)
        {
            var model = campaignService.GetCampaignPlanDetail(pageID);
            model.CrumbInfo.AddCrumb("企划管理", Url.Action("List", "Campaign", new { area = "Campaign" }));
            model.CrumbInfo.AddCrumb("企划活动详情");

            var campaignTypeList = GetService<ICommonService>().GetCommonFieldList("CampaignType");
            ViewData["CampaignTypeList"] = new SelectList(campaignTypeList, "FieldValue", "FieldName", model.CampaignType);

            var publishTimeList = GetService<ICommonService>().GetCommonFieldList("PublishTime");
            ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName", model.PublishTime);

            if (model.PublishStatus == 2)
            {
                return View("Detail2", model);//Detail2:当企划页面已经发布了后就锁定它
            }

            return View(model);
        }

        /// <summary>
        /// 提交新增企划活动信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(CampaignPlanDetailModel model)
        {

            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("企划管理", Url.Action("List", "Campaign", new { area = "Campaign" }));
                model.CrumbInfo.AddCrumb("创建企划活动");

                var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
                ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name", model.ProjectId);

                var campaignTypeList = GetService<ICommonService>().GetCommonFieldList("CampaignType");
                ViewData["CampaignTypeList"] = new SelectList(campaignTypeList, "FieldValue", "FieldName", model.CampaignType);

                var publishTimeList = GetService<ICommonService>().GetCommonFieldList("PublishTime");
                ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName", model.PublishTime);

                return View(model);
            }

            model.UserInfo = new NHH.Models.Common.UserInfo
            {
                UserId = Context.UserID,
                UserName = Context.UserName
            };

            var campaignId = campaignService.AddCampaign(model);
            return RedirectToAction("List", "Campaign", new { area = "Campaign" });
        }

        /// <summary>
        /// 提交编辑页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CampaignPlanDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("企划管理", Url.Action("List", "Campaign", new { area = "Campaign" }));
                model.CrumbInfo.AddCrumb("企划活动详情");

                var campaignTypeList = GetService<ICommonService>().GetCommonFieldList("CampaignType");
                ViewData["CampaignTypeList"] = new SelectList(campaignTypeList, "FieldValue", "FieldName", model.CampaignType);

                return View(model);
            }


            model.UserInfo = new NHH.Models.Common.UserInfo
            {
                UserId = Context.UserID,
                UserName = Context.UserName
            };
            campaignService.UpdateCampaign(model);

            return RedirectToAction("List", "Campaign", new { area = "Campaign" });
        }
    }
}