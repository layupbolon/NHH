using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NHH.Models.Common; 

namespace NHH.Models.Estate
{
    /// <summary>
    /// 维修列表
    /// </summary>
    public class RepairListModel : BaseModel
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        public RepairListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 维修列表
        /// </summary>
        public IList<RepairInfo> RepairList { get; set; }

        public IList<AttachmentInfo> AttachmentInfos { get; set; }

    }
}
