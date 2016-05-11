using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼宇列表Model
    /// </summary>
    public class BuildingListModel
    {
        public int ProjectId { get; set; }
        
        /// <summary>
        /// 楼宇列表
        /// </summary>
        public List<BuildingDetailInfo> BuildingList { get; set; }
    }
}
