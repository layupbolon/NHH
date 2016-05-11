using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectKiosk
{
    public class KioskListModel
    {
        /// <summary>
        /// 项目信息
        /// </summary>
        public ProjectCommonInfo ProjectInfo { get; set; }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        public List<BuildingCommonInfo> BuildingList { get; set; }

        /// <summary>
        /// 楼层列表
        /// </summary>
        public List<FloorCommonInfo> FloorList { get; set; }

        public List<KioskDetailModel> KioskList { get; set; }
    }
}
