CREATE TABLE [dbo].[Project_Unit] (
    [UnitID]            INT             IDENTITY (1, 1) NOT NULL,
    [ProjectID]         INT             NOT NULL,
    [BuildingID]        INT             NOT NULL,
    [FloorID]           INT             NOT NULL,
    [UnitNumber]        NVARCHAR (50)   NOT NULL,
    [UnitAera]          DECIMAL (18, 2) NOT NULL,
    [UnitStatus]        INT             CONSTRAINT [DF_Project_Unit_UnitStatus] DEFAULT ((1)) NOT NULL,
    [UnitType]          INT             NOT NULL,
    [BizTypeID]         INT             NULL,
    [BizCategoryID]     INT             NULL,
    [StoreID]           INT             NULL,
    [ContractStatus]    INT             CONSTRAINT [DF__Project_U__Contr__55BFB948] DEFAULT ((0)) NOT NULL,
    [ContractEndDate]   DATETIME        NULL,
    [UnitMapFileName]   VARCHAR (500)   NULL,
    [FloorMapLocation]  VARCHAR (50)    NULL,
    [FloorMapDimension] VARCHAR (MAX)   NULL,
    [Tag]               NVARCHAR (50)   NULL,
    [Status]            INT             CONSTRAINT [DF__Project_U__Statu__57A801BA] DEFAULT ((1)) NOT NULL,
    [InDate]            DATETIME        CONSTRAINT [DF__Project_U__InDat__589C25F3] DEFAULT (getdate()) NOT NULL,
    [InUser]            INT             CONSTRAINT [DF__Project_U__InUse__59904A2C] DEFAULT ((0)) NOT NULL,
    [EditDate]          DATETIME        NULL,
    [EditUser]          INT             NULL,
    CONSTRAINT [PK_Project_Unit] PRIMARY KEY CLUSTERED ([UnitID] ASC),
    CONSTRAINT [FK_Project_Unit_BizCategory] FOREIGN KEY ([BizCategoryID]) REFERENCES [dbo].[BizCategory] ([BizCategoryID]),
    CONSTRAINT [FK_Project_Unit_BizType] FOREIGN KEY ([BizTypeID]) REFERENCES [dbo].[BizType] ([BizTypeID]),
    CONSTRAINT [FK_Project_Unit_Building] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Project_Building] ([BuildingID]),
    CONSTRAINT [FK_Project_Unit_Floor] FOREIGN KEY ([FloorID]) REFERENCES [dbo].[Project_Floor] ([FloorID]),
    CONSTRAINT [FK_Project_Unit_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Unit_BuildingID]
    ON [dbo].[Project_Unit]([BuildingID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Unit_FloorID]
    ON [dbo].[Project_Unit]([FloorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Unit_ProjectID]
    ON [dbo].[Project_Unit]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Unit_Status]
    ON [dbo].[Project_Unit]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Unit_UnitType]
    ON [dbo].[Project_Unit]([UnitType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目商铺信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼宇ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'BuildingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼层ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'FloorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商铺号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'UnitNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计租面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'UnitAera';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商铺类型，主力店/次主力店/步行街/', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营业态，主力店/零售/亲子/餐饮/生活服务/娱乐/文化/休闲', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'BizTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营品类，女装/箱包/数码/儿童玩具/西式快餐/...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'BizCategoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前商户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'StoreID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'签约状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'ContractStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'合同到期时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'ContractEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'平面图位置 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'UnitMapFileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'坐标', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'FloorMapLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商铺矢量坐标围栏', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'FloorMapDimension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标签,主力店/次主力店/步行街; 标杆品牌，可多个', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'Tag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Unit', @level2type = N'COLUMN', @level2name = N'Status';

