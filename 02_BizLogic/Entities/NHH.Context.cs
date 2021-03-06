﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using NHH.Framework;
    using NHH.Framework.Data;
    
    [DbConfigurationType(typeof(NHHDbConfiguration))]
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
    
        public virtual DbSet<Approve_Config> Approve_Config { get; set; }
        public virtual DbSet<Approve_Process> Approve_Process { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Banner_Location> Banner_Location { get; set; }
        public virtual DbSet<BannerInfo> BannerInfo { get; set; }
        public virtual DbSet<BizCategory> BizCategory { get; set; }
        public virtual DbSet<BizType> BizType { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<Campaign_Attachment> Campaign_Attachment { get; set; }
        public virtual DbSet<Campaign_Comment> Campaign_Comment { get; set; }
        public virtual DbSet<Campaign_Page> Campaign_Page { get; set; }
        public virtual DbSet<Campaign_Result> Campaign_Result { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Company_Finance> Company_Finance { get; set; }
        public virtual DbSet<Complaint> Complaint { get; set; }
        public virtual DbSet<Complaint_Attachment> Complaint_Attachment { get; set; }
        public virtual DbSet<Complaint_Comment> Complaint_Comment { get; set; }
        public virtual DbSet<Complaint_Log> Complaint_Log { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Contract_Appendix> Contract_Appendix { get; set; }
        public virtual DbSet<Contract_Brand> Contract_Brand { get; set; }
        public virtual DbSet<Contract_PaymentTerms> Contract_PaymentTerms { get; set; }
        public virtual DbSet<Contract_RemindRecord> Contract_RemindRecord { get; set; }
        public virtual DbSet<Contract_Supplementary> Contract_Supplementary { get; set; }
        public virtual DbSet<Contract_Template> Contract_Template { get; set; }
        public virtual DbSet<Contract_Unit> Contract_Unit { get; set; }
        public virtual DbSet<Contract_UnitSpec> Contract_UnitSpec { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Dictionary> Dictionary { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Finance_Accrual> Finance_Accrual { get; set; }
        public virtual DbSet<Finance_Item> Finance_Item { get; set; }
        public virtual DbSet<Finance_Settlement> Finance_Settlement { get; set; }
        public virtual DbSet<Finance_SettlementDetail> Finance_SettlementDetail { get; set; }
        public virtual DbSet<FloorMap_Unit> FloorMap_Unit { get; set; }
        public virtual DbSet<Intention> Intention { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Merchant> Merchant { get; set; }
        public virtual DbSet<Merchant_Attachment> Merchant_Attachment { get; set; }
        public virtual DbSet<Merchant_Brand> Merchant_Brand { get; set; }
        public virtual DbSet<Merchant_Documents> Merchant_Documents { get; set; }
        public virtual DbSet<Merchant_Documents_DecorateRequest> Merchant_Documents_DecorateRequest { get; set; }
        public virtual DbSet<Merchant_Documents_DelayOperateRequest> Merchant_Documents_DelayOperateRequest { get; set; }
        public virtual DbSet<Merchant_Documents_GetOutRequest> Merchant_Documents_GetOutRequest { get; set; }
        public virtual DbSet<Merchant_Finance> Merchant_Finance { get; set; }
        public virtual DbSet<Merchant_Message> Merchant_Message { get; set; }
        public virtual DbSet<Merchant_Request> Merchant_Request { get; set; }
        public virtual DbSet<Merchant_Revenue> Merchant_Revenue { get; set; }
        public virtual DbSet<Merchant_Role> Merchant_Role { get; set; }
        public virtual DbSet<Merchant_Store> Merchant_Store { get; set; }
        public virtual DbSet<Merchant_StoreImage> Merchant_StoreImage { get; set; }
        public virtual DbSet<Merchant_StoreMeter> Merchant_StoreMeter { get; set; }
        public virtual DbSet<Merchant_User> Merchant_User { get; set; }
        public virtual DbSet<Merchant_UserPaper> Merchant_UserPaper { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Message_Template> Message_Template { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NHHCG_ADPosition> NHHCG_ADPosition { get; set; }
        public virtual DbSet<NHHCG_Complaint> NHHCG_Complaint { get; set; }
        public virtual DbSet<NHHCG_Feedback> NHHCG_Feedback { get; set; }
        public virtual DbSet<NHHCG_Kiosk> NHHCG_Kiosk { get; set; }
        public virtual DbSet<NHHCG_Pic> NHHCG_Pic { get; set; }
        public virtual DbSet<NHHCG_Project> NHHCG_Project { get; set; }
        public virtual DbSet<NHHCG_Unit> NHHCG_Unit { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Project_AdPoint> Project_AdPoint { get; set; }
        public virtual DbSet<Project_Batch> Project_Batch { get; set; }
        public virtual DbSet<Project_BatchUnit> Project_BatchUnit { get; set; }
        public virtual DbSet<Project_BizConfig> Project_BizConfig { get; set; }
        public virtual DbSet<Project_Building> Project_Building { get; set; }
        public virtual DbSet<Project_Floor> Project_Floor { get; set; }
        public virtual DbSet<Project_Kiosk> Project_Kiosk { get; set; }
        public virtual DbSet<Project_Owner> Project_Owner { get; set; }
        public virtual DbSet<Project_Unit> Project_Unit { get; set; }
        public virtual DbSet<Project_UnitAdjust> Project_UnitAdjust { get; set; }
        public virtual DbSet<Project_UnitLeasing> Project_UnitLeasing { get; set; }
        public virtual DbSet<Project_UnitPlan> Project_UnitPlan { get; set; }
        public virtual DbSet<Project_UnitRequest> Project_UnitRequest { get; set; }
        public virtual DbSet<Project_UnitSpec> Project_UnitSpec { get; set; }
        public virtual DbSet<Project_UnitSpec_Template> Project_UnitSpec_Template { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Repair> Repair { get; set; }
        public virtual DbSet<Repair_Attachment> Repair_Attachment { get; set; }
        public virtual DbSet<Repair_Comment> Repair_Comment { get; set; }
        public virtual DbSet<Repair_Log> Repair_Log { get; set; }
        public virtual DbSet<Repair_Material> Repair_Material { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<Report_Config> Report_Config { get; set; }
        public virtual DbSet<Report_Field> Report_Field { get; set; }
        public virtual DbSet<Sys_Function> Sys_Function { get; set; }
        public virtual DbSet<Sys_Menu> Sys_Menu { get; set; }
        public virtual DbSet<Sys_Role> Sys_Role { get; set; }
        public virtual DbSet<Sys_Role_Function> Sys_Role_Function { get; set; }
        public virtual DbSet<Sys_Role_Menu> Sys_Role_Menu { get; set; }
        public virtual DbSet<Sys_User> Sys_User { get; set; }
        public virtual DbSet<Sys_User_Message> Sys_User_Message { get; set; }
        public virtual DbSet<Sys_User_Role> Sys_User_Role { get; set; }
        public virtual DbSet<Utility_Bill> Utility_Bill { get; set; }
        public virtual DbSet<View_CompanyType> View_CompanyType { get; set; }
        public virtual DbSet<View_Condition> View_Condition { get; set; }
        public virtual DbSet<View_ContractStatus> View_ContractStatus { get; set; }
        public virtual DbSet<View_FloorMap_Unit> View_FloorMap_Unit { get; set; }
        public virtual DbSet<View_GenderType> View_GenderType { get; set; }
        public virtual DbSet<View_Project_AdPoint> View_Project_AdPoint { get; set; }
        public virtual DbSet<View_Project_Kiosk> View_Project_Kiosk { get; set; }
        public virtual DbSet<View_Project_Unit> View_Project_Unit { get; set; }
        public virtual DbSet<View_Project_Unit_Contract> View_Project_Unit_Contract { get; set; }
        public virtual DbSet<View_Project_UnitExt> View_Project_UnitExt { get; set; }
        public virtual DbSet<View_Project_UnitRequest> View_Project_UnitRequest { get; set; }
        public virtual DbSet<View_ProjectInfoShow> View_ProjectInfoShow { get; set; }
        public virtual DbSet<View_Repair_Complaint_Supervisor> View_Repair_Complaint_Supervisor { get; set; }
        public virtual DbSet<View_Report_Contract> View_Report_Contract { get; set; }
        public virtual DbSet<View_Report_Project_Count> View_Report_Project_Count { get; set; }
        public virtual DbSet<View_Report_Project_Unit> View_Report_Project_Unit { get; set; }
        public virtual DbSet<View_Role_Function> View_Role_Function { get; set; }
        public virtual DbSet<View_Role_Menu> View_Role_Menu { get; set; }
        public virtual DbSet<View_UnitStatus> View_UnitStatus { get; set; }
        public virtual DbSet<View_UnitType> View_UnitType { get; set; }
        public virtual DbSet<View_User_Function> View_User_Function { get; set; }
        public virtual DbSet<View_User_Menu> View_User_Menu { get; set; }
    }
}
