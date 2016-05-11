CREATE TABLE [dbo].[Finance_Item] (
    [ItemID]   INT           IDENTITY (1, 1) NOT NULL,
    [ItemName] NVARCHAR (50) NOT NULL,
    [ItemType] INT           DEFAULT ((9)) NOT NULL,
    [Status]   INT           DEFAULT ((1)) NOT NULL,
    [InDate]   DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]   INT           DEFAULT ((0)) NOT NULL,
    [EditDate] DATETIME      NULL,
    [EditUser] INT           NULL,
    CONSTRAINT [PK_Finance_Item] PRIMARY KEY CLUSTERED ([ItemID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Item_Status]
    ON [dbo].[Finance_Item]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'科目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Item', @level2type = N'COLUMN', @level2name = N'ItemID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'科目名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Item', @level2type = N'COLUMN', @level2name = N'ItemName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'科目类型 1-收入，2-代收，3-预收，5-支出，6-代付，7-预支，9-其他', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Item', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Item', @level2type = N'COLUMN', @level2name = N'Status';

