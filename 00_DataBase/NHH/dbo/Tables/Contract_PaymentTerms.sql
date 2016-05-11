CREATE TABLE [dbo].[Contract_PaymentTerms] (
    [PaymentTermsID]       INT             IDENTITY (1, 1) NOT NULL,
    [ContractID]           INT             NOT NULL,
    [PaymentTermsType]     INT             NOT NULL,
    [PaymentItemID]        INT             NOT NULL,
    [PaymentPeriod]        INT             NOT NULL,
    [DepositMonthly]       INT             NULL,
    [PaymentDailyAmount]   DECIMAL (18, 2) NULL,
    [PaymentMonthlyAmount] DECIMAL (18, 2) NULL,
    [IncreaseExpression]   NVARCHAR (MAX)  NULL,
    [CommissionExpression] NVARCHAR (MAX)  NULL,
    [Status]               INT             DEFAULT ((1)) NOT NULL,
    [InDate]               DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]               INT             DEFAULT ((0)) NOT NULL,
    [EditDate]             DATETIME        NULL,
    [EditUser]             INT             NULL,
    CONSTRAINT [PK_Contract_PaymentTerms] PRIMARY KEY CLUSTERED ([PaymentTermsID] ASC),
    CONSTRAINT [FK_Contract_PaymentTerms_Contract] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_Contract_PaymentTerms_PaymentItem] FOREIGN KEY ([PaymentItemID]) REFERENCES [dbo].[Finance_Item] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_PaymentTerms_ContractID]
    ON [dbo].[Contract_PaymentTerms]([ContractID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_PaymentTerms_PaymentPeriod]
    ON [dbo].[Contract_PaymentTerms]([PaymentPeriod] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_PaymentTerms_PaymentTermsType]
    ON [dbo].[Contract_PaymentTerms]([PaymentTermsType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_PaymentTerms_Status]
    ON [dbo].[Contract_PaymentTerms]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户租约支付条款', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'ContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁方式：租金、扣点、租金与扣点两者取高', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'PaymentTermsType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'费用科目ID，租金，物业费；从财务科目表中选取', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'PaymentItemID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金账期：月结、季结、半年结、年结', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'费用押金月数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'DepositMonthly';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'费用金额（日），日租金、日物业费', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'PaymentDailyAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'费用金额（月），月租金、月物业费', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'PaymentMonthlyAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'费用阶梯配置表达式（JSON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'IncreaseExpression';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扣点阶梯配置表达式(JSON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'CommissionExpression';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract_PaymentTerms', @level2type = N'COLUMN', @level2name = N'Status';

