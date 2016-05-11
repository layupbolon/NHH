CREATE TABLE [dbo].[Company_Finance] (
    [CompanyFinanceID] INT           IDENTITY (1, 1) NOT NULL,
    [CompanyID]        INT           NOT NULL,
    [BankAccount]      VARCHAR (25)  NULL,
    [BankName]         NVARCHAR (50) NULL,
    [SubBranch]        NVARCHAR (50) NULL,
    [AccountName]      NVARCHAR (50) NULL,
    [AlipayAccount]    VARCHAR (50)  NULL,
    [WechatAccount]    VARCHAR (50)  NULL,
    [FinanceContact]   NVARCHAR (50) NULL,
    [FinancePhone]     VARCHAR (25)  NULL,
    [InDate]           DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]           INT           DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME      NULL,
    [EditUser]         INT           NULL,
    CONSTRAINT [PK_Company_Finance] PRIMARY KEY CLUSTERED ([CompanyFinanceID] ASC),
    CONSTRAINT [FK_Company_Finance_Company] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Company_Finance_CompanyID]
    ON [dbo].[Company_Finance]([CompanyID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司财务信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'银行账号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'BankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开户银行', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'BankName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开户支行', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'SubBranch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开户名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'AccountName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'支付宝账号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'AlipayAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'微信商家账号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'WechatAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'财务联系人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'FinanceContact';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'财务联系电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company_Finance', @level2type = N'COLUMN', @level2name = N'FinancePhone';

