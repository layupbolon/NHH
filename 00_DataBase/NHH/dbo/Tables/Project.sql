CREATE TABLE [dbo].[Project] (
    [ProjectID]           INT             IDENTITY (1, 1) NOT NULL,
    [ProjectCode]         NVARCHAR (50)   NULL,
    [BriefName]           NVARCHAR (50)   NULL,
    [ProjectName]         NVARCHAR (50)   NOT NULL,
    [ProjectSummary]      NVARCHAR (MAX)  NULL,
    [RegionID]            INT             NOT NULL,
    [ProvinceID]          INT             NOT NULL,
    [CityID]              INT             NOT NULL,
    [AddressLine]         NVARCHAR (100)  NULL,
    [AddressInfo]         NVARCHAR (100)  NULL,
    [Zipcode]             VARCHAR (15)    NULL,
    [Latitude]            DECIMAL (18, 4) NULL,
    [Longitude]           DECIMAL (18, 4) NULL,
    [GroundArea]          DECIMAL (18, 2) NULL,
    [UndergroundArea]     DECIMAL (18, 2) NULL,
    [TotalArea]           DECIMAL (18, 2) NULL,
    [RenderingFileName]   VARCHAR (500)   NULL,
    [PlanSummary]         NVARCHAR (MAX)  NULL,
    [PlanHighlight]       NVARCHAR (MAX)  NULL,
    [ParkingLotNum]       INT             NULL,
    [AdPointNum]          INT             NULL,
    [MultiBizPositionNum] INT             NULL,
    [GrandOpeningDate]    DATETIME        NULL,
    [Stage]               INT             NOT NULL,
    [ManageCompanyID]     INT             NOT NULL,
    [BuildingNum]         INT             NULL,
    [Status]              INT             CONSTRAINT [DF__tmp_ms_xx__Statu__5649C92D] DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME        CONSTRAINT [DF__tmp_ms_xx__InDat__573DED66] DEFAULT (getdate()) NOT NULL,
    [InUser]              INT             CONSTRAINT [DF__tmp_ms_xx__InUse__5832119F] DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME        NULL,
    [EditUser]            INT             NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([ProjectID] ASC),
    CONSTRAINT [FK_Project_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([CityID]),
    CONSTRAINT [FK_Project_ManageCompany] FOREIGN KEY ([ManageCompanyID]) REFERENCES [dbo].[Company] ([CompanyID]),
    CONSTRAINT [FK_Project_Provice] FOREIGN KEY ([ProvinceID]) REFERENCES [dbo].[Province] ([ProvinceID]),
    CONSTRAINT [FK_Project_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([RegionID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_CityID]
    ON [dbo].[Project]([CityID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_ManageCompanyID]
    ON [dbo].[Project]([ManageCompanyID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_ProvinceID]
    ON [dbo].[Project]([ProvinceID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_RegionID]
    ON [dbo].[Project]([RegionID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Stage]
    ON [dbo].[Project]([Stage] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Status]
    ON [dbo].[Project]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'ProjectCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目简称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'BriefName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'ProjectName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目简介', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'ProjectSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域，东北', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'RegionID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'省', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'ProvinceID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'CityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'AddressLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'位置', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'AddressInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮编', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'Zipcode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'纬度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'Latitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'Longitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地上建筑面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'GroundArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地下建筑面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'UndergroundArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总建筑面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'TotalArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'效果图文件路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'RenderingFileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规划定位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'PlanSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规划亮点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'PlanHighlight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'停车位数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'ParkingLotNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'广告位数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'AdPointNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'多种经营点位数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'MultiBizPositionNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开业日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'GrandOpeningDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'阶段，筹备期/新开业/培育期/成长期/成熟期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'Stage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'管理公司ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'ManageCompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼宇数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'BuildingNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'Status';

