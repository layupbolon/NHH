using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商户用户证件服务接口
    /// </summary>
    public interface IMerchantUserPaperService
    {
        /// <summary>
        /// 获取证件列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        MerchantUserPaperListModel GetPaperList(int userId);

        /// <summary>
        /// 添加商户附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddPaper(MerchantUserPaperInfo model);
        
        /// <summary>
        /// 删除单个用户附件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        bool DelPaper(int userId, int paperId);
    }
}
