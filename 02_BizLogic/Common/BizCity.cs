using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.BizLogic;

namespace NHH.BizLogic.Common
{
    public class BizCity :BizCacheObject<NHHEntities, City>
    {
        /// <summary>
        /// 获取市区ID
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "CityID";
            }
        }
        /// <summary>
        /// 根据省获取城市
        /// </summary>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        public List<City> GetCity(int ProvinceID) 
        {
            List<City> list = new List<City>();
            list = base.GetAllList(p => p.ProvinceID == ProvinceID);
            return list;
        }
        /// <summary>
        /// 根据缓存获取城市下拉列表
        /// </summary>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        public List<City> GetCityByCache(int ProvinceID) 
        {
            List<City> list = new List<City>();
            list = base.TryCacheAllList(p => p.ProvinceID == ProvinceID);
            return list;
        }
    }
}
