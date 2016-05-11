using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project
{
    /// <summary>
    /// 铺位管理服务
    /// </summary>
    public class ProjectUnitService : NHHService<NHHEntities>, IProjectUnitService
    {
        /// <summary>
        /// 根据铺位号取铺位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectUnitInfo GetUnitInfo(int unitId)
        {
            var result = new ProjectUnitInfo();
            var query = from pu in Context.Project_Unit
                join pb in Context.Project_Building on pu.BuildingID equals pb.BuildingID
                join pf in Context.Project_Floor on pu.FloorID equals pf.FloorID
                where pu.UnitID == unitId
                select new
                {
                    pu.UnitID,
                    pu.BuildingID,
                    pu.FloorID,
                    pu.UnitNumber,
                    pb.BuildingName,
                    pf.FloorName
                };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                result.UnitID = entity.UnitID;
                result.BuildingID = entity.BuildingID;
                result.FloorID = entity.FloorID;
                result.UnitNumber = entity.BuildingName + " " + entity.FloorName + " " + entity.UnitNumber;
            }
            return result;
        }
    }
}
