using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 报表字段
    /// </summary>
    public class ReportFieldInfo
    {
        #region 私有字段
        private bool display = true;
        #endregion

        /// <summary>
        /// 报表ID
        /// </summary>
        public int ReportID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int FieldID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string FieldTitle { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string FieldClass { get; set; }

        /// <summary>
        /// 排序名称
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 是否可排序
        /// </summary>
        public int Sortable { get; set; }

        /// <summary>
        /// 是否导出
        /// </summary>
        public int Exportable { get; set; }

        /// <summary>
        /// 前端格式
        /// </summary>
        public string Formatter { get; set; }

        public int InUser { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display
        {
            get { return display; }
            set { display = value; }
        }
    }
}
