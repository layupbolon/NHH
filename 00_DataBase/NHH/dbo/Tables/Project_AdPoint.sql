CREATE TABLE [dbo].[Project_AdPoint] (
    [AdPointID]        INT             IDENTITY (1, 1) NOT NULL,
    [ProjectID]        INT             NOT NULL,
    [BuildingID]       INT             NOT NULL,
    [FloorID]          INT             NOT NULL,
    [AdPointNumber]    NVARCHAR (20)   NOT NULL,
    [AdPointType]      INT             DEFAULT ((0)) NOT NULL,
    [AdPointMedia]     INT             DEFAULT ((0)) NOT NULL,
    [Location]         NVARCHAR (50)   NULL,
    [FloorMapLocation] NVARCHAR (50)   NULL,
    [Rent]             DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [ContractStatus]   INT             DEFAULT ((0)) NOT NULL,
    [ContractEnd]      DATETIME        NULL,
    [Status]           INT             DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]           INT             DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME        NULL,
    [EditUser]         INT             NULL,
    CONSTRAINT [PK_Project_AdPoint] PRIMARY KEY CLUSTERED ([AdPointID] ASC),
    CONSTRAINT [FK_Project_AdPoint_Building] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Project_Building] ([BuildingID]),
    CONSTRAINT [FK_Project_AdPoint_Floor] FOREIGN KEY ([FloorID]) REFERENCES [dbo].[Project_Floor] ([FloorID]),
    CONSTRAINT [FK_Project_AdPoint_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_AdPoint_BuildingID]
    ON [dbo].[Project_AdPoint]([BuildingID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_AdPoint_FloorID]
    ON [dbo].[Project_AdPoint]([FloorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_AdPoint_ProjectID]
    ON [dbo].[Project_AdPoint]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_AdPoint_Status]
    ON [dbo].[Project_AdPoint]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目广告位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼宇ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'BuildingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼层ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'FloorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'广告位号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'AdPointNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'广告类型：1-室内、2-室外', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'AdPointType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'广告媒介：灯箱、柱体、墙体、LOGO、侧旗、吊幔等', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'AdPointMedia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'位置描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'Location';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'坐标', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'FloorMapLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'Rent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'签约状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'ContractStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合同到期时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'ContractEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_AdPoint', @level2type = N'COLUMN', @level2name = N'Status';

