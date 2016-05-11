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
            Expression<Func<NHH.Entities.Project, bool>> where = project => project.Status == 1;
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
        /// 获取项目列表
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public List<ProjectCommonInfo> GetProjectList(string projectName)
        {
            var list = new List<ProjectCommonInfo>();
            Expression<Func<NHH.Entities.Project, bool>> where = project => project.ProjectName.StartsWith(projectName) && project.Status == 1;
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

            var query = from b in Context.Project_Building
                        where b.ProjectID == projectId && b.Status == 1
                        select new
                        {
                            b.ProjectID,
                            b.BuildingID,
                            b.BuildingName,
                        };

            var entityList = query.ToList();

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

            var query = from f in Context.Project_Floor
                        join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                        where f.ProjectID == projectId && f.Status == 1
                        select new
                        {
                            f.FloorID,
                            f.FloorNumber,
                            f.FloorMapFileName,
                            b.BuildingID,
                            b.BuildingName,
                        };

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
                    FloorNumber = entity.FloorNumber,
                    FloorMapFileName = entity.FloorMapFileName,
                });
            });

            return list;
        }
    }
}
