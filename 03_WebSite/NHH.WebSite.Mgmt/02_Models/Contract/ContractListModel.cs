using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 租约列表
    /// </summary>
    public class ContractListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ContractListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 租约列表
        /// </summary>
        public List<ContractInfo> ContractList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
