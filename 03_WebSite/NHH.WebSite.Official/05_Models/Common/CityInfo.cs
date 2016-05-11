using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 城市信息
    /// </summary>
    public class CityInfo
    {
        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
