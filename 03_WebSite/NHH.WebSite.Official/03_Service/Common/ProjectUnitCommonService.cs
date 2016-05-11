using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 商铺公共服务
    /// </summary>
    public class ProjectUnitCommonService : NHHService<NHHEntities>, IProjectUnitCommonService
    {
        /// <summary>
        /// 获取商铺公共基础信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectUnitCommonInfo GetProjectUnitCommonInfo(int unitId)
        {
            var info = new ProjectUnitCommonInfo();

            var query = from unit in Context.Project_Unit
                        join type in Context.View_UnitType on unit.UnitType equals type.UnitTypeID
                        where unit.UnitID == unitId
                        select new
                        {
                            unit.ProjectID,
                            unit.UnitID,
                            unit.UnitAera,
                            unit.UnitNumber,
                            unit.FloorID,
                            type.UnitTypeID,
                            type.UnitTypeName,
                            unit.BizTypeID,
                            unit.BizType.BizTypeName,
                        };


            var entity = query.FirstOrDefault();
            if (entity == null)
                return info;

            info.UnitId = entity.UnitID;
            info.BizType.Id = (entity.BizTypeID.HasValue ? entity.BizTypeID.Value : 0);
            info.BizType.Name = entity.BizTypeName == null ? "" : entity.BizTypeName;
            info.InternalUnitArea = 0;
            info.UnitArea = entity.UnitAera;
            info.UnitNumber = entity.UnitNumber;
            info.UnitType.Id = entity.UnitTypeID;
            info.UnitType.Name = entity.UnitTypeName;

            #region  楼层信息

            var query1 = from f in Context.Project_Floor
                         join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                         join p in Context.Project on f.ProjectID equals p.ProjectID
                         where f.FloorID == entity.FloorID
                         select new
                         {
                             f.FloorID,
                             f.FloorNumber,
                             b.BuildingID,
                             b.BuildingName,
                             p.ProjectID,
                             p.ProjectName,
                         };

            var floorEntity = query1.FirstOrDefault();
            if (floorEntity != null)
            {
                info.FloorInfo.FloorId = floorEntity.FloorID;
                info.FloorInfo.FloorNumber = floorEntity.FloorNumber;
                info.FloorInfo.BuildingId = floorEntity.BuildingID;
                info.FloorInfo.BuildingName = floorEntity.BuildingName;
                info.ProjectId = floorEntity.ProjectID;
                info.ProjectName = floorEntity.ProjectName;
            }
            #endregion

            return info;
        }
    }
}
