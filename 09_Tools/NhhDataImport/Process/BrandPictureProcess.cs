using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 品牌图片处理
    /// </summary>
    public class BrandPictureProcess : PictureProcess<BrandEntity>
    {
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void ValidData(BrandEntity entity)
        {
            if (entity.BrandID <= 0)
            {
                throw new NhhException("品牌图片上传，品牌ID无效");
            }
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override string GetFileNames(BrandEntity entity)
        {
            return entity.BrandIcon;
        }

        /// <summary>
        /// 获取上传后的路径
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected override string GetUploadPath(BrandEntity entity, int fileSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取上传的文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected override string GetNewFilePath(BrandEntity entity, string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newFileNames"></param>
        protected override void UpdateData(BrandEntity entity, List<string> newFileNames)
        {
            throw new NotImplementedException();
        }
    }
}
