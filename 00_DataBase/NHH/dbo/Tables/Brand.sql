CREATE TABLE [dbo].[Brand] (
    [BrandID]          INT             IDENTITY (1, 1) NOT NULL,
    [BrandName]        NVARCHAR (50)   NOT NULL,
    [BrandLevel]       INT             NOT NULL,
    [BrandIcon]        NVARCHAR (200)  NULL,
    [BizTypeID]        INT             NULL,
    [BizCategoryID]    INT             NULL,
    [ExistingProject]  NVARCHAR (500)  NULL,
    [Revenue]          INT             NULL,
    [AreaUsage]        DECIMAL (18, 2) NULL,
    [FloorBearing]     INT             NULL,
    [FloorHeight]      INT             NULL,
    [ElectricityUsage] INT             NULL,
    [WaterUsage]       INT             NULL,
    [DrainUsage]       INT             NULL,
    [GasUsage]         INT             NULL,
    [SmokeUsage]       INT             NULL,
    [Status]           INT             DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]           INT             DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME        NULL,
    [EditUser]         INT             NULL,
    CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED ([BrandID] ASC),
    CONSTRAINT [FK_Brand_BizCategory] FOREIGN KEY ([BizCategoryID]) REFERENCES [dbo].[BizCategory] ([BizCategoryID]),
    CONSTRAINT [FK_Brand_BizType] FOREIGN KEY ([BizTypeID]) REFERENCES [dbo].[BizType] ([BizTypeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Brand_BizCategoryID]
    ON [dbo].[Brand]([BizCategoryID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Brand_BizTypeID]
    ON [dbo].[Brand]([BizTypeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Brand_BrandLevel]
    ON [dbo].[Brand]([BrandLevel] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Brand_Status]
    ON [dbo].[Brand]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'BrandID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'BrandName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌级次', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'BrandLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌图标路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'BrandIcon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营业态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'BizTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营品类', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'BizCategoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌入驻项目', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'ExistingProject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌年平均营业额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'Revenue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'面积需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'AreaUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼板承重', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'FloorBearing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼高', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'FloorHeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电量需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'ElectricityUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供水需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'WaterUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排水需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'DrainUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'燃气需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'GasUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排烟需求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'SmokeUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Brand', @level2type = N'COLUMN', @level2name = N'Status';

