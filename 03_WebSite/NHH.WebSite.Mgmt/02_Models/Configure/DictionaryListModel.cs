using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 数据字典列表
    /// </summary>
    public class DictionaryListModel : BaseModel
    {
        public DictionaryListQueryInfo QueryInfo { get; set; }

        public List<DictionaryInfo> DictionaryList { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
