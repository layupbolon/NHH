//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NHH.Entities
{
    using System;
    using System.Collections.Generic;
    using NHH.Framework;
    using NHH.Framework.Data;
    
    
    public partial class Merchant_UserPaper : IStatusFlag,IEditable
    {
        public int PaperID { get; set; }
        public int UserID { get; set; }
        public int PaperType { get; set; }
        public string PaperNumber { get; set; }
        public string PaperName { get; set; }
        public string PaperPath { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public int AuditStatus { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Merchant_User Merchant_User { get; set; }
    }
}
