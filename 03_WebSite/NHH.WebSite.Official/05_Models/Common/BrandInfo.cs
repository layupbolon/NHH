using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 品牌基本信息
    /// </summary>
    public class BrandInfo
    {
        /// <summary>
        /// 品牌ID
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [Required(ErrorMessage = "请输入品牌名")]
        public string BrandName { get; set; }

        /// <summary>
        /// 品牌级次
        /// </summary>
        [Required(ErrorMessage = "请输入品牌级次")]
        public int BrandLevel { get; set; }

        public BrandLevelInfo BrandLevelInfo { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        [Required(ErrorMessage = "请输入业态")]
        public int BizTypeId { get; set; }

        public BizTypeInfo BizTypeInfo { get; set; }

        /// <summary>
        /// 品类
        /// </summary>
        [Required(ErrorMessage = "请输入品类")]
        public int BizCategoryId { get; set; }

        public BizCategoryInfo BizCategoryInfo { get; set; }

    }
}
