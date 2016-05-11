using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Logging
{
    public class OptLogListModel : BaseModel
    {
        /// <summary>
        /// 查询消息
        /// </summary>
        public OptLogQueryInfo QueryInfo { get; set; }


        /// <summary>
        /// 业务操作日志信息列表
        /// </summary>
        public List<OptLogModel> OptLogList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
