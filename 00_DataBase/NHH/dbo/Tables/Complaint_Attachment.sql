CREATE TABLE [dbo].[Complaint_Attachment] (
    [AttachmentID]   INT            IDENTITY (1, 1) NOT NULL,
    [ComplaintID]    INT            NOT NULL,
    [AttachmentName] NVARCHAR (100) NULL,
    [AttachmentPath] NVARCHAR (500) NULL,
    [Status]         INT            DEFAULT ((1)) NOT NULL,
    [InDate]         DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]         INT            DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME       NULL,
    [EditUser]       INT            NULL,
    CONSTRAINT [PK_Complaint_Attachment] PRIMARY KEY CLUSTERED ([AttachmentID] ASC),
    CONSTRAINT [FK_Complaint_Attachment_Complaint] FOREIGN KEY ([ComplaintID]) REFERENCES [dbo].[Complaint] ([ComplaintID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_Attachment_ComplaintID]
    ON [dbo].[Complaint_Attachment]([ComplaintID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_Attachment_Status]
    ON [dbo].[Complaint_Attachment]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Attachment', @level2type = N'COLUMN', @level2name = N'ComplaintID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件存储路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentPath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Attachment', @level2type = N'COLUMN', @level2name = N'Status';

