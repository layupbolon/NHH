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
    /// 商铺
    /// </summary>
    [AllowAnonymous]
    public class ProjectUnitController : NHHController
    {
        /// <summary>
        /// 上传商铺图片
        /// </summary>
        /// <param name="floorId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(int floorId, int? unitId)
        {
            var result = new FileUploadResult();
            //读取项目信息
            var floorInfo = GetService<IProjectService>().GetFloorInfo(floorId);
            if (floorInfo == null)
            {
                result.Message = "未找到相关楼层";
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
            string path = BuildDirectory(floorInfo, 0);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileExt);
            string filePath = Path.Combine(path, fileName);
            file.SaveAs(filePath);

            //100
            path = BuildDirectory(floorInfo, 100);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 100, 0, "W");
            //800
            path = BuildDirectory(floorInfo, 800);
            PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 800, 0, "W");

            result.Result = true;
            result.FilePath = string.Format("Project/Original/{0}/ProjectUnit/{1}/{2}/{3}", floorInfo.ProjectId, floorInfo.BuildingId, floorInfo.FloorId, fileName);
            result.FileUrl = string.Format("{0}{1}", result.Domain, result.FilePath.Replace("Original", "S100"));
            result.Message = "文件上传成功";

            return Json(result);
        }

        /// <summary>
        /// 生成目录
        /// </summary>
        /// <param name="floorInfo"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private string BuildDirectory(FloorInfo floorInfo, int fileSize)
        {
            string path = HttpContext.Server.MapPath("/Project/");
            //文件尺寸
            if (fileSize > 0)
            {
                path = DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));
            }
            else
            {
                path = DirectoryUtil.BuildDir(path, "Original");
            }
            //项目
            path = DirectoryUtil.BuildDir(path, floorInfo.ProjectId.ToString());
            //商铺
            path = DirectoryUtil.BuildDir(path, "ProjectUnit");
            //楼宇
            path = DirectoryUtil.BuildDir(path, floorInfo.BuildingId.ToString());
            //楼层
            path = DirectoryUtil.BuildDir(path, floorInfo.FloorId.ToString());
            return path;
        }
    }
}
