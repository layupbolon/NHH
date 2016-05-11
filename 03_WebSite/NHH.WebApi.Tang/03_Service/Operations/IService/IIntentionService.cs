using NHH.Models.Operations;

namespace NHH.Service.Operations.IService
{
    public interface IIntentionService
    {
        /// <summary>
        /// 添加入驻意向
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddIntention(IntentionInfo model);
    }
}
