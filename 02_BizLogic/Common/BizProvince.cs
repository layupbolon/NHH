using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.BizLogic;

namespace NHH.BizLogic.Common
{
    public class BizProvince : BizCacheObject<NHHEntities, Province>
    {
        /// <summary>
        /// 获取省ID
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "ProvinceID";
            }
        }
        /// <summary>
        /// 根据区域获取省
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public List<Province> GetProvince(int RegionId) 
        {
            List<Province> list = new List<Province>();
            list=base.GetAllList(p => p.RegionID == RegionId);
            return list;
        }
        /// <summary>
        /// 根据缓存获取省列表
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public List<Province> GetProvinceByCche(int RegionId) 
        {
            List<Province> list = new List<Province>(); 
            list = base.TryCacheAllList(p => p.RegionID == RegionId);
            return list;
        }
    }
}
