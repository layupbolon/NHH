using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Contract;
using NHH.Service.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract
{
    /// <summary>
    /// 商铺服务
    /// </summary>
    public class ProjectUnitService : NHHService<NHHEntities>, IProjectUnitService
    {
        /// <summary>
        /// 获取商铺列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectUnitListModel GetProjectUnitList(ProjectUnitQueryInfo queryInfo)
        {
            var model = new ProjectUnitListModel();
            model.ProjectUnitList = new List<ProjectUnitInfo>();
            model.PagingInfo = new Models.Common.PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;
            

            if (!queryInfo.ProjectId.HasValue)
            {
                return model;
            }

            var query = from unit in Context.Project_Unit
                        join project in Context.Project on unit.ProjectID equals project.ProjectID
                        join building in Context.Project_Building on unit.BuildingID equals building.BuildingID
                        join floor in Context.Project_Floor on unit.FloorID equals floor.FloorID
                        join type in Context.View_UnitType on unit.UnitType equals type.UnitTypeID
                        join status in Context.View_UnitStatus on unit.UnitStatus equals status.UnitStatusID
                        where unit.Status == 1 && unit.ProjectID == queryInfo.ProjectId.Value
                        select new
                        {
                            project.ProjectID,
                            project.ProjectName,
                            building.BuildingName,
                            floor.FloorID,
                            floor.FloorNumber,
                            unit.UnitID,
                            unit.UnitNumber,
                            unit.UnitAera,
                            type.UnitTypeID,
                            type.UnitTypeName,
                            status.UnitStatusID,
                            status.UnitStatusName,
                        };

            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }

            if (!string.IsNullOrEmpty(queryInfo.UnitNumber) && queryInfo.UnitNumber.Length > 0)
            {
                query = query.Where(item => item.UnitNumber == queryInfo.UnitNumber);
            }

            if (queryInfo.UnitStatus.HasValue)
            {
                query = query.Where(item => item.UnitStatusID == queryInfo.UnitStatus.Value);
            }

            model.PagingInfo.TotalCount = query.Count();

            var entityList = query.OrderBy(item => item.UnitID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList == null || entityList.Count == 0)
                return model;

            entityList.ForEach(entity =>
            {
                var unit = new ProjectUnitInfo
                {
                    ProjectName = entity.ProjectName,
                    UnitId = entity.UnitID,
                    UnitNumber = entity.UnitNumber,
                    UnitArea = entity.UnitAera,
                    UnitType = entity.UnitTypeName,
                    UnitStatus = entity.UnitStatusName,
                };
                string strFormat = entity.FloorNumber < 1 ? "{0} B{1}" : "{0} {1}F";
                unit.FloorName = string.Format(strFormat, entity.BuildingName, Math.Abs((int)entity.FloorNumber));
                model.ProjectUnitList.Add(unit);
            });


            return model;
        }
    }
}
