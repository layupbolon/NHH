using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 品牌列表
    /// </summary>
    public class BrandListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public BrandListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 品牌列表信息
        /// </summary>
        public List<BrandInfo> BrandList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

    }
}
