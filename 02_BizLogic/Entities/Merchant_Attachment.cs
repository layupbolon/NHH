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
    
    
    public partial class Merchant_Attachment : IStatusFlag,IEditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Merchant_Attachment()
        {
            this.Merchant_Brand = new HashSet<Merchant_Brand>();
        }
    
        public int AttachmentID { get; set; }
        public int MerchantID { get; set; }
        public int AttachmentType { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public string AttachmentCode { get; set; }
        public int AuditStatus { get; set; }
    
        public virtual Merchant Merchant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_Brand> Merchant_Brand { get; set; }
    }
}
