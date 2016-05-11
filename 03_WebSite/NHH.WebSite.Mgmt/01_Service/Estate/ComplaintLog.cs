using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate
{
    /// <summary>
    /// 投诉日志
    /// </summary>
    public class ComplaintLog : NHHService<NHHEntities>
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="info"></param>
        public void Add(ComplaintInfo info)
        {
            Log(info, (ComplaintInfo info2) =>
            {
                return string.Format("{0}提交投诉单", info2.UserInfo.UserName);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        public void Update(ComplaintInfo info)
        {
            Log(info, (ComplaintInfo info2) =>
            {
                return string.Format("{0}更新投诉单", info2.UserInfo.UserName);
            });
        }

        /// <summary>
        /// 指派
        /// </summary>
        /// <param name="info"></param>
        public void Assgin(ComplaintInfo info)
        {
            Log(info, (ComplaintInfo info2) =>
            {
                return string.Format("投诉单已经指派给{0}", info2.ServiceUserName);
            });
        }

        /// <summary>
        /// 重新指派
        /// </summary>
        /// <param name="info"></param>
        public void ReAssgin(ComplaintInfo info)
        {
            Log(info, (ComplaintInfo info2) =>
            {
                return string.Format("投诉单重新指派给{0}，原因：{1}，备注：{2}", info2.ServiceUserName, info2.ReAssignReason, info2.Remarks);
            });
        }

        /// <summary>
        /// 搁置
        /// </summary>
        /// <param name="shelveInfo"></param>
        public void Shelve(ComplaintShelveInfo shelveInfo)
        {
            var complaintInfo = new ComplaintInfo
            {
                ComplaintId = shelveInfo.ComplaintId,
                UserInfo = shelveInfo.UserInfo,
                ReAssignReason = shelveInfo.ShelveReason,
                Remarks = shelveInfo.Remarks,
            };
            Log(complaintInfo, (ComplaintInfo info2) =>
            {
                return string.Format("投诉单已搁置，原因：{0}，备注：{1}", info2.ReAssignReason, info2.Remarks);
            });
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="info"></param>
        public void Finish(ComplaintInfo info)
        {
            Log(info, (ComplaintInfo info2) =>
            {
                return "投诉单已处理完成";
            });
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="buildLogText"></param>
        private void Log(ComplaintInfo info, Func<ComplaintInfo, string> buildLogText)
        {
            try
            {
                var entity = new Complaint_Log();
                entity.ComplaintID = info.ComplaintId;
                entity.LogTime = DateTime.Now;
                entity.LogUserID = info.UserInfo.UserId;
                entity.LogUserName = info.UserInfo.UserName;
                entity.LogText = buildLogText(info);
                entity.EditDate = DateTime.Now;
                entity.EditUser = info.UserInfo.UserId;
                var instance = CreateCacheBizObject<Complaint_Log>();
                instance.Insert(entity);
                this.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.ToString();
                //后续记录业务异常
            }
        }
    }
}
