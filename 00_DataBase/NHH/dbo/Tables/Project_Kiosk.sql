CREATE TABLE [dbo].[Project_Kiosk] (
    [KioskID]          INT             IDENTITY (1, 1) NOT NULL,
    [ProjectID]        INT             NOT NULL,
    [BuildingID]       INT             NOT NULL,
    [FloorID]          INT             NOT NULL,
    [KioskNumber]      NVARCHAR (20)   NOT NULL,
    [KioskType]        INT             DEFAULT ((0)) NOT NULL,
    [Location]         NVARCHAR (50)   NULL,
    [FloorMapLocation] NVARCHAR (50)   NULL,
    [Area]             DECIMAL (18, 2) NULL,
    [BizTypeID]        INT             NULL,
    [OccupyRate]       DECIMAL (18, 2) NULL,
    [Rent]             DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [ContractStatus]   INT             DEFAULT ((0)) NOT NULL,
    [ContractEnd]      DATETIME        NULL,
    [Status]           INT             DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]           INT             DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME        NULL,
    [EditUser]         INT             NULL,
    CONSTRAINT [PK_Project_Kiosk] PRIMARY KEY CLUSTERED ([KioskID] ASC),
    CONSTRAINT [FK_Project_Kiosk_Building] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Project_Building] ([BuildingID]),
    CONSTRAINT [FK_Project_Kiosk_Floor] FOREIGN KEY ([FloorID]) REFERENCES [dbo].[Project_Floor] ([FloorID]),
    CONSTRAINT [FK_Project_Kiosk_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Kiosk_BuildingID]
    ON [dbo].[Project_Kiosk]([BuildingID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Kiosk_FloorID]
    ON [dbo].[Project_Kiosk]([FloorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Kiosk_ProjectID]
    ON [dbo].[Project_Kiosk]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Kiosk_Status]
    ON [dbo].[Project_Kiosk]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目多经点位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼宇ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'BuildingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼层ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'FloorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'多经点位号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'KioskNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'多经点位类型：1-室内、2-室外', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'KioskType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'位置描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'Location';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'坐标', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'FloorMapLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计租面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营业态，主力店/零售/亲子/餐饮/生活服务/娱乐/文化/休闲', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'BizTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用率', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'OccupyRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'租金', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'Rent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'签约状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'ContractStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合同到期时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'ContractEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Kiosk', @level2type = N'COLUMN', @level2name = N'Status';

