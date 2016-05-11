using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using NHH.Framework.Utility;
using System.Data.SqlClient;
namespace NhhRepairService
{
    /// <summary>
    /// 任务
    /// </summary>
    public class OvertimeTask : ITask
    {
        protected string TaskName = "RepairTask";
        protected DateTime estimatedFinishTime = DateTime.Now;

        /// <summary>
        /// 发送消息到智能平台
        /// </summary>
        public void SendMessage()
        {
            var messages = GetMessage();

            if (messages == null || messages.Count == 0)
                return;

            messages.ForEach(message =>
            {
                try
                {
                    ServiceLog.Log("OverTime Task", "开始发送消息到智能平台...", "");
                    ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    ServiceLog.Log(TaskName, ex.StackTrace, "Exception");
                }
            });

        }

        /// <summary>
        /// 处理需要发送的消息
        /// </summary>
        /// <param name="message"></param>
        public void ProcessMessage(RepairInfo info)
        {
            int interval = 30;
            int.TryParse(ConfigurationManager.AppSettings["Interval"].ToString(), out interval);
            var governor = GetGovernorUserInfo(info.ProjectId);//获取工程主管信息
            var msg = new UserMessage();
            msg.SourceType = 6;
            msg.SourceRefId = info.RepairId;

            if (governor == null)
                return;

            ///待处理维修单据
            if (info.RepairStatus == 1)
            {
                msg.UserId = governor.GovernorUserId;
                msg.Subject = "维修单提报";
                msg.Content = string.Format("你好！维修单据{0}已经提报", info.RepairId);
                var repairLink = string.Format(ConfigurationManager.AppSettings["RepairLink"].ToString(), info.RepairId);
                msg.Content += string.Format("<a href='{0}' class='a-buletxt'>详情请点击此处>></a>", repairLink);
                InsertMessage(msg);

                ServiceLog.Log("OverTime Task", "待处理维修单据处理完成...", "Insert");
                return;
            }

            //处理中的维修单据
            if (info.RepairStatus == 2 && info.EstimatedFinishTime.HasValue)
            {
                //超时（第一次）
                var firstOverTime = info.EstimatedFinishTime.Value <= DateTime.Now && DateTime.Now < info.EstimatedFinishTime.Value.AddMinutes(interval);//第一次超时发一次
                if (firstOverTime)
                {
                    msg.UserId = governor.GovernorUserId;
                    msg.Subject = "维修单超时(第一次)";
                    msg.Content = string.Format("你好！维修单据{0}已经超时，", info.RepairId);
                    var repairLink = string.Format(ConfigurationManager.AppSettings["RepairLink"].ToString(), info.RepairId);
                    msg.Content += string.Format("<a href='{0}' class='a-buletxt'>详情请点击此处>></a>", repairLink);
                    InsertMessage(msg);
                    ServiceLog.Log("OverTime Task", "超时（第一次)处理完成...", "Insert");
                    return;
                }

                var overTimeHour = 5;//超时6个小时
                //超时（6小时）
                var overTime6Hours = info.EstimatedFinishTime.Value.AddMinutes(overTimeHour) <= DateTime.Now && DateTime.Now < info.EstimatedFinishTime.Value.AddMinutes(overTimeHour).AddMinutes(interval);//超时6个小时发一次
                if (overTime6Hours)
                {
                    //发给工程主管
                    msg.UserId = governor.GovernorUserId;
                    msg.Subject = "维修单超时(超时6小时)";
                    msg.Content = string.Format("你好！维修单据{0}已经超时6小时，", info.RepairId);
                    var repairLink = string.Format(ConfigurationManager.AppSettings["RepairLink"].ToString(), info.RepairId);
                    msg.Content += string.Format("<a href='{0}' class='a-buletxt'>详情请点击此处>></a>", repairLink);
                    InsertMessage(msg);
                    ServiceLog.Log("OverTime Task", "超时（6小时）,发给工程主管完成...", "Insert");

                    //发给工程经理
                    var manager = GetManagerUserInfo(governor.GovernorUserId);//获取工程主管的上级，即工程经理
                    if (manager != null)
                    {
                        msg.UserId = manager.ManagerUserId;
                        InsertMessage(msg);
                        ServiceLog.Log("OverTime Task", "超时（6小时）,发给工程经理完成...", "Insert");
                    }
                    return;
                }

                //超时（12小时）或者超时12小时后，每12小时
                overTimeHour = 10;//超时12个小时
                var multiple = new TimeSpan(DateTime.Now.Ticks - info.EstimatedFinishTime.Value.Ticks).TotalMinutes / overTimeHour;//超12小时倍数
                var addHours = overTimeHour * Math.Floor(multiple);//小时增加数，时间节点
                var overTimePer12Hours = info.EstimatedFinishTime.Value.AddMinutes(addHours) <= DateTime.Now && DateTime.Now < info.EstimatedFinishTime.Value.AddMinutes(addHours).AddMinutes(interval);//超时12个小时,并且超过12小时候每12小时发一次
                overTimePer12Hours = overTimePer12Hours && addHours >= overTimeHour;//去掉第一次超时的时间点
                if (overTimePer12Hours)
                {
                    //发给工程主管
                    msg.UserId = governor.GovernorUserId;
                    msg.Subject = string.Format("维修单超时(超时{0}小时)", addHours);
                    msg.Content = string.Format("你好！维修单据{0}已经超时{1}小时，", info.RepairId, addHours);
                    var repairLink = string.Format(ConfigurationManager.AppSettings["RepairLink"].ToString(), info.RepairId);
                    msg.Content += string.Format("<a href='{0}' class='a-buletxt'>详情请点击此处>></a>", repairLink);
                    InsertMessage(msg);
                    ServiceLog.Log("OverTime Task", "超时（12小时）或者超时12小时后，每12小时,发给工程主管完成...", "Insert");
                    //发给工程经理
                    var manager = GetManagerUserInfo(governor.GovernorUserId);//获取工程主管的上级，即工程经理
                    if (manager != null)
                    {
                        msg.UserId = manager.ManagerUserId;
                        InsertMessage(msg);
                        ServiceLog.Log("OverTime Task", "超时（12小时）或者超时12小时后，每12小时,发给工程经理完成...", "Insert");

                        //发给项目总经理
                        var projectManager = GetManagerUserInfo(manager.ManagerUserId);
                        if (projectManager != null)
                        {
                            msg.UserId = projectManager.ManagerUserId;
                            InsertMessage(msg);
                            ServiceLog.Log("OverTime Task", "超时（12小时）或者超时12小时后，每12小时,发给项目总经理完成...", "Insert");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有需要发送的消息的维修单
        /// </summary>
        /// <returns></returns>
        protected List<RepairInfo> GetMessage()
        {
            var strCmd = string.Format(@"SELECT 
                                         r.RepairID,
                                         r.ProjectID,
                                         r.RequestTime,
                                         r.EstimatedFinishTime,
                                         r.RepairStatus
                                         FROM [dbo].[Repair](Nolock) r
                                         WHERE r.RepairStatus=1 OR r.RepairStatus=2");
            var repair = SqlHelper.ExecuteDataTable(strCmd);

            if (repair == null || repair.Rows.Count == 0)
            {
                return null;
            }

            var repairList = new List<RepairInfo>();
            foreach (DataRow row in repair.Rows)
            {
                var info = new RepairInfo();
                info.RepairId = (int)row["RepairID"];
                info.ProjectId = (int)row["ProjectID"];
                info.RequestTime = (DateTime)row["RequestTime"];
                DateTime.TryParse(row["EstimatedFinishTime"].ToString(), out estimatedFinishTime);
                info.EstimatedFinishTime = estimatedFinishTime;
                info.RepairStatus = (int)row["RepairStatus"];
                repairList.Add(info);
            }

            ServiceLog.Log("OverTime Task", "获取所有需要发送的消息的维修单...", "Select");
            return repairList;
        }

        /// <summary>
        /// 获取工程主管系统用户信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public UserInfoEntity GetGovernorUserInfo(int projectId)
        {
            var strCmd = string.Format(@"SELECT  TOP 1
                                         vpb.GovernorUserId,
		                                 vpb.ManagerUserId	 
	                                     FROM [dbo].[View_Repair_Complaint_Supervisor](Nolock) vpb
	                                     WHERE vpb.ProjectID={0} AND vpb.BizConfigType={1}", projectId, ConfigurationManager.AppSettings["BizConfigType"]);
            var governor = SqlHelper.ExecuteDataTable(strCmd);
            if (governor.Rows.Count == 0 || governor == null) return null;

            var governorUserId = governor.Rows[0]["GovernorUserId"];//负责人
            var managerUserId = governor.Rows[0]["ManagerUserId"];//部门经理
            var userInfo = new UserInfoEntity();
            userInfo.GovernorUserId = governorUserId.ToString() != "" ? (int)governorUserId : (int)managerUserId;

            ServiceLog.Log("OverTime Task", "获取工程主管系统用户信息...", "Select");
            return userInfo;
        }

        /// <summary>
        /// 获取工程经理或者项目经理系统用户信息
        /// </summary>
        /// <param name="governorUserId"></param>
        /// <returns></returns>
        public UserInfoEntity GetManagerUserInfo(int userId)
        {
            var strCmd = string.Format(@"SELECT TOP 1
                                         ssu.UserID AS ManagerUserId
                                         FROM  [dbo].[Sys_User](Nolock) su
                                         INNER JOIN [dbo].[Employee] em on su.EmployeeID=em.EmployeeID
                                         INNER JOIN [dbo].[Sys_User] ssu on em.SupervisorID=ssu.EmployeeID
                                         WHERE su.UserID={0}", userId);
            var manager = SqlHelper.ExecuteDataTable(strCmd);
            if (manager.Rows.Count == 0 || manager == null) return null;

            var userInfo = new UserInfoEntity();
            userInfo.ManagerUserId = (int)manager.Rows[0]["ManagerUserId"];

            ServiceLog.Log("OverTime Task", "获取工程经理或者项目经理系统用户信息...", "Select");
            return userInfo;
        }

        /// <summary>
        /// 插入消息到UserMessage表
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int InsertMessage(UserMessage msg)
        {
            var strCmd = string.Format(@"INSERT INTO [dbo].[Sys_User_Message]
                                        ([UserID]
                                        ,[Subject]
                                        ,[Content]
                                        ,[SourceType]
                                        ,[SourceRefID]
                                        ,[Status]
                                        ,[InDate]
                                        ,[InUser]
                                        ,[EditDate]
                                        ,[EditUser])
                                         VALUES
                                        (@UserID
                                        ,@Subject
                                        ,@Content
                                        ,@SourceType
                                        ,@SourceRefID
                                        ,@Status
                                        ,@InDate
                                        ,@InUser
                                        ,@EditDate
                                        ,@EditUser)");

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@UserID",msg.UserId),  
                new SqlParameter("@Subject",msg.Subject), 
                new SqlParameter("@Content",msg.Content),
                new SqlParameter("@SourceType",msg.SourceType),
                new SqlParameter("@SourceRefID",msg.SourceRefId),
                new SqlParameter("@Status",msg.Status),
                new SqlParameter("@InDate",msg.InDate),
                new SqlParameter("@InUser",msg.InUser),
                new SqlParameter("@EditDate",msg.EditDate),
                new SqlParameter("@EditUser",msg.EditUser),
            };

            return SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }

    }
}
