CREATE TABLE [dbo].[Merchant_User] (
    [UserID]              INT             IDENTITY (1, 1) NOT NULL,
    [MerchantID]          INT             NOT NULL,
    [StoreID]             INT             NULL,
    [RoleID]              INT             DEFAULT ((1)) NOT NULL,
    [WechatOpenID]        VARCHAR (200)   NULL,
    [UserName]            VARCHAR (50)    NOT NULL,
    [Password]            VARCHAR (200)   NOT NULL,
    [Email]               VARCHAR (100)   NULL,
    [Moblie]              VARCHAR (50)    NULL,
    [NickName]            NVARCHAR (50)   NULL,
    [IDNumber]            VARCHAR (50)    NULL,
    [PhotoFile]           NVARCHAR (500)  NULL,
    [Gender]              INT             DEFAULT ((0)) NOT NULL,
    [Birthday]            DATETIME        NULL,
    [Nation]              NVARCHAR (50)   NULL,
    [Education]           INT             NULL,
    [MaritalStatus]       INT             NULL,
    [PoliticalStatus]     NVARCHAR (50)   NULL,
    [Height]              DECIMAL (18, 2) NULL,
    [Weight]              DECIMAL (18, 2) NULL,
    [Address]             NVARCHAR (500)  NULL,
    [ZipCode]             VARCHAR (20)    NULL,
    [EmergencyContact]    NVARCHAR (50)   NULL,
    [EmergencyPhone]      VARCHAR (50)    NULL,
    [EducationExperience] NVARCHAR (MAX)  NULL,
    [WorkExperience]      NVARCHAR (MAX)  NULL,
    [FamilyMembers]       NVARCHAR (MAX)  NULL,
    [Status]              INT             DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]              INT             DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME        NULL,
    [EditUser]            INT             NULL,
    CONSTRAINT [PK_Merchant_User] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Merchant_User_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]),
    CONSTRAINT [FK_Merchant_User_Store] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Merchant_Store] ([StoreID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_User]
    ON [dbo].[Merchant_User]([UserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_User_MerchantID]
    ON [dbo].[Merchant_User]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_User_Status]
    ON [dbo].[Merchant_User]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_User_StoreID]
    ON [dbo].[Merchant_User]([StoreID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户用户信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属商户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属商铺', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'StoreID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属角色', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'RoleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'微信授权', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'WechatOpenID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登录账号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登录密码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮箱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'手机', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Moblie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示名称（姓名）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'NickName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'身份证号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'IDNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户头像路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'PhotoFile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'性别', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'生日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Birthday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'民族', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Nation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'学历: 初中、高中、大专、本科、硕士、博士等', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Education';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'婚否: 未婚、已婚等', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'MaritalStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'政治面貌: 群众、共青团员、共产党员等', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'PoliticalStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'身高 cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Height';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'体重 kg', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮编', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'ZipCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'紧急联系人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'EmergencyContact';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'紧急联系人电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'EmergencyPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'教育经历', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'EducationExperience';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工作经历', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'WorkExperience';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'家庭成员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'FamilyMembers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_User', @level2type = N'COLUMN', @level2name = N'Status';

