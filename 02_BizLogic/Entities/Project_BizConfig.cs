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
    
    
    public partial class Project_BizConfig : IStatusFlag,IEditable
    {
        public int BizConfigID { get; set; }
        public int BizConfigType { get; set; }
        public int ProjectID { get; set; }
        public Nullable<int> BuildingID { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
        public string Param5 { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual Project_Building Project_Building { get; set; }
    }
}