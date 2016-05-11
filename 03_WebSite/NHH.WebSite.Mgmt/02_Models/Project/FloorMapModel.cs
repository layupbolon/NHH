using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼层平面图
    /// </summary>
    public class FloorMapModel : BaseModel
    {
        /// <summary>
        /// 查询消息
        /// </summary>
        public FloorMapQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 楼层列表
        /// </summary>
        public List<FloorCommonInfo> FloorList { get; set; }

        /// <summary>
        /// 楼层平面图单元列表
        /// </summary>
        public List<FloorMapUnitInfo> FloorMapUnitList { get; set; }

        /// <summary>
        /// 楼层平面图脚本
        /// </summary>
        public string FloorMapScript { get; set; }
    }
}
