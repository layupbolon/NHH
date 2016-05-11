using NHH.Entities;
using NHH.Framework.Exceptions;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Project;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
        /// 获取商铺列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectUnitListModel GetProjectUnitList(ProjectUnitListQueryInfo queryInfo)
        {
            var model = new ProjectUnitListModel();
            model.QueryInfo = queryInfo;
            model.BuildingList = new List<BuildingCommonInfo>();
            model.FloorList = new List<FloorCommonInfo>();
            model.ProjectUnitList = new List<ProjectUnitInfo>();

            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;

            if (!queryInfo.ProjectId.HasValue)
                return model;

            #region 楼宇列表
            var query0 = from b in Context.Project_Building
                         where b.ProjectID == queryInfo.ProjectId.Value && b.Status == 1
                         select new
                         {
                             b.ProjectID,
                             b.BuildingID,
                             b.BuildingName,
                         };

            var buildingList = query0.ToList();
            if (buildingList != null)
            {
                buildingList.ForEach(building =>
                {
                    model.BuildingList.Add(new BuildingCommonInfo
                    {
                        Id = building.BuildingID,
                        Name = building.BuildingName,
                        ProjectId = building.ProjectID,
                        FloorList = new List<FloorCommonInfo>()
                    });
                });
            }

            #endregion

            #region 楼层列表
            var query1 = from f in Context.Project_Floor
                         join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                         where f.ProjectID == queryInfo.ProjectId && f.Status == 1
                         select new
                         {
                             b.BuildingID,
                             b.BuildingName,
                             f.FloorID,
                             f.FloorName,
                             //商铺数量
                             UnitNumber = (from u in Context.Project_Unit
                                           where u.FloorID == f.FloorID && u.Status == 1
                                           select u).Count()
                         };
            var floorList = query1.ToList();
            if (floorList != null)
            {
                floorList.ForEach(floor =>
                {
                    model.FloorList.Add(new FloorCommonInfo
                    {
                        FloorId = floor.FloorID,
                        FloorName = string.Format("{0} {1}", floor.BuildingName, floor.FloorName),
                        BuildingId = floor.BuildingID,
                        BuildingName = floor.BuildingName,
                    });
                });

                model.BuildingList.ForEach(building =>
                {
                    var floorTempList = floorList.FindAll(floor => floor.BuildingID == building.Id);
                    if (floorTempList != null)
                    {
                        floorTempList.ForEach(floor =>
                        {
                            building.FloorList.Add(new FloorCommonInfo
                            {
                                FloorId = floor.FloorID,
                                FloorName = floor.FloorName,
                                BuildingId = floor.BuildingID,
                                BuildingName = floor.BuildingName,
                                UnitNumber = floor.UnitNumber,
                            });
                        });
                    }
                });
            }

            #endregion

            #region 商铺列表
            var query = from u in Context.View_Project_Unit
                        join type in Context.View_UnitType on u.UnitType equals type.UnitTypeID into temp1
                        from type1 in temp1.DefaultIfEmpty()
                        join status in Context.View_UnitStatus on u.UnitStatus equals status.UnitStatusID into temp2
                        from status1 in temp2.DefaultIfEmpty()
                        where u.ProjectID == queryInfo.ProjectId && u.Status == 1
                        select new
                        {
                            u.ProjectID,
                            u.UnitID,
                            u.UnitNumber,
                            u.UnitAera,
                            u.UnitMapFileName,
                            u.FloorMapLocation,
                            u.UnitType,
                            u.FloorID,
                            u.FloorNumber,
                            u.BuildingID,
                            u.BuildingName,
                            u.FloorName,
                            u.FloorMapFileName,
                            type1.UnitTypeName,
                            status1.UnitStatusName,
                        };

            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }

            if (queryInfo.BuildingId.HasValue)
            {
                query = query.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                query = query.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }

            model.PagingInfo.TotalCount = query.Count();

            var unitList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (unitList != null)
            {
                unitList.ForEach(unit =>
                {
                    var unitInfo = new ProjectUnitInfo
                    {
                        ProjectId = unit.ProjectID,
                        UnitId = unit.UnitID,
                        UnitNumber = unit.UnitNumber,
                        UnitArea = unit.UnitAera,
                        //InternalUnitArea = unit.InternalUnitAera,
                        UnitMapFileName = unit.UnitMapFileName,
                        FloorMapLocation = unit.FloorMapLocation,
                        BuildingName = unit.BuildingName,
                        FloorName = unit.FloorName,
                        FloorMapFileName = unit.FloorMapFileName,
                        UnitTypeId = unit.UnitType
                    };
                    unitInfo.UnitTypeName = unit.UnitTypeName;
                    unitInfo.BuildingName = unit.BuildingName;
                    unitInfo.FloorName = unit.FloorName;
                    unitInfo.UnitStatusName = unit.UnitStatusName;
                    if (!string.IsNullOrEmpty(unitInfo.FloorMapLocation))
                    {
                        var arr = unitInfo.FloorMapLocation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (arr.Length > 0)
                        {
                            int x = 0;
                            int.TryParse(arr[0], out x);
                            unitInfo.FloorMapX = x;
                        }
                        if (arr.Length > 1)
                        {
                            int y = 0;
                            int.TryParse(arr[1], out y);
                            unitInfo.FloorMapY = y;
                        }
                    }
                    model.ProjectUnitList.Add(unitInfo);
                });
            }
            #endregion

            return model;
        }

        /// <summary>
        /// 添加商铺
        /// </summary>
        /// <param name="model"></param>
        public MessageBag<bool> AddProjectUnit(ProjectUnitInfoModel model)
        {
            var result = new MessageBag<bool>();
            result.Desc = "添加成功";

            var query = from pu in Context.Project_Unit
                        where pu.Status == 1 && pu.FloorID == model.FloorId && pu.UnitNumber == model.UnitNumber
                        select pu.UnitID;

            if (query.Any())
            {
                result.Code = -1;
                result.Desc = "已有相同铺位编号";
                result.Key = "UnitNumber";
                return result;
            }

            var entity = new Project_Unit
            {
                ProjectID = model.ProjectId,
                BuildingID = model.BuildingId,
                FloorID = model.FloorId,
                ContractEndDate = DateTime.Now,
                ContractStatus = 0,
                UnitMapFileName = model.UnitMapFileName,
                FloorMapLocation = string.Format("{0},{1}", model.FloorMapX, model.FloorMapY),
                Tag = "",
                UnitAera = model.UnitArea,
                UnitNumber = model.UnitNumber,
                UnitType = model.UnitTypeId,
                UnitStatus = 1,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.UserId,
                EditDate = DateTime.Now,
                EditUser = model.UserId,
            };

            var bll = CreateBizObject<Project_Unit>();
            bll.Insert(entity);
            this.SaveChanges();

            return result;
        }

        /// <summary>
        /// 更新商铺信息
        /// </summary>
        /// <param name="model"></param>
        public void UpdateProjectUnit(ProjectUnitInfoModel model)
        {
            var bll = CreateBizObject<Project_Unit>();
            var entity = bll.GetBySysNo(model.UnitId);

            if (entity != null)
            {
                entity.UnitAera = model.UnitArea;
                entity.UnitNumber = model.UnitNumber;
                entity.UnitType = model.UnitTypeId;
                entity.UnitMapFileName = model.UnitMapFileName;
                entity.FloorMapLocation = string.Format("{0},{1}", model.FloorMapX, model.FloorMapY);
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.UserId;
            }
            bll.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 删除商铺
        /// </summary>
        /// <param name="unitId"></param>
        public MessageBag<bool> DelProjectUnit(int unitId)
        {
            var message = new MessageBag<bool>();
            message.Data = false;
            var bll = CreateBizObject<Project_Unit>();
            var entity = bll.GetBySysNo(unitId);
            if (entity.UnitStatus == 4)
            {
                message.Code = -1;
                message.Desc = "商铺处于出租状态，无法作废";
                return message;
            }
            bll.Delete(unitId);
            this.SaveChanges();
            message.Data = true;
            return message;
        }

        /// <summary>
        /// 获取商铺信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectUnitInfoModel GetProjectUnitInfo(int unitId)
        {
            var model = new ProjectUnitInfoModel();

            var query = from u in Context.View_Project_Unit
                        join us in Context.View_UnitStatus on u.UnitStatus equals us.UnitStatusID
                        join ut in Context.View_UnitType on u.UnitType equals ut.UnitTypeID
                        where u.UnitID == unitId
                        select new
                        {
                            u.UnitID,
                            u.UnitNumber,
                            u.UnitAera,
                            u.UnitType,
                            u.FloorMapLocation,
                            u.UnitMapFileName,
                            u.FloorMapFileName,
                            u.FloorMapDimension,
                            u.ProjectID,
                            u.FloorID,
                            u.FloorName,
                            u.BuildingID,
                            u.BuildingName,
                            u.FloorNumber,
                            u.UnitStatus,
                            us.UnitStatusName,
                            ut.UnitTypeName,
                        };

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.UnitId = entity.UnitID;
                model.UnitNumber = entity.UnitNumber;
                model.ProjectId = entity.ProjectID;
                model.BuildingId = entity.BuildingID;
                model.BuildingName = entity.BuildingName;
                model.FloorId = entity.FloorID;
                model.FloorName = entity.FloorName;
                model.UnitArea = entity.UnitAera;
                //model.InternalUnitArea = entity.InternalUnitAera;                
                model.UnitTypeId = entity.UnitType;
                model.UnitMapFileName = entity.UnitMapFileName;
                model.FloorMapLocation = entity.FloorMapLocation;
                model.FloorMapFileName = entity.FloorMapFileName;
                model.UnitStatus = entity.UnitStatus;
                model.UnitStatusName = entity.UnitStatusName;
                model.UnitTypeName = entity.UnitTypeName;
            }
            if (!string.IsNullOrEmpty(model.FloorMapLocation))
            {
                var arr = model.FloorMapLocation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (arr.Length > 0)
                {
                    int x = 0;
                    int.TryParse(arr[0], out x);
                    model.FloorMapX = x;
                }
                if (arr.Length > 1)
                {
                    int y = 0;
                    int.TryParse(arr[1], out y);
                    model.FloorMapY = y;
                }
            }
            return model;
        }

        /// <summary>
        /// 获取楼宇信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public BuildingCommonInfo GetBuildingInfo(int buildingId)
        {
            var info = new BuildingCommonInfo();

            var query = from b in Context.Project_Building
                        where b.BuildingID == buildingId
                        select new
                        {
                            b.BuildingID,
                            b.BuildingName,
                            b.ProjectID,
                        };


            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                info.Id = entity.BuildingID;
                info.Name = entity.BuildingName;
                info.ProjectId = entity.ProjectID;
            }
            return info;
        }

        /// <summary>
        /// 获取楼层信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public FloorCommonInfo GetFloorInfo(int floorId)
        {
            var info = new FloorCommonInfo();

            var query = from f in Context.Project_Floor
                        join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                        where f.FloorID == floorId
                        select new
                        {
                            b.BuildingID,
                            b.BuildingName,
                            b.ProjectID,
                            f.FloorID,
                            f.FloorName,
                            f.FloorNumber,
                            f.FloorMapFileName,
                        };


            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                info.BuildingId = entity.BuildingID;
                info.BuildingName = entity.BuildingName;
                info.ProjectId = entity.ProjectID;
                info.FloorId = entity.FloorID;
                info.FloorName = entity.FloorName;
                info.FloorNumber = entity.FloorNumber;
                info.FloorMapFileName = entity.FloorMapFileName;
            }
            return info;
        }

        /// <summary>
        /// 根据floorId获取商铺列表
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public List<ProjectUnitInfoModel> GetProjectUnitModelList(int floorId, int page, PagingInfo pagingInfo)
        {
            List<ProjectUnitInfoModel> list = new List<ProjectUnitInfoModel>();
            var query = from u in Context.Project_Unit
                        join type in Context.View_UnitType on u.UnitType equals type.UnitTypeID into temp1
                        from type1 in temp1.DefaultIfEmpty()
                        join status in Context.View_UnitStatus on u.UnitStatus equals status.UnitStatusID into temp2
                        from status1 in temp2.DefaultIfEmpty()
                        where u.Status == 1 && u.FloorID == floorId
                        select new
                        {
                            u.ProjectID,
                            u.UnitID,
                            u.BuildingID,
                            u.UnitNumber,
                            u.UnitAera,
                            u.UnitMapFileName,
                            u.FloorMapLocation,
                            u.UnitType,
                            type1.UnitTypeName,
                            status1.UnitStatusName

                        };
            if (query != null)
            {
                query.ToList().ForEach(unit =>
                {
                    var unitInfo = new ProjectUnitInfoModel
                    {
                        ProjectId = unit.ProjectID,
                        BuildingId = unit.BuildingID,
                        UnitId = unit.UnitID,
                        UnitNumber = unit.UnitNumber,
                        UnitArea = unit.UnitAera,
                        UnitMapFileName = unit.UnitMapFileName,
                        FloorMapLocation = unit.FloorMapLocation,
                        UnitTypeId = unit.UnitType,
                        UnitTypeName = unit.UnitTypeName,
                    };
                    unitInfo.UnitTypeName = unit.UnitTypeName;
                    unitInfo.UnitStatusName = unit.UnitStatusName;
                    list.Add(unitInfo);
                });
            }
            //PagingInfo pagingInfo = new PagingInfo();
            pagingInfo.TotalCount = list.Count;
            pagingInfo.PageIndex = page;
            list = list.Skip(pagingInfo.SkipNum).Take(pagingInfo.TakeNum).ToList();
            return list;
        }

        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitNumber"></param>
        /// <returns></returns>
        public bool IsExists(int projectId, string unitNumber)
        {
            if (projectId <= 0)
                return false;

            if (string.IsNullOrEmpty(unitNumber) || unitNumber.Length == 0)
                return false;

            var query = from pu in Context.Project_Unit
                        where pu.ProjectID == projectId && pu.Status == 1 && pu.UnitNumber == unitNumber
                        select pu.UnitNumber;

            return query.Any();
        }

        /// <summary>
        /// 获取铺位公共信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitNumber"></param>
        /// <returns></returns>
        public ProjectUnitCommonInfo GetCommonInfo(int projectId, string unitNumber)
        {
            var commonInfo = new ProjectUnitCommonInfo();
            var entity = (from pu in Context.View_Project_Unit
                          where pu.ProjectID == projectId && pu.Status == 1 && pu.UnitNumber == unitNumber
                          select new
                          {
                              pu.UnitID,
                              pu.UnitNumber,
                              pu.UnitAera,
                              pu.UnitStatus,
                              pu.BuildingName,
                              pu.FloorName
                          }).FirstOrDefault();
            if (entity != null)
            {
                commonInfo.UnitId = entity.UnitID;
                commonInfo.UnitNumber = entity.UnitNumber;
                commonInfo.UnitArea = entity.UnitAera;
                commonInfo.UnitStatus = entity.UnitStatus;
                commonInfo.BuildingName = entity.BuildingName;
                commonInfo.FloorName = entity.FloorName;
            }
            return commonInfo;
        }
    }
}
