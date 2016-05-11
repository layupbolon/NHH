CREATE TABLE [dbo].[Repair_Attachment] (
    [AttachmentID]   INT            IDENTITY (1, 1) NOT NULL,
    [RepairID]       INT            NOT NULL,
    [AttachmentName] NVARCHAR (100) NULL,
    [AttachmentPath] NVARCHAR (500) NULL,
    [Status]         INT            DEFAULT ((1)) NOT NULL,
    [InDate]         DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]         INT            DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME       NULL,
    [EditUser]       INT            NULL,
    CONSTRAINT [PK_Repair_Attachment] PRIMARY KEY CLUSTERED ([AttachmentID] ASC),
    CONSTRAINT [FK_Repair_Attachment_Repair] FOREIGN KEY ([RepairID]) REFERENCES [dbo].[Repair] ([RepairID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_Attachment_RepairID]
    ON [dbo].[Repair_Attachment]([RepairID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Repair_Attachment_Status]
    ON [dbo].[Repair_Attachment]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修单附件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Attachment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Attachment', @level2type = N'COLUMN', @level2name = N'RepairID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件存储路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentPath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Repair_Attachment', @level2type = N'COLUMN', @level2name = N'Status';

