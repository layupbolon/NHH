using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 面包屑信息
    /// </summary>
    public class CrumbInfo
    {
        private List<CrumbItem> _crumbList = new List<CrumbItem> { };

        /// <summary>
        /// 面包屑列表
        /// </summary>
        public List<CrumbItem> CrumbList
        {
            get { return _crumbList; }
        }

        /// <summary>
        /// 添加面包屑
        /// </summary>
        /// <param name="text"></param>
        public void AddCrumb(string text)
        {
            _crumbList.Add(new CrumbItem(text));
        }

        /// <summary>
        /// 添加面包屑
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        public void AddCrumb(string text, string url)
        {
            _crumbList.Add(new CrumbItem(text, url));
        }
    }

    /// <summary>
    /// 面包屑
    /// </summary>
    public class CrumbItem
    {
        public string Text { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// 面包屑
        /// </summary>
        /// <param name="text"></param>
        public CrumbItem(string text)
        {
            Text = text;
        }

        /// <summary>
        /// 面包屑
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        public CrumbItem(string text, string url)
        {
            Text = text;
            Url = url;
        }
    }
}

