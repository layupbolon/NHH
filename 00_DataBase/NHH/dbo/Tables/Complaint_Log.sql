CREATE TABLE [dbo].[Complaint_Log] (
    [LogID]       INT            IDENTITY (1, 1) NOT NULL,
    [ComplaintID] INT            NOT NULL,
    [LogText]     NVARCHAR (MAX) NOT NULL,
    [LogUserID]   INT            NOT NULL,
    [LogUserName] NVARCHAR (50)  NOT NULL,
    [LogTime]     DATETIME       NOT NULL,
    [EditDate]    DATETIME       NULL,
    [EditUser]    INT            NULL,
    CONSTRAINT [PK_Complaint_Log] PRIMARY KEY CLUSTERED ([LogID] ASC),
    CONSTRAINT [FK_Complaint_Log_Complaint] FOREIGN KEY ([ComplaintID]) REFERENCES [dbo].[Complaint] ([ComplaintID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_Log_ComplaintID]
    ON [dbo].[Complaint_Log]([ComplaintID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_Log_LogUserID]
    ON [dbo].[Complaint_Log]([LogUserID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉单业务日志', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Log';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日志ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Log', @level2type = N'COLUMN', @level2name = N'LogID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Log', @level2type = N'COLUMN', @level2name = N'ComplaintID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日志信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Log', @level2type = N'COLUMN', @level2name = N'LogText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'记录人ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Log', @level2type = N'COLUMN', @level2name = N'LogUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'记录人名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Log', @level2type = N'COLUMN', @level2name = N'LogUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'记录时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Log', @level2type = N'COLUMN', @level2name = N'LogTime';

