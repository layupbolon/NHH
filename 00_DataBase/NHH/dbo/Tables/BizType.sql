CREATE TABLE [dbo].[BizType] (
    [BizTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [BizTypeName] NVARCHAR (50) NOT NULL,
    [Status]      INT           DEFAULT ((1)) NOT NULL,
    [InDate]      DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]      INT           DEFAULT ((0)) NOT NULL,
    [EditDate]    DATETIME      NULL,
    [EditUser]    INT           NULL,
    CONSTRAINT [PK_BizType] PRIMARY KEY CLUSTERED ([BizTypeID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_BizType_Status]
    ON [dbo].[BizType]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营业态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业态ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizType', @level2type = N'COLUMN', @level2name = N'BizTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业态名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BizType', @level2type = N'COLUMN', @level2name = N'BizTypeName';

