using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Pdf
{
    public class PdfAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Pdf";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Pdf_default",
                "Pdf/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Pdf.Controllers" }
            );
        }
    }
}