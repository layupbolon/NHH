using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户用户信息
    /// </summary>
    public class MerchantUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 商户信息
        /// </summary>
        public MerchantCommonInfo MerchantInfo { get; set; }

        /// <summary>
        /// 商铺ID
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色信息
        /// </summary>
        public RoleCommonInfo RoleInfo { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Required(ErrorMessage="请填写用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage="请填写用户姓名")]
        public string NickName { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Required(ErrorMessage="请填写手机")]
        public string Mobile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WechatOpenId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 相片
        /// </summary>
        public string PhotoFile { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "请选择性别")]
        public int Gender { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public Nullable<DateTime> Birthday { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation{ get; set; }
        
        public Nullable<int> Education { get; set; }
        
        public Nullable<int> MaritalStatus { get; set; }
        
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticalStatus { get; set; }
        
        /// <summary>
        /// 身高
        /// </summary>
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "只能输入数字,最多保留2位")]
        public Nullable<decimal> Height { get; set; }
        
        /// <summary>
        /// 体重
        /// </summary>
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "只能输入数字,最多保留2位")]
        public Nullable<decimal> Weight { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string EmergencyContact { get; set; }
        
        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        public string EmergencyPhone { get; set; }
        
        /// <summary>
        /// 教育经历
        /// </summary>
        public string EducationExperience { get; set; }
        
        /// <summary>
        /// 工作经历
        /// </summary>
        public string WorkExperience { get; set; }
        
        /// <summary>
        /// 身份证
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 家庭成员
        /// </summary>
        public string FamilyMembers { get; set; }
    }
}
