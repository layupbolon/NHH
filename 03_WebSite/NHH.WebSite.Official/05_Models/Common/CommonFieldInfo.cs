using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 公共字段信息
    /// </summary>
    public class CommonFieldInfo
    {
        public int FieldId { get; set; }

        public string FieldName { get; set; }

        public int FieldValue { get; set; }

        public string FieldDesc { get; set; }
    }
}
