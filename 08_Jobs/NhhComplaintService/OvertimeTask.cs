using NHH.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhComplaintService
{
    /// <summary>
    /// 任务
    /// </summary>
    public class OvertimeTask : ITask
    {
        protected string TaskName = "CompalintTask";
        protected DateTime estimatedFinishTime;

        /// <summary>
        /// 发送消息到智能管理平台
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
        /// <param name="info"></param>
        public void ProcessMessage(ComplaintInfo info)
        {
            int interval = 30;
            int.TryParse(ConfigurationManager.AppSettings["Interval"].ToString(), out interval);

            var governor = GetGovernorUserInfo(info.ProjectId);//获取工程主管信息
            var msg = new UserMessage();
            msg.SourceType = 7;
            msg.SourceRefId = info.ComplaintId;

            if (governor == null)
                return;

            //待处理投诉单据
            if (info.ComplaintStatus == 1)
            {
                msg.UserId = governor.GovernorUserId;
                msg.Subject = "投诉单提报";
                msg.Content = "你好！投诉单据" + info.ComplaintId + "已经提报，";
                var complaintLink = string.Format(ConfigurationManager.AppSettings["ComplaintLink"].ToString(), info.ComplaintId);
                msg.Content += string.Format("<a href='{0}' class='a-buletxt'>详情请点击此处>></a>", complaintLink);
                InsertMessage(msg);

                ServiceLog.Log("OverTime Task", "待处理投诉单据处理完成...", "Insert");
                return;
            }

            if (info.ComplaintStatus == 2)
            {
                //超时（20小时）或者超时20小时后，每20小时
                var overTimeHour = 10;//超时20个小时设置
                var multiple = new TimeSpan(DateTime.Now.Ticks - info.RequestTime.Ticks).TotalMinutes / overTimeHour;//超20小时倍数
                var addHours = overTimeHour * Math.Floor(multiple);//小时增加数，时间节点
                var overTimePer20Hours = info.RequestTime.AddMinutes(addHours) <= DateTime.Now && DateTime.Now < info.RequestTime.AddMinutes(addHours).AddMinutes(interval);//超时20个小时,并且超过20小时候每20小时发一次
                overTimePer20Hours = overTimePer20Hours && addHours >= overTimeHour;//去掉第一次超时的时间点
                if (overTimePer20Hours)
                {
                    //发给工程主管
                    msg.UserId = governor.GovernorUserId;
                    msg.Subject = "投诉单超时(超时20个小时)";
                    msg.Content = "你好！投诉单据" + info.ComplaintId + "已经超时" + addHours + "小时，";
                    var complaintLink = string.Format(ConfigurationManager.AppSettings["ComplaintLink"].ToString(), info.ComplaintId);
                    msg.Content += string.Format("<a href='{0}' class='a-buletxt'>详情请点击此处>></a>", complaintLink);
                    InsertMessage(msg);

                    ServiceLog.Log("OverTime Task", "超时（20小时）或者超时20小时后，每20小时,发给工程主管完成...", "Insert");
                    //发给工程经理
                    var manager = GetManagerUserInfo(governor.GovernorUserId);//获取工程主管的上级，即工程经理
                    if (manager != null)
                    {
                        msg.UserId = manager.ManagerUserId;
                        InsertMessage(msg);

                        ServiceLog.Log("OverTime Task", "超时（20小时）或者超时20小时后，每20小时,发给工程经理完成...", "Insert");
                        //发给项目总经理
                        var projectManager = GetManagerUserInfo(manager.ManagerUserId);
                        if (projectManager != null)
                        {
                            msg.UserId = projectManager.ManagerUserId;
                            InsertMessage(msg);

                            ServiceLog.Log("OverTime Task", "超时（20小时）或者超时20小时后，每20小时,发给项目总经理完成...", "Insert");
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 获取所有需要发送的消息的投诉单
        /// </summary>
        /// <returns></returns>
        protected List<ComplaintInfo> GetMessage()
        {
            var strCmd = string.Format(@"SELECT 
                                         c.ComplaintID,
                                         c.ProjectID,
                                         c.RequestTime,
                                         c.ComplaintStatus
                                         FROM [dbo].[Complaint](Nolock) c
                                         WHERE c.ComplaintStatus=1 OR c.ComplaintStatus=2");
            var repair = SqlHelper.ExecuteDataTable(strCmd);

            if (repair == null || repair.Rows.Count == 0)
            {
                return null;
            }

            var complaintList = new List<ComplaintInfo>();
            foreach (DataRow row in repair.Rows)
            {
                var info = new ComplaintInfo();
                info.ComplaintId = (int)row["ComplaintID"];
                info.ProjectId = (int)row["ProjectID"];
                info.RequestTime = (DateTime)row["RequestTime"];
                info.ComplaintStatus = (int)row["ComplaintStatus"];
                complaintList.Add(info);
            }
            return complaintList;
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
            if (manager.Rows.Count == 0) return null;

            var userInfo = new UserInfoEntity();
            userInfo.ManagerUserId = (int)manager.Rows[0]["ManagerUserId"];

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
