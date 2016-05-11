CREATE TABLE [dbo].[Sys_Function] (
    [FunctionID]          INT            IDENTITY (1, 1) NOT NULL,
    [FunctionKey]         VARCHAR (200)  NOT NULL,
    [FunctionName]        NVARCHAR (200) NOT NULL,
    [FunctionDescription] NVARCHAR (500) NULL,
    [Status]              INT            DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]              INT            DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME       NULL,
    [EditUser]            INT            NULL,
    CONSTRAINT [PK_Sys_Function] PRIMARY KEY CLUSTERED ([FunctionID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_Function_FunctionKey]
    ON [dbo].[Sys_Function]([FunctionKey] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_Function_Status]
    ON [dbo].[Sys_Function]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'功能ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Function', @level2type = N'COLUMN', @level2name = N'FunctionID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'功能键值，需按业务规则填写，可读不重复，如：NHH.Common.Company.Edit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Function', @level2type = N'COLUMN', @level2name = N'FunctionKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'功能名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Function', @level2type = N'COLUMN', @level2name = N'FunctionName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'功能描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Function', @level2type = N'COLUMN', @level2name = N'FunctionDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Function', @level2type = N'COLUMN', @level2name = N'Status';

