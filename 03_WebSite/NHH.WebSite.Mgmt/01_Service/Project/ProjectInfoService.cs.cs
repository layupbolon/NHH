using NHH.BizLogic.Common;
using NHH.Entities;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
        /// 获取项目列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectListModel GetProjectList(ProjectListQueryInfo queryInfo)
        {
            var model = new ProjectListModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;

            model.ProjectList = new List<ProjectInfo>();

            var query = from p in Context.Project
                        join s in Context.Dictionary on p.Stage equals s.FieldValue
                        where p.Status == 1 && s.FieldType == "ProjectStage"
                        select new
                        {
                            p.ProjectID,
                            p.ProjectName,
                            p.RegionID,
                            p.ProvinceID,
                            p.Province.ProvinceName,
                            p.CityID,
                            p.City.CityName,
                            p.RenderingFileName,
                            p.Stage,
                            ProjectStage = s.FieldName,
                            p.GrandOpeningDate,
                            p.TotalArea,
                            p.AdPointNum,
                            p.ParkingLotNum,
                            TotalRentArea = (from b in Context.Project_Building
                                             where b.Status == 1 && b.ProjectID == p.ProjectID
                                             select b.TotalRentArea).Sum(),
                            MainProjectCount = (from u1 in Context.Project_Unit
                                                where u1.Status == 1 && u1.ProjectID == p.ProjectID && u1.UnitType == 1
                                                select u1.UnitID).Count(),
                            PedestrianStreet = (from u2 in Context.Project_Unit
                                                where u2.Status == 1 && u2.ProjectID == p.ProjectID && u2.UnitType == 3
                                                select u2.UnitID).Count(),
                        };

            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.RegionId.HasValue)
            {
                query = query.Where(item => item.RegionID == queryInfo.RegionId.Value);
            }
            if (queryInfo.ProvinceId.HasValue)
            {
                query = query.Where(item => item.ProvinceID == queryInfo.ProvinceId.Value);
            }
            if (queryInfo.CityId.HasValue)
            {
                query = query.Where(item => item.CityID == queryInfo.CityId.Value);
            }
            if (queryInfo.Stage.HasValue)
            {
                query = query.Where(item => item.Stage == queryInfo.Stage.Value);
            }

            model.PagingInfo.TotalCount = query.Count();
            if (string.IsNullOrEmpty(queryInfo.OrderBy) || queryInfo.OrderBy.Length == 0)
            {
                queryInfo.OrderBy = "ProjectName";
            }            
            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null && entityList.Count > 0)
            {
                entityList.ForEach(entity =>
                {
                    var project = new ProjectInfo
                    {
                        ProjectID = entity.ProjectID,
                        ProjectName = entity.ProjectName,
                        RenderingFileName = entity.RenderingFileName,
                        ProjectStage = entity.ProjectStage,
                        ProvinceAndcity = string.Format("{0}{1}", entity.ProvinceName, entity.CityName),
                        GrandOpeningDate = entity.GrandOpeningDate.HasValue ? entity.GrandOpeningDate.Value.ToString("yyyy-MM-dd") : "",
                        TotalArea = entity.TotalArea.HasValue ? entity.TotalArea.Value : 0,
                        AdPointNum = entity.AdPointNum.HasValue ? entity.AdPointNum.Value : 0,
                        ParkingLotNum = entity.ParkingLotNum.HasValue ? entity.ParkingLotNum.Value : 0,
                        TotalRentArea = entity.TotalRentArea.HasValue ? entity.TotalRentArea.Value : 0,
                        MainProjectCount = entity.MainProjectCount,
                        PedestrianStreet = entity.PedestrianStreet,
                    };
                    //业主公司
                    project.OwnerList = GetProjectOwnerList(entity.ProjectID);
                    model.ProjectList.Add(project);
                });
            }

            return model;
        }

        /// <summary>
        /// 获取项目详细信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectDetailModel GetProjectDetail(int projectId)
        {
            var model = new ProjectDetailModel();

            var query = from p in Context.Project
                        where p.Status == 1 && p.ProjectID == projectId
                        select new
                        {
                            p.ProjectID,
                            p.ProjectName,
                        };

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.ProjectId = entity.ProjectID;
                model.ProjectName = entity.ProjectName;
            }

            return model;
        }

        /// <summary>
        /// 获取项目基础信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectBasicInfoModel GetProjectBasicInfo(int projectId)
        {
            var model = new ProjectBasicInfoModel();

            var query = from p in Context.Project
                        join c2 in Context.Company on p.ManageCompanyID equals c2.CompanyID
                        where p.Status == 1 && p.ProjectID == projectId
                        select new
                        {
                            p.ProjectID,
                            p.ProjectName,
                            p.RegionID,
                            p.Region.RegionName,
                            p.ProvinceID,
                            p.Province.ProvinceName,
                            p.CityID,
                            p.City.CityName,
                            p.Stage,
                            p.AddressLine,
                            p.AddressInfo,
                            p.Zipcode,
                            p.ManageCompanyID,
                            p.Longitude,
                            p.Latitude,
                            p.ProjectSummary,
                            p.PlanHighlight,
                            p.PlanSummary,
                            ManageCompanyName = c2.BriefName,
                        };

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.RegionName = entity.RegionName;
                model.AddressLine = entity.AddressLine;
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.ProjectName;
                model.AddressInfo = entity.AddressInfo;
                model.AddressLine = entity.AddressLine;
                model.RegionID = entity.RegionID;
                model.ProvinceID = entity.ProvinceID;
                model.CityID = entity.CityID;
                model.ProvinceAndcity = string.Format("{0}{1}", entity.ProvinceName, entity.CityName);
                model.ZipCode = entity.Zipcode;
                model.Longitude = entity.Longitude.HasValue ? entity.Longitude.Value : 0;
                model.Latitude = entity.Latitude.HasValue ? entity.Latitude.Value : 0;
                model.ProjectSummary = entity.ProjectSummary;
                model.PlanSummary = entity.PlanSummary;
                model.PlanHighlight = entity.PlanHighlight;
                model.ProjectSummary = entity.ProjectSummary;
                model.TotalArea = model.UndergroundArea + model.GroundArea;
                model.ManageCompanyName = entity.ManageCompanyName;
                model.ManageCompanyId = entity.ManageCompanyID;

                model.OwnerList = GetProjectOwnerList(entity.ProjectID);
            }
            return model;
        }

        /// <summary>
        /// 获取项目的详细信息
        /// 包括楼宇，楼层等
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectDetailInfoModel GetProjectDetailInfo(int projectId)
        {
            var model = new ProjectDetailInfoModel();

            var query = from p in Context.Project
                        join s in Context.Dictionary on p.Stage equals s.FieldValue
                        where p.Status == 1 && p.ProjectID == projectId && s.FieldType == "ProjectStage"
                        select new
                        {
                            p.ProjectID,
                            p.Stage,
                            p.ProjectName,
                            p.TotalArea,
                            p.GroundArea,
                            p.UndergroundArea,
                            p.GrandOpeningDate,
                            p.RenderingFileName,
                            p.AdPointNum,
                            p.ParkingLotNum,
                            p.MultiBizPositionNum,
                            p.PlanHighlight,
                            p.PlanSummary,
                            ProjectStage = s.FieldName,
                        };

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.ProjectName;
                model.PlanSummary = entity.PlanSummary;
                model.PlanHighlight = entity.PlanHighlight;
                model.ProjectStage = entity.ProjectStage;
                model.Stage = entity.Stage;
                model.GroundArea = entity.GroundArea.HasValue ? entity.GroundArea.Value : 0;
                model.UndergroundArea = entity.UndergroundArea.HasValue ? entity.UndergroundArea.Value : 0;
                model.TotalArea = entity.TotalArea.HasValue ? entity.TotalArea.Value : 0;
                model.GrandOpeningDate = entity.GrandOpeningDate.HasValue ? entity.GrandOpeningDate.Value.ToString("yyyy-MM-dd") : "";
                model.AdPointNum = entity.AdPointNum.HasValue ? entity.AdPointNum.Value : 0;
                model.ParkingLotNum = entity.ParkingLotNum.HasValue ? entity.ParkingLotNum.Value : 0;
                model.MultiBizPositionNum = entity.MultiBizPositionNum.HasValue ? entity.MultiBizPositionNum.Value : 0;
                model.RenderingFileName = entity.RenderingFileName;
                model.OwnerList = GetProjectOwnerList(entity.ProjectID);
            }

            return model;
        }


        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public BuildingListModel GetBuildingList(int projectId)
        {
            var model = new BuildingListModel();
            model.ProjectId = projectId;
            model.BuildingList = new List<BuildingDetailInfo>();
            var query = from b in Context.Project_Building
                        where b.Status == 1 && b.ProjectID == projectId
                        select new
                        {
                            b.ProjectID,
                            b.BuildingID,
                            b.BuildingName,
                            b.GroundFloorNum,
                            b.UndergroundFloorNum,
                            b.BuildingGroundArea,
                            b.BuildingUndergroundArea,
                            b.TotalConstructionArea,
                            b.PlanHighlight,
                            b.PlanSummary,
                            b.ContractStore,
                            b.RenderingFileName,
                            b.TotalRentArea,
                            b.TotalRentUnit,
                            FloorList = (from f in Context.Project_Floor
                                         where f.BuildingID == b.BuildingID
                                         select new
                                         {
                                             f.FloorID,
                                             f.FloorNumber,
                                             f.FloorName,
                                             f.FloorMapFileName,
                                             f.FloorDescription,
                                         }).ToList(),
                        };

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var building = new BuildingDetailInfo
                    {
                        ProjectID = entity.ProjectID,
                        BuildingID = entity.BuildingID,
                        BuildingName = entity.BuildingName,
                        GroundFloorNum = entity.GroundFloorNum.HasValue ? entity.GroundFloorNum.Value : 0,
                        UndergroundFloorNum = entity.UndergroundFloorNum.HasValue ? entity.UndergroundFloorNum.Value : 0,
                        BuildingGroundArea = entity.BuildingGroundArea.HasValue ? entity.BuildingGroundArea.Value : 0,
                        BuildingUndergroundArea = entity.BuildingUndergroundArea.HasValue ? entity.BuildingUndergroundArea.Value : 0,
                        TotalConstructionArea = entity.TotalConstructionArea.HasValue ? entity.TotalConstructionArea.Value : 0,
                        PlanHighlight = entity.PlanHighlight,
                        PlanSummary = entity.PlanSummary,
                        ContractStore = entity.ContractStore,
                        RenderingFileName = entity.RenderingFileName,
                        TotalRentArea = entity.TotalRentArea.HasValue ? entity.TotalRentArea.Value : 0,
                        TotalRentUnit = entity.TotalRentUnit.HasValue ? entity.TotalRentUnit.Value : 0
                    };
                    //楼层列表
                    building.GroundFloorList = new List<FloorInfo>();
                    building.UndergroundFloorList = new List<FloorInfo>();
                    if (entity.FloorList != null)
                    {
                        entity.FloorList.ForEach(floorEntity =>
                        {
                            var floor = new FloorInfo
                            {
                                FloorID = floorEntity.FloorID,
                                FloorNumber = floorEntity.FloorNumber,
                                FloorName = floorEntity.FloorName,
                                FloorMapFileName = floorEntity.FloorMapFileName,
                                FloorDescription = floorEntity.FloorDescription,
                            };
                            if (floorEntity.FloorNumber < 1)
                            {
                                building.UndergroundFloorList.Add(floor);
                            }
                            else
                            {
                                building.GroundFloorList.Add(floor);
                            }
                        });
                    }
                    model.BuildingList.Add(building);
                });
            }

            return model;
        }

        /// <summary>
        /// 获取楼宇详细信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public BuildingDetailInfoModel GetBuildingDetail(int buildingId)
        {
            var model = new BuildingDetailInfoModel();
            var query = from b in Context.Project_Building
                        where b.Status == 1 && b.BuildingID == buildingId
                        select new
                        {
                            b.ProjectID,
                            b.BuildingID,
                            b.BuildingCode,
                            b.BuildingName,
                            b.GroundFloorNum,
                            b.UndergroundFloorNum,
                            b.BuildingGroundArea,
                            b.BuildingUndergroundArea,
                            b.TotalConstructionArea,
                            b.PlanHighlight,
                            b.PlanSummary,
                            b.ContractStore,
                            b.RenderingFileName
                        };

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.ProjectID = entity.ProjectID;
                model.BuildingID = entity.BuildingID;
                model.BuildingCode = entity.BuildingCode;
                model.BuildingName = entity.BuildingName;
                model.GroundFloorNum = entity.GroundFloorNum.HasValue ? entity.GroundFloorNum.Value : 0;
                model.UndergroundFloorNum = entity.UndergroundFloorNum.HasValue ? entity.UndergroundFloorNum.Value : 0;
                model.BuildingGroundArea = entity.BuildingGroundArea.HasValue ? entity.BuildingGroundArea.Value : 0;
                model.BuildingUndergroundArea = entity.BuildingUndergroundArea.HasValue ? entity.BuildingUndergroundArea.Value : 0;
                model.TotalConstructionArea = entity.TotalConstructionArea.HasValue ? entity.TotalConstructionArea.Value : 0;
                model.PlanHighlight = entity.PlanHighlight;
                model.PlanSummary = entity.PlanSummary;
                model.ContractStore = entity.ContractStore;
                model.RenderingFileName = entity.RenderingFileName;
            }
            return model;
        }

        /// <summary>
        /// 获取楼层列表
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public FloorListModel GetFloorList(int buildingId)
        {
            var model = new FloorListModel();
            model.BuildingId = buildingId;
            model.BuildingList = new List<BuildingCommonInfo>();
            model.FloorList = new List<FloorInfo>();

            #region 楼宇列表
            var query0 = from b1 in Context.Project_Building
                         join b2 in Context.Project_Building on b1.ProjectID equals b2.ProjectID
                         where b1.Status == 1 && b2.Status == 1 && b1.BuildingID == buildingId
                         select new
                         {
                             b2.BuildingID,
                             b2.BuildingName,
                             b2.GroundFloorNum,
                             b2.UndergroundFloorNum
                         };
            var buildingList = query0.ToList();
            if (buildingList != null)
            {
                //buildingList.ForEach(building =>
                //{
                //    model.BuildingList.Add(new BuildingCommonInfo
                //    {
                //        Id = building.BuildingID,
                //        Name = building.BuildingName,
                //    });

                //});
                foreach (var building in buildingList)
                {
                    model.BuildingList.Add(new BuildingCommonInfo
                    {
                        Id = building.BuildingID,
                        Name = building.BuildingName,
                    });
                    if (building.BuildingID == buildingId)
                    {
                        model.CurBuildingName = building.BuildingName;
                    }
                }
            }
            #endregion

            var query = from f in Context.Project_Floor
                        where f.Status == 1 && f.BuildingID == buildingId
                        select new
                        {
                            f.FloorID,
                            f.FloorNumber,
                            f.FloorDescription,
                            f.FloorMapFileName,
                            f.TotalRentArea,
                            f.TotalRentUnit,
                            f.BuildingID,
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
                        FloorDescription = entity.FloorDescription,
                        FloorMapFileName = entity.FloorMapFileName,
                        TotalRentArea = entity.TotalRentArea.HasValue ? entity.TotalRentArea.Value : 0,
                        TotalRentUnit = entity.TotalRentUnit.HasValue ? entity.TotalRentUnit.Value : 0,
                    };
                    model.FloorList.Add(floor);
                });
            }
            model.FloorList.OrderBy(p => p.FloorNumber);
            return model;
        }

        /// <summary>
        /// 获取楼层详细信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public FloorDetailModel GetFloorDetail(int floorId)
        {
            var model = new FloorDetailModel();

            var query = from f in Context.Project_Floor
                        where f.Status == 1 && f.FloorID == floorId
                        select new
                        {
                            f.FloorID,
                            f.FloorNumber,
                            f.FloorName,
                            f.FloorDescription,
                            f.FloorMapFileName,
                            f.BuildingID,
                        };

            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.FloorID = entity.FloorID;
                model.FloorNumber = entity.FloorNumber;
                model.FloorName = entity.FloorName;
                model.FloorDescription = entity.FloorDescription;
                model.BuildingID = entity.BuildingID;
                model.FloorMapFileName = entity.FloorMapFileName;
            }
            return model;
        }

        /// <summary>
        /// 获取楼层信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public FloorInfo GetFloorInfo(int floorId)
        {
            var info = new FloorInfo();
            var query = from f in Context.Project_Floor
                        where f.FloorID == floorId
                        select f;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                info.BuildingID = entity.BuildingID;
                info.FloorID = entity.FloorID;
                info.FloorNumber = entity.FloorNumber;
                info.FloorName = entity.FloorName;
                info.FloorMapFileName = entity.FloorMapFileName;
                info.FloorDescription = entity.FloorDescription;
                info.TotalRentArea = entity.TotalRentArea.HasValue ? entity.TotalRentArea.Value : 0;
                info.TotalRentUnit = entity.TotalRentUnit.HasValue ? entity.TotalRentUnit.Value : 0;
            }
            return info;
        }

        /// <summary>
        /// 根据detail查下楼层信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public FloorInfo GetFloorInfoByDetail(int buildingid, int floorNum)
        {
            var info = new FloorInfo();
            var query = from f in Context.Project_Floor
                        where f.BuildingID == buildingid && f.FloorNumber == floorNum
                        select f;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                info.BuildingID = entity.BuildingID;
                info.FloorID = entity.FloorID;
                info.FloorNumber = entity.FloorNumber;
                info.FloorMapFileName = entity.FloorMapFileName;
                info.FloorDescription = entity.FloorDescription;
                info.TotalRentArea = entity.TotalRentArea.HasValue ? entity.TotalRentArea.Value : 0;
                info.TotalRentUnit = entity.TotalRentUnit.HasValue ? entity.TotalRentUnit.Value : 0;
            }
            return info;
        }

        /// <summary>
        /// 获取项目效果图
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public string GetRenderingFileName(int projectId)
        {
            var query = from p in Context.Project
                        where p.ProjectID == projectId
                        select new
                        {
                            p.RenderingFileName
                        };
            var result = query.FirstOrDefault();
            if (result == null)
                return string.Empty;
            return result.RenderingFileName;
        }

        /// <summary>
        /// 获取项目业主列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        private List<ProjectOwnerInfo> GetProjectOwnerList(int projectId)
        {
            var list = new List<ProjectOwnerInfo>();
            var query = from c in Context.Project_Owner
                        where c.Status == 1 && c.ProjectID == projectId
                        select c;
            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new ProjectOwnerInfo
                    {
                        OwnerID = entity.OwnerID,
                        ProjectID = entity.ProjectID,
                        CompanyID = entity.CompanyID,
                        CompanyName = entity.CompanyName,
                    });
                });
            }
            return list;
        }
    }
}
