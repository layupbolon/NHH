using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Utility;
using NHH.Models.Common.Enum;

namespace NHH.Models.Common
{
    public class ApiResult<T> where T : new()
    {
        public ApiResult()
        {
            Code = (int)ApiResultEnum.Error;
            Desc = EnumHelper.GetEnumDescription(ApiResultEnum.Error);
        }
        public ApiResult(ApiResultEnum apiResultEnum)
        {
            Code = (int)apiResultEnum;
            Desc = EnumHelper.GetEnumDescription(apiResultEnum);
        }

        /// <summary>
        /// 返回的状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 返回对象
        /// </summary>
        public T Data { get; set; }
    }
    public class ApiResult
    {
        public ApiResult()
        {
            Code = (int)ApiResultEnum.Error;
            Desc = EnumHelper.GetEnumDescription(ApiResultEnum.Error);
        }

        public ApiResult(ApiResultEnum apiResultEnum)
        {
            Code = (int) apiResultEnum;
            Desc = EnumHelper.GetEnumDescription(apiResultEnum);
        }

        /// <summary>
        /// 返回的状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Desc { get; set; }
    }

}
