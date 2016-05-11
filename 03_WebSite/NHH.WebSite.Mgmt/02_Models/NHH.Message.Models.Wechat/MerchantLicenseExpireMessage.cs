using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Wechat;

namespace NHH.Message.Models.Wechat
{
    public class MerchantLicenseExpireMessage : requestData
    {
        /// <summary>
        /// 标题
        /// </summary>
        public requestFieldDetail first { get; set; }

        /// <summary>
        /// 证照类型
        /// </summary>
        public requestFieldDetail keyword1 { get; set; }

        /// <summary>
        /// 证件数量
        /// </summary>
        public requestFieldDetail keyword2 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public requestFieldDetail remark { get; set; }
    }
}
