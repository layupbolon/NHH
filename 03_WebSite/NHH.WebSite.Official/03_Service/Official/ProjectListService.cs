using System.Linq;
using System.Text;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Service.Official.IService;

namespace NHH.Service.Official
{
    public class ProjectListService : NHHService<NHHEntities>, IProjectListService
    {
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public string GetProjectList()
        {
            var query = from p in Context.NHHCG_Project
                        where p.Status == 1
                        select new
                        {
                            p.ProjectID,
                            p.ProjectName,
                            p.Position,
                            p.Feature,
                            ImagePath =
                                Context.NHHCG_Pic.Where(a => a.RefID == p.ProjectID && a.Type == 101)
                                    .OrderBy(a => a.Seq)
                                    .Select(a => a.Path)
                                    .FirstOrDefault()
                        };

            if (!query.Any()) return string.Empty;

            var list = query.ToList();
            var sb = new StringBuilder();
            var i = 1;
            list.ForEach(info =>
            {
                sb.AppendLine(i % 3 == 1
                    ? "<div class=\"th3-column fl htSection\">"
                    : "<div class=\"th3-column fl htSection ml20\">");
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\">",
                    string.Format("Project.aspx?projectID={0}", info.ProjectID));
                sb.AppendFormat("<img src=\"{0}\">", Framework.Utility.UrlHelper.GetImageUrl(info.ImagePath));
                sb.AppendLine("<div class=\"txt-ad pjname\">");
                sb.AppendFormat("<h3>{0}</h3>", info.ProjectName);
                sb.AppendFormat("<p>{0}</p></div>", info.Position);
                sb.AppendLine("<div class=\"txt-ad mask-content mask-content-small\">");
                sb.AppendFormat("<h3 class=\"tit-20\">{0}</h3>", info.ProjectName);
                sb.AppendFormat("<p>{0}</p>", info.Feature);
                sb.AppendFormat("<span class=\"cmnBtn btnB mt30\" href=\"{0}\">查看详情</span>",
                    string.Format("Project.aspx?projectID={0}", info.ProjectID));
                sb.AppendLine("</div><i class=\"mask-bg\"></i></a></div>");

                i++;
            });

            return sb.ToString();
        }
    }
}
