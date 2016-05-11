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
//    /// 维修详情
//    /// </summary>
//    public class RepairDetailModel : RepairInfo_
//    {
//        private CrumbInfo _crumbInfo = new CrumbInfo();

//        /// <summary>
//        /// 面包屑
//        /// </summary>
//        public CrumbInfo CrumbInfo
//        {
//            get { return _crumbInfo; }
//        }

//        [Required(ErrorMessage = "请输入铺位号或者店铺名称")]
//        public string Location { get; set; }

//        public string RepairDesc { get; set; }

//        public DateTime InDate { get; set; }

//        public string RequestUserName { get; set; }

//        public string RequestContact { get; set; }

//        public string RepairUserName { get; set; }


//        public string AttchmentImgPathList { get; set; }

//        /// <summary>
//        /// 维修人的联系方式
//        /// </summary>
//        public string RepairMobile { get; set; }

//        public IList<AttachmentInfo> AttachmentInfos
//        {
//            get;
//            set;
//        }

//        /// <summary>
//        /// 是否是公共区域
//        /// </summary>
//        [Required(ErrorMessage = "请选择位置描述")]
//        public int IsCommon { get; set; }


//        /// <summary>
//        /// 报修描述
//        /// </summary>
//        [Required(ErrorMessage = "请输入报修描述")]
//        public string RequestDesc { get; set; }
//        /// <summary>
//        /// 楼层
//        /// </summary
//        [Required(ErrorMessage = "请选择楼层")]
//        public int? FloorId { get; set; }

//        /// <summary>
//        /// 项目ID
//        /// </summary>
//        [Required(ErrorMessage = "请选择项目")]
//        public int ProjectId { get; set; }

//        /// <summary>
//        /// 维修接受时间
//        /// </summary>
//        public DateTime? AcceptTime { get; set; }

//        /// <summary>
//        /// 维修完成时间
//        /// </summary>
//        public DateTime? RepairFinshTime { get; set; }

//    }
//}
