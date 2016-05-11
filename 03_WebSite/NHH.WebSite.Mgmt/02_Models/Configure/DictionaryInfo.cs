using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 数据字典信息
    /// </summary>
    public class DictionaryInfo
    {
        public int FieldID { get; set; }

        public string FieldType { get; set; }

        public int FieldValue { get; set; }

        public string FieldName { get; set; }

        public string FieldDesc { get; set; }

    }
}
