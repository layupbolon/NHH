using NHH.Framework.Web;
using NHH.Models.Image;
using NHH.Service.Image.IService;
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
    /// 项目
    /// </summary>
    [AllowAnonymous]
    public class ProjectController : NHHController
    {
        /// <summary>
        /// 上传项目效果图
        /// </summary>
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

            //读取项目信息            
            //项目文件夹
            string path = BuildProjectDirectory(0);

            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileExt);
            string filePath = Path.Combine(path, fileName);
            file.SaveAs(filePath);
            //100
            path = BuildProjectDirectory(100);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 100, 0, "W");
            //800
            path = BuildProjectDirectory(800);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 800, 0, "W");

            result.Result = true;
            result.FilePath = string.Format("Project/Original/{0}", fileName);
            result.FileUrl = string.Format("{0}{1}", result.Domain, result.FilePath.Replace("Original", "S100"));
            result.Message = "文件上传成功";
            return Json(result);
        }


        /// <summary>
        /// 上传楼宇效果图
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadBuilding(int projectId)
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

            //原始文件
            string path = buildBuildingDirectory(projectId, 0);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileExt);
            string filePath = Path.Combine(path, fileName);
            file.SaveAs(filePath);
            //100
            path = buildBuildingDirectory(projectId, 100);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 100, 0, "W");
            //800
            path = buildBuildingDirectory(projectId, 800);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 800, 0, "W");

            result.Result = true;
            result.FilePath = string.Format("Project/Original/{0}/Building/{1}", projectId, fileName);
            result.FileUrl = string.Format("{0}{1}", result.Domain, result.FilePath.Replace("Original", "S100"));
            result.Message = "文件上传成功";
            return Json(result);
        }

        /// <summary>
        /// 上传楼层效果图
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFloor(int buildingId)
        {
            var result = new FileUploadResult();

            var buildingInfo = GetService<IProjectService>().GetBuildingInfo(buildingId);
            if (buildingInfo == null)
            {
                result.Message = "未找到相关楼宇";
                return Json(result);
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

            //原始文件
            string path = buildFloorDirectory(buildingInfo, 0);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}-{2}{3}", buildingInfo.Id, DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileExt);
            string filePath = Path.Combine(path, fileName);
            file.SaveAs(filePath);
            //100
            path = buildFloorDirectory(buildingInfo, 100);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 100, 0, "W");
            //800
            path = buildFloorDirectory(buildingInfo, 800);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 800, 0, "W");

            result.Result = true;
            result.FilePath = string.Format("Project/Original/{0}/Floor/{1}", buildingInfo.ProjectId, fileName);
            result.FileUrl = string.Format("{0}{1}", result.Domain, result.FilePath.Replace("Original", "S100"));
            result.Message = "文件上传成功";
            return Json(result);
        }


        /// <summary>
        /// 生成项目文件夹
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private string BuildProjectDirectory(int fileSize)
        {
            string path = HttpContext.Server.MapPath("/Project/");
            if (fileSize > 0)
                return DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));

            return DirectoryUtil.BuildDir(path, "Original");
        }


        /// <summary>
        /// 生成楼宇文件夹
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private string buildBuildingDirectory(int projectId, int fileSize)
        {
            string path = HttpContext.Server.MapPath("/Project/");
            if (fileSize > 0)
                path = DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));
            else
                path = DirectoryUtil.BuildDir(path, "Original");
            path = DirectoryUtil.BuildDir(path, projectId.ToString());
            path = DirectoryUtil.BuildDir(path, "Building");
            return path;
        }

        /// <summary>
        /// 生成楼层文件夹
        /// </summary>
        /// <param name="buildingInfo"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private string buildFloorDirectory(BuildingInfo buildingInfo, int fileSize)
        {
            string path = HttpContext.Server.MapPath("/Project/");
            if (fileSize > 0)
                path = DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));
            else
                path = DirectoryUtil.BuildDir(path, "Original");
            path = DirectoryUtil.BuildDir(path, buildingInfo.ProjectId.ToString());
            path = DirectoryUtil.BuildDir(path, "Floor");
            return path;
        }
    }
}
