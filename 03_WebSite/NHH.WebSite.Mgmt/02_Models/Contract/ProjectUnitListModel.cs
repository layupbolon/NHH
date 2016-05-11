using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 商铺列表
    /// </summary>
    public class ProjectUnitListModel : BaseModel
    {
        /// <summary>
        /// 商铺列表
        /// </summary>
        public List<ProjectUnitInfo> ProjectUnitList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
