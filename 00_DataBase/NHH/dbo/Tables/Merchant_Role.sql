CREATE TABLE [dbo].[Merchant_Role] (
    [RoleID]     INT           IDENTITY (1, 1) NOT NULL,
    [MerchantID] INT           NOT NULL,
    [RoleName]   NVARCHAR (50) NOT NULL,
    [Status]     INT           DEFAULT ((1)) NOT NULL,
    [InDate]     DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]     INT           DEFAULT ((0)) NOT NULL,
    [EditDate]   DATETIME      NULL,
    [EditUser]   INT           NULL,
    CONSTRAINT [PK_Merchant_Role] PRIMARY KEY CLUSTERED ([RoleID] ASC),
    CONSTRAINT [FK_Merchant_Role_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Role_MerchantID]
    ON [dbo].[Merchant_Role]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Role_Status]
    ON [dbo].[Merchant_Role]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Role';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Role', @level2type = N'COLUMN', @level2name = N'RoleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属商户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Role', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Role', @level2type = N'COLUMN', @level2name = N'RoleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Role', @level2type = N'COLUMN', @level2name = N'Status';

