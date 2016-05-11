using System;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Operations;
using NHH.Service.Operations.IService;

namespace NHH.Service.Operations
{
    public class ComplaintService : NHHService<NHHEntities>, IComplaintService
    {
        /// <summary>
        /// 添加官网投诉建议
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddComplain(NHHCGComplain model)
        {
            var instance = CreateBizObject<NHHCG_Feedback>();
            var entity = new NHHCG_Feedback()
            {
                CustomerName = model.Name,
                Phone = model.Tel,
                FeedbackType = model.ComplainType,
                UserType = model.Role,
                Remark = model.ComplainDetail,
                Status = 1,
                InUser = 1,
                InDate = DateTime.Now
            };

            instance.Insert(entity);
            SaveChanges();
            return true;
        }
    }
}
