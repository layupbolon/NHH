using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.Kiosk
{
    public class KioskListModel : BaseModel
    {
        /// <summary>
        /// 分页
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// 查询项
        /// </summary>
        public KioskQueryInfo QueryInfo { get; set; } 

        /// <summary>
        /// 多经点位列表
        /// </summary>
        public IList<KioskListInfo> KioskList { get; set; }

    }
}
