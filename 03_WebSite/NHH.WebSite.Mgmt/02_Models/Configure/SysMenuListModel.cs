using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 菜单列表
    /// </summary>
    public class SysMenuListModel : BaseModel
    {
        public SysMenuListQueryInfo QueryInfo { get; set; }

        public List<SysMenuInfo> MenuList { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
