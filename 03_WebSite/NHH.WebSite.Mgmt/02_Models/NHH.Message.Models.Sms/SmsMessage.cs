using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Message.Models.Sms
{
    /// <summary>
    /// 短信消息
    /// </summary>
    public class SmsMessage
    {
        private string signName = "立天唐人";

        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 参数
        /// Json格式
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// 短信签名
        /// </summary>
        public string SignName
        {
            get { return signName; }
            set { signName = value; }
        }
    }
}
