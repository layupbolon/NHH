using System;
using System.Web.UI;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Official
{
    public partial class News : Page
    {
        public int newsID;
        public INewsService service;
        public NewsInfo currentNews;
        public NewsInfo nextNews;
        public NewsInfo previousNews;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["newsID"] == null || !int.TryParse(Request.QueryString["newsID"], out newsID))
                Response.Redirect("NewsList.aspx");

            service = NHHServiceFactory.Instance.CreateService<INewsService>();

            currentNews = service.GetNews(newsID);
            nextNews = service.GetNextNews(newsID);
            previousNews = service.GetPreviousNews(newsID);
        }
    }
}