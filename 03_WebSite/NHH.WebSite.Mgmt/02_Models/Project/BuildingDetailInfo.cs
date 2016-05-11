using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼宇详细信息
    /// </summary>
    public class BuildingDetailInfo : BuildingInfo
    {
        /// <summary>
        /// 效果图列表
        /// </summary>
        public List<string> RenderingFileList
        {
            get
            {
                var list = new List<string>();
                if (!string.IsNullOrEmpty(RenderingFileName))
                {
                    if (RenderingFileName.EndsWith(","))
                    {
                        RenderingFileName = RenderingFileName.Substring(0, RenderingFileName.Length - 1);
                    }
                    list = RenderingFileName.Split(new char[] { ',' }).ToList();
                }
                return list;
            }
        }

        /// <summary>
        /// 地上楼层列表
        /// </summary>
        public List<FloorInfo> GroundFloorList { get; set; }

        /// <summary>
        /// 地下楼层列表
        /// </summary>
        public List<FloorInfo> UndergroundFloorList { get; set; }
    }
}
