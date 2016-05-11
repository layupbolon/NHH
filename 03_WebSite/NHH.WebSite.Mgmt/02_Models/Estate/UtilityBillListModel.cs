using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 水电煤气费抄表列表
    /// </summary>
    public class UtilityBillListModel : BaseModel
    {
        /// <summary>
        /// 水电煤气费抄表信息
        /// </summary>
        public List<UtilityBillInfo> UtilityBillList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
        
        /// <summary>
        /// 查询信息
        /// </summary>
        public UtilityBillQueryInfo QueryInfo { get; set; }
    }
}
