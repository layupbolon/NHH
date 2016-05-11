using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 品牌列表查询信息
    /// </summary>
    public class BrandListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 品牌列表查询信息
        /// </summary>
        public BrandListQueryInfo()
        {
            OrderBy = "BrandID";
        }

        public string BrandName { get; set; }
    }
}
