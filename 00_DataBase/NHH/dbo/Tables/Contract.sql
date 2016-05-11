CREATE TABLE [dbo].[Contract] (
    [ContractID]          INT             IDENTITY (1, 1) NOT NULL,
    [ContractCode]        NVARCHAR (50)   NULL,
    [ContractType]        INT             NOT NULL,
    [ContractStatus]      INT             NOT NULL,
    [ProjectID]           INT             NOT NULL,
    [ContractArea]        DECIMAL (18, 2) NOT NULL,
    [ContractUnitRent]    DECIMAL (18, 2) NOT NULL,
    [ContractMgmtFee]     DECIMAL (18, 2) NOT NULL,
    [MerchantID]          INT             NOT NULL,
    [SignerName]          NVARCHAR (25)   NOT NULL,
    [SignerIDNumber]      VARCHAR (25)    NOT NULL,
    [SignerPhone]         VARCHAR (25)    NOT NULL,
    [ManageCompanyID]     INT             NOT NULL,
    [OperatorName]        NVARCHAR (25)   NOT NULL,
    [OperatorPhone]       VARCHAR (25)    NOT NULL,
    [ContractLength]      INT             NULL,
    [ContractStartDate]   DATETIME        NULL,
    [ContractEndDate]     DATETIME        NULL,
    [RentFreeLength]      INT             NULL,
    [RentFreeStartDate]   DATETIME        NULL,
    [RentFreeEndDate]     DATETIME        NULL,
    [AccessDate]          DATETIME        NULL,
    [DecorationLength]    INT             NULL,
    [DecorationStartDate] DATETIME        NULL,
    [DecorationEndDate]   DATETIME        NULL,
    [DecorationMgmtFee]   DECIMAL (18, 2) NULL,
    [DecorationBond]      DECIMAL (18, 2) NULL,
    [Condition]           INT             NULL,
    [BidBond]             DECIMAL (18, 2) NULL,
    [ManageBond]          DECIMAL (18, 2) NULL,
    [QualityBond]         DECIMAL (18, 2) NULL,
    [ParkingLotNum]       INT             NULL,
    [ParkingLotMemo]      NVARCHAR (500)  NULL,
    [AdPointNum]          INT             NULL,
    [AdPointMemo]         NVARCHAR (500)  NULL,
    [Status]              INT             CONSTRAINT [DF__Contract__Status__656C112C] DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME        CONSTRAINT [DF__Contract__InDate__66603565] DEFAULT (getdate()) NOT NULL,
    [InUser]              INT             CONSTRAINT [DF__Contract__InUser__6754599E] DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME        NULL,
    [EditUser]            INT             NULL,
    CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED ([ContractID] ASC),
    CONSTRAINT [FK_Contract_ManageCompany] FOREIGN KEY ([ManageCompanyID]) REFERENCES [dbo].[Company] ([CompanyID]),
    CONSTRAINT [FK_Contract_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]),
    CONSTRAINT [FK_Contract_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_ContractType]
    ON [dbo].[Contract]([ContractType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_MerchantID]
    ON [dbo].[Contract]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_ProjectID]
    ON [dbo].[Contract]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract_Status]
    ON [dbo].[Contract]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约类型，铺位、多经点位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租约状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合约总面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合约租金单价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractUnitRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合约物业费单价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractMgmtFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'签约人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SignerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'签约人身份证号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SignerIDNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'签约人联系电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SignerPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'管理公司ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ManageCompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经办人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'OperatorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经办人联系电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'OperatorPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合同期（天）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合同起始日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合同结束日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'免租期（天）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RentFreeLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'免租开始日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RentFreeStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'免租结束日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RentFreeEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'进场日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'AccessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修期（天）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DecorationLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修开始日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DecorationStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修结束日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DecorationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修管理费', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DecorationMgmtFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修保证金', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DecorationBond';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交付标准', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Condition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'质量保证金', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'QualityBond';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'停车位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ParkingLotNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'停车位备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ParkingLotMemo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'广告位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'AdPointNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'广告位备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'AdPointMemo';

