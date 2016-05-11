CREATE TABLE [dbo].[Contract_Supplementary] (
    [SupplementaryID]        INT             IDENTITY (1, 1) NOT NULL,
    [ContractID]             INT             NOT NULL,
    [SupplementaryType]      INT             NOT NULL,
    [SignerName]             NVARCHAR (25)   NOT NULL,
    [SignerIDNumber]         VARCHAR (25)    NOT NULL,
    [SignerPhone]            VARCHAR (25)    NOT NULL,
    [ManageCompanyID]        INT             NOT NULL,
    [ContractArea]           DECIMAL (18, 2) NULL,
    [ContractUnitRent]       DECIMAL (18, 2) NULL,
    [ContractMgmtFee]        DECIMAL (18, 2) NULL,
    [MerchantID]             INT             NULL,
    [ContractLength]         INT             NULL,
    [ContractStartDate]      DATETIME        NULL,
    [ContractEndDate]        DATETIME        NULL,
    [OperatorName]           NVARCHAR (25)   NOT NULL,
    [OperatorPhone]          VARCHAR (25)    NOT NULL,
    [SupplementaryContent]   NVARCHAR (MAX)  NOT NULL,
    [SupplementaryStartDate] DATETIME        NOT NULL,
    [SupplementaryEndDate]   DATETIME        NOT NULL,
    [Status]                 INT             DEFAULT ((1)) NOT NULL,
    [InDate]                 DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]                 INT             DEFAULT ((0)) NOT NULL,
    [EditDate]               DATETIME        NULL,
    [EditUser]               INT             NULL,
    CONSTRAINT [PK_Contract_Supplementary] PRIMARY KEY CLUSTERED ([SupplementaryID] ASC),
    CONSTRAINT [FK_Contract_Supplementary_Contract] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_Contract_Supplementary_ManageCompany] FOREIGN KEY ([ManageCompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Supplementary_ContractID]
    ON [dbo].[Contract_Supplementary]([ContractID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Supplementary_Status]
    ON [dbo].[Contract_Supplementary]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Supplementary_SupplementaryType]
    ON [dbo].[Contract_Supplementary]([SupplementaryType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户租约补充协议', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'ContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'协议类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'SupplementaryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'协议人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'SignerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'协议人身份证号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'SignerIDNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'协议人联系电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'SignerPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'管理公司ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'ManageCompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'变更：租约面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'ContractArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'变更：租约租金单价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'ContractUnitRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'变更：租约物业费单价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'ContractMgmtFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'变更：商户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'变更：合同期（天）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'ContractLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'变更：合同起始日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'ContractStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'变更：合同结束日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'ContractEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经办人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'OperatorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经办人联系电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'OperatorPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'协议内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'SupplementaryContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'协议起始日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'SupplementaryStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'协议结束日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'SupplementaryEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_Supplementary', @level2type = N'COLUMN', @level2name = N'Status';

