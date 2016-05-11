using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Project;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project
{
    /// <summary>
    /// 楼层平面图服务
    /// </summary>
    public class FloorMapService : NHHService<NHHEntities>, IFloorMapService
    {
        /// <summary>
        /// 获取楼层平面图列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public List<FloorMapUnitInfo> GetFloorMapList(FloorMapQueryInfo queryInfo)
        {
            int floorId = queryInfo.FloorId.HasValue ? queryInfo.FloorId.Value : -1;

            var list = new List<FloorMapUnitInfo>();
            var query = from fm in Context.FloorMap_Unit
                        where fm.FloorID == floorId
                        select fm;

            var entityList = query.ToList();
            if (entityList != null && entityList.Count > 0)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new FloorMapUnitInfo
                    {
                        Comments = entity.Comments,
                        FloorID = entity.FloorID,
                        FloorMapUnitID = entity.FloorMapUnitID,
                        PathLine = entity.PathLine,
                        PathQuad1 = entity.PathQuad1,
                        PathQuad2 = entity.PathQuad2,
                        PathQuad3 = entity.PathQuad3,
                        PathQuad4 = entity.PathQuad4,
                        TextPosition = entity.TextPosition,
                        Type = entity.Type,
                        UnitID = entity.UnitID,
                    });
                });
            }

            return list;
        }

        /// <summary>
        /// 获取楼层铺位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public List<FloorMapUnitInfo> GetUnitList(FloorMapQueryInfo queryInfo)
        {
            var list = new List<FloorMapUnitInfo>();
            var query = from f in Context.View_FloorMap_Unit
                        where f.FloorID == queryInfo.FloorId
                        select new
                        {
                            f.UnitID,
                            f.UnitNumber,
                            f.FloorMapUnitID,
                            f.ContractStatus,
                            f.ProjectUnitStatus,
                        };

            if (queryInfo.ContractStatus.HasValue)
            {
                query = query.Where(item => item.ContractStatus == queryInfo.ContractStatus.Value);
            }

            if (queryInfo.ProjectUnitStatus.HasValue)
            {
                query = query.Where(item => item.ProjectUnitStatus == queryInfo.ProjectUnitStatus.Value);
            }

            var entityList = query.OrderBy(item => item.UnitID).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new FloorMapUnitInfo
                    {
                        UnitID = entity.UnitID,
                        UnitNumber = entity.UnitNumber,
                        FloorMapUnitID = entity.FloorMapUnitID,
                    });
                });

            }
            return list;
        }

        /// <summary>
        /// 获取楼层平面图
        /// </summary>
        /// <param name="floorMapUnitId"></param>
        /// <returns></returns>
        public FloorMapUnitInfo GetFloorMapInfo(int floorMapUnitId)
        {
            var query = from f in Context.View_FloorMap_Unit
                        where f.FloorMapUnitID == floorMapUnitId
                        select f;
            var entity = query.FirstOrDefault();
            if (entity == null)
                return null;

            var info = new FloorMapUnitInfo
            {
                UnitID = entity.UnitID,
                FloorID = entity.FloorID,
                UnitNumber = entity.UnitNumber,
                UnitAera = entity.UnitAera,
                BizTypeID = entity.BizTypeID.HasValue ? entity.BizTypeID.Value : 0,
                ContractStatus = entity.ContractStatus,
                ContractStatusName = entity.ContractStatusName,
                UnitType = entity.UnitType,
                UnitTypeName = entity.UnitTypeName,
                ProjectUnitStatus = entity.ProjectUnitStatus,
                UnitStatusName = entity.UnitTypeName,
                ContractStartDate = entity.ContractStartDate,
                ContractEndDate = entity.ContractEndDate,
                ContractArea = entity.ContractArea,
                ContractUnitRent = entity.ContractUnitRent,
                ContractMgmtFee = entity.ContractMgmtFee,
                ContractLength = entity.ContractLength,
                DecorationLength = entity.DecorationLength,
                DecorationEndDate = entity.DecorationEndDate,
                QualityBond = entity.QualityBond,
                AdPointNum = entity.AdPointNum,
                ParkingLotNum = entity.ParkingLotNum,
                FloorMapUnitID = entity.FloorMapUnitID,
            };
            return info;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        public void UpdateFloorMapUnit(FloorMapUnitInfo info)
        {
            if (!info.FloorMapUnitID.HasValue)
                return;

            var bll = CreateBizObject<NHH.Entities.FloorMap_Unit>();
            var entity = bll.GetBySysNo(info.FloorMapUnitID.Value);
            if (entity == null)
                return;
            entity.UnitID = info.UnitID;
            entity.EditDate = DateTime.Now;
            entity.EditUser = 0;
            bll.Update(entity);
            this.SaveChanges();
        }
    }
}
