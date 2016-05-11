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
    /// 项目楼层图片处理
    /// </summary>
    public class ProjectFloorPictureProcess : PictureProcess<ProjectFloorEntity>
    {
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void ValidData(ProjectFloorEntity entity)
        {
            if (entity.FloorID <= 0)
                throw new NhhException("项目楼层图片上传，楼层ID无效");
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override string GetFileNames(ProjectFloorEntity entity)
        {
            return entity.FloorMapFileName;
        }

        /// <summary>
        /// 获取上传目录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected override string GetUploadPath(ProjectFloorEntity entity, int fileSize)
        {
            string path = DirectoryUtil.BuildDir(Processed, "Project");
            if (fileSize > 0)
                path = DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));
            else
                path = DirectoryUtil.BuildDir(path, "Original");
            path = DirectoryUtil.BuildDir(path, entity.ProjectID.ToString());
            path = DirectoryUtil.BuildDir(path, "Floor");
            return path;
        }

        /// <summary>
        /// 获取上传后的文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected override string GetNewFilePath(ProjectFloorEntity entity, string fileName)
        {
            return string.Format("Project/Original/{0}/Floor/{1}", entity.ProjectID, fileName);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newFileNames"></param>
        protected override void UpdateData(ProjectFloorEntity entity, List<string> newFileNames)
        {
            entity.FloorMapFileName = string.Join(",", newFileNames);
            string strCmd = @"UPDATE [dbo].[Project_Floor]
                            SET [FloorMapFileName] = @FloorMapFileName
                            WHERE [FloorID]=@FloorID";

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@FloorMapFileName", entity.FloorMapFileName),
                new SqlParameter("@FloorID", entity.FloorID),
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
