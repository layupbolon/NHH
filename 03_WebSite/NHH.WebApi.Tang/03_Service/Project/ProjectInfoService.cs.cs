using NHH.BizLogic.Common;
using NHH.Entities;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Service;
using System.Linq.Expressions;
using NHH.Models.Project;
using System.Data;
using NHH.Models.Common;
using System.Data.Common;
using System.Data.SqlClient;


namespace NHH.Service.Project
{
    /// <summary>
    /// 项目信息服务
    /// </summary>
    public class ProjectInfoService : NHHService<NHHEntities>, IProjectInfoService
    {
       /// <summary>
        /// 获取项目详细信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectInfoModel GetProjectInfo(int projectId)
        {
            var model = new ProjectInfoModel();

            var query = from p in Context.Project
                        where p.Status == 1 && p.ProjectID == projectId
                        select new
                        {
                            p.ProjectID,
                            p.ProjectName,
                            p.TotalArea,
                            p.RegionID,
                            p.ProvinceID,
                            p.CityID,
                            p.AddressLine,
                            p.AddressInfo,
                            p.Zipcode,
                            p.ManageCompanyID,
                            p.Longitude,
                            p.Latitude,
                            p.GroundArea,
                            p.UndergroundArea,
                            p.GrandOpeningDate,
                            p.RenderingFileName,
                            p.AdPointNum,
                            p.ParkingLotNum,
                            p.MultiBizPositionNum,
                            p.PlanHighlight,
                            p.PlanSummary,
                        };

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.ProjectName;
                model.AddressInfo = entity.AddressInfo;
                model.AddressLine = entity.AddressLine;
                model.RegionID = entity.RegionID;
                model.ProvinceID = entity.ProvinceID;
                model.CityID = entity.CityID;
                model.ZipCode = entity.Zipcode;
                model.Longitude = entity.Longitude.HasValue ? entity.Longitude.Value : 0;
                model.Latitude = entity.Latitude.HasValue ? entity.Latitude.Value : 0;
                model.ManageCompanyId = entity.ManageCompanyID;
                model.PlanSummary = entity.PlanSummary;
                model.PlanHighlight = entity.PlanHighlight;
                model.GroundArea = entity.GroundArea.HasValue ? entity.GroundArea.Value : 0;
                model.UndergroundArea = entity.UndergroundArea.HasValue ? entity.UndergroundArea.Value : 0;
                model.GrandOpeningDate = entity.GrandOpeningDate.HasValue ? entity.GrandOpeningDate.Value.ToString("yyyy-MM-dd") : "";
                model.AdPointNum = entity.AdPointNum.HasValue ? entity.AdPointNum.Value : 0;
                model.ParkingLotNum = entity.ParkingLotNum.HasValue ? entity.ParkingLotNum.Value : 0;
                model.MultiBizPositionNum = entity.MultiBizPositionNum.HasValue ? entity.MultiBizPositionNum.Value : 0;
                model.RenderingFileName = entity.RenderingFileName;
                model.TotalArea = model.UndergroundArea + model.GroundArea;
            }

            return model;
        }

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<BuildingInfo> GetBuildingList(int projectId)
        {
            var result = new List<BuildingInfo>();
            var query = from b in Context.Project_Building
                        where b.Status == 1 && b.ProjectID == projectId
                        select new
                        {
                            b.ProjectID,
                            b.BuildingID,
                            b.BuildingName,
                        };

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var building = new BuildingInfo
                    {
                        ProjectID = entity.ProjectID,
                        BuildingID = entity.BuildingID,
                        BuildingName = entity.BuildingName
                    };
                    result.Add(building);
                });
            }

            return result;
        }
        /// <summary>
        /// 获取楼层列表
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public List<FloorInfo> GetFloorList(int buildingId)
        {
            var result = new List<FloorInfo>();
            var query = from f in Context.Project_Floor
                        where f.Status == 1 && f.BuildingID == buildingId
                        select new
                        {
                            f.FloorID,
                            f.ProjectID,
                            f.BuildingID,
                            f.FloorNumber
                        };
            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var floor = new FloorInfo
                    {
                        BuildingID = entity.BuildingID,
                        FloorID = entity.FloorID,
                        FloorNumber = entity.FloorNumber,
                        ProjectID = entity.ProjectID
                    };
                    result.Add(floor);
                });
            }
            result.OrderBy(p => p.FloorNumber);
            return result;
        }
    }
}
