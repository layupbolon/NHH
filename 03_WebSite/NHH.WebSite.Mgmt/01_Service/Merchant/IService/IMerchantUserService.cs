using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商家用户服务接口
    /// </summary>
    public interface IMerchantUserService
    {
        /// <summary>
        /// 获取商户用户列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        MerchantUserListModel GetUserList(MerchantUserListQueryInfo queryInfo);

        /// <summary>
        /// 获取商户用户详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        MerchantUserDetailModel GetUserDetail(int userId);

        /// <summary>
        ///新增
        /// </summary>
        /// <param name="MerchantId"></param>
        /// <returns></returns>
        MessageBag<bool> AddUser(MerchantUserDetailModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        bool UpdateUser(MerchantUserDetailModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteUser(int merchantId, int userId);

        /// <summary>
        /// 按商铺分组列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        List<MerchantUserGroupInfo> GetUserGroupList(MerchantUserGroupQueryInfo queryInfo);
    }
}
