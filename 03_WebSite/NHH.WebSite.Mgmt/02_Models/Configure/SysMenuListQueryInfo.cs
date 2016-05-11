using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 菜单列表查询信息
    /// </summary>
    public class SysMenuListQueryInfo : QueryInfo
    {
        public int? ParentId { get; set; }
    }
}
