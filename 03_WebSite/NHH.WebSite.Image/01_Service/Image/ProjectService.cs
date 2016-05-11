using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Image;
using NHH.Service.Image.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Image
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public class ProjectService : NHHService<NHHEntities>, IProjectService
    {
        /// <summary>
        /// 获取楼宇信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public BuildingInfo GetBuildingInfo(int buildingId)
        {
            var info = new BuildingInfo();

            var query = from b in Context.Project_Building
                        where b.BuildingID == buildingId && b.Status == 1
                        select new
                        {
                            b.ProjectID,
                            b.BuildingID,
                            b.BuildingName,
                        };

            var entity = query.FirstOrDefault();

            if (entity == null)
                return info;

            info.ProjectId = entity.ProjectID;
            info.Id = entity.BuildingID;
            return info;
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public FloorInfo GetFloorInfo(int floorId)
        {
            var info = new FloorInfo();

            var query = from f in Context.Project_Floor
                        join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                        where f.FloorID == floorId && f.Status == 1
                        select new
                        {
                            f.ProjectID,
                            f.FloorID,
                            f.FloorNumber,
                            b.BuildingID,
                        };

            var entity = query.FirstOrDefault();

            if (entity == null)
                return info;

            info.ProjectId = entity.ProjectID;
            info.BuildingId = entity.BuildingID;
            info.FloorId = entity.FloorID;

            return info;
        }
    }
}
