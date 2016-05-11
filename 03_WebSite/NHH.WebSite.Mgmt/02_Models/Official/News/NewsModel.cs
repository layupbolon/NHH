using System;
using System.ComponentModel.DataAnnotations;
using NHH.Models.Approve;

namespace NHH.Models.Official
{
    public class NewsModel : ApproveBaseModel
    {
        private int _newsID;

        /// <summary>
        /// 新闻ID
        /// </summary>
        public int NewsID
        {
            get { return _newsID; }
            set { _newsID = value; }
        }

        /// <summary>
        /// 新闻类型
        /// </summary>
        public int NewsType { get; set; }

        /// <summary>
        /// 新闻类型名称
        /// </summary>
        public string NewsTypeName { get; set; }

        /// <summary>
        /// 新闻目标 1唐小二 2官网
        /// </summary>
        public int NewsTarget { get; set; }

        /// <summary>
        /// 新闻目标名称
        /// </summary>
        public string NewsTargetName { get; set; }

        /// <summary>
        /// 新闻标题
        /// </summary>
        [Required(ErrorMessage = "请输入新闻标题")]
        public string NewsTitle { get; set; }

        /// <summary>
        /// 新闻内容
        /// </summary>
        [Required(ErrorMessage = "请输入新闻内容")]
        public string NewsContent { get; set; }

        /// <summary>
        /// 新闻简介
        /// </summary>
        [Required(ErrorMessage = "请输入新闻简介")]
        public string NewsBrief { get; set; }

        /// <summary>
        /// 横幅图片
        /// </summary>
        public string NewsCover { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 项目ID 
        /// 如果是唐小二必填
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 新闻状态
        /// -1无效 1有效
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 新闻状态名称
        /// </summary>
        public string NewsStatus { get; set; }

        /// <summary>
        /// 置顶
        /// 0否  1是
        /// </summary>
        public int Stick { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishDate { get; set; }

        /// <summary>
        /// 发布时间节点
        /// </summary>
        public int PublishDateTime { get; set; }

        /// <summary>
        /// 提交用户
        /// </summary>
        public int InUser { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime InDate { get; set; }

        public int? EditUser { get; set; }

        public DateTime? EditDate { get; set; }

        public override int RefID
        {
            get { return _newsID; }
            set { _newsID = value; }
        }

        public override string ApproveUrl
        {
            get { return "/Official/News/Approve"; }
        }

        public override string RedirectUrl
        {
            get { return "/Official/News/List"; }
        }

        /// <summary>
        /// 当前操作人
        /// </summary>
        public int UserID { get; set; }
    }
}
