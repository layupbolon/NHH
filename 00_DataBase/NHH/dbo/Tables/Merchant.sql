CREATE TABLE [dbo].[Merchant] (
    [MerchantID]          INT            IDENTITY (1, 1) NOT NULL,
    [MerchantType]        INT            DEFAULT ((1)) NOT NULL,
    [MerchantCode]        VARCHAR (10)   NULL,
    [BriefName]           NVARCHAR (50)  NULL,
    [MerchantName]        NVARCHAR (200) NOT NULL,
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
    [ContactEmail]        VARCHAR (100)  NULL,
    [ContactPhone]        VARCHAR (25)   NULL,
    [Status]              INT            DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]              INT            DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME       NULL,
    [EditUser]            INT            NULL,
    CONSTRAINT [PK_Merchant] PRIMARY KEY CLUSTERED ([MerchantID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_MerchantType]
    ON [dbo].[Merchant]([MerchantType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Status]
    ON [dbo].[Merchant]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户类型，法人、自然人等', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'MerchantType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'MerchantCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户简称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'BriefName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'MerchantName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'省', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'ProvinceID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'CityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'AddressLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮编', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'Zipcode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'营业执照注册号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'LicenseID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'注册省', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'RegisterProvinceID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'注册市', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'RegisterCityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'注册地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'RegisterAddressLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'法人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'OwnerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'ContactName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人身份证号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'ContactIDNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人邮箱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'ContactEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'ContactPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant', @level2type = N'COLUMN', @level2name = N'Status';

