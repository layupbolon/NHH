CREATE TABLE [dbo].[Finance_Settlement] (
    [SettlementID]     INT             IDENTITY (1, 1) NOT NULL,
    [SettlementCode]   VARCHAR (20)    NOT NULL,
    [SettlementType]   INT             DEFAULT ((1)) NOT NULL,
    [MerchantID]       INT             NOT NULL,
    [MerchantTitle]    NVARCHAR (200)  NOT NULL,
    [CompanyID]        INT             NOT NULL,
    [CompanyTitle]     NVARCHAR (200)  NOT NULL,
    [ContractID]       INT             NULL,
    [UnitName]         NVARCHAR (200)  NULL,
    [StartDate]        DATETIME        NULL,
    [EndDate]          DATETIME        NULL,
    [EstimatedDate]    DATETIME        NULL,
    [SettlementNote]   NVARCHAR (500)  NULL,
    [ReceivableAmount] DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [DiscountAmount]   DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [ConfirmAmount]    DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [ConfirmTime]      DATETIME        NULL,
    [ConfirmStatus]    INT             DEFAULT ((1)) NOT NULL,
    [SettlementAmount] DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [SettlementTime]   DATETIME        NULL,
    [SettlementStatus] INT             DEFAULT ((1)) NOT NULL,
    [InvoiceAmount]    DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [InvoiceTime]      DATETIME        NULL,
    [InvoiceStatus]    INT             DEFAULT ((1)) NOT NULL,
    [OperatorID]       INT             NULL,
    [OperatorName]     NVARCHAR (25)   NULL,
    [Status]           INT             DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]           INT             DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME        NULL,
    [EditUser]         INT             NULL,
    CONSTRAINT [PK_Finance_Settlement] PRIMARY KEY CLUSTERED ([SettlementID] ASC),
    CONSTRAINT [FK_Finance_Settlement_Company] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID]),
    CONSTRAINT [FK_Finance_Settlement_Contract] FOREIGN KEY ([ContractID]) REFERENCES [dbo].[Contract] ([ContractID]),
    CONSTRAINT [FK_Finance_Settlement_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Settlement_CompanyID]
    ON [dbo].[Finance_Settlement]([CompanyID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Settlement_ConfirmStatus]
    ON [dbo].[Finance_Settlement]([ConfirmStatus] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Settlement_ContractID]
    ON [dbo].[Finance_Settlement]([ContractID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Settlement_InvoiceStatus]
    ON [dbo].[Finance_Settlement]([InvoiceStatus] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Settlement_MerchantID]
    ON [dbo].[Finance_Settlement]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Settlement_OperatorID]
    ON [dbo].[Finance_Settlement]([OperatorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Settlement_SettlementStatus]
    ON [dbo].[Finance_Settlement]([SettlementStatus] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Finance_Settlement_SettlementType]
    ON [dbo].[Finance_Settlement]([SettlementType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'SettlementID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算单号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'SettlementCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算单类型 1-收款，2-付款', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'SettlementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户抬头', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'MerchantTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司抬头', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'CompanyTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'ContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'铺位名称 昆仑唐人\精致楼\1F\W101,W102', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'UnitName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账期开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账期结束时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'预计结算日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'EstimatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算单备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'SettlementNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应收金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'ReceivableAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'折扣金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'DiscountAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'确认金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'ConfirmAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'确认时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'ConfirmTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'确认状态 未确认、已确认', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'ConfirmStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'已结算金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'SettlementAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'SettlementTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算状态 未结算、部分结算、已结算', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'SettlementStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'已开票金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'InvoiceAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开票时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'InvoiceTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开票状态 待开票、部分开票、已开票', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'InvoiceStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经办人ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'OperatorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经办人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'OperatorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Finance_Settlement', @level2type = N'COLUMN', @level2name = N'Status';

