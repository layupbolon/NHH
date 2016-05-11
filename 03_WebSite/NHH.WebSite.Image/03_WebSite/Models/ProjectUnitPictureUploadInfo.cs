using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHH.WebSite.Image.Models
{
    /// <summary>
    /// 商铺图片上传信息
    /// </summary>
    public class ProjectUnitPictureUploadInfo
    {
        /// <summary>
        /// 楼层Id
        /// </summary>
        public int FloorId { get; set; }

        /// <summary>
        /// 商铺Id
        /// </summary>
        public int? UnitId { get; set; }
    }
}