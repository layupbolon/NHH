using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Message
{
    public class MerchantMessage
    {
        private readonly CrumbInfo _crumbInfo = new CrumbInfo();

        /// <summary>
        /// 面包屑
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get { return _crumbInfo; }
        }
        public int MerchantID { get; set; }
        public string MerchantName { get; set; }

        public string MerchantUserName { get; set; }
        public string NickName { get; set; }
        public int StoreID { get; set; }
        public string StroeName { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "请填写标题")]
        [StringLength(200, ErrorMessage = "标题长度过长")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "请填写内容")]
        public string Content { get; set; }
        public int SourceType { get; set; }
        public int SourceRefID { get; set; }
        public int Status { get; set; }

        public int EmployeeID { get; set; }
        public System.DateTime InDate { get; set; }

        public int InUser { get; set; }

        public System.DateTime EditDate { get; set; }

        public int EditUser { get; set; }

        public string Objserialize { get; set; }
    }
}
