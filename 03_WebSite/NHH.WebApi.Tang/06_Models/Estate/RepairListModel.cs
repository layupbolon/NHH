using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Estate
{
    public class RepairListModel
    {
        /// <summary>
        /// 商户列表Model
        /// </summary>
        public List<RepairInfo> RepairList { get; set; }

        /// <summary>
        ///分页信息 
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
