using NhhDataImport.Entity;
using NhhDataImport.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 项目图片处理
    /// </summary>
    public class ProjectPictureProcess : PictureProcess<ProjectEntity>
    {
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidData(ProjectEntity entity)
        {
            if (entity.ProjectID <= 0)
                throw new NhhException("项目图片上传，项目ID无效");
        }

        /// <summary>
        /// 获取文件名称列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override string GetFileNames(ProjectEntity entity)
        {
            return entity.RenderingFileName;
        }

        /// <summary>
        /// 获取上传目录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected override string GetUploadPath(ProjectEntity entity, int fileSize)
        {
            string path = DirectoryUtil.BuildDir(Processed, "Project");
            if (fileSize > 0)
                return DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));

            return DirectoryUtil.BuildDir(path, "Original");
        }

        /// <summary>
        /// 获取新的文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected override string GetNewFilePath(ProjectEntity entity, string fileName)
        {
            return string.Format("Project/Original/{0}", fileName);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newFileNames"></param>
        protected override void UpdateData(ProjectEntity entity, List<string> newFileNames)
        {
            entity.RenderingFileName = string.Join(",", newFileNames);
            string strCmd = @"Update [dbo].[Project]
                            Set RenderingFileName=@RenderingFileName
                            Where ProjectID=@ProjectID";

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@RenderingFileName", entity.RenderingFileName),
                new SqlParameter("@ProjectID", entity.ProjectID),
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
