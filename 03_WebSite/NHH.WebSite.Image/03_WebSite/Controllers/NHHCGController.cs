using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.WebSite.Image.Models;

namespace NHH.WebSite.Image.Controllers
{
    public class NHHCGController : NHHController
    {
        /// <summary>
        /// 上传官网相关图片
        /// </summary>
        /// <param name="refId"></param>
        /// <param name="type">1店铺 2广告位 3多经点位</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(int refId,int type)
        {
            var result = new FileUploadResult();

            string typeName = "";
            switch (type)
            {
                case 1:
                    typeName = "NHHCG/store/";
                    break;
                case 2:
                    typeName = "NHHCG/ADPosition/";
                    break;
                case 3:
                    typeName = "NHHCG/Kiosk/";
                    break;
                default:
                    typeName = "NHHCG/Other/";
                    break;
            }

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

            //读取项目信息            
            //项目文件夹
            string path = BuildProjectDirectory(0,type);

            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileExt);
            string filePath = Path.Combine(path, fileName);
            file.SaveAs(filePath);
            //100
            path = BuildProjectDirectory(100,type);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 100, 0, "W");
            //500
            path = BuildProjectDirectory(500,type);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 500, 0, "W");
            //800
            path = BuildProjectDirectory(800,type);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 800, 0, "W");

            result.Result = true;
            result.FilePath = string.Format(typeName + "Original/{0}", fileName);
            result.FileUrl = string.Format("{0}{1}", result.Domain, result.FilePath.Replace("Original", "S100"));
            result.Message = "文件上传成功";
            return Json(result);
        }

        /// <summary>
        /// 生成项目文件夹
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="type">1店铺 2广告位 3多经点位</param>
        /// <returns></returns>
        private string BuildProjectDirectory(int fileSize,int type)
        {
            string typeName = "";
            switch (type)
            {
                case 1:
                    typeName = "/NHHCG/store/";
                    break;
                case 2:
                    typeName = "/NHHCG/ADPosition/";
                    break;
                case 3:
                    typeName = "/NHHCG/Kiosk/";
                    break;
                default:
                    typeName = "/NHHCG/Other/";
                    break;
            }
            string path = HttpContext.Server.MapPath(typeName);
            if (fileSize > 0)
                return DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));

            return DirectoryUtil.BuildDir(path, "Original");
        }
    }
}