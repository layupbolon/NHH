using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.BizLogic;
//using NHH.Service.Common;
//using NHH.WebSite.Management.Service;

namespace NHH.BizLogic.Common
{
    public class BizRegion : BizCacheObject<NHHEntities, Region>
    {
        /// <summary>
        /// 获取区域id
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "RegionID";
            }
        }
        /// <summary>
        ///获取区域列表
        /// </summary>
        /// <returns></returns>
        public List<Region> GetRegion() 
        {
            List<Region> list = new List<Region>();
            list=base.GetAllList();
            return list;
        }
        /// <summary>
        /// 通过缓存获取地域列表
        /// </summary>
        /// <returns></returns>
        public List<Region> GetRegionByCache() 
        {
            List<Region> list = new List<Region>();
            list = base.TryCacheAllList();
            return list;
        }
    }
}
