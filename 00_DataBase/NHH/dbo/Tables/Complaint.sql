CREATE TABLE [dbo].[Complaint] (
    [ComplaintID]         INT            IDENTITY (1, 1) NOT NULL,
    [ComplaintType]       INT            NOT NULL,
    [ProjectID]           INT            NOT NULL,
    [MerchantID]          INT            NULL,
    [StoreID]             INT            NULL,
    [RequestSrcType]      INT            DEFAULT ((1)) NOT NULL,
    [RequestUserID]       INT            NULL,
    [RequestUserName]     NVARCHAR (50)  NULL,
    [RequestTarget]       NVARCHAR (50)  NULL,
    [RequestDesc]         NVARCHAR (MAX) NULL,
    [RequestTime]         DATETIME       NULL,
    [AcceptUserID]        INT            NULL,
    [AcceptUserName]      NVARCHAR (50)  NULL,
    [AcceptTime]          DATETIME       NULL,
    [Important]           INT            DEFAULT ((1)) NOT NULL,
    [EstimatedFinishTime] DATETIME       NULL,
    [InvestigationDesc]   NVARCHAR (MAX) NULL,
    [ServiceUserID]       INT            NULL,
    [ServiceUserName]     NVARCHAR (50)  NULL,
    [ServiceStartTime]    DATETIME       NULL,
    [ServiceFinishTime]   DATETIME       NULL,
    [ServiceResult]       INT            NULL,
    [ServiceDesc]         NVARCHAR (MAX) NULL,
    [ComplaintStatus]     INT            NOT NULL,
    [Status]              INT            DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]              INT            DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME       NULL,
    [EditUser]            INT            NULL,
    CONSTRAINT [PK_Complaint] PRIMARY KEY CLUSTERED ([ComplaintID] ASC),
    CONSTRAINT [FK_Complaint_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]),
    CONSTRAINT [FK_Complaint_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID]),
    CONSTRAINT [FK_Complaint_Store] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Merchant_Store] ([StoreID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_AcceptUserID]
    ON [dbo].[Complaint]([AcceptUserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_ComplaintStatus]
    ON [dbo].[Complaint]([ComplaintStatus] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_ComplaintType]
    ON [dbo].[Complaint]([ComplaintType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_MerchantID]
    ON [dbo].[Complaint]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_ProjectID]
    ON [dbo].[Complaint]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_RequestUserID]
    ON [dbo].[Complaint]([RequestUserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_ServiceUserID]
    ON [dbo].[Complaint]([ServiceUserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Complaint_StoreID]
    ON [dbo].[Complaint]([StoreID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ComplaintID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉类型：服务类、商品类、维修类、活动类、设备设施、其他。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ComplaintType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'StoreID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉来源:1-商户投诉；2-运营投诉', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'RequestSrcType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉人ID，如果是商户投诉，存的是商户用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'RequestUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'RequestUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉对象', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'RequestTarget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉情况描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'RequestDesc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'RequestTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'受理人ID：员工ID，指派人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'AcceptUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'受理人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'AcceptUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'受理时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'AcceptTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'重要程度 1-普通，2-重要', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'Important';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'预计完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'EstimatedFinishTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'调查记录', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'InvestigationDesc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'客服ID：员工ID，受指派人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ServiceUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'客服姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ServiceUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ServiceStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ServiceFinishTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理结果', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ServiceResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理结果描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ServiceDesc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投诉状态，待处理、调查中、已完结', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'ComplaintStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Complaint', @level2type = N'COLUMN', @level2name = N'Status';

