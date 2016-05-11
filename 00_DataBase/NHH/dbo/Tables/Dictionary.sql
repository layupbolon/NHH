CREATE TABLE [dbo].[Dictionary] (
    [FieldID]    INT           IDENTITY (1, 1) NOT NULL,
    [FieldType]  VARCHAR (20)  NOT NULL,
    [FieldValue] INT           NOT NULL,
    [FieldName]  NVARCHAR (20) NOT NULL,
    [FieldDesc]  NVARCHAR (50) NULL,
    CONSTRAINT [PK_Dictionary] PRIMARY KEY CLUSTERED ([FieldID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据字典表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dictionary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dictionary', @level2type = N'COLUMN', @level2name = N'FieldType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dictionary', @level2type = N'COLUMN', @level2name = N'FieldValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dictionary', @level2type = N'COLUMN', @level2name = N'FieldName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dictionary', @level2type = N'COLUMN', @level2name = N'FieldDesc';

