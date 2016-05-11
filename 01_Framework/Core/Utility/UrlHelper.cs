﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Utility
{
    /// <summary>
    /// URL助手
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetImageUrl(string path)
        {
            if (string.IsNullOrEmpty(path) || path.Length == 0)
                return "/content/assets/images/default-img.jpg";
            return string.Format("{0}{1}", ParamManager.GetStringValue("ImageSite"), path);
        }

        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string GetImageUrl(string path, int fileSize)
        {
            if (string.IsNullOrEmpty(path) || path.Length == 0)
            {
                return "/content/assets/images/default-img.jpg";
            }
            var imageUrl = string.Format("{0}{1}", ParamManager.GetStringValue("ImageSite"), path);
            imageUrl = imageUrl.Replace("Original", string.Format("S{0}", fileSize));
            return imageUrl;
        }
    }
}
