﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nhh.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NHHEntities : DbContext
    {
        public NHHEntities()
            : base("name=NHHEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Sys_User_Message> Sys_User_Message { get; set; }
        public virtual DbSet<Contract_RemindRecord> Contract_RemindRecord { get; set; }
    }
}