using NHH.Models.Approve;
using NHH.Models.Contract;
using NHH.Service.Approve.IService;

namespace NHH.Service.Contract.IService
{
    /// <summary>
    /// 特殊单据服务接口
    /// </summary>
    public interface IDocumentsService : IApprove
    {
        /// <summary>
        /// 获取特殊单据列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        DocumentsListModel GetDocumentsList(DocumentsQueryInfo queryInfo);

        /// <summary>
        /// 获取出门申请单详细
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        GetOutDetaiInfo GetOutDetaiInfo(int documentID, int configType = (int)ConfigTypeEnum.出门申请单);

        /// <summary>
        /// 获取装修申请单详细
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        DecorateDetailInfo GetDecorateDetailInfo(int documentID, int configType = (int)ConfigTypeEnum.装修申请单);

        /// <summary>
        /// 获取延时经营申请单详细
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        DelayOperateDetailInfo GetDelayOperateDetailInfo(int documentID, int configType = (int)ConfigTypeEnum.延时经营申请单);
    }
}
