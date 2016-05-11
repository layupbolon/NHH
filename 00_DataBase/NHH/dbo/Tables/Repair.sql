CREATE TABLE [dbo].[Repair] (
    [RepairID]            INT             IDENTITY (1, 1) NOT NULL,
    [RepairType]          INT             NOT NULL,
    [ProjectID]           INT             NOT NULL,
    [StoreID]             INT             NULL,
    [FloorID]             INT             NULL,
    [UnitID]              INT             NULL,
    [IsCommon]            INT             NOT NULL,
    [Location]            NVARCHAR (500)  NULL,
    [RequestSrcType]      INT             DEFAULT ((1)) NOT NULL,
    [RequestDesc]         NVARCHAR (MAX)  NULL,
    [RequestUserID]       INT             NULL,
    [RequestUserName]     NVARCHAR (50)   NULL,
    [RequestTime]         DATETIME        NULL,
    [AcceptUserID]        INT             NULL,
    [AcceptUserName]      NVARCHAR (50)   NULL,
    [AcceptTime]          DATETIME        NULL,
    [Important]           INT             DEFAULT ((1)) NOT NULL,
    [QuoteAmount]         DECIMAL (18, 2) NULL,
    [EstimatedFinishTime] DATETIME        NULL,
    [RepairUserID]        INT             NULL,
    [RepairUserName]      NVARCHAR (50)   NULL,
    [RepairStartTime]     DATETIME        NULL,
    [RepairFinishTime]    DATETIME        NULL,
    [RepairResult]        INT             NULL,
    [RepairDesc]          NVARCHAR (MAX)  NULL,
    [RepairStatus]        INT             NOT NULL,
    [Status]              INT             DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]              INT             DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME        NULL,
    [EditUser]            INT             NULL,
    CONSTRAINT [PK_Repair] PRIMARY KEY CLUSTERED ([RepairID] ASC),
    CONSTRAINT [FK_Repair_Floor] FOREIGN KEY ([FloorID]) REFERENCES [dbo].[Project_Floor] ([FloorID]),
    CONSTRAINT [FK_Repair_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID]),
    CONSTRAINT [FK_Repair_Store] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Merchant_Store] ([StoreID]),
    CONSTRAINT [FK_Repair_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Project_Unit] ([UnitID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_AcceptUserID]
    ON [dbo].[Repair]([AcceptUserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_FloorID]
    ON [dbo].[Repair]([FloorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_IsCommon]
    ON [dbo].[Repair]([IsCommon] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_Project]
    ON [dbo].[Repair]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_RepairStatus]
    ON [dbo].[Repair]([RepairStatus] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_RepairType]
    ON [dbo].[Repair]([RepairType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_RepairUserID]
    ON [dbo].[Repair]([RepairUserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_RequestUserID]
    ON [dbo].[Repair]([RequestUserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_StoreID]
    ON [dbo].[Repair]([StoreID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_UnitID]
    ON [dbo].[Repair]([UnitID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修单信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修类型：排水、强电、暖通、装饰、消防设备、安防设备、其他。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'StoreID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼层ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'FloorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'铺位ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'UnitID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否公共区域', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'IsCommon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'位置描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'Location';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报修来源 1-商户报修；2-运营报修', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RequestSrcType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报修情况描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RequestDesc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报修人ID，如果是商户报修，存的是商户用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RequestUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报修人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RequestUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报修时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RequestTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'受理人ID：员工ID，指派人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'AcceptUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'受理人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'AcceptUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'受理时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'AcceptTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'重要程度 1-普通，2-重要', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'Important';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修报价, 对商户铺内报修，可能是有偿维修，需要一个报价，维修完成产生费用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'QuoteAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'预计完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'EstimatedFinishTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修人ID：员工ID，受指派人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairFinishTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修结果', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修结果描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairDesc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修状态，待处理、已受理、维修中、已完结', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'RepairStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair', @level2type = N'COLUMN', @level2name = N'Status';

