using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    public class BrandUtil
    {
        /// <summary>
        /// 获取品牌ID
        /// </summary>
        /// <param name="brandName"></param>
        /// <returns></returns>
        public static int GetBrandId(string brandName)
        {
            if (string.IsNullOrEmpty(brandName) || brandName.Length == 0)
                return 0;
            var strCmd = string.Format(@"SELECT TOP 1 [BrandID]
                                          FROM [dbo].[Brand](Nolock)
                                          Where BrandName='{0}'", brandName);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取品牌级次
        /// </summary>
        /// <param name="brandLevel"></param>
        /// <returns></returns>
        public static int GetBrandLevel(string brandLevel)
        {
            if (string.IsNullOrEmpty(brandLevel) || brandLevel.Length == 0)
                return 0;

            var strCmd = string.Format(@"SELECT TOP 1 [FieldValue]
                                          FROM [dbo].[Dictionary](Nolock)
                                          Where FieldType='BrandLevel' And FieldName='{0}'", brandLevel);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取品牌经营形式
        /// </summary>
        /// <param name="brandType"></param>
        /// <returns></returns>
        public static int GetBrandType(string brandType)
        {
            if (string.IsNullOrEmpty(brandType) || brandType.Length == 0)
                return 0;

            var strCmd = string.Format(@"SELECT TOP 1 [FieldValue]
                                          FROM [dbo].[Dictionary](Nolock)
                                          Where FieldType='BrandType' And FieldName='{0}'", brandType);
            return SqlHelper.ExecuteScalar(strCmd);
        }
    }
}
