using NHH.Models.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Image.IService
{
    /// <summary>
    /// 项目服务接口
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// 获取楼宇信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        BuildingInfo GetBuildingInfo(int buildingId);

        /// <summary>
        /// 获取楼层信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        FloorInfo GetFloorInfo(int floorId);
    }
}
