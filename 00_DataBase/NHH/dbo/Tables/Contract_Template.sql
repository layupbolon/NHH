CREATE TABLE [dbo].[Contract_Template] (
    [TemplateID]      INT            IDENTITY (1, 1) NOT NULL,
    [TemplateType]    INT            NOT NULL,
    [TemplateName]    NVARCHAR (100) NOT NULL,
    [TemplateContent] NVARCHAR (MAX) NOT NULL,
    [Status]          INT            DEFAULT ((1)) NOT NULL,
    [InDate]          DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]          INT            DEFAULT ((0)) NOT NULL,
    [EditDate]        DATETIME       NULL,
    [EditUser]        INT            NULL,
    CONSTRAINT [PK_Contract_Template] PRIMARY KEY CLUSTERED ([TemplateID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Template_Status]
    ON [dbo].[Contract_Template]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Template_TemplateType]
    ON [dbo].[Contract_Template]([TemplateType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户租约模板', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Template';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'模板类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Template', @level2type = N'COLUMN', @level2name = N'TemplateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'模板名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Template', @level2type = N'COLUMN', @level2name = N'TemplateName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'模板内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Template', @level2type = N'COLUMN', @level2name = N'TemplateContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Template', @level2type = N'COLUMN', @level2name = N'Status';

