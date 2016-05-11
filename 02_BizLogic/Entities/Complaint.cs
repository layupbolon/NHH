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
    
    
    public partial class Complaint : IStatusFlag,IEditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Complaint()
        {
            this.Complaint_Attachment = new HashSet<Complaint_Attachment>();
            this.Complaint_Comment = new HashSet<Complaint_Comment>();
            this.Complaint_Log = new HashSet<Complaint_Log>();
        }
    
        public int ComplaintID { get; set; }
        public int ComplaintType { get; set; }
        public int ProjectID { get; set; }
        public Nullable<int> MerchantID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public int RequestSrcType { get; set; }
        public Nullable<int> RequestUserID { get; set; }
        public string RequestUserName { get; set; }
        public string RequestTarget { get; set; }
        public string RequestDesc { get; set; }
        public Nullable<System.DateTime> RequestTime { get; set; }
        public Nullable<int> AcceptUserID { get; set; }
        public string AcceptUserName { get; set; }
        public Nullable<System.DateTime> AcceptTime { get; set; }
        public int Important { get; set; }
        public Nullable<System.DateTime> EstimatedFinishTime { get; set; }
        public string InvestigationDesc { get; set; }
        public Nullable<int> ServiceUserID { get; set; }
        public string ServiceUserName { get; set; }
        public Nullable<System.DateTime> ServiceStartTime { get; set; }
        public Nullable<System.DateTime> ServiceFinishTime { get; set; }
        public Nullable<int> ServiceResult { get; set; }
        public string ServiceDesc { get; set; }
        public int ComplaintStatus { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint_Attachment> Complaint_Attachment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint_Comment> Complaint_Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint_Log> Complaint_Log { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual Project Project { get; set; }
        public virtual Merchant_Store Merchant_Store { get; set; }
    }
}