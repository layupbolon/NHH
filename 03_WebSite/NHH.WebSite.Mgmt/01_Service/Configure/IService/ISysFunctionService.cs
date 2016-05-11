using NHH.Models.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure.IService
{
    public interface ISysFunctionService
    {
        /// <summary>
        /// 功能模块列表
        /// </summary>
        /// <returns></returns>
        List<SysFunctionInfo> GetFunctionList(string keyword);

        /// <summary>
        /// 根据查询条件获取列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        SysFunctionListModel GetFunctionList(SysFunctionListQueryInfo queryInfo);

        /// <summary>
        /// 新增功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddFunction(SysFunctionDetailModel model);

        /// <summary>
        /// 更新功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateFunction(SysFunctionDetailModel model);

        /// <summary>
        /// 获取功能模块详情
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        SysFunctionDetailModel GetFunctionDetail(int functionId);
    }
}
