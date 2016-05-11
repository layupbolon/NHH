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
    
    
    public partial class Contract_Appendix : IStatusFlag,IEditable
    {
        public int AppendixID { get; set; }
        public int ContractID { get; set; }
        public int AppendixType { get; set; }
        public int AppendixTemplate { get; set; }
        public string AppendixName { get; set; }
        public string AppendixPath { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Contract Contract { get; set; }
    }
}
