using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 楼宇公共信息
    /// </summary>
    public class BuildingCommonInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 楼层列表
        /// </summary>
        public List<FloorCommonInfo> FloorList { get; set; }
    }
}
