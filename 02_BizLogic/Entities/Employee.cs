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
    
    
    public partial class Employee : IStatusFlag,IEditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Department = new HashSet<Department>();
            this.Employee1 = new HashSet<Employee>();
            this.Sys_User = new HashSet<Sys_User>();
        }
    
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Moblie { get; set; }
        public string Phone { get; set; }
        public string Ext { get; set; }
        public int Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string IDNumber { get; set; }
        public string Tag { get; set; }
        public Nullable<int> SupervisorID { get; set; }
        public int DepartmentID { get; set; }
        public int CompanyID { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department { get; set; }
        public virtual Department Department1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_User> Sys_User { get; set; }
    }
}
