CREATE TABLE [dbo].[Merchant_Revenue] (
    [RevenueID]  INT             IDENTITY (1, 1) NOT NULL,
    [StoreID]    INT             NOT NULL,
    [MerchantID] INT             NOT NULL,
    [StartDate]  DATETIME        NOT NULL,
    [EndDate]    DATETIME        NOT NULL,
    [Revenue]    DECIMAL (18, 2) NOT NULL,
    [AfterTax]   DECIMAL (18, 2) NULL,
    [TaxRate]    DECIMAL (18, 2) NULL,
    [Status]     INT             DEFAULT ((1)) NOT NULL,
    [InDate]     DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]     INT             DEFAULT ((0)) NOT NULL,
    [EditDate]   DATETIME        NULL,
    [EditUser]   INT             NULL,
    CONSTRAINT [PK_Merchant_Revenue] PRIMARY KEY CLUSTERED ([RevenueID] ASC),
    CONSTRAINT [FK_Merchant_Revenue_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]),
    CONSTRAINT [FK_Merchant_Revenue_Store] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Merchant_Store] ([StoreID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Revenue_MerchantID]
    ON [dbo].[Merchant_Revenue]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Revenue_Status]
    ON [dbo].[Merchant_Revenue]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Revenue_StoreID]
    ON [dbo].[Merchant_Revenue]([StoreID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Revenue', @level2type = N'COLUMN', @level2name = N'StoreID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Revenue', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账期开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Revenue', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账期结束时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Revenue', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'税前营业额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Revenue', @level2type = N'COLUMN', @level2name = N'Revenue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'税后营业额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Revenue', @level2type = N'COLUMN', @level2name = N'AfterTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'税率', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Revenue', @level2type = N'COLUMN', @level2name = N'TaxRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Revenue', @level2type = N'COLUMN', @level2name = N'Status';

