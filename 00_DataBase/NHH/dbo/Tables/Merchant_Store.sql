CREATE TABLE [dbo].[Merchant_Store] (
    [StoreID]        INT             IDENTITY (1, 1) NOT NULL,
    [StoreName]      NVARCHAR (50)   NULL,
    [MerchantID]     INT             NOT NULL,
    [BrandName]      NVARCHAR (50)   NULL,
    [BizTypeID]      INT             NOT NULL,
    [BizCategoryID]  INT             NOT NULL,
    [RentArea]       DECIMAL (18, 2) NOT NULL,
    [RentStartDate]  DATETIME        NOT NULL,
    [RentEndDate]    DATETIME        NOT NULL,
    [RentLength]     INT             NULL,
    [RentContractID] INT             NOT NULL,
    [Status]         INT             DEFAULT ((1)) NOT NULL,
    [InDate]         DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]         INT             DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME        NULL,
    [EditUser]       INT             NULL,
    CONSTRAINT [PK_Merchant_Store] PRIMARY KEY CLUSTERED ([StoreID] ASC),
    CONSTRAINT [FK_Merchant_Store_BizCategory] FOREIGN KEY ([BizCategoryID]) REFERENCES [dbo].[BizCategory] ([BizCategoryID]),
    CONSTRAINT [FK_Merchant_Store_BizType] FOREIGN KEY ([BizTypeID]) REFERENCES [dbo].[BizType] ([BizTypeID]),
    CONSTRAINT [FK_Merchant_Store_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]),
    CONSTRAINT [FK_Merchant_Store_RentContract] FOREIGN KEY ([RentContractID]) REFERENCES [dbo].[Contract] ([ContractID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Store_BizCategoryID]
    ON [dbo].[Merchant_Store]([BizCategoryID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Store_BizTypeID]
    ON [dbo].[Merchant_Store]([BizTypeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Store_MerchantID]
    ON [dbo].[Merchant_Store]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Store_RentContractID]
    ON [dbo].[Merchant_Store]([RentContractID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Store_Status]
    ON [dbo].[Merchant_Store]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户商铺信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺显示名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'StoreName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'BrandName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营业态，主力店/零售/亲子/餐饮/生活服务/娱乐/文化/休闲,详见商业规划出具的《功能业态分类表》', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'BizTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营品类，女装,或"箱包"，或"数码"，或"儿童玩具"，或"西式快餐"，或"水吧"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'BizCategoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'RentArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'RentStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁结束时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'RentEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁期（月）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'RentLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁合同ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'RentContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Store', @level2type = N'COLUMN', @level2name = N'Status';

