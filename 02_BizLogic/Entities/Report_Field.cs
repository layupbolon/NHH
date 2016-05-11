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
    
    
    public partial class Report_Field : IStatusFlag,IEditable
    {
        public int FieldID { get; set; }
        public int ReportID { get; set; }
        public string FieldName { get; set; }
        public string FieldTitle { get; set; }
        public string FieldClass { get; set; }
        public string SortName { get; set; }
        public Nullable<int> Sortable { get; set; }
        public int Exportable { get; set; }
        public string Formatter { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Report Report { get; set; }
    }
}
