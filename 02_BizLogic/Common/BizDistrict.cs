using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.BizLogic;

namespace NHH.BizLogic.Common
{
    public class BizDistrict : BizCacheObject<NHHEntities, District>
    {
        /// <summary>
        /// 获取市区ID
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "DistrictID";
            }
        }
       /// <summary>
       /// 根据城市获取城市区域
       /// </summary>
       /// <param name="CityID"></param>
       /// <returns></returns>
        public List<District> GetDistrict(int CityID) 
        {
            List<District> list = new List<District>();
            list = base.GetAllList(p => p.CityID == CityID);
            return list;
        }

        public List<District> GetDistrictByCache(int CityID) 
        {
            List<District> list = new List<District>();
            list = base.TryCacheAllList(p => p.CityID == CityID);
            return list;
        }
    }
}
