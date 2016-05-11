using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHH.WebSite.Image.Models
{
    /// <summary>
    /// 文件上传结果
    /// </summary>
    public class FileUploadResult
    {
        //可从配置中取
        private string _domain = NHH.Framework.Utility.ParamManager.GetStringValue("ImageSite");
        private bool _result = false;
        private string _message = "";
        private string _fileId = DateTime.Now.ToString("HHmmssfff");

        /// <summary>
        /// 文件ID
        /// 默认值采用当前时间
        /// 也可以是数据自增ID
        /// </summary>
        public string FileId
        {
            get { return _fileId; }
            set { _fileId = value; }
        }

        /// <summary>
        /// 上传前的文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 上传后的文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件全路径
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain
        {
            get
            {
                if (!_domain.EndsWith("/"))
                {
                    _domain += "/";

                }
                return _domain;
            }
            set { _domain = value; }
        }

        /// <summary>
        /// 是否上传成功
        /// </summary>
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}