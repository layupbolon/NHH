using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class BuildingMainInfo

    {
        int buildingID;
        /// <summary>
        /// 楼宇id
        /// </summary>
        public int BuildingID
        {
            get { return buildingID; }
            set { buildingID = value; }
        }
        int projectID;
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
        string buildingName;
        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuildingName
        {
            get { return buildingName; }
            set { buildingName = value; }
        }
        int groundFloorNum;
        /// <summary>
        /// 地上楼层数
        /// </summary>
        public int GroundFloorNum
        {
            get { return groundFloorNum; }
            set { groundFloorNum = value; }
        }
        int undergroundFloorNum;
        /// <summary>
        ///地下楼层数
        /// </summary>
        public int UndergroundFloorNum
        {
            get { return undergroundFloorNum; }
            set { undergroundFloorNum = value; }
        }
        decimal totalConstructionArea;
       /// <summary>
       /// 总建筑面积
       /// </summary>
        public decimal TotalConstructionArea
        {
            get { return totalConstructionArea; }
            set { totalConstructionArea = value; }
        }
        decimal totalRentArea;
        /// <summary>
        /// 总计租面积
        /// </summary>
        public decimal TotalRentArea
        {
            get { return totalRentArea; }
            set { totalRentArea = value; }
        }
        decimal totalRentUnit;
        /// <summary>
        /// 总计租单元
        /// </summary>
        public decimal TotalRentUnit
        {
            get { return totalRentUnit; }
            set { totalRentUnit = value; }
        }
        string renderingFileName;
        /// <summary>
        /// 图片url
        /// </summary>
        public string RenderingFileName
        {
            get { return renderingFileName; }
            set { renderingFileName = value; }
        }
    }
}
