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
    public class SysFunctionService : NHHService<NHHEntities>, ISysFunctionService
    {
        /// <summary>
        /// 功能模块列表
        /// </summary>
        /// <returns></returns>
        public List<SysFunctionInfo> GetFunctionList(string keyword)
        {
            var functionList = new List<SysFunctionInfo>();
            var query = from r in Context.Sys_Function
                        where r.FunctionKey.Contains(keyword) || r.FunctionName.Contains(keyword) && r.Status == 1
                        select new
                        {
                            r.FunctionID,
                            r.FunctionKey,
                            r.FunctionName
                        };
            var queryList = query.ToList();
            if (queryList != null)
            {
                queryList.ForEach(item =>
                   {
                       var function = new SysFunctionInfo()
                       {
                           FunctionId = item.FunctionID,
                           FunctionKey = item.FunctionKey,
                           FunctionName = item.FunctionName
                       };
                       functionList.Add(function);
                   });
            }
            return functionList;
        }

        /// <summary>
        /// 根据查询条件获取列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public SysFunctionListModel GetFunctionList(SysFunctionListQueryInfo queryInfo)
        {
            var model = new SysFunctionListModel();
            model.PagingInfo = new PagingInfo()
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1

            };
            model.FunctionList = new List<SysFunctionInfo>();
            model.QueryInfo = queryInfo;

            var query = from sf in Context.Sys_Function
                        where sf.Status == 1
                        select new
                        {
                            sf.FunctionID,
                            sf.FunctionKey,
                            sf.FunctionName,
                            sf.FunctionDescription
                        };
            if (!string.IsNullOrEmpty(queryInfo.FunctionName))
            {
                query = query.Where(m => m.FunctionName.Contains(queryInfo.FunctionName));
            }

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(m => m.FunctionID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null)
            {
                entityList.ForEach(item =>
                {
                    var info = new SysFunctionInfo();
                    info.FunctionId = item.FunctionID;
                    info.FunctionKey = item.FunctionKey;
                    info.FunctionName = item.FunctionName;
                    info.FunctionDescription = item.FunctionDescription;
                    model.FunctionList.Add(info);
                });
            }
            return model;
        }

        /// <summary>
        /// 新增功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddFunction(SysFunctionDetailModel model)
        {
            var instance = CreateBizObject<Sys_Function>();
            var entity = new Sys_Function();

            entity.FunctionKey = model.FunctionKey;
            entity.FunctionName = model.FunctionName;
            entity.FunctionDescription = model.FunctionDescription;
            entity.Status = 1;
            entity.InDate = DateTime.Now;
            entity.InUser = model.UserInfo.UserId;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;

            instance.Insert(entity);
            this.SaveChanges();

            return true;

        }

        /// <summary>
        /// 更新功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFunction(SysFunctionDetailModel model)
        {
            var instance = CreateBizObject<Sys_Function>();
            var entity = instance.GetBySysNo(model.FunctionId);

            if (entity == null)
                return false;

            entity.FunctionKey = model.FunctionKey;
            entity.FunctionName = model.FunctionName;
            entity.FunctionDescription = model.FunctionDescription;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;

            instance.Update(entity);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获取功能模块详情
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public SysFunctionDetailModel GetFunctionDetail(int functionId)
        {
            var model = new SysFunctionDetailModel();

            var query = from sf in Context.Sys_Function
                        where sf.FunctionID == functionId
                        select new
                        {
                            sf.FunctionID,
                            sf.FunctionKey,
                            sf.FunctionName,
                            sf.FunctionDescription
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.FunctionId = entity.FunctionID;
                model.FunctionKey = entity.FunctionKey;
                model.FunctionName = entity.FunctionName;
                model.FunctionDescription = entity.FunctionDescription;
            }

            return model;
        }

    }
}
