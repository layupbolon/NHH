CREATE TABLE [dbo].[Complaint_Comment] (
    [CommentID]   INT             IDENTITY (1, 1) NOT NULL,
    [ComplaintID] INT             NOT NULL,
    [Speed]       DECIMAL (18, 2) NULL,
    [Quality]     DECIMAL (18, 2) NULL,
    [Attitude]    DECIMAL (18, 2) NULL,
    [Overall]     DECIMAL (18, 2) NULL,
    [Comment]     NVARCHAR (500)  NULL,
    [Additional]  NVARCHAR (500)  NULL,
    [Status]      INT             DEFAULT ((1)) NOT NULL,
    [InDate]      DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]      INT             DEFAULT ((0)) NOT NULL,
    [EditDate]    DATETIME        NULL,
    [EditUser]    INT             NULL,
    CONSTRAINT [PK_Complaint_Comment] PRIMARY KEY CLUSTERED ([CommentID] ASC),
    CONSTRAINT [FK_Complaint_Comment_Complaint] FOREIGN KEY ([ComplaintID]) REFERENCES [dbo].[Complaint] ([ComplaintID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_Comment_ComplaintID]
    ON [dbo].[Complaint_Comment]([ComplaintID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_Comment_Status]
    ON [dbo].[Complaint_Comment]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'CommentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'ComplaintID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'响应速度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'Speed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉结果', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'Quality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'服务态度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'Attitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总体评价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'Overall';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'追加评论', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'Additional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint_Comment', @level2type = N'COLUMN', @level2name = N'Status';

