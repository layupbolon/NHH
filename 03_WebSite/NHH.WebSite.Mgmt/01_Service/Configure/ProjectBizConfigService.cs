using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure
{
    /// <summary>
    /// 项目的业务配置
    /// </summary>
    public class ProjectBizConfigService : NHHService<NHHEntities>, IProjectBizConfigService
    {
        /// <summary>
        /// 获取项目业务配置列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectBizConfigListModel GetBizConfigList(ProjectBizConfigListQueryInfo queryInfo)
        {
            var model = new ProjectBizConfigListModel();
            model.QueryInfo = queryInfo;
            model.ConfigList = new List<ProjectBizConfigInfo>();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };
            var query = from c in Context.Project_BizConfig
                        join p in Context.Project on c.ProjectID equals p.ProjectID
                        join d in Context.Dictionary on c.BizConfigType equals d.FieldValue
                        where d.FieldType == "BizConfigType"
                        select new
                        {
                            c.BizConfigID,
                            c.BizConfigType,
                            c.BuildingID,
                            c.Param1,
                            c.Param2,
                            c.Param3,
                            c.Param4,
                            c.Param5,
                            c.Remark,
                            c.ProjectID,
                            p.ProjectName,
                            BizConfigTypeName = d.FieldName,
                            BuildingName = (from b in Context.Project_Building
                                            where b.BuildingID == c.BuildingID
                                            select b.BuildingName).FirstOrDefault()
                        };

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(item => item.BizConfigType).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new ProjectBizConfigInfo
                    {
                        BizConfigID = entity.BizConfigID,
                        BizConfigType = entity.BizConfigType,
                        BizConfigTypeName = entity.BizConfigTypeName,
                        Param1 = entity.Param1,
                        Param2 = entity.Param2,
                        Param3 = entity.Param3,
                        Param4 = entity.Param4,
                        Param5 = entity.Param5,
                        ProjectID = entity.ProjectID,
                        ProjectName = entity.ProjectName,
                        Remark = entity.Remark,
                        BuildingID = entity.BuildingID,
                        BuildingName = entity.BuildingName,
                    };
                    model.ConfigList.Add(info);
                });
            }
            return model;
        }

        /// <summary>
        /// 获取项目业务配置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectBizConfigInfo GetBizConfig(int type, int projectId)
        {
            var info = new ProjectBizConfigInfo();
            var query = from pbc in Context.Project_BizConfig
                        where pbc.BizConfigType == type && pbc.ProjectID == projectId && !pbc.BuildingID.HasValue
                        select pbc;
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                info.BizConfigID = entity.BizConfigID;
                info.BizConfigType = entity.BizConfigType;
                info.ProjectID = entity.ProjectID;
                info.BuildingID = entity.BuildingID;
                info.Param1 = entity.Param1;
                info.Param2 = entity.Param2;
                info.Param3 = entity.Param3;
                info.Param4 = entity.Param4;
                info.Param5 = entity.Param5;
                info.Remark = entity.Remark;
            }
            return info;
        }

        /// <summary>
        /// 获取项目业务配置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public ProjectBizConfigInfo GetBizConfig(int type, int projectId, int buildingId)
        {
            var info = new ProjectBizConfigInfo();
            var query = from pbc in Context.Project_BizConfig
                        where pbc.BizConfigType == type && pbc.ProjectID == projectId && pbc.BuildingID == buildingId
                        select pbc;
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                info.BizConfigID = entity.BizConfigID;
                info.BizConfigType = entity.BizConfigType;
                info.ProjectID = entity.ProjectID;
                info.BuildingID = entity.BuildingID;
                info.Param1 = entity.Param1;
                info.Param2 = entity.Param2;
                info.Param3 = entity.Param3;
                info.Param4 = entity.Param4;
                info.Param5 = entity.Param5;
                info.Remark = entity.Remark;
            }
            return info;
        }

        /// <summary>
        /// 根据当前系统用户获取项目业务配置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ProjectBizConfigInfo GetBizConfig(int type, UserInfo userInfo)
        {
            var info = new ProjectBizConfigInfo();
            var query = from su in Context.Sys_User
                        join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                        join pbc in Context.Project_BizConfig on em.CompanyID.ToString() equals pbc.Param1
                        where pbc.BizConfigType == type && su.UserID == userInfo.UserId
                        select pbc;

            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                info.BizConfigID = entity.BizConfigID;
                info.BizConfigType = entity.BizConfigType;
                info.ProjectID = entity.ProjectID;
                info.BuildingID = entity.BuildingID;
                info.Param1 = entity.Param1;
                info.Param2 = entity.Param2;
                info.Param3 = entity.Param3;
                info.Param4 = entity.Param4;
                info.Param5 = entity.Param5;
                info.Remark = entity.Remark;
            }
            return info;
        }

        /// <summary>
        /// 获取当前系统用户所在公司所管的所有项目
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public List<int> GetProjectList(int type, UserInfo userInfo)
        {
            var list = new List<int>();
            var query = from su in Context.Sys_User
                        join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                        join pbc in Context.Project_BizConfig on em.CompanyID.ToString() equals pbc.Param1
                        where pbc.BizConfigType == type && su.UserID == userInfo.UserId
                        select pbc;

            var entityList = query.ToList();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    list.Add(entity.ProjectID);
                }
            }
            return list;
        }
    }
}
