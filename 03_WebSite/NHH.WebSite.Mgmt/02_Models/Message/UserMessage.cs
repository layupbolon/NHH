using NHH.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace NHH.Models.Message
{
    /// <summary>
    /// 用户消息
    /// </summary>
    public class UserMessage
    {
        public int MessageID { get; set; }

        public string DepartmentName { get; set; }

        public int UserID { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        [StringLength(200, ErrorMessage = "标题长度过长")]
        [Required(ErrorMessage = "请标题")]

        public string Subject { get; set; }
        [Required(ErrorMessage = "请内容")]

        public string Content { get; set; }

        public int SourceType { get; set; }

        public string SourceTypeName { get; set; }

        public int? SourceRefID { get; set; }

        public int Status { get; set; }

        public string MessageStatus { get; set; }

        /// <summary>
        /// 消息提醒时间
        /// </summary>
        public DateTime InDate { get; set; }

        /// <summary>
        ///详情Url
        /// </summary>
        public string DetailUrl { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }

        public string Title { get; set; }
    }
}
