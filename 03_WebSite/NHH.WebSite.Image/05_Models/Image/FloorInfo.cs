using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Image
{
    /// <summary>
    /// 楼层信息
    /// </summary>
    public class FloorInfo
    {
        public int ProjectId { get; set; }

        public int BuildingId { get; set; }

        public int FloorId { get; set; }
    }
}
