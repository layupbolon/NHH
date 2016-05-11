using NHH.Framework.Utility;
using NHH.Framework.Wechat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Utility
{
    /// <summary>
    /// 项目业务配置
    /// </summary>
    public class ProjectBizConfigDA
    {
        /// <summary>
        /// 获取微信公众号开发者Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="bizConfigType"></param>
        /// <returns></returns>
        public static configData GetWechatDevelopIdInfo(int projectId, int bizConfigType)
        {
            var strCmd = string.Format(@"Select pb.[Param1] as AppId
                                               ,pb.[Param2] as AppSecrect
                                               From [dbo].[Project_BizConfig](Nolock) pb
                                               Where pb.[BizConfigType]={0} And pb.[ProjectID]={1} and status=1", bizConfigType, projectId);
            var table = SqlHelper.ExecuteDataTable(strCmd);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return null;

            DataRow row = table.Rows[0];

            var info = new configData
            {
                AppId = row["AppId"].ToString(),
                AppSecrect = row["AppSecrect"].ToString()
            };

            return info;
        }
    }
}
