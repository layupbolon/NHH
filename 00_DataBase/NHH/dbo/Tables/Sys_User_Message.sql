CREATE TABLE [dbo].[Sys_User_Message] (
    [MessageID]   INT            IDENTITY (1, 1) NOT NULL,
    [UserID]      INT            NOT NULL,
    [Subject]     NVARCHAR (200) NOT NULL,
    [Content]     NVARCHAR (MAX) NOT NULL,
    [SourceType]  INT            NOT NULL,
    [SourceRefID] INT            NULL,
    [Status]      INT            DEFAULT ((1)) NOT NULL,
    [InDate]      DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]      INT            DEFAULT ((0)) NOT NULL,
    [EditDate]    DATETIME       NULL,
    [EditUser]    INT            NULL,
    CONSTRAINT [PK_Sys_User_Message] PRIMARY KEY CLUSTERED ([MessageID] ASC),
    CONSTRAINT [FK_Sys_User_Message_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Sys_User] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_Message_SourceRefID]
    ON [dbo].[Sys_User_Message]([SourceRefID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_Message_SourceType]
    ON [dbo].[Sys_User_Message]([SourceType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_Message_Status]
    ON [dbo].[Sys_User_Message]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_Message_UserID]
    ON [dbo].[Sys_User_Message]([UserID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Message', @level2type = N'COLUMN', @level2name = N'MessageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Message', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Message', @level2type = N'COLUMN', @level2name = N'Subject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Message', @level2type = N'COLUMN', @level2name = N'Content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'来源类型：1-通知；2-租约；3-账单；4-催款；5-活动；6-报修；7-投诉；', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Message', @level2type = N'COLUMN', @level2name = N'SourceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'来源单据ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Message', @level2type = N'COLUMN', @level2name = N'SourceRefID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态：未读、已读、删除', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Message', @level2type = N'COLUMN', @level2name = N'Status';

