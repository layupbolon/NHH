using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.BizLogic;

namespace NHH.BizLogic.Common
{
    /// <summary>
    /// 公司信息业务操作类
    /// </summary>
    public class BizCompany : BizObject<NHHEntities, Company>
    {
        /// <summary>
        /// 获取公司信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "CompanyID";
            }
        }

        /// <summary>
        /// 根据公司类型获取公司信息
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public List<Company> GetCompany(int CompanyType)
        {
            List<Company> list = new List<Company>();
            list = base.GetAllList(p => p.CompanyType == CompanyType);
            return list;
        }
    }
}
