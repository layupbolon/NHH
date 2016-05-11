using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Service.Common.IService;

namespace NHH.Service.Common
{
    public class WorkflowService : NHHService<NHHEntities>, IWorkflowService
    {
        /// <summary>
        /// 获取审核工作流流程列表
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="groupNum">如果GroupNum为0则取所有Group的</param>
        /// <returns></returns>
        public List<ApproveProcess> GetApproveProcessList(int configType, int groupNum)
        {
            List<ApproveProcess> result = new List<ApproveProcess>();
            var query = from ap in Context.Approve_Process
                join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                where ac.ConfigType == configType
                select new
                {
                    ap.ProcessID,
                    ap.ConfigID,
                    ap.GroupNum,
                    ap.RefID,
                    ap.Approver,
                    ap.ApproveTime,
                    ap.Reason,
                    ap.Result,
                    ac.ConfigType,
                    ac.Step,
                    ac.ShowDepartmentName,
                    ac.RoleID
                };
            if (groupNum > 0)
            {
                query = query.Where(item => item.GroupNum == groupNum);
            }
            query = query.OrderBy(item => item.GroupNum).ThenBy(item => item.Step);

            var entityList = query.ToList();

            int maxGroupNum = 0;
            if (entityList.Any())
            {
                maxGroupNum = (entityList.Select(e => e.GroupNum)).Max();
            }

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    if (entity.GroupNum == maxGroupNum || entity.Approver != null)
                    {
                        var contract = new ApproveProcess();
                        contract.ProcessID = entity.ProcessID;
                        contract.ConfigID = entity.ConfigID;
                        contract.GroupNum = entity.GroupNum;
                        contract.RefID = entity.RefID;
                        contract.ApproveTime = entity.ApproveTime;
                        contract.Approver = entity.Approver;
                        contract.Reason = entity.Reason;
                        contract.Result = entity.Result;
                        contract.ConfigType = entity.ConfigType;
                        contract.Step = entity.Step;
                        contract.ShowDepartmentName = entity.ShowDepartmentName;
                        result.Add(contract);
                    }
                });
            }

            return result;
        }
        /// <summary>
        /// 初始化工作流审批流程
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public bool InitProcess(int configType,int refId)
        {
            //取当前相关流程的最大的组号+1，没取到则为1
            int groupNum = (from ap in Context.Approve_Process
                join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                where ac.ConfigType == configType && ap.RefID == refId
                orderby ap.GroupNum descending 
                select ap.GroupNum).FirstOrDefault() + 1;

            var configList =
                (from ac in Context.Approve_Config where ac.ConfigType == configType orderby ac.Step ascending select ac)
                    .ToList();
            if (configList != null)
            {
                int i = 0;
                configList.ForEach(entity =>
                {
                    var processEntity = new ApproveProcess();
                    processEntity.ConfigID = entity.ConfigID;
                    processEntity.GroupNum = groupNum;
                    processEntity.RefID = refId;
                    if (InsertApproveProcess(processEntity))
                        i++;
                });
                if (i > 0) return true;
            }
            return false;
        }

        /// <summary>
        /// 重新初始化工作流审批流程
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="refId"></param>
        /// <remarks>也就是Approve_Process表中重新创建一套，原有GroupNum+1</remarks>
        public void ResetProcess(int configType, int refId)
        {
            //判断当前流程最大组号Step为1的记录是否有审批过，如果有则需要InitProcess，没有则用原来的流程
            int? approver = (from ap in Context.Approve_Process
                join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                where ac.ConfigType == configType && ap.RefID == refId && ac.Step == 1
                orderby ap.GroupNum descending
                select ap.Approver).FirstOrDefault();
            if (approver.HasValue)  //如果有值，需要初始化新的一整套流程
                InitProcess(configType, refId);
        }
        /// <summary>
        /// 获取工作流当前已审核人列表
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="refId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<CommonSysUserInfo> GetCurrentApprovers(int configType, int refId, int companyId)
        {
            List<CommonSysUserInfo> result = new List<CommonSysUserInfo>();
            var ApproveEntityList = (from ap in Context.Approve_Process
                                     join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                                     where ac.ConfigType == configType && ap.RefID == refId
                                     select new
                                     {
                                         ap.Approver,
                                         ac.Step,
                                         ap.GroupNum,
                                         ac.RoleID
                                     }).ToList();
            int maxGroupNum = 0;
            if (ApproveEntityList.Any())
            {
                maxGroupNum = (ApproveEntityList.Select(e => e.GroupNum)).Max();
            }
            int roleID =
                ApproveEntityList.Where(item => item.GroupNum == maxGroupNum && item.Approver != null)
                    .OrderBy(item => item.Step)
                    .Select(item => item.RoleID)
                    .FirstOrDefault();
            var query = from ur in Context.Sys_User_Role
                        join su in Context.Sys_User on ur.UserID equals su.UserID
                        join ep in Context.Employee on su.EmployeeID equals ep.EmployeeID
                        join cp in Context.Company on ep.CompanyID equals cp.CompanyID
                        join dp in Context.Department on ep.DepartmentID equals dp.DepartmentID
                        where ep.CompanyID == companyId && ur.RoleID == roleID
                        select new
                        {
                            su.UserID,
                            su.UserName,
                            ep.EmployeeName,
                            ep.Title,
                            ep.Email,
                            ep.Moblie,
                            ep.CompanyID,
                            cp.CompanyName,
                            ep.DepartmentID,
                            dp.DepartmentName
                        };
            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new CommonSysUserInfo();
                    contract.UserID = entity.UserID;
                    contract.UserName = entity.UserName;
                    contract.EmployeeName = entity.EmployeeName;
                    contract.Title = entity.Title;
                    contract.Email = entity.Email;
                    contract.Mobile = entity.Moblie;
                    contract.CompanyID = entity.CompanyID;
                    contract.CompanyName = entity.CompanyName;
                    contract.DepartmentID = entity.DepartmentID;
                    contract.DepartmentName = entity.DepartmentName;
                    result.Add(contract);
                });
            }
            return result;

        }
        /// <summary>
        /// 获取工作流下个待审核人列表
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="refId"></param>
        /// <param name="comanyId"></param>
        /// <returns></returns>
        public List<CommonSysUserInfo> GetNextApprovers(int configType, int refId, int companyId)
        {
            List<CommonSysUserInfo> result = new List<CommonSysUserInfo>();
            var ApproveEntityList = (from ap in Context.Approve_Process
                join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                where ac.ConfigType == configType && ap.RefID == refId
                select new
                {
                    ap.Approver,
                    ac.Step,
                    ap.GroupNum,
                    ac.RoleID
                }).ToList();

            int maxGroupNum = 0;
            if (ApproveEntityList.Any())
            {
                maxGroupNum = (ApproveEntityList.Select(e => e.GroupNum)).Max();
            }

            int roleID =
                ApproveEntityList.Where(item => item.GroupNum == maxGroupNum && item.Approver == null)
                    .OrderBy(item => item.Step)
                    .Select(item => item.RoleID)
                    .FirstOrDefault();

            var query = from ur in Context.Sys_User_Role
                join su in Context.Sys_User on ur.UserID equals su.UserID
                join ep in Context.Employee on su.EmployeeID equals ep.EmployeeID
                join cp in Context.Company on ep.CompanyID equals cp.CompanyID
                join dp in Context.Department on ep.DepartmentID equals dp.DepartmentID
                where ep.CompanyID == companyId && ur.RoleID == roleID
                select new
                {
                    su.UserID,
                    su.UserName,
                    ep.EmployeeName,
                    ep.Title,
                    ep.Email,
                    ep.Moblie,
                    ep.CompanyID,
                    cp.CompanyName,
                    ep.DepartmentID,
                    dp.DepartmentName
                };
            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new CommonSysUserInfo();
                    contract.UserID = entity.UserID;
                    contract.UserName = entity.UserName;
                    contract.EmployeeName = entity.EmployeeName;
                    contract.Title = entity.Title;
                    contract.Email = entity.Email;
                    contract.Mobile = entity.Moblie;
                    contract.CompanyID = entity.CompanyID;
                    contract.CompanyName = entity.CompanyName;
                    contract.DepartmentID = entity.DepartmentID;
                    contract.DepartmentName = entity.DepartmentName;
                    result.Add(contract);
                });
            }
            return result;
        }


        #region private
        /// <summary>
        /// 添加审批记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool InsertApproveProcess(ApproveProcess model)
        {
            var instance = CreateBizObject<Approve_Process>();
            var entity = new Approve_Process()
            {
                ConfigID = model.ConfigID,
                GroupNum = model.GroupNum,
                RefID = model.RefID
            };
            if (model.Approver.HasValue)
                entity.Approver = model.Approver.Value;
            if (model.ApproveTime.HasValue)
                entity.ApproveTime = model.ApproveTime.Value;
            if (string.IsNullOrEmpty(model.Reason))
                entity.Reason = model.Reason;
            if (model.Result.HasValue)
                entity.Result = model.Result;
            instance.Insert(entity);
            this.SaveChanges();
            model.ProcessID = entity.ProcessID;
            return model.ProcessID > 0;
        }
        #endregion private
    }
}
