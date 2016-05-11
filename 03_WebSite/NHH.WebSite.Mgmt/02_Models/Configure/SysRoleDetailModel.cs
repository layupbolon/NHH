using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    public class SysRoleDetailModel : SysRoleInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo { };

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get
            { return _crumbInfo; }
        }

        /// <summary>
        /// 增加功能Id集合
        /// </summary>
        public string AddFunctionList { get; set; }

        /// <summary>
        /// 移除功能Id集合
        /// </summary>
        public string RemoveFuctionList { get; set; }

        /// <summary>
        /// 功能Id集合
        /// </summary>
        public string FunctionIdList { get; set; }

        /// <summary>
        /// 角色下面有哪些功能模块
        /// </summary>
        public List<SysFunctionInfo> RoleFunctionList { get; set; }

        public int InUser { get; set; }

    }
}
