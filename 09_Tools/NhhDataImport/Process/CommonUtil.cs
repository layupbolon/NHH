using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace NhhDataImport.Process
{
    public class CommonUtil
    {
        /// <summary>
        /// 获取省份ID
        /// </summary>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        public static int GetProvinceId(string provinceName)
        {
            if (string.IsNullOrEmpty(provinceName) || provinceName.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [ProvinceID]
                                            FROM [dbo].[Province](Nolock)
                                            Where ProvinceName='{0}'", provinceName);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取城市ID
        /// </summary>
        /// <param name="provinceName"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public static int GetCityId(string provinceName, string cityName)
        {
            if (string.IsNullOrEmpty(provinceName) || provinceName.Length == 0 ||
                string.IsNullOrEmpty(cityName) || cityName.Length == 0)
                return 0;

            string strCmd = string.Format(@"SELECT TOP 1 [CityID]
                                          FROM [dbo].[City](Nolock) as A
                                          Inner join dbo.Province(Nolock) as B ON A.ProvinceID=B.ProvinceID
                                          Where ProvinceName='{0}' and CityName='{1}'", provinceName, cityName);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取指定名称的String参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public static string GetStringValue(string name, string def = "")
        {

            if (string.IsNullOrEmpty(name))
            {
                return def;
            }

            string value = ConfigurationManager.AppSettings[name].ToString();

            return value == string.Empty ? def : value;
        }

        /// <summary>
        /// 获取业态ID
        /// </summary>
        /// <param name="bizType"></param>
        /// <returns></returns>
        public static int GetBizTypeId(string bizType)
        {
            if (string.IsNullOrEmpty(bizType) || bizType.Length == 0)
                return 0;

            string strCmd = string.Format(@"SELECT TOP 1 [BizTypeID]
                                          FROM [dbo].[BizType](Nolock)
                                          Where BizTypeName='{0}'", bizType);

            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取经营品类
        /// </summary>
        /// <param name="bizCategory"></param>
        /// <returns></returns>
        public static int GetBizCategoryId(string bizCategory)
        {
            if (string.IsNullOrEmpty(bizCategory) || bizCategory.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [BizCategoryID]
                                          FROM [dbo].[BizCategory](Nolock)
                                          Where BizCategoryName='{0}'", bizCategory);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取数据字典的值
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int GetDictionaryValue(string fieldType, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldType) || fieldType.Length == 0 ||
                string.IsNullOrEmpty(fieldName) || fieldName.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [FieldValue]
                                          FROM [dbo].[Dictionary](Nolock)
                                          Where FieldType='{0}' And FieldName='{1}'", fieldType, fieldName);
            return SqlHelper.ExecuteScalar(strCmd);
        }
        /// <summary>
        /// 获取区域ID
        /// </summary>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public static int GetRegionId(string regionName)
        {
            if (string.IsNullOrEmpty(regionName) || regionName.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [RegionID]
                                          FROM [dbo].[Region](Nolock) 
                                          Where RegionName='{0}'", regionName);
            return SqlHelper.ExecuteScalar(strCmd);
        }
        /// <summary>
        /// 获取公司ID
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="companyType"></param>
        /// <returns></returns>
        public static int GetCompanyId(string companyName, int companyType)
        {
            if (string.IsNullOrEmpty(companyName) || companyName.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [CompanyID]
                                          FROM [dbo].[Company](Nolock) 
                                          Where CompanyName='{0}' and CompanyType='{1}' and Status=1", companyName, companyType);
            return SqlHelper.ExecuteScalar(strCmd);
        }
    }
}
