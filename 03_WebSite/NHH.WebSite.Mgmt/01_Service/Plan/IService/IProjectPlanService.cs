using NHH.Models.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Plan.IService
{
    /// <summary>
    /// 招商计划服务接口
    /// </summary>
    public interface IProjectPlanService
    {
        /// <summary>
        /// 获取招商计划列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectPlanListModel GetProjectPlanList(ProjectPlanListQueryInfo queryInfo);

        /// <summary>
        /// 获取筹划铺位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectPlanUnitInfo GetUnitInfo(int unitId);
        
        /// <summary>
        /// 获取商铺招商租赁规划
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectUnitPlanModel GetUnitPlan(int unitId);

        /// <summary>
        /// 获取商铺招商租赁信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectUnitInfoModel GetRentInfo(int unitId);

        /// <summary>
        /// 获取商铺的招商信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectUnitLeasingModel GetUnitLeasing(int unitId);

        /// <summary>
        /// 获取商铺的交付标准
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectUnitSpecModel GetUnitSpec(int unitId);

        /// <summary>
        /// 获取商铺的商户责任
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        ProjectStoreSpecModel GetStoreSpec(int unitId);

        /// <summary>
        /// 保存商铺的招商租凭规划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveUnitPlan(ProjectUnitPlanModel model);

        /// <summary>
        /// 保存商铺的招商租赁信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveRentInfo(ProjectUnitInfoModel model);

        /// <summary>
        /// 保存商铺招商信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveUnitLeasing(ProjectUnitLeasingModel model);

        /// <summary>
        /// 保存商铺交付标准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveUnitSpec(ProjectUnitSpecModel model);

        /// <summary>
        /// 保存商铺商户责任
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveStoreSpec(ProjectStoreSpecModel model);

        /// <summary>
        /// 获取交付标准模板列表
        /// </summary>
        /// <returns></returns>
        List<ProjectUnitSpecTemplateModel> GetProjectUnitSpecTemplateList();

        /// <summary>
        /// 获取交付标准模板信息
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        ProjectUnitSpecTemplateModel GetProjectUnitSpecTemplateInfo(int templateId);

    }
}
