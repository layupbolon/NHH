CREATE TABLE [dbo].[Repair_Material] (
    [RepairMaterialID] INT             IDENTITY (1, 1) NOT NULL,
    [RepairID]         INT             NOT NULL,
    [MaterialID]       INT             NOT NULL,
    [Quantity]         DECIMAL (18, 2) DEFAULT ((1)) NOT NULL,
    [Status]           INT             DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]           INT             DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME        NULL,
    [EditUser]         INT             NULL,
    CONSTRAINT [PK_Repair_Material] PRIMARY KEY CLUSTERED ([RepairMaterialID] ASC),
    CONSTRAINT [FK_Repair_Material_Material] FOREIGN KEY ([MaterialID]) REFERENCES [dbo].[Material] ([MaterialID]),
    CONSTRAINT [FK_Repair_Material_Repair] FOREIGN KEY ([RepairID]) REFERENCES [dbo].[Repair] ([RepairID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_Material_MaterialID]
    ON [dbo].[Repair_Material]([MaterialID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_Material_RepairID]
    ON [dbo].[Repair_Material]([RepairID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_Material_Status]
    ON [dbo].[Repair_Material]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Material', @level2type = N'COLUMN', @level2name = N'RepairID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Material', @level2type = N'COLUMN', @level2name = N'MaterialID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Material', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Material', @level2type = N'COLUMN', @level2name = N'Status';

