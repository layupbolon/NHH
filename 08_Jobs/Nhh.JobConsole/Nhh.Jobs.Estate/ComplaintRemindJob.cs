using log4net;
using Nhh.Jobs.Model;
using Nhh.Jobs.Utility;
using NHH.Framework.Utility;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Estate
{
    /// <summary>
    /// 投诉超时提醒
    /// </summary>
    public sealed class ComplaintRemindJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ComplaintRemindJob));

        /// <summary>
        /// 执行JOB
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var complaintList = GetComplaintList();

            if (complaintList == null || complaintList.Count == 0)
                return;

            _logger.Info(string.Format("本次共有{0}条投诉待处理", complaintList.Count));

            complaintList.ForEach(complaint =>
            {
                try
                {
                    //判断今天是否已发过消息
                    int messageId = UserMessageDA.GetMessageId(7, complaint.ComplaintId);
                    if (messageId > 0)
                        return;

                    int governorUserId = 0;
                    int managerUserId = 0;
                    var message = ProcessComplaintInfo(complaint, out governorUserId, out managerUserId);
                    if (message == null)
                        return;
                    //加链接
                    var complaintLink = string.Format(ParamHelper.GetString("ComplaintLink"), complaint.ComplaintId);
                    message.Content += string.Format("<a href='{0}' class='blue'>详情请点击此处</a>", complaintLink);

                    //发给工程主管
                    UserMessageDA.Add(message);

                    if (governorUserId > 0)
                    {
                        message.UserId = governorUserId;
                        UserMessageDA.Add(message);
                    }

                    if (managerUserId > 0)
                    {
                        message.UserId = managerUserId;
                        UserMessageDA.Add(message);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("投诉处理失败", ex);
                }

            });
        }

        /// <summary>
        /// 处理需要发送的消息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="governorUserId"></param>
        /// <param name="managerUserId"></param>
        /// <returns></returns>
        public UserMessageInfo ProcessComplaintInfo(ComplaintInfo info, out int governorUserId, out int managerUserId)
        {
            governorUserId = 0;
            managerUserId = 0;
            //获取工程主管信息
            var governorInfo = UserInfoDA.GetGovernorUserInfo(info.ProjectId, 101);
            if (governorInfo == null || governorInfo.GovernorUserId <= 1)
            {
                _logger.Error("未能获取到工程主管信息");
                return null;
            }

            var message = new UserMessageInfo
            {
                SourceType = 7,
                SourceRefId = info.ComplaintId,
            };

            #region 待处理投诉单据
            if (info.ComplaintStatus == 1)
            {
                message.UserId = governorInfo.GovernorUserId;
                message.Subject = "您收到一条新的投诉单据";
                message.Content = string.Format("你好！您收到一条新的投诉单据，单据编号：{0}，", info.ComplaintId);
                return message;
            }
            #endregion

            #region 超时投诉单据
            if (info.ComplaintStatus == 2)
            {
                var hourNum = (int)((DateTime.Now - info.RequestTime).TotalHours);
                //发给工程主管
                message.UserId = governorInfo.GovernorUserId;
                message.Subject = "投诉单据已超时一天未完结";
                message.Content = string.Format("你好！投诉单据 {0} 尚未完结，已超时{1}小时，请及时处理", info.ComplaintId, hourNum);

                //获取工程主管的上级，即工程经理
                governorUserId = UserInfoDA.GetManagerUserId(governorInfo.GovernorUserId);
                //发给项目总经理
                managerUserId = UserInfoDA.GetManagerUserId(governorUserId);
                return message;
            }
            #endregion

            _logger.Error("投诉单据状态不在处理范围");
            return null;
        }

        /// <summary>
        /// 获取所有需要发送的消息的投诉单
        /// </summary>
        /// <returns></returns>
        private List<ComplaintInfo> GetComplaintList()
        {
            var strCmd = string.Format(@"SELECT c.ComplaintID,
		                                        c.ProjectID,
		                                        c.RequestTime,
		                                        c.ComplaintStatus
                                        FROM [dbo].[Complaint](Nolock) c
                                        WHERE c.ComplaintStatus=1
                                        Union All
                                        SELECT c.ComplaintID,
		                                        c.ProjectID,
		                                        c.RequestTime,
		                                        c.ComplaintStatus
                                        FROM [dbo].[Complaint](Nolock) c
                                        WHERE c.ComplaintStatus=2 and DATEDIFF(DAY, RequestTime,GETDATE())>1");
            var table = SqlHelper.ExecuteDataTable(strCmd);

            if (table == null || table.Rows == null || table.Rows.Count == 0)
            {
                return null;
            }

            var complaintList = new List<ComplaintInfo>();
            foreach (DataRow row in table.Rows)
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
    }
}
