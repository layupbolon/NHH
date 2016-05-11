using NHH.Framework.Web;
using NHH.WebSite.Image.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Image.Controllers
{
    /// <summary>
    /// 企划
    /// </summary>
    [AllowAnonymous]
    public class CampaignController : NHHController
    {

        /// <summary>
        /// 上传企划活动页面图片
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(int projectId)
        {
            var result = new FileUploadResult();

            HttpPostedFileBase file = Request.Files["Filedata"];
            int fileLength = file.ContentLength;
            if (file == null || fileLength == 0)
            {
                result.Message = "请选择上传文件";
                return Json(result);
            }

            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExt = fileInfo.Extension.ToLower();
            if (fileExt != ".jpg" && fileExt != ".png")
            {
                result.Message = string.Format("不支持上传{0}类型文件", fileExt);
                return Json(result);
            }
            result.FileName = fileInfo.Name;

            //项目文件夹
            DateTime date = DateTime.Now;
            string path = BuildDirectory(projectId, 0, date);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileExt);
            string filePath = Path.Combine(path, fileName);
            file.SaveAs(filePath);

            //100
            path = BuildDirectory(projectId, 100, date);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 100, 0, "W");
            //800
            path = BuildDirectory(projectId, 800, date);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 800, 0, "W");

            result.Result = true;
            result.FilePath = string.Format("Campaign/Original/{0}/{1}/{2}/{3}/{4}", projectId, date.Year, date.Month, date.Day, fileName);
            result.FileUrl = string.Format("{0}{1}", result.Domain, result.FilePath.Replace("Original", "S100"));
            result.Message = "文件上传成功";
            return Json(result);
        }

        /// <summary>
        /// 生成目录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="fileSize"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private string BuildDirectory(int projectId, int fileSize, DateTime date)
        {
            string path = HttpContext.Server.MapPath("/Campaign/");
            //文件尺寸
            if (fileSize > 0)
            {
                path = DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));
            }
            else
            {
                path = DirectoryUtil.BuildDir(path, "Original");
            }
            //项目文件夹
            path = DirectoryUtil.BuildDir(path, projectId.ToString());
            //年月日
            path = DirectoryUtil.BuildDir(path, date.Year.ToString());
            path = DirectoryUtil.BuildDir(path, date.Month.ToString());
            path = DirectoryUtil.BuildDir(path, date.Day.ToString());
            return path;
        }
    }
}