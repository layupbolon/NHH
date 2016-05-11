using NHH.Models.Operations;

namespace NHH.Service.Operations.IService
{
    public interface IComplaintService
    {
        /// <summary>
        /// 添加官网投诉建议
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddComplain(NHHCGComplain model);
    }
}
