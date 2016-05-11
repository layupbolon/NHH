using NHH.Models.Documents;

namespace NHH.Service.Documents.IService
{
    public interface IDocumentService
    {
        /// <summary>
        /// 商户特殊单据列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        MerchantDocumentListModel GetMerchantDocumentList(MerchantDocumentListQuery queryInfo);

        /// <summary>
        /// 创建装修申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddDecorateRequest(DecorateRequestInfo model);

        /// <summary>
        /// 创建出门申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddGetOutRequest(GetOutRequestInfo model);

        /// <summary>
        /// 创建延时运营申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddDelayOperateRequest(DelayOperateRequestInfo model);

        /// <summary>
        /// 更新装修申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateDecorateRequest(DecorateRequestInfo model);

        /// <summary>
        /// 更新出门申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateGetOutRequest(GetOutRequestInfo model);

        /// <summary>
        /// 更新延时运营申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateDelayOperateRequest(DelayOperateRequestInfo model);

        /// <summary>
        /// 获取装修申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        DecorateRequestInfo GetDecorateRequest(int documentId);

        /// <summary>
        /// 获取出门申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        GetOutRequestInfo GetGetOutRequest(int documentId);

        /// <summary>
        /// 获取延时运营申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        DelayOperateRequestInfo GetDelayOperateRequest(int documentId);

        /// <summary>
        /// 作废特殊单据
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        bool CancelDocumentRequest(int documentId);
    }
}
