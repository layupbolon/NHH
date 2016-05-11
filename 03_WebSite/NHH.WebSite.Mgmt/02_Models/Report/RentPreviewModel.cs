using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// 租金预估
    /// </summary>
    public class RentPreviewModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public RentPreviewQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 商铺列表
        /// </summary>
        public List<RentPreviewUnitItemInfo> UnitList { get; set; }
    }
}
