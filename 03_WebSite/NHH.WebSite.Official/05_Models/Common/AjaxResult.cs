using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// Ajax请求结果
    /// </summary>
    public class AjaxResult
    {
        private int _code = 0;
        private string _message = "Ok";

        /// <summary>
        /// Ajax请求结果
        /// </summary>
        public AjaxResult()
        {
        }

        /// <summary>
        /// 状态代码，默认为0-成功
        /// </summary>
        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess
        {
            get { return this.Code == 0; }
        }
    }

    /// <summary>
    /// Ajax请求结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AjaxResult<T> : AjaxResult
        where T : new()
    {

        /// <summary>
        /// Ajax请求结果
        /// </summary>
        /// <param name="data">实体信息</param>
        public AjaxResult(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// 数据内容
        /// </summary>
        public T Data { get; set; }

    }
}
