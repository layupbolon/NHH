CREATE TABLE [dbo].[Finance_SettlementDetail] (
    [DetailID]     INT             IDENTITY (1, 1) NOT NULL,
    [SettlementID] INT             NOT NULL,
    [AccrualID]    INT             NULL,
    [ItemID]       INT             NOT NULL,
    [ItemName]     NVARCHAR (50)   NULL,
    [Price]        DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [Unit]         NVARCHAR (10)   NULL,
    [Quantity]     DECIMAL (18, 2) DEFAULT ((1)) NOT NULL,
    [Amount]       DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [Remark]       NVARCHAR (500)  NULL,
    [Status]       INT             DEFAULT ((1)) NOT NULL,
    [InDate]       DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]       INT             DEFAULT ((0)) NOT NULL,
    [EditDate]     DATETIME        NULL,
    [EditUser]     INT             NULL,
    CONSTRAINT [PK_Finance_SettlementDetail] PRIMARY KEY CLUSTERED ([DetailID] ASC),
    CONSTRAINT [FK_Finance_SettlementDetail_Accrual] FOREIGN KEY ([AccrualID]) REFERENCES [dbo].[Finance_Accrual] ([AccrualID]),
    CONSTRAINT [FK_Finance_SettlementDetail_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Finance_Item] ([ItemID]),
    CONSTRAINT [FK_Finance_SettlementDetail_Settlement] FOREIGN KEY ([SettlementID]) REFERENCES [dbo].[Finance_Settlement] ([SettlementID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_SettlementDetail_ItemID]
    ON [dbo].[Finance_SettlementDetail]([ItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_SettlementDetail_SettlementID]
    ON [dbo].[Finance_SettlementDetail]([SettlementID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算单明细', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'明细ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'DetailID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算单ID 所属结算单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'SettlementID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'费用ID 应收、付明细ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'AccrualID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'科目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'ItemID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'科目名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'ItemName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账款金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'Amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'明细备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'Remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_SettlementDetail', @level2type = N'COLUMN', @level2name = N'Status';

