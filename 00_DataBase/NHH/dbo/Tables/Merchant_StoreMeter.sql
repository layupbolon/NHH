
CREATE TABLE [dbo].[Merchant_StoreMeter](
	[MeterID] [int] IDENTITY(1,1) NOT NULL,
	[MerchantID] [int] NOT NULL,
	[StoreID] [int] NOT NULL,
	[MeterType] [int] NOT NULL,
	[MeterCode] [nvarchar](50) NOT NULL,
	[MeterAttr] [varchar](50) NOT NULL,
	[LastReading] [datetime] NULL,
	[LastNumber] [decimal](18, 2) NOT NULL,
	[Status] [int] NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUser] [int] NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [int] NULL,
 CONSTRAINT [PK_Merchant_StoreMeter] PRIMARY KEY CLUSTERED 
(
	[MeterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[Merchant_StoreMeter]  WITH CHECK ADD  CONSTRAINT [FK_Merchant_StoreMeter_Merchant] FOREIGN KEY([MerchantID])
REFERENCES [dbo].[Merchant] ([MerchantID])
GO

ALTER TABLE [dbo].[Merchant_StoreMeter] CHECK CONSTRAINT [FK_Merchant_StoreMeter_Merchant]
GO

ALTER TABLE [dbo].[Merchant_StoreMeter]  WITH CHECK ADD  CONSTRAINT [FK_Merchant_StoreMeter_Merchant_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Merchant_Store] ([StoreID])
GO

ALTER TABLE [dbo].[Merchant_StoreMeter] CHECK CONSTRAINT [FK_Merchant_StoreMeter_Merchant_Store]
GO


