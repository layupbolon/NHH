using NHH.Entities;
using NHH.Framework.Service;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 报表定制信息
    /// </summary>
    public class ReportConfigService : NHHService<NHHEntities>, IReportConfigService
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reportCode"></param>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        public MessageBag<bool> Save(int userId, string reportCode, string fieldList)
        {
            var result = new MessageBag<bool>();
            result.Code = -1;

            var reportId = (from r in Context.Report
                            where r.ReportCode == reportCode
                            select r.ReportID).FirstOrDefault();

            var bll = CreateBizObject<Report_Config>();

            var config = bll.TopOne(item => item.UserID == userId && item.ReportID == reportId);

            if (config != null)
            {
                config.FieldList = fieldList;
                config.EditDate = DateTime.Now;
                config.EditUser = userId;
                bll.Update(config);
            }
            else
            {
                config = new Report_Config
                {
                    UserID = userId,
                    ReportID = reportId,
                    FieldList = fieldList,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = userId,
                };
                bll.Insert(config);
            }

            SaveChanges();
            result.Desc = "更新成功";
            result.Code = 0;
            return result;
        }
    }
}
