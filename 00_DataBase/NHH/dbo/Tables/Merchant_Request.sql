CREATE TABLE [dbo].[Merchant_Request] (
    [RequestID]        INT             IDENTITY (1, 1) NOT NULL,
    [MerchantID]       INT             NULL,
    [BrandID]          VARCHAR (50)    NULL,
    [ProjectID]        INT             NULL,
    [BuildingIDs]      VARCHAR (50)    NULL,
    [FloorIDs]         VARCHAR (50)    NULL,
    [UnitIDs]          VARCHAR (500)   NULL,
    [SpecialRequest]   NVARCHAR (500)  NULL,
    [AreaRequest]      DECIMAL (18, 2) NULL,
    [FloorBearing]     INT             NULL,
    [FloorHeight]      INT             NULL,
    [RentLength]       INT             NULL,
    [RentMethod]       NVARCHAR (50)   NULL,
    [PaymentMethod]    NVARCHAR (50)   NULL,
    [Condition]        NVARCHAR (50)   NULL,
    [DecorationLength] INT             NULL,
    [ElectricityUsage] INT             NULL,
    [WaterUsage]       INT             NULL,
    [DrainUsage]       INT             NULL,
    [GasUsage]         INT             NULL,
    [SmokeUsage]       INT             NULL,
    [Status]           INT             DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]           INT             DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME        NULL,
    [EditUser]         INT             NULL,
    CONSTRAINT [PK_Store_Request] PRIMARY KEY CLUSTERED ([RequestID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'BrandID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼宇', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'BuildingIDs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼层', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'FloorIDs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单元', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'UnitIDs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'特殊要求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'SpecialRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'面积要求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'AreaRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼板承重', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'FloorBearing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼高', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'FloorHeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租期（月）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'RentLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁方式，纯租金/纯扣点/租金和扣点两者取高', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'RentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'付款方式，月付/季付/年付', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'PaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交付条件，毛坯/统装/精装', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'Condition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修期限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'DecorationLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电量需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'ElectricityUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供水需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'WaterUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排水需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'DrainUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'燃气需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'GasUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排烟需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Request', @level2type = N'COLUMN', @level2name = N'SmokeUsage';

