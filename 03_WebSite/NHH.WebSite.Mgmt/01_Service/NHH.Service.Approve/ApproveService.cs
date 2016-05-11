using System.Collections.Generic;
using System.Linq;
using System.Security;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Approve;
using NHH.Service.Approve.IService;

namespace NHH.Service.Approve
{
    public class ApproveService : NHHService<NHHEntities>, IApproveService
    {
        /// <summary>
        /// 添加审批数据
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        public bool UpdateApprove(ApproveModel approve)
        {
            var query = from ap in Context.Approve_Process
                        join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                        where
                            ap.RefID == approve.RefID && ap.GroupNum == approve.GroupNum && ac.ConfigType == approve.ConfigType
                        select new
                        {
                            ap.ProcessID,
                            ap.Approver,
                            ap.ApproveTime,
                            ap.Reason,
                            ap.Result
                        };

            var processID = query.Where(a => !a.Approver.HasValue).Select(a => a.ProcessID).Min();

            var instance = CreateBizObject<Approve_Process>();
            var entity = instance.TopOne(a => a.ProcessID == processID);
            if (entity != null)
            {
                string notPassReason = string.Empty;
                if (approve.Result == 2)
                {
                    var checkOptions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckOptions>>(approve.CheckOptions);
                    var result = (from c in checkOptions
                                  join d in Context.Dictionary.Where(a => a.FieldType == "CheckOptions") on c.Option equals
                                      d.FieldValue
                                  where c.Required == 1 && c.Checked == 0
                                  select "【" + d.FieldName + "】").ToList();
                    if (result.Any())
                    {
                        notPassReason = string.Join(",", result) + "  检查不通过";
                    }
                }

                entity.Approver = approve.Approver;
                entity.ApproveTime = approve.ApproveTime;
                entity.Reason = approve.Result == 2
                    ? string.Format("{0}，{1}", notPassReason, approve.Reason)
                    : approve.Reason;
                entity.Result = approve.Result;
                entity.CheckOptions = approve.CheckOptions;

                instance.Update(entity);
                SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断流程是否完全通过
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        public bool IsApprovePass(ApproveModel approve)
        {
            var query = from ac in Context.Approve_Config
                        join ap in Context.Approve_Process on ac.ConfigID equals ap.ConfigID into queryResult
                        from ap in queryResult.DefaultIfEmpty()
                        where
                            ac.ConfigType == approve.ConfigType && ap.RefID == approve.RefID && ap.GroupNum == approve.GroupNum
                        select new
                        {
                            ac.ConfigID,
                            ac.Step,
                            ap.ProcessID,
                            ap.Approver,
                            ap.Result
                        };

            return query.Any(q => q.Step == query.Max(a => a.Step) && q.Approver.HasValue && q.Result == 1);
        }

        /// <summary>
        /// 获取单据审核记录
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        public List<ApproveRecordInfo> GetDocumentApproveProcess(int refID, int configType)
        {
            var query = from c in Context.Approve_Config
                        join p in Context.Approve_Process on c.ConfigID equals p.ConfigID into queryResult
                        from p in queryResult.DefaultIfEmpty()
                        join u in Context.Sys_User on p.Approver equals u.UserID into queryResult2
                        from u in queryResult2.DefaultIfEmpty()
                        join r in Context.Sys_Role on c.RoleID equals r.RoleID into queryResult3
                        from r in queryResult3.DefaultIfEmpty()
                        where p.RefID == refID && c.ConfigType == configType
                        select new
                        {
                            p.ProcessID,
                            p.ConfigID,
                            p.GroupNum,
                            c.ConfigType,
                            p.RefID,
                            p.Approver,
                            ApproverName = u.UserName,
                            r.RoleName,
                            ShowDeptName = c.ShowDepartmentName,
                            p.Reason,
                            p.ApproveTime,
                            p.Result,
                            p.CheckOptions
                        };

            if (!query.Any())
                return new List<ApproveRecordInfo>();

            var maxGroupNum = query.Max(a => a.GroupNum);

            var model = new List<ApproveRecordInfo>();

            query.ToList().ForEach(info =>
            {
                if (info.Approver.HasValue || info.GroupNum == maxGroupNum)
                {
                    List<string> result = new List<string>();
                    if (!string.IsNullOrEmpty(info.CheckOptions))
                    {
                        var checkOptions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckOptions>>(info.CheckOptions);
                        result = (from c in checkOptions
                                  join d in Context.Dictionary.Where(a => a.FieldType == "CheckOptions") on c.Option equals
                                      d.FieldValue
                                  where c.Checked == 1
                                  select "【" + d.FieldName + "】").ToList();
                    }

                    model.Add(new ApproveRecordInfo()
                    {
                        ProcessID = info.ProcessID,
                        ConfigID = info.ConfigID,
                        GroupNum = info.GroupNum,
                        ConfigType = info.ConfigType,
                        RefID = info.RefID,
                        Approver = info.Approver,
                        ApproverName = info.ApproverName,
                        RoleName = info.RoleName,
                        ShowDeptName = info.ShowDeptName,
                        Reason = info.Reason,
                        ApproveTime = info.ApproveTime,
                        Result = info.Result,
                        CheckOptions = result.Any()? string.Join(",",result):string.Empty
                    });
                }
            });

            return model;
        }

        /// <summary>
        /// 获取当前审核人需要勾选的项
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        public List<CheckOptions> GetCheckOptions(int refID, int configType)
        {
            var query = from c in Context.Approve_Config
                        join p in Context.Approve_Process on c.ConfigID equals p.ConfigID into queryResult
                        from p in queryResult.DefaultIfEmpty()
                        where c.ConfigType == configType && p.RefID == refID
                        select new
                        {
                            p.ConfigID,
                            p.GroupNum,
                            p.Approver,
                            c.CheckOptions
                        };

            if (!query.Any())
                return new List<CheckOptions>();

            var maxGroup = query.Max(a => a.GroupNum);

            if(!query.Any(a => a.GroupNum == maxGroup && !a.Approver.HasValue))
                return new List<CheckOptions>();

            var configID = query.Where(a => a.GroupNum == maxGroup && !a.Approver.HasValue).Min(a => a.ConfigID);
            var result = query.Where(a => a.ConfigID == configID && a.GroupNum == maxGroup).Select(a => a.CheckOptions).FirstOrDefault();

            if (string.IsNullOrEmpty(result))
                return new List<CheckOptions>();

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckOptions>>(result);
            if (model == null || !model.Any())
                return new List<CheckOptions>();

            var checkOptions = from m in model
                join d in Context.Dictionary on m.Option equals d.FieldValue
                where d.FieldType == "CheckOptions"
                select new CheckOptions
                {
                    Option = m.Option,
                    OptionName = d.FieldName,
                    Required = m.Required,
                    Checked = m.Checked
                };

            return checkOptions.ToList();
        }
    }
}
