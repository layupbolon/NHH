using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼层列表Model
    /// </summary>
    public class FloorListModel : BaseModel
    {
        public int ProjectId { get; set; }

        public int BuildingId { get; set; }

        public int GroundNum { get; set; }
        public int UnderGroundNum { get; set; }

        public string CurBuildingName { get; set; }
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
        public List<FloorInfo> FloorList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
