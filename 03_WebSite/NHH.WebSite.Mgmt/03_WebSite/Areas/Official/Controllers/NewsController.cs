using System;
using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Approve;
using NHH.Models.Official;
using NHH.Models.Official.Project;
using NHH.Service.Common.IService;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Management.Areas.Official.Controllers
{
    public class NewsController : NHHController
    {
        private readonly INewsService newsService;

        public NewsController()
        {
            newsService = GetService<INewsService>();
        }

        public ActionResult List(NewsQueryInfo newsQueryInfo)
        {
            var model = newsService.GetNewsListModel(newsQueryInfo);
            model.CrumbInfo.AddCrumb("新闻资讯管理");

            ICommonService commonService = GetService<ICommonService>();
            var newsTypeList = commonService.GetCommonFieldList("NewsType");
            ViewData["NewsTypeList"] = new SelectList(newsTypeList, "FieldValue", "FieldName",
                newsQueryInfo.NewsType ?? 0);

            var newsTargetList = commonService.GetCommonFieldList("NewsTarget");
            ViewData["NewsTargetList"] = new SelectList(newsTargetList, "FieldValue", "FieldName",
                newsQueryInfo.NewsTarget ?? 0);

            var newsStatusList = commonService.GetCommonFieldList("NewsStatus");
            ViewData["NewsStatusList"] = new SelectList(newsStatusList, "FieldValue", "FieldName",
                newsQueryInfo.NewsStatus ?? 0);

            //唐小二
            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name", newsQueryInfo.ProjectID ?? 0);

            //官网
            var nhhProjectList = GetService<INHHCGService>().GetProjectList();
            ViewData["NhhProjectList"] = new SelectList(nhhProjectList, "ProjectID", "ProjectName", newsQueryInfo.ProjectID ?? 0);

            return View(model);
        }

        public ActionResult Add()
        {
            var model = new NewsModel();
            model.CrumbInfo.AddCrumb("新闻资讯管理", Url.Action("List", "News", new { area = "Official" }));
            model.CrumbInfo.AddCrumb("创建新闻资讯");

            var commonService = GetService<ICommonService>();

            var newsTypeList = commonService.GetCommonFieldList("NewsType");
            ViewData["NewsTypeList"] = new SelectList(newsTypeList, "FieldValue", "FieldName");
            var newsTargetList = commonService.GetCommonFieldList("NewsTarget");
            ViewData["NewsTargetList"] = new SelectList(newsTargetList, "FieldValue", "FieldName");
            var companyList = GetService<ICompanyCommonService>().GetCompanyList();
            ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name");
            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");
            var publishTimeList = commonService.GetCommonFieldList("PublishTime");
            ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName");

            return View(model);
        }

        public ActionResult Edit(int newsId)
        {
            var model = newsService.GetNewsDetail(newsId);
            model.CrumbInfo.AddCrumb("新闻资讯管理", Url.Action("List", "News", new { area = "Official" }));
            model.CrumbInfo.AddCrumb("新闻资讯编辑");

            var commonService = GetService<ICommonService>();

            var newsTypeList = commonService.GetCommonFieldList("NewsType");
            ViewData["NewsTypeList"] = new SelectList(newsTypeList, "FieldValue", "FieldName");
            var newsTargetList = commonService.GetCommonFieldList("NewsTarget");
            ViewData["NewsTargetList"] = new SelectList(newsTargetList, "FieldValue", "FieldName");
            var companyList = GetService<ICompanyCommonService>().GetCompanyList();
            ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name");
            //唐小二
            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name");

            //官网
            var nhhProjectList = GetService<INHHCGService>().GetProjectList();
            nhhProjectList.Insert(0, new NHHCGProjectModel() { ProjectID = 0, ProjectName = "全部" });
            ViewData["NhhProjectList"] = new SelectList(nhhProjectList, "ProjectID", "ProjectName");
            var publishTimeList = commonService.GetCommonFieldList("PublishTime");
            ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName");

            return View(model);
        }

        public ActionResult Detail(int newsId)
        {
            var model = newsService.GetNewsDetail(newsId);
            model.CrumbInfo.AddCrumb("新闻资讯管理", Url.Action("List", "News", new { area = "Official" }));
            model.CrumbInfo.AddCrumb("新闻资讯详情");

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(NewsModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("新闻资讯管理", Url.Action("List", "News", new { area = "Official" }));
                model.CrumbInfo.AddCrumb("创建新闻资讯");

                var commonService = GetService<ICommonService>();

                var newsTypeList = commonService.GetCommonFieldList("NewsType");
                ViewData["NewsTypeList"] = new SelectList(newsTypeList, "FieldValue", "FieldName");
                var newsTargetList = commonService.GetCommonFieldList("NewsTarget");
                ViewData["NewsTargetList"] = new SelectList(newsTargetList, "FieldValue", "FieldName");
                var companyList = GetService<ICompanyCommonService>().GetCompanyList();
                ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name");
                var projectList = GetService<IProjectCommonService>().GetProjectList();
                ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");
                var publishTimeList = commonService.GetCommonFieldList("PublishTime");
                ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName");

                return View(model);
            }

            if (string.IsNullOrEmpty(model.NewsCover))
            {
                ModelState.AddModelError("Error", "请上传横幅图片");

                model.CrumbInfo.AddCrumb("新闻资讯管理", Url.Action("List", "News", new { area = "Official" }));
                model.CrumbInfo.AddCrumb("创建新闻资讯");

                var commonService = GetService<ICommonService>();

                var newsTypeList = commonService.GetCommonFieldList("NewsType");
                ViewData["NewsTypeList"] = new SelectList(newsTypeList, "FieldValue", "FieldName");
                var newsTargetList = commonService.GetCommonFieldList("NewsTarget");
                ViewData["NewsTargetList"] = new SelectList(newsTargetList, "FieldValue", "FieldName");
                var companyList = GetService<ICompanyCommonService>().GetCompanyList();
                ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name");
                var projectList = GetService<IProjectCommonService>().GetProjectList();
                ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");
                var publishTimeList = commonService.GetCommonFieldList("PublishTime");
                ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName");

                return View(model);
            }

            model.UserID = NHHWebContext.Current.UserID;
            var success = newsService.AddNews(model);
            return RedirectToAction("List", "News", new { area = "Official" });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("新闻资讯管理", Url.Action("List", "News", new { area = "Official" }));
                model.CrumbInfo.AddCrumb("新闻资讯编辑");

                var commonService = GetService<ICommonService>();

                var newsTypeList = commonService.GetCommonFieldList("NewsType");
                ViewData["NewsTypeList"] = new SelectList(newsTypeList, "FieldValue", "FieldName");
                var newsTargetList = commonService.GetCommonFieldList("NewsTarget");
                ViewData["NewsTargetList"] = new SelectList(newsTargetList, "FieldValue", "FieldName");
                var companyList = GetService<ICompanyCommonService>().GetCompanyList();
                ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name");
                //唐小二
                var projectList = GetService<IProjectCommonService>().GetProjectList();
                ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name", model.ProjectID);

                //官网
                var nhhProjectList = GetService<INHHCGService>().GetProjectList();
                nhhProjectList.Insert(0, new NHHCGProjectModel() {ProjectID = 0, ProjectName = "全部"});
                ViewData["NhhProjectList"] = new SelectList(nhhProjectList, "ProjectID", "ProjectName", model.ProjectID);
                var publishTimeList = commonService.GetCommonFieldList("PublishTime");
                ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName");

                return View(model);
            }

            if (string.IsNullOrEmpty(model.NewsCover))
            {
                ModelState.AddModelError("Error", "请上传横幅图片");

                model.CrumbInfo.AddCrumb("新闻资讯管理", Url.Action("List", "News", new { area = "Official" }));
                model.CrumbInfo.AddCrumb("创建新闻资讯");

                var commonService = GetService<ICommonService>();

                var newsTypeList = commonService.GetCommonFieldList("NewsType");
                ViewData["NewsTypeList"] = new SelectList(newsTypeList, "FieldValue", "FieldName");
                var newsTargetList = commonService.GetCommonFieldList("NewsTarget");
                ViewData["NewsTargetList"] = new SelectList(newsTargetList, "FieldValue", "FieldName");
                var companyList = GetService<ICompanyCommonService>().GetCompanyList();
                ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name");
                //唐小二
                var projectList = GetService<IProjectCommonService>().GetProjectList();
                ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name", model.ProjectID);

                //官网
                var nhhProjectList = GetService<INHHCGService>().GetProjectList();
                nhhProjectList.Insert(0, new NHHCGProjectModel() { ProjectID = 0, ProjectName = "全部" });
                ViewData["NhhProjectList"] = new SelectList(nhhProjectList, "ProjectID", "ProjectName", model.ProjectID);
                var publishTimeList = commonService.GetCommonFieldList("PublishTime");
                ViewData["PublishTimeList"] = new SelectList(publishTimeList, "FieldValue", "FieldName");

                return View(model);
            }

            model.UserID = NHHWebContext.Current.UserID;
            var success = newsService.EditNews(model);
            return RedirectToAction("List", "News", new { area = "Official" });
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Stick(int? newsID)
        {
            var success = newsService.StickNews(newsID, NHHWebContext.Current.UserID);
            return Json(success);
        }

        /// <summary>
        /// 取消置顶
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UnStick(int? newsID)
        {
            var success = newsService.UnStickNews(newsID, NHHWebContext.Current.UserID);
            return Json(success);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Publish(int? newsID)
        {
            var success = newsService.PublishNews(newsID, NHHWebContext.Current.UserID);
            return Json(success);
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Cancel(int? newsID)
        {
            var success = newsService.CancelNews(newsID, NHHWebContext.Current.UserID);
            return Json(success);
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
            approve.UserID = NHHWebContext.Current.UserID;
            var result = newsService.Approve(approve);
            return Json(result);
        }
    }
}