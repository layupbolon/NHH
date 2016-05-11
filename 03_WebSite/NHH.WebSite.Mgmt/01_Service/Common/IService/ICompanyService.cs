using NHH.Models.Common;
using NHH.Models.Common.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 公司服务接口
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// 关联公司详情
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        CompanyDetailModel GetCompanyDetail(int companyId);

        ///// <summary>
        ///// 关联公司首页数据
        ///// </summary>
        ///// <returns></returns>
        //List<CompanyListModel> GetCompanyList();

        /// <summary>
        /// 关联公司查询页面
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        CompanyListModel GetCompanyList(CompanyListQueryInfo queryInfo);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ShowModel"></param>
        /// <param name="finance"></param>
        bool AddCompany(CompanyDetailModel ShowModel, FinanceModel finance);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="companyId"></param>
        void DeleteCompany(int companyId);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="DetailModel"></param>
        /// <param name="finance"></param>
        void UpdateCompany(CompanyDetailModel DetailModel, FinanceModel finance);
    }
}
