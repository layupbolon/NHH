using NHH.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 楼层平面图服务
    /// </summary>
    public interface IFloorMapService
    {
        /// <summary>
        /// 获取楼层平面图列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        List<FloorMapUnitInfo> GetFloorMapList(FloorMapQueryInfo queryInfo);

        /// <summary>
        /// 获取楼层铺位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        List<FloorMapUnitInfo> GetUnitList(FloorMapQueryInfo queryInfo);

        /// <summary>
        /// 获取楼层平面图详情信息
        /// </summary>
        /// <param name="floorMapUnitId"></param>
        /// <returns></returns>
        FloorMapUnitInfo GetFloorMapInfo(int floorMapUnitId);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        void UpdateFloorMapUnit(FloorMapUnitInfo info);
    }
}
