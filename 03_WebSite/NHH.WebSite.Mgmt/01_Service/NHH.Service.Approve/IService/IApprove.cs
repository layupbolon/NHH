using NHH.Models.Approve;

namespace NHH.Service.Approve.IService
{
    public interface IApprove
    {
        /// <summary>
        /// 添加审批意见
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        bool Approve(ApproveModel approve);
    }
}
