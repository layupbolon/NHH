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
    /// 维修超时提醒JOB
    /// </summary>
    public sealed class RepairRemindJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(RepairRemindJob));

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var repairList = GetPreRepairList();

            if (repairList == null || repairList.Count == 0)
                return;

            _logger.Info(string.Format("本次共{0}条待处理维修数据", repairList.Count));

            repairList.ForEach(repair =>
            {
                try
                {

                    int governorUserId = 0;
                    int managerUserId = 0;
                    var message = ProcessRepairInfo(repair, out governorUserId, out managerUserId);
                    if (message == null)
                        return;

                    //加链接
                    var repairLink = string.Format(ParamHelper.GetString("RepairLink"), repair.RepairId);
                    message.Content += string.Format("<a href='{0}' class='blue'>,详情请点击此处</a>", repairLink);

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
                    _logger.Error("维修处理失败", ex);
                }
            });
        }

        /// <summary>
        /// 处理维修消息
        /// </summary>
        /// <param name="repair"></param>
        /// <param name="governorUserId"></param>
        /// <param name="managerUserId"></param>
        /// <returns></returns>
        private UserMessageInfo ProcessRepairInfo(RepairInfo repair, out int governorUserId, out int managerUserId)
        {
            governorUserId = 0;
            managerUserId = 0;
            //获取工程主管信息
            var governorInfo = UserInfoDA.GetGovernorUserInfo(repair.ProjectId, 102);
            if (governorInfo == null || governorInfo.GovernorUserId <= 1)
                return null;

            var message = new UserMessageInfo
            {
                SourceType = 6,
                SourceRefId = repair.RepairId
            };

            #region 待处理维修单据
            if (repair.RepairStatus == 1)
            {
                //判断今天是否已发过消息ID
                int messageId = UserMessageDA.GetMessageId(6, repair.RepairId);
                if (messageId > 0)
                    return null;

                message.UserId = governorInfo.GovernorUserId;
                message.Subject = "您收到一条新的报修单据";
                message.Content = "你好！您收到了一条新的报修单据，";
                return message;
            }
            #endregion

            //处理中的维修单据
            if (repair.RepairStatus != 2)
                return null;

            if (!repair.EstimatedFinishTime.HasValue)
                return null;

            var finishTime = repair.EstimatedFinishTime.Value;

            #region 第一次超时发一次
            //超时（第一次）

            var startTime = finishTime;
            var endTime = finishTime.AddHours(6);
            var isTimeout = startTime <= DateTime.Now && DateTime.Now < endTime;
            if (isTimeout)
            {
                //判断某一个时间段内是否已发送过消息Id
                int messageId = UserMessageDA.GetMessageId(6, repair.RepairId, startTime, endTime);
                if (messageId > 0)
                    return null;

                message.UserId = governorInfo.GovernorUserId;
                message.Subject = string.Format("维修单据 {0} 尚未处理，已超时", repair.RepairId);
                message.Content = string.Format("你好！维修单据 {0} 尚未安排处理，已超时，请及时处理", repair.RepairId);
                return message;
            }
            #endregion

            #region 超时6个小时
            var overTimeHour = 6;
            //超时6个小时发一次
            startTime = finishTime.AddHours(overTimeHour);
            endTime = finishTime.AddHours(12);
            isTimeout = startTime <= DateTime.Now && DateTime.Now < endTime;
            if (isTimeout)
            {
                //判断某一个时间段内是否已发送过消息Id
                int messageId = UserMessageDA.GetMessageId(6, repair.RepairId, startTime, endTime);
                if (messageId > 0)
                    return null;

                //发给工程主管
                message.Subject = string.Format("维修单据 {0} 超时6小时", repair.RepairId);
                message.Content = string.Format("你好！维修单据 {0} 已经超时6小时，请及时处理", repair.RepairId);

                ////获取工程主管的上级，即工程经理
                governorUserId = UserInfoDA.GetManagerUserId(governorInfo.GovernorUserId);
                return message;
            }
            #endregion

            #region 超时（12小时）或者超时12小时后，每12小时
            overTimeHour = 12;//超时12个小时
            var multiple = new TimeSpan(DateTime.Now.Ticks - finishTime.Ticks).TotalHours / overTimeHour;//超12小时倍数
            var addHours = overTimeHour * Math.Floor(multiple);//小时增加数，时间节点
            //超时12个小时,并且超过12小时候每12小时发一次    

            startTime = finishTime.AddHours(addHours);
            endTime = finishTime.AddHours(addHours + 12);
            isTimeout = startTime <= DateTime.Now && DateTime.Now < endTime;
            isTimeout = isTimeout && addHours >= overTimeHour;//去掉第一次超时的时间点
            if (isTimeout)
            {
                //判断某一个时间段内是否已发送过消息Id
                int messageId = UserMessageDA.GetMessageId(6, repair.RepairId, startTime, endTime);
                if (messageId > 0)
                    return null;

                //发给项目总经理
                message.UserId = governorInfo.GovernorUserId;
                message.Subject = string.Format("维修单据 {0} 超时{1}小时", repair.RepairId, addHours);
                message.Content = string.Format("你好！维修单据 {0} 已经超时{1}小时，请及时处理", repair.RepairId, addHours);

                //获取工程主管的上级，即工程经理
                governorUserId = UserInfoDA.GetManagerUserId(governorInfo.GovernorUserId);
                //发给项目总经理
                managerUserId = UserInfoDA.GetManagerUserId(governorUserId);
                return message;
            }
            #endregion
            return null;
        }

        /// <summary>
        /// 获取待处理维修列表
        /// </summary>
        /// <returns></returns>
        private List<RepairInfo> GetPreRepairList()
        {
            var strCmd = string.Format(@"SELECT 
                                         r.RepairID,
                                         r.ProjectID,
                                         r.RequestTime,
                                         r.EstimatedFinishTime,
                                         r.RepairStatus
                                         FROM [dbo].[Repair](Nolock) r
                                         WHERE r.RepairStatus=1 OR r.RepairStatus=2");
            var table = SqlHelper.ExecuteDataTable(strCmd);

            if (table == null || table.Rows.Count == 0)
            {
                return null;
            }

            DateTime estimatedFinishTime = DateTime.Now;

            var repairList = new List<RepairInfo>();
            foreach (DataRow row in table.Rows)
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
            return repairList;
        }
    }
}
