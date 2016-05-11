//using NHH.Models.Common;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NHH.Models.Estate
//{
//    /// <summary>
//    /// 投诉详细信息
//    /// </summary>
//    public class ComplaintDetailModel : ComplaintInfo
//    {
//        private CrumbInfo _crumbInfo = new CrumbInfo();

//        /// <summary>
//        /// 面包屑
//        /// </summary>
//        public CrumbInfo CrumbInfo
//        {
//            get { return _crumbInfo; }
//        }

//        /// <summary>
//        /// 投诉证据图信息
//        /// </summary>
//        public IList<AttachmentInfo> AttachmentInfoList { get; set; }

//        /// <summary>
//        /// 用于页面存放多张投诉证据拼接图，用“，”分开
//        /// </summary>
//        public string AttchmentImgPathList { get; set; }

//        public int ProjectID { get; set; }

//        public int MerchantId { get; set; }

//        public int StoreId { get; set; }

//        public int RequestUserId { get; set; }

//        public string RequestUserName { get; set; }

//        [Required(ErrorMessage = "请输入投诉内容")]
//        public string RequestDesc { get; set; }

//        public int AcceptUserId { get; set; }

//        public string AcceptUserName { get; set; }

//        public DateTime? AcceptTime { get; set; }

//        public DateTime? StartTime { get; set; }

//        public DateTime? FinishTime { get; set; }

//        [Required(ErrorMessage = "请选择投诉对象")]
//        public string RequestTarget { get; set; }

//        public string InvestigationDesc { get; set; }

//        public string ServiceDesc { get; set; }

//    }
//}
