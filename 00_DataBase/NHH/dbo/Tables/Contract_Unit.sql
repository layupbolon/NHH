CREATE TABLE [dbo].[Contract_Unit] (
    [ContractUnitID] INT             IDENTITY (1, 1) NOT NULL,
    [ContractID]     INT             NOT NULL,
    [UnitID]         INT             NOT NULL,
    [UnitAvgAera]    DECIMAL (18, 2) NULL,
    [Status]         INT             DEFAULT ((1)) NOT NULL,
    [InDate]         DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]         INT             DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME        NULL,
    [EditUser]       INT             NULL,
    CONSTRAINT [PK_Contract_Unit] PRIMARY KEY CLUSTERED ([ContractUnitID] ASC),
    CONSTRAINT [FK_Contract_Unit_Contract] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Unit_ContractID]
    ON [dbo].[Contract_Unit]([ContractID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Unit_Status]
    ON [dbo].[Contract_Unit]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Unit_UnitID]
    ON [dbo].[Contract_Unit]([UnitID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户租约铺位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约铺位ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Unit', @level2type = N'COLUMN', @level2name = N'ContractUnitID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Unit', @level2type = N'COLUMN', @level2name = N'ContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'铺位 or 多经点位ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Unit', @level2type = N'COLUMN', @level2name = N'UnitID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计租面积分摊', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Unit', @level2type = N'COLUMN', @level2name = N'UnitAvgAera';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Unit', @level2type = N'COLUMN', @level2name = N'Status';

