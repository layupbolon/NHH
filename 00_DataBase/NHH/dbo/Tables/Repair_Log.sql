CREATE TABLE [dbo].[Repair_Log] (
    [LogID]       INT            IDENTITY (1, 1) NOT NULL,
    [RepairID]    INT            NOT NULL,
    [LogText]     NVARCHAR (MAX) NOT NULL,
    [LogUserID]   INT            NOT NULL,
    [LogUserName] NVARCHAR (50)  NOT NULL,
    [LogTime]     DATETIME       NOT NULL,
    [EditDate]    DATETIME       NULL,
    [EditUser]    INT            NULL,
    CONSTRAINT [PK_Repair_Log] PRIMARY KEY CLUSTERED ([LogID] ASC),
    CONSTRAINT [FK_Repair_Log_Repair] FOREIGN KEY ([RepairID]) REFERENCES [dbo].[Repair] ([RepairID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_Log_LogUserID]
    ON [dbo].[Repair_Log]([LogUserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_Log_RepairID]
    ON [dbo].[Repair_Log]([RepairID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修单业务日志', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Log';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日志ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Log', @level2type = N'COLUMN', @level2name = N'LogID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Log', @level2type = N'COLUMN', @level2name = N'RepairID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日志信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Log', @level2type = N'COLUMN', @level2name = N'LogText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'记录人ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Log', @level2type = N'COLUMN', @level2name = N'LogUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'记录人名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Log', @level2type = N'COLUMN', @level2name = N'LogUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'记录时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Log', @level2type = N'COLUMN', @level2name = N'LogTime';

