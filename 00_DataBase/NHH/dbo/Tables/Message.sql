CREATE TABLE [dbo].[Message] (
    [MessageID]   INT            IDENTITY (1, 1) NOT NULL,
    [MessageType] INT            NOT NULL,
    [Priority]    INT            NOT NULL,
    [Recipient]   NVARCHAR (500) NOT NULL,
    [Subject]     NVARCHAR (500) NULL,
    [Content]     NVARCHAR (MAX) NOT NULL,
    [Link]        NVARCHAR (500) NULL,
    [Status]      INT            DEFAULT ((1)) NOT NULL,
    [InDate]      DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]      INT            DEFAULT ((0)) NOT NULL,
    [EditDate]    DATETIME       NULL,
    [EditUser]    INT            NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([MessageID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统消息表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'MessageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息类型，1-邮件；2-短信；3-微信', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'MessageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'优先级别（数值越小优先级越高）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接收人（邮箱地址、手机号、OpenID）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Recipient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Subject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息附加链接', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Link';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态（待发送、发送中、已发送、已取消）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Status';

