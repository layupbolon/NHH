using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 意向列表查询信息
    /// </summary>
    public class RequestListQueryInfo : QueryInfo
    {
        public int? UnitId { get; set; }
    }
}
