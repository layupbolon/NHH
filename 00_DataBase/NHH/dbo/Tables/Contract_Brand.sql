CREATE TABLE [dbo].[Contract_Brand] (
    [ContractBrandID] INT      IDENTITY (1, 1) NOT NULL,
    [ContractID]      INT      NOT NULL,
    [MerchantBrandID] INT      NOT NULL,
    [Status]          INT      DEFAULT ((1)) NOT NULL,
    [InDate]          DATETIME DEFAULT (getdate()) NOT NULL,
    [InUser]          INT      DEFAULT ((0)) NOT NULL,
    [EditDate]        DATETIME NULL,
    [EditUser]        INT      NULL,
    CONSTRAINT [PK_Contract_Brand] PRIMARY KEY CLUSTERED ([ContractBrandID] ASC),
    CONSTRAINT [FK_Contract_Brand_Contract] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_Contract_Brand_MerchantBrand] FOREIGN KEY ([MerchantBrandID]) REFERENCES [dbo].[Merchant_Brand] ([MerchantBrandID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Brand_ContractID]
    ON [dbo].[Contract_Brand]([ContractID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Brand_MerchantBrandID]
    ON [dbo].[Contract_Brand]([MerchantBrandID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户租约品牌', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Brand';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约品牌ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Brand', @level2type = N'COLUMN', @level2name = N'ContractBrandID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Brand', @level2type = N'COLUMN', @level2name = N'ContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户品牌ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Brand', @level2type = N'COLUMN', @level2name = N'MerchantBrandID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Brand', @level2type = N'COLUMN', @level2name = N'Status';

