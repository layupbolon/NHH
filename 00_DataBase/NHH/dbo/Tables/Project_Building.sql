CREATE TABLE [dbo].[Project_Building] (
    [BuildingID]              INT             IDENTITY (1, 1) NOT NULL,
    [ProjectID]               INT             NOT NULL,
    [BuildingCode]            NVARCHAR (50)   NULL,
    [BuildingName]            NVARCHAR (50)   NOT NULL,
    [GroundFloorNum]          INT             NULL,
    [UndergroundFloorNum]     INT             NULL,
    [BuildingGroundArea]      DECIMAL (18, 2) NULL,
    [BuildingUndergroundArea] DECIMAL (18, 2) NULL,
    [TotalConstructionArea]   DECIMAL (18, 2) NULL,
    [TotalRentArea]           DECIMAL (18, 2) NULL,
    [TotalRentUnit]           INT             NULL,
    [PlanSummary]             NVARCHAR (MAX)  NULL,
    [PlanHighlight]           NVARCHAR (MAX)  NULL,
    [RenderingFileName]       VARCHAR (500)   NULL,
    [ContractStore]           NVARCHAR (500)  NULL,
    [Status]                  INT             DEFAULT ((1)) NOT NULL,
    [InDate]                  DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]                  INT             DEFAULT ((0)) NOT NULL,
    [EditDate]                DATETIME        NULL,
    [EditUser]                INT             NULL,
    CONSTRAINT [PK_Project_Building] PRIMARY KEY CLUSTERED ([BuildingID] ASC),
    CONSTRAINT [FK_Project_Building_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Building_ProjectID]
    ON [dbo].[Project_Building]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Building_Status]
    ON [dbo].[Project_Building]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目楼宇信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼宇编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'BuildingCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼宇名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'BuildingName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地面楼层数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'GroundFloorNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地下楼层数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'UndergroundFloorNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地面建筑面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'BuildingGroundArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地下建筑面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'BuildingUndergroundArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总建筑面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'TotalConstructionArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总计租面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'TotalRentArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总出租单元数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'TotalRentUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规划定位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'PlanSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规划亮点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'PlanHighlight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'效果图路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'RenderingFileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入驻商家', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'ContractStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Building', @level2type = N'COLUMN', @level2name = N'Status';

