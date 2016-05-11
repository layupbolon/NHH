CREATE TABLE [dbo].[BizCategory] (
    [BizCategoryID]   INT           IDENTITY (1, 1) NOT NULL,
    [BizTypeID]       INT           NOT NULL,
    [BizCategoryName] NVARCHAR (50) NULL,
    [ParentID]        INT           NULL,
    [Status]          INT           DEFAULT ((1)) NOT NULL,
    [InDate]          DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]          INT           DEFAULT ((0)) NOT NULL,
    [EditDate]        DATETIME      NULL,
    [EditUser]        INT           NULL,
    CONSTRAINT [PK_BizCategory] PRIMARY KEY CLUSTERED ([BizCategoryID] ASC),
    CONSTRAINT [FK_BizCategory_BizCategory] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[BizCategory] ([BizCategoryID]),
    CONSTRAINT [FK_BizCategory_BizType] FOREIGN KEY ([BizTypeID]) REFERENCES [dbo].[BizType] ([BizTypeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_BizCategory_BizTypeID]
    ON [dbo].[BizCategory]([BizTypeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BizCategory_ParentID]
    ON [dbo].[BizCategory]([ParentID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BizCategory_Status]
    ON [dbo].[BizCategory]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营品类', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品类ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizCategory', @level2type = N'COLUMN', @level2name = N'BizCategoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属业态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizCategory', @level2type = N'COLUMN', @level2name = N'BizTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品类名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizCategory', @level2type = N'COLUMN', @level2name = N'BizCategoryName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级品类', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizCategory', @level2type = N'COLUMN', @level2name = N'ParentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizCategory', @level2type = N'COLUMN', @level2name = N'Status';

