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
    /// <summary>10002-001
    /// 项目服务
    /// </summary>
    public class ProjectCommonService : NHHService<NHHEntities>, IProjectCommonService
    {
        /// <summary>
        /// 获取项目基本信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectCommonInfo GetProjectInfo(int projectId)
        {
            var info = new ProjectCommonInfo();
            var entity = CreateBizObject<NHH.Entities.Project>().GetBySysNo(projectId);

            if (entity != null)
            {
                info.Id = entity.ProjectID;
                info.Name = entity.ProjectName;
            }
            return info;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public List<ProjectCommonInfo> GetProjectList()
        {
            var list = new List<ProjectCommonInfo>();

            var query = from p in Context.Project
                        where p.Status == 1
                        select new
                        {
                            p.ProjectID,
                            p.ProjectName,
                        };

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new ProjectCommonInfo
                    {
                        Id = entity.ProjectID,
                        Name = entity.ProjectName,
                    });
                });
            }
            return list;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public List<ProjectCommonInfo> GetProjectList(int userId)
        {
            var list = new List<ProjectCommonInfo>();

            var query = from p in Context.Project
                        where p.Status == 1
                        select new
                        {
                            p.ProjectID,
                            p.ProjectName,
                            p.ManageCompanyID
                        };

            query = query.Where(item => (from u in Context.Sys_User
                                         join e in Context.Employee on u.EmployeeID equals e.EmployeeID
                                         where e.Status == 1 && u.Status == 1 && e.CompanyID == item.ManageCompanyID && u.UserID == userId
                                         select e.EmployeeID).Any());

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new ProjectCommonInfo
                    {
                        Id = entity.ProjectID,
                        Name = entity.ProjectName,
                    });

                });
            }

            return list;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public List<ProjectCommonInfo> GetProjectList(string projectName)
        {
            var list = new List<ProjectCommonInfo>();
            Expression<Func<NHH.Entities.Project, bool>> where = project => project.ProjectName.Contains(projectName) && project.Status == 1;
            var entityList = CreateBizObject<NHH.Entities.Project>().GetAllList(where);

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new ProjectCommonInfo
                    {
                        Id = entity.ProjectID,
                        Name = entity.ProjectName,
                    });

                });
            }
            return list;
        }

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<BuildingCommonInfo> GetBuildingList(int projectId)
        {
            var list = new List<BuildingCommonInfo>();

            var entityList = (from b in Context.Project_Building
                              where b.ProjectID == projectId && b.Status == 1
                              select new
                              {
                                  b.ProjectID,
                                  b.BuildingID,
                                  b.BuildingName,
                              }).ToList();

            if (entityList == null || entityList.Count == 0)
                return list;

            entityList.ForEach(entity =>
            {
                list.Add(new BuildingCommonInfo
                {
                    ProjectId = entity.ProjectID,
                    Id = entity.BuildingID,
                    Name = entity.BuildingName,
                });
            });

            return list;
        }

        /// <summary>
        /// 获取楼宇组
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<BuildingCommonInfo> GetBuildingGroup(int projectId)
        {
            var list = new List<BuildingCommonInfo>();
            var entityList = (from b in Context.Project_Building
                              where b.Status == 1 && b.ProjectID == projectId
                              select new
                              {
                                  b.BuildingID,
                                  b.BuildingName,
                              }).ToList();

            if (entityList == null || entityList.Count == 0)
                return list;

            entityList.ForEach(entity =>
            {
                var info = new BuildingCommonInfo
                {
                    Id = entity.BuildingID,
                    Name = entity.BuildingName,
                    FloorList = new List<FloorCommonInfo>()
                };
                #region 楼层列表
                var floorList = (from f in Context.Project_Floor
                                 where f.Status == 1 && f.BuildingID == entity.BuildingID
                                 select new
                                 {
                                     f.FloorID,
                                     f.FloorName
                                 }).ToList();
                if (floorList != null && floorList.Count > 0)
                {
                    floorList.ForEach(floor =>
                    {
                        info.FloorList.Add(new FloorCommonInfo
                        {
                            FloorId = floor.FloorID,
                            FloorName = floor.FloorName,
                        });
                    });
                }
                #endregion
                list.Add(info);
            });

            return list;
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
            info.Name = entity.BuildingName;

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
                        where f.FloorID == floorId && f.Status == 1
                        select new
                        {
                            f.ProjectID,
                            f.FloorID,
                            f.FloorName,
                            f.FloorNumber,
                            f.FloorMapFileName,
                            b.BuildingID,
                            b.BuildingName,
                        };

            var entity = query.FirstOrDefault();

            if (entity == null)
                return info;

            info.ProjectId = entity.ProjectID;
            info.BuildingId = entity.BuildingID;
            info.BuildingName = entity.BuildingName;
            info.FloorId = entity.FloorID;
            info.FloorName = entity.FloorName;
            info.FloorNumber = entity.FloorNumber;
            info.FloorMapFileName = entity.FloorMapFileName;

            return info;
        }

        /// <summary>
        /// 获取楼宇楼层列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<FloorCommonInfo> GetAllFloor(int projectId)
        {
            var list = new List<FloorCommonInfo>();

            var entityList = (from f in Context.Project_Floor
                              join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                              where f.ProjectID == projectId && f.Status == 1
                              select new
                              {
                                  f.FloorID,
                                  f.FloorName,
                                  f.FloorNumber,
                                  f.FloorMapFileName,
                                  b.BuildingID,
                                  b.BuildingName,
                              }).ToList();

            if (entityList == null || entityList.Count == 0)
                return list;

            entityList.ForEach(entity =>
            {
                list.Add(new FloorCommonInfo
                {
                    BuildingId = entity.BuildingID,
                    BuildingName = entity.BuildingName,
                    FloorId = entity.FloorID,
                    FloorName = string.Format("{0} {1}", entity.BuildingName, entity.FloorName),
                    FloorNumber = entity.FloorNumber,
                    FloorMapFileName = entity.FloorMapFileName,
                });
            });

            return list;
        }

        /// <summary>
        /// 获取楼层列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public List<FloorCommonInfo> GetFloorList(int projectId, int? buildingId)
        {
            var list = new List<FloorCommonInfo>();

            var query = from f in Context.Project_Floor
                        join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                        where f.ProjectID == projectId && f.Status == 1
                        select new
                        {
                            f.FloorID,
                            f.FloorName,
                            f.FloorNumber,
                            f.FloorMapFileName,
                            b.BuildingID,
                            b.BuildingName,
                        };
            if (buildingId.HasValue)
            {
                query = query.Where(item => item.BuildingID == buildingId.Value);
            }

            var entityList = query.ToList();

            if (entityList == null || entityList.Count == 0)
                return list;

            entityList.ForEach(entity =>
            {
                list.Add(new FloorCommonInfo
                {
                    BuildingId = entity.BuildingID,
                    BuildingName = entity.BuildingName,
                    FloorId = entity.FloorID,
                    FloorName = string.Format("{0} {1}", entity.BuildingName, entity.FloorName),
                    FloorNumber = entity.FloorNumber,
                    FloorMapFileName = entity.FloorMapFileName,
                });
            });

            return list;
        }
    }
}
