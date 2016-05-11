using NhhDataImport.Entity;
using NhhDataImport.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 项目楼宇图片处理
    /// </summary>
    public class ProjectBuildingPictureProcess : PictureProcess<ProjectBuildingEntity>
    {
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void ValidData(ProjectBuildingEntity entity)
        {
            if (entity.BuildingID <= 0)
                throw new NhhException("项目楼宇图片上传，楼宇ID无效");
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override string GetFileNames(ProjectBuildingEntity entity)
        {
            return entity.RenderingFileName;
        }

        /// <summary>
        /// 获取上传目录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected override string GetUploadPath(ProjectBuildingEntity entity, int fileSize)
        {
            string path = DirectoryUtil.BuildDir(Processed, "Project");
            if (fileSize > 0)
                path = DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));
            else
                path = DirectoryUtil.BuildDir(path, "Original");
            path = DirectoryUtil.BuildDir(path, entity.ProjectID.ToString());
            path = DirectoryUtil.BuildDir(path, "Building");
            return path;
        }

        /// <summary>
        /// 获取上传后的文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected override string GetNewFilePath(ProjectBuildingEntity entity, string fileName)
        {
            return string.Format("Project/Original/{0}/Building/{1}", entity.ProjectID, fileName);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newFileNames"></param>
        protected override void UpdateData(ProjectBuildingEntity entity, List<string> newFileNames)
        {
            entity.RenderingFileName = string.Join(",", newFileNames);
            string strCmd = @"UPDATE [dbo].[Project_Building]
                            SET [RenderingFileName] = @RenderingFileName
                            WHERE [BuildingID]=@BuildingID";

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@RenderingFileName", entity.RenderingFileName),
                new SqlParameter("@BuildingID", entity.BuildingID),
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
