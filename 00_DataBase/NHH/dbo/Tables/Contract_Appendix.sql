CREATE TABLE [dbo].[Contract_Appendix] (
    [AppendixID]       INT            IDENTITY (1, 1) NOT NULL,
    [ContractID]       INT            NOT NULL,
    [AppendixType]     INT            NOT NULL,
    [AppendixTemplate] INT            NOT NULL,
    [AppendixName]     NVARCHAR (100) NOT NULL,
    [AppendixPath]     VARCHAR (500)  NOT NULL,
    [Status]           INT            DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]           INT            DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME       NULL,
    [EditUser]         INT            NULL,
    CONSTRAINT [PK_Contract_Appendix] PRIMARY KEY CLUSTERED ([AppendixID] ASC),
    CONSTRAINT [FK_Contract_Appendix_Contract] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Appendix_AppendixType]
    ON [dbo].[Contract_Appendix]([AppendixType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Appendix_ContractID]
    ON [dbo].[Contract_Appendix]([ContractID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Appendix_Status]
    ON [dbo].[Contract_Appendix]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户租约附件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Appendix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Appendix', @level2type = N'COLUMN', @level2name = N'ContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Appendix', @level2type = N'COLUMN', @level2name = N'AppendixType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件模板', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Appendix', @level2type = N'COLUMN', @level2name = N'AppendixTemplate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Appendix', @level2type = N'COLUMN', @level2name = N'AppendixName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件扫描副本文件地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Appendix', @level2type = N'COLUMN', @level2name = N'AppendixPath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Appendix', @level2type = N'COLUMN', @level2name = N'Status';

