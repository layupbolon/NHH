using NHH.Models.Approve;
using NHH.Models.Plan.MerchantLicense;
using NHH.Service.Approve.IService;

namespace NHH.Service.Plan.IService
{
    public interface IMerchantLicenseService : IApprove
    {
        /// <summary>
        /// 获取商户证照列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        MerchantLicenseListModel GetMerchantLicenseList(MerchantLicenseQueryModel queryInfo);

        /// <summary>
        /// 获取商户证照
        /// </summary>
        /// <param name="attID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        MerchantLicenseModel GetMerchantLicense(int attID,int configType = (int)ConfigTypeEnum.证照);
    }
}
