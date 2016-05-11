CREATE TABLE [dbo].[Project_UnitLeasing] (
    [UnitLeasingID]          INT      IDENTITY (1, 1) NOT NULL,
    [UnitID]                 INT      NOT NULL,
    [LeasingDepartmentID]    INT      NULL,
    [LeasingPersonID]        INT      NULL,
    [LeasingFinishDate]      DATETIME NULL,
    [FireProtectionAuth]     INT      NULL,
    [MeasureReviewDate]      DATETIME NULL,
    [DesignDate]             DATETIME NULL,
    [FireProtectionAuthDate] DATETIME NULL,
    [AccessDate]             DATETIME NULL,
    [DecorationStartDate]    DATETIME NULL,
    [DecorationEndDate]      DATETIME NULL,
    [Status]                 INT      DEFAULT ((1)) NOT NULL,
    [InDate]                 DATETIME DEFAULT (getdate()) NOT NULL,
    [InUser]                 INT      DEFAULT ((0)) NOT NULL,
    [EditDate]               DATETIME NULL,
    [EditUser]               INT      NULL,
    CONSTRAINT [PK_Project_UnitLeasing] PRIMARY KEY CLUSTERED ([UnitLeasingID] ASC),
    CONSTRAINT [FK_Project_UnitLeasing_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Project_Unit] ([UnitID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitLeasing_LeasingDepartmentID]
    ON [dbo].[Project_UnitLeasing]([LeasingDepartmentID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitLeasing_LeasingPersonID]
    ON [dbo].[Project_UnitLeasing]([LeasingPersonID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitLeasing_Status]
    ON [dbo].[Project_UnitLeasing]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_UnitLeasing_UnitID]
    ON [dbo].[Project_UnitLeasing]([UnitID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'招商筹划信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'铺位ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'UnitID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'招商责任部门', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'LeasingDepartmentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'招商责任人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'LeasingPersonID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'招商完成时间节点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'LeasingFinishDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否需要消防报审', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'FireProtectionAuth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'复尺完成时间节点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'MeasureReviewDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户出图完成时间节点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'DesignDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消防报审完成时间节点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'FireProtectionAuthDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'进场时间节点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'AccessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修开始时间节点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'DecorationStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装修结束时间节点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'DecorationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_UnitLeasing', @level2type = N'COLUMN', @level2name = N'Status';

