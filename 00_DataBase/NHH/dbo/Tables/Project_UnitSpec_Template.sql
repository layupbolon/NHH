CREATE TABLE [dbo].[Project_UnitSpec_Template] (
    [TemplateID]       INT            IDENTITY (1, 1) NOT NULL,
    [TemplateName]     NVARCHAR (50)  NOT NULL,
    [SpecType]         INT            NOT NULL,
    [Floor]            NVARCHAR (500) NULL,
    [Ceiling]          NVARCHAR (500) NULL,
    [Wall]             NVARCHAR (500) NULL,
    [Pillar]           NVARCHAR (500) NULL,
    [FloorBearing]     NVARCHAR (500) NULL,
    [WaterSupply]      NVARCHAR (500) NULL,
    [WaterDrain]       NVARCHAR (500) NULL,
    [Door]             NVARCHAR (500) NULL,
    [Logo]             NVARCHAR (500) NULL,
    [ElectricityUsage] NVARCHAR (500) NULL,
    [FireProtection]   NVARCHAR (500) NULL,
    [Broadcasting]     NVARCHAR (500) NULL,
    [AirCondition]     NVARCHAR (500) NULL,
    [Smoke]            NVARCHAR (500) NULL,
    [Security]         NVARCHAR (500) NULL,
    [Wiring]           NVARCHAR (500) NULL,
    [Water]            NVARCHAR (500) NULL,
    [Gas]              NVARCHAR (500) NULL,
    [Status]           INT            DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]           INT            DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME       NULL,
    [EditUser]         INT            NULL,
    CONSTRAINT [PK_Project_UnitSpec_Template] PRIMARY KEY CLUSTERED ([TemplateID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitSpec_Template_SpecType]
    ON [dbo].[Project_UnitSpec_Template]([SpecType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitSpec_Template_Status]
    ON [dbo].[Project_UnitSpec_Template]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交付标准与商户责任模板', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'模板编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'TemplateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'模板名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'TemplateName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'类型，交付标准、商户责任', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'SpecType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地面', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Floor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顶面', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Ceiling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'墙', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Wall';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'柱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Pillar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼板承重', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'FloorBearing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'给水', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'WaterSupply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排水', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'WaterDrain';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺门面', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Door';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标牌', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Logo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'ElectricityUsage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消防系统', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'FireProtection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'广播系统', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Broadcasting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'空调系统', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'AirCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排油烟系统', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Smoke';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安防系统', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Security';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'综合布线', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Wiring';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上下水', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Water';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'燃气', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Gas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitSpec_Template', @level2type = N'COLUMN', @level2name = N'Status';

