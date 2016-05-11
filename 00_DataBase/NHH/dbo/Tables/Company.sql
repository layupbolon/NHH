CREATE TABLE [dbo].[Company] (
    [CompanyID]           INT            IDENTITY (10, 1) NOT NULL,
    [CompanyType]         INT            NOT NULL,
    [CompanyCode]         VARCHAR (10)   NULL,
    [BriefName]           NVARCHAR (50)  NULL,
    [CompanyName]         NVARCHAR (200) NOT NULL,
    [ProvinceID]          INT            NULL,
    [CityID]              INT            NULL,
    [AddressLine]         NVARCHAR (100) NULL,
    [Zipcode]             VARCHAR (15)   NULL,
    [LicenseID]           NVARCHAR (50)  NULL,
    [RegisterProvinceID]  INT            NULL,
    [RegisterCityID]      INT            NULL,
    [RegisterAddressLine] NVARCHAR (100) NULL,
    [OwnerName]           NVARCHAR (25)  NULL,
    [ContactName]         NVARCHAR (25)  NULL,
    [ContactIDNumber]     VARCHAR (25)   NULL,
    [ContactEmail]        NVARCHAR (50)  NULL,
    [ContactPhone]        VARCHAR (25)   NULL,
    [OwnerCompanyID]      INT            NULL,
    [Status]              INT            DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]              INT            DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME       NULL,
    [EditUser]            INT            NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    CONSTRAINT [FK_Company_Owner] FOREIGN KEY ([OwnerCompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Company_CompanyType]
    ON [dbo].[Company]([CompanyType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Company_OwnerCompanyID]
    ON [dbo].[Company]([OwnerCompanyID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Company_Status]
    ON [dbo].[Company]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'CompanyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司简称 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'BriefName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'省', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ProvinceID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'CityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'AddressLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮编', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'Zipcode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'营业执照注册号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'LicenseID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'注册省', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'RegisterProvinceID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'注册市', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'RegisterCityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'注册地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'RegisterAddressLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'法人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'OwnerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ContactName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人身份证号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ContactIDNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人邮箱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ContactEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ContactPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'母公司ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'OwnerCompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'Status';

