using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Operations;

namespace NHH.Service.Operations.IService
{
    public interface IIntentionService
    {
        /// <summary>
        /// 获取入驻意向表单列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        IntentionList GetIntentionList(IntentionQuery queryInfo);

        /// <summary>
        /// 获取入驻意向表单
        /// </summary>
        /// <param name="intentionId"></param>
        /// <returns></returns>
        IntentionInfo GetIntention(int intentionId);

        /// <summary>
        /// 更新接单人与接单状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ProcessIntention(IntentionInfo model);
    }
}
