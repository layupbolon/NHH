CREATE TABLE [dbo].[Project_UnitPlan] (
    [UnitPlanID]         INT             IDENTITY (1, 1) NOT NULL,
    [UnitID]             INT             NOT NULL,
    [BizTypeID]          INT             NULL,
    [BizCategoryID]      INT             NULL,
    [UnitType]           INT             NULL,
    [IsBenchmarking]     INT             NULL,
    [StandardRent]       DECIMAL (18, 2) NULL,
    [RecommendedRent]    DECIMAL (18, 2) NULL,
    [QuotationRent]      DECIMAL (18, 2) NULL,
    [StandardMgmtFee]    DECIMAL (18, 2) NULL,
    [RentLengthUpper]    INT             NULL,
    [RentLengthBottom]   INT             NULL,
    [IncreaseType]       INT             NULL,
    [IncreaseAmountType] INT             NULL,
    [IncreaseAmount]     DECIMAL (18, 2) NULL,
    [IncreaseStartYears] INT             NULL,
    [IncreaseStepYears]  INT             NULL,
    [DepositMonthly]     INT             NULL,
    [PaymentPeriod]      INT             NULL,
    [BidBond]            DECIMAL (18, 2) NULL,
    [ManageBond]         DECIMAL (18, 2) NULL,
    [RentFreeLength]     INT             NULL,
    [DecorationLength]   INT             NULL,
    [DecorationMgmtFee]  DECIMAL (18, 2) NULL,
    [DecorationBond]     DECIMAL (18, 2) NULL,
    [QualityBond]        DECIMAL (18, 2) NULL,
    [ParkingLotNum]      INT             NULL,
    [AdPointNum]         INT             NULL,
    [Condition]          INT             NULL,
    [Status]             INT             DEFAULT ((1)) NOT NULL,
    [InDate]             DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]             INT             DEFAULT ((0)) NOT NULL,
    [EditDate]           DATETIME        NULL,
    [EditUser]           INT             NULL,
    CONSTRAINT [PK_Project_UnitPlan] PRIMARY KEY CLUSTERED ([UnitPlanID] ASC),
    CONSTRAINT [FK_Project_UnitPlan_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Project_Unit] ([UnitID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitPlan_Status]
    ON [dbo].[Project_UnitPlan]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitPlan_UnitID]
    ON [dbo].[Project_UnitPlan]([UnitID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitPlan_UnitType]
    ON [dbo].[Project_UnitPlan]([UnitType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商铺规划', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'铺位ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'UnitID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营业态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'BizTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营品类', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'BizCategoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商铺类型，主力店/次主力店/步行街/', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租决租金标准', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'StandardRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建议租金标准', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'RecommendedRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁报价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'QuotationRent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租决物业费标准', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'StandardMgmtFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁年限上限（月）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'RentLengthUpper';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租赁年限下限（月）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'RentLengthBottom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金递增类型，递增或递减', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'IncreaseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金递增值类型，￥ OR %', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'IncreaseAmountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金递增值，10元 OR 5%', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'IncreaseAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金递增起始年数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'IncreaseStartYears';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金递增间隔年数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'IncreaseStepYears';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金押金月数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'DepositMonthly';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金账期：月结、季结、半年结、年结', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商铺质保金要求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'BidBond';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物业保证金要求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'ManageBond';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'免租期上限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'RentFreeLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修期上限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'DecorationLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修管理费要求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'DecorationMgmtFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修保证金要求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'DecorationBond';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'质量保证金', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'QualityBond';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'免费停车位上限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'ParkingLotNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'免费广告位上限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'AdPointNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交付标准，精装/统装/毛坯（交房标准详见房产技术条件）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitPlan', @level2type = N'COLUMN', @level2name = N'Condition';

