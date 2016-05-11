CREATE TABLE [dbo].[Utility_Bill](
	[BillID] [int] IDENTITY(1,1) NOT NULL,
	[MeterID] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[PrevNumber] [decimal](18, 2) NOT NULL,
	[CurNumber] [decimal](18, 2) NOT NULL,
	[UseNumber] [decimal](18, 2) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[BillAmount] [decimal](18, 2) NOT NULL,
	[OperatorID] [int] NULL,
	[OperatorName] [nvarchar](25) NULL,
	[Remark] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUser] [int] NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [int] NULL,
 CONSTRAINT [PK_Utility_Bill] PRIMARY KEY CLUSTERED 
(
	[BillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Utility_Bill] ADD  DEFAULT ((0)) FOR [PrevNumber]
GO

ALTER TABLE [dbo].[Utility_Bill] ADD  DEFAULT ((0)) FOR [CurNumber]
GO

ALTER TABLE [dbo].[Utility_Bill] ADD  DEFAULT ((0)) FOR [UseNumber]
GO

ALTER TABLE [dbo].[Utility_Bill] ADD  DEFAULT ((0)) FOR [UnitPrice]
GO

ALTER TABLE [dbo].[Utility_Bill] ADD  DEFAULT ((0)) FOR [BillAmount]
GO

ALTER TABLE [dbo].[Utility_Bill] ADD  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [dbo].[Utility_Bill] ADD  DEFAULT (getdate()) FOR [InDate]
GO

ALTER TABLE [dbo].[Utility_Bill] ADD  DEFAULT ((0)) FOR [InUser]
GO

ALTER TABLE [dbo].[Utility_Bill]  WITH CHECK ADD  CONSTRAINT [FK_Utility_Bill_Merchant_StoreMeter] FOREIGN KEY([MeterID])
REFERENCES [dbo].[Merchant_StoreMeter] ([MeterID])
GO

ALTER TABLE [dbo].[Utility_Bill] CHECK CONSTRAINT [FK_Utility_Bill_Merchant_StoreMeter]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抄表记录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'BillID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商铺计量器ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'MeterID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抄表开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'StartDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抄表结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'EndDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上期抄表数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'PrevNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本期抄表数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'CurNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实用表数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'UseNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'UnitPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抄表金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'BillAmount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抄表人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'OperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抄表人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'OperatorName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Utility_Bill', @level2type=N'COLUMN',@level2name=N'Status'
GO


