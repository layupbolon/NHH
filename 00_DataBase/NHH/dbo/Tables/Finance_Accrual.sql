CREATE TABLE [dbo].[Finance_Accrual] (
    [AccrualID]  INT             IDENTITY (1, 1) NOT NULL,
    [MerchantID] INT             NOT NULL,
    [ContractID] INT             NULL,
    [StoreID]    INT             NULL,
    [CompanyID]  INT             NOT NULL,
    [ItemID]     INT             NOT NULL,
    [RPFlag]     INT             DEFAULT ((1)) NOT NULL,
    [PCFlag]     INT             DEFAULT ((0)) NOT NULL,
    [FWFlag]     INT             DEFAULT ((0)) NOT NULL,
    [Price]      DECIMAL (18, 2) NULL,
    [Unit]       NVARCHAR (10)   NULL,
    [Quantity]   DECIMAL (18, 2) NULL,
    [StartDate]  DATETIME        NULL,
    [EndDate]    DATETIME        NULL,
    [Amount]     DECIMAL (18, 2) NOT NULL,
    [Remark]     NVARCHAR (500)  NULL,
    [Status]     INT             DEFAULT ((1)) NOT NULL,
    [InDate]     DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]     INT             DEFAULT ((0)) NOT NULL,
    [EditDate]   DATETIME        NULL,
    [EditUser]   INT             NULL,
    CONSTRAINT [PK_Finance_Accrual] PRIMARY KEY CLUSTERED ([AccrualID] ASC),
    CONSTRAINT [FK_Finance_Accrual_Company] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID]),
    CONSTRAINT [FK_Finance_Accrual_Contract] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_Finance_Accrual_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Finance_Item] ([ItemID]),
    CONSTRAINT [FK_Finance_Accrual_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]),
    CONSTRAINT [FK_Finance_Accrual_Store] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Merchant_Store] ([StoreID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Accrual_CompanyID]
    ON [dbo].[Finance_Accrual]([CompanyID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Accrual_ContractID]
    ON [dbo].[Finance_Accrual]([ContractID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Accrual_ItemID]
    ON [dbo].[Finance_Accrual]([ItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Accrual_MerchantID]
    ON [dbo].[Finance_Accrual]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Accrual_Status]
    ON [dbo].[Finance_Accrual]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Accrual_StoreID]
    ON [dbo].[Finance_Accrual]([StoreID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账款ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'AccrualID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID,付款方ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'ContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'StoreID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司ID,收款方ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'科目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'ItemID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收付标识，默认收款', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'RPFlag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'预收标识，默认非预收', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'PCFlag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代收标识，默认非代收', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'FWFlag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账款开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账款结束时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账款金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'Amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'Remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Accrual', @level2type = N'COLUMN', @level2name = N'Status';

