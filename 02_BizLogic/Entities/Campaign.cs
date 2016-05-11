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
    
    
    public partial class Campaign : IStatusFlag,IEditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Campaign()
        {
            this.Campaign_Attachment = new HashSet<Campaign_Attachment>();
            this.Campaign_Comment = new HashSet<Campaign_Comment>();
            this.Campaign_Page = new HashSet<Campaign_Page>();
            this.Campaign_Result = new HashSet<Campaign_Result>();
        }
    
        public int CampaignID { get; set; }
        public string CampaignCode { get; set; }
        public string CampaignName { get; set; }
        public int CampaignType { get; set; }
        public string CampaignBrief { get; set; }
        public int CampaignStatus { get; set; }
        public int ProjectID { get; set; }
        public string Location { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<decimal> Budget { get; set; }
        public Nullable<int> PlanUserID { get; set; }
        public string PlanUserName { get; set; }
        public Nullable<System.DateTime> PlanTime { get; set; }
        public Nullable<int> RunUserID { get; set; }
        public string RunUserName { get; set; }
        public Nullable<System.DateTime> RunTime { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
        public string SEO_Title { get; set; }
        public string KeyWords { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Campaign_Attachment> Campaign_Attachment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Campaign_Comment> Campaign_Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Campaign_Page> Campaign_Page { get; set; }
        public virtual Sys_User Sys_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Campaign_Result> Campaign_Result { get; set; }
        public virtual Sys_User Sys_User1 { get; set; }
    }
}
