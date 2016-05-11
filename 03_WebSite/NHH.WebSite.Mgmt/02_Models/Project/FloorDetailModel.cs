using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼层详细信息
    /// </summary>
    public class FloorDetailModel : FloorInfo
    {
        public List<string> Imglist { get; set; }
    }
}
