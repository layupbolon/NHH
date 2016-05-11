using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Plan;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Plan
{
    /// <summary>
    /// 项目招商批次服务
    /// </summary>
    public class ProjectBatchService : NHHService<NHHEntities>, IProjectBatchService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectBatchListModel GetList(ProjectBatchListQueryInfo queryInfo)
        {
            var model = new ProjectBatchListModel();
            model.QueryInfo = queryInfo;
            model.List = new List<ProjectBatchInfo>();
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1,
            };

            var query = from pb in Context.Project_Batch
                        where pb.Status == 1
                        select new
                        {
                            pb.ProjectID,
                            pb.BatchCode,
                            pb.BatchID,
                        };

            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            else
            {
                query = query.Where(item => item.ProjectID == 0);
            }

            model.PagingInfo.TotalCount = query.Count();

            //排序
            query = query.OrderBy(queryInfo.OrderExpression);
            //分页
            if (queryInfo.Paging.HasValue && queryInfo.Paging.Value == 1)
            {
                query = query.Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
            }
            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new ProjectBatchInfo
                    {
                        BatchID = entity.BatchID,
                        BatchCode = entity.BatchCode,
                        ProjectID = entity.ProjectID,
                    };
                    model.List.Add(info);
                });
            }
            return model;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        public MessageBag<bool> Add(ProjectBatchInfo info)
        {
            var result = new MessageBag<bool>();
            result.Code = -1;

            if (string.IsNullOrEmpty(info.BatchCode) || info.BatchCode.Length == 0)
            {
                result.Desc = "请输入批次编号";
                return result;
            }

            var check = from p in Context.Project
                        where p.ProjectID == info.ProjectID
                        select p.ProjectID;

            if (!check.Any())
            {
                result.Desc = "项目不存在";
                return result;
            }

            var entity = new Project_Batch
            {
                ProjectID = info.ProjectID,
                BatchCode = info.BatchCode,
                Status = 1,
                InDate = DateTime.Now,
                InUser = info.InUser,
            };
            var bll = CreateBizObject<Project_Batch>();
            bll.Insert(entity);
            SaveChanges();

            result.Code = 0;
            result.Desc = "新增成功";
            return result;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="info"></param>
        public MessageBag<bool> Edit(ProjectBatchInfo info)
        {
            var result = new MessageBag<bool>();
            result.Code = -1;

            var bll = CreateBizObject<Project_Batch>();

            var entity = bll.TopOne(item => item.BatchID == info.BatchID);

            if (entity == null)
            {
                result.Desc = "批次不存在";
                return result;
            }

            entity.BatchCode = info.BatchCode;

            bll.Update(entity);
            SaveChanges();

            result.Code = 0;
            result.Desc = "编辑成功";
            return result;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public ProjectBatchDetailModel GetDetail(int batchId)
        {
            var model = new ProjectBatchDetailModel();
            var entity = (from pb in Context.Project_Batch
                          join p in Context.Project on pb.ProjectID equals p.ProjectID
                          where pb.BatchID == batchId
                          select new
                          {
                              p.ProjectName,
                              pb.BatchID,
                              pb.BatchCode,
                              pb.ProjectID,
                          }).FirstOrDefault();

            if (entity != null)
            {
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.ProjectName;
                model.BatchID = entity.BatchID;
                model.BatchCode = entity.BatchCode;
            }

            return model;
        }

        /// <summary>
        /// 获取项目招商批次列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<ProjectBatchInfo> GetBatchList(int projectId)
        {
            var list = new List<ProjectBatchInfo>();
            var query = from pb in Context.Project_Batch
                        where pb.Status == 1 && pb.ProjectID == projectId
                        select new
                        {
                            pb.BatchID,
                            pb.BatchCode,
                        };
            if (!query.Any())
            {
                return list;
            }

            var entityList = query.ToList();
            entityList.ForEach(entity =>
            {
                list.Add(new ProjectBatchInfo
                {
                    BatchID = entity.BatchID,
                    BatchCode = entity.BatchCode,
                });
            });
            return list;
        }
    }
}
