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
    
    
    public partial class Merchant : IStatusFlag,IEditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Merchant()
        {
            this.Complaint = new HashSet<Complaint>();
            this.Contract = new HashSet<Contract>();
            this.Finance_Accrual = new HashSet<Finance_Accrual>();
            this.Finance_Settlement = new HashSet<Finance_Settlement>();
            this.Merchant_Attachment = new HashSet<Merchant_Attachment>();
            this.Merchant_Brand = new HashSet<Merchant_Brand>();
            this.Merchant_Finance = new HashSet<Merchant_Finance>();
            this.Merchant_Message = new HashSet<Merchant_Message>();
            this.Merchant_Revenue = new HashSet<Merchant_Revenue>();
            this.Merchant_Role = new HashSet<Merchant_Role>();
            this.Merchant_Store = new HashSet<Merchant_Store>();
            this.Merchant_StoreImage = new HashSet<Merchant_StoreImage>();
            this.Merchant_StoreMeter = new HashSet<Merchant_StoreMeter>();
            this.Merchant_User = new HashSet<Merchant_User>();
        }
    
        public int MerchantID { get; set; }
        public int MerchantType { get; set; }
        public string MerchantCode { get; set; }
        public string BriefName { get; set; }
        public string MerchantName { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> CityID { get; set; }
        public string AddressLine { get; set; }
        public string Zipcode { get; set; }
        public string LicenseID { get; set; }
        public Nullable<int> RegisterProvinceID { get; set; }
        public Nullable<int> RegisterCityID { get; set; }
        public string RegisterAddressLine { get; set; }
        public string OwnerName { get; set; }
        public string ContactName { get; set; }
        public string ContactIDNumber { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint> Complaint { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contract { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Finance_Accrual> Finance_Accrual { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Finance_Settlement> Finance_Settlement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_Attachment> Merchant_Attachment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_Brand> Merchant_Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_Finance> Merchant_Finance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_Message> Merchant_Message { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_Revenue> Merchant_Revenue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_Role> Merchant_Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_Store> Merchant_Store { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_StoreImage> Merchant_StoreImage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_StoreMeter> Merchant_StoreMeter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Merchant_User> Merchant_User { get; set; }
    }
}
