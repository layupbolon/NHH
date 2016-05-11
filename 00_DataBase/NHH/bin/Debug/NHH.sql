/*
NHH 的部署脚本

此代码由工具生成。
如果重新生成此代码，则对此文件的更改可能导致
不正确的行为并将丢失。
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "NHH"
:setvar DefaultFilePrefix "NHH"
:setvar DefaultDataPath "C:\Users\Admin\AppData\Local\Microsoft\VisualStudio\SSDT\NHH.WebSite.Mgmt"
:setvar DefaultLogPath "C:\Users\Admin\AppData\Local\Microsoft\VisualStudio\SSDT\NHH.WebSite.Mgmt"

GO
:on error exit
GO
/*
请检测 SQLCMD 模式，如果不支持 SQLCMD 模式，请禁用脚本执行。
要在启用 SQLCMD 模式后重新启用脚本，请执行:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'要成功执行此脚本，必须启用 SQLCMD 模式。';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
正在删除列 [dbo].[Utility_Bill].[BillType]，可能会出现数据丢失。

正在删除列 [dbo].[Utility_Bill].[ContractID]，可能会出现数据丢失。

正在删除列 [dbo].[Utility_Bill].[MerchantID]，可能会出现数据丢失。

正在删除列 [dbo].[Utility_Bill].[UnitName]，可能会出现数据丢失。

必须添加表 [dbo].[Utility_Bill] 中的列 [dbo].[Utility_Bill].[MeterID]，但该列无默认值，也不允许使用 NULL 值。如果表中包含数据，ALTER 脚本将不能工作。为避免此问题，必须: 向该列添加默认值，或者将其标记为允许使用 NULL 值，或者启用智能默认值生成功能作为部署选项。
*/

IF EXISTS (select top 1 1 from [dbo].[Utility_Bill])
    RAISERROR (N'检测到行。由于可能丢失数据，正在终止架构更新。', 16, 127) WITH NOWAIT

GO
PRINT N'正在删除 [dbo].[Utility_Bill].[IX_Utility_Bill_BillType]...';


GO
DROP INDEX [IX_Utility_Bill_BillType]
    ON [dbo].[Utility_Bill];


GO
PRINT N'正在删除 [dbo].[Utility_Bill].[IX_Utility_Bill_MerchantID]...';


GO
DROP INDEX [IX_Utility_Bill_MerchantID]
    ON [dbo].[Utility_Bill];


GO
PRINT N'正在删除 unnamed constraint on [dbo].[Utility_Bill]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [DF__Utility_B__PrevN__2AF556D4];


GO
PRINT N'正在删除 unnamed constraint on [dbo].[Utility_Bill]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [DF__Utility_B__CurNu__2BE97B0D];


GO
PRINT N'正在删除 unnamed constraint on [dbo].[Utility_Bill]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [DF__Utility_B__UseNu__2CDD9F46];


GO
PRINT N'正在删除 unnamed constraint on [dbo].[Utility_Bill]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [DF__Utility_B__UnitP__2DD1C37F];


GO
PRINT N'正在删除 unnamed constraint on [dbo].[Utility_Bill]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [DF__Utility_B__BillA__2EC5E7B8];


GO
PRINT N'正在删除 unnamed constraint on [dbo].[Utility_Bill]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [DF__Utility_B__Statu__2FBA0BF1];


GO
PRINT N'正在删除 unnamed constraint on [dbo].[Utility_Bill]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [DF__Utility_B__InDat__30AE302A];


GO
PRINT N'正在删除 unnamed constraint on [dbo].[Utility_Bill]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [DF__Utility_B__InUse__31A25463];


GO
PRINT N'正在删除 [dbo].[FK_Utility_Bill_Merchant]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [FK_Utility_Bill_Merchant];


GO
PRINT N'正在删除 [dbo].[FK_Utility_Bill_Contract]...';


GO
ALTER TABLE [dbo].[Utility_Bill] DROP CONSTRAINT [FK_Utility_Bill_Contract];


GO
PRINT N'正在开始重新生成表 [dbo].[Utility_Bill]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Utility_Bill] (
    [BillID]       INT             IDENTITY (1, 1) NOT NULL,
    [MeterID]      INT             NOT NULL,
    [StartDate]    DATETIME        NOT NULL,
    [EndDate]      DATETIME        NOT NULL,
    [PrevNumber]   DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [CurNumber]    DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [UseNumber]    DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [UnitPrice]    DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [BillAmount]   DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [OperatorID]   INT             NULL,
    [OperatorName] NVARCHAR (25)   NULL,
    [Remark]       NVARCHAR (500)  NULL,
    [Status]       INT             DEFAULT ((1)) NOT NULL,
    [InDate]       DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]       INT             DEFAULT ((0)) NOT NULL,
    [EditDate]     DATETIME        NULL,
    [EditUser]     INT             NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Utility_Bill] PRIMARY KEY CLUSTERED ([BillID] ASC) ON [PRIMARY]
) ON [PRIMARY];

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Utility_Bill])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Utility_Bill] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Utility_Bill] ([BillID], [StartDate], [EndDate], [PrevNumber], [CurNumber], [UseNumber], [UnitPrice], [BillAmount], [OperatorID], [OperatorName], [Remark], [Status], [InDate], [InUser], [EditDate], [EditUser])
        SELECT   [BillID],
                 [StartDate],
                 [EndDate],
                 [PrevNumber],
                 [CurNumber],
                 [UseNumber],
                 [UnitPrice],
                 [BillAmount],
                 [OperatorID],
                 [OperatorName],
                 [Remark],
                 [Status],
                 [InDate],
                 [InUser],
                 [EditDate],
                 [EditUser]
        FROM     [dbo].[Utility_Bill]
        ORDER BY [BillID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Utility_Bill] OFF;
    END

DROP TABLE [dbo].[Utility_Bill];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Utility_Bill]', N'Utility_Bill';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Utility_Bill]', N'PK_Utility_Bill', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'正在创建 [dbo].[Merchant_StoreMeter]...';


GO
CREATE TABLE [dbo].[Merchant_StoreMeter] (
    [MeterID]     INT             IDENTITY (1, 1) NOT NULL,
    [MerchantID]  INT             NOT NULL,
    [StoreID]     INT             NOT NULL,
    [MeterType]   INT             NOT NULL,
    [MeterCode]   NVARCHAR (50)   NOT NULL,
    [MeterAttr]   VARCHAR (50)    NOT NULL,
    [LastReading] DATETIME        NULL,
    [LastNumber]  DECIMAL (18, 2) NOT NULL,
    [Status]      INT             NOT NULL,
    [InDate]      DATETIME        NOT NULL,
    [InUser]      INT             NOT NULL,
    [EditDate]    DATETIME        NULL,
    [EditUser]    INT             NULL,
    CONSTRAINT [PK_Merchant_StoreMeter] PRIMARY KEY CLUSTERED ([MeterID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'正在创建 [dbo].[FK_Utility_Bill_Merchant_StoreMeter]...';


GO
ALTER TABLE [dbo].[Utility_Bill] WITH NOCHECK
    ADD CONSTRAINT [FK_Utility_Bill_Merchant_StoreMeter] FOREIGN KEY ([MeterID]) REFERENCES [dbo].[Merchant_StoreMeter] ([MeterID]);


GO
PRINT N'正在创建 [dbo].[FK_Merchant_StoreMeter_Merchant]...';


GO
ALTER TABLE [dbo].[Merchant_StoreMeter] WITH NOCHECK
    ADD CONSTRAINT [FK_Merchant_StoreMeter_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]);


GO
PRINT N'正在创建 [dbo].[FK_Merchant_StoreMeter_Merchant_Store]...';


GO
ALTER TABLE [dbo].[Merchant_StoreMeter] WITH NOCHECK
    ADD CONSTRAINT [FK_Merchant_StoreMeter_Merchant_Store] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Merchant_Store] ([StoreID]);


GO
PRINT N'正在改变 [dbo].[View_Report_Contract]...';


GO

ALTER VIEW [dbo].[View_Report_Contract]
AS
SELECT   dbo.View_ContractStatus.ContractStatusName, dbo.Contract.ContractID, dbo.Contract.ContractType, 
                dbo.Contract.ContractStatus, dbo.Contract.ProjectID, dbo.Contract.MerchantID, dbo.Brand.BrandName, 
                dbo.Contract.DecorationLength, dbo.Contract.DecorationEndDate, dbo.Contract.ContractEndDate, 
                dbo.Contract.ContractLength, dbo.Contract.QualityBond, dbo.Contract.AdPointNum, dbo.Contract.ParkingLotNum, 
                dbo.Contract_Unit.UnitID, dbo.Contract_Unit.UnitAvgAera, dbo.Contract.ContractStartDate, dbo.Brand.BrandLevel, 
                dbo.Contract.ContractArea, dbo.Contract.ContractUnitRent, dbo.Contract.ContractMgmtFee, 
                dbo.Merchant_Brand.MerchantBrandID, dbo.Contract.ContractCode
FROM      dbo.Contract INNER JOIN
                dbo.View_ContractStatus ON dbo.Contract.ContractStatus = dbo.View_ContractStatus.ContractStatusID INNER JOIN
                dbo.Contract_Unit ON dbo.Contract.ContractID = dbo.Contract_Unit.ContractID INNER JOIN
                dbo.Brand INNER JOIN
                dbo.Merchant_Brand ON dbo.Brand.BrandID = dbo.Merchant_Brand.BrandID INNER JOIN
                dbo.Contract_Brand ON dbo.Merchant_Brand.MerchantBrandID = dbo.Contract_Brand.MerchantBrandID ON 
                dbo.Contract.ContractID = dbo.Contract_Brand.ContractID
GO
PRINT N'正在刷新 [dbo].[View_FloorMap_Unit]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[View_FloorMap_Unit]';


GO
PRINT N'正在刷新 [dbo].[View_Project_Unit_Contract]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[View_Project_Unit_Contract]';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[BillID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�����¼ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'BillID';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[MeterID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'���̼�����ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'MeterID';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[StartDate].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�����ʼʱ��', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[EndDate].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'�������ʱ��', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[PrevNumber].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'���ڳ�����', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'PrevNumber';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[CurNumber].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'���ڳ�����', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'CurNumber';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[UseNumber].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ʵ�ñ���', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'UseNumber';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[UnitPrice].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'����', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'UnitPrice';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[BillAmount].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'BillAmount';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[OperatorID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'������ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'OperatorID';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[OperatorName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'����������', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'OperatorName';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[Remark].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'��ע', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'Remark';


GO
PRINT N'正在创建 [dbo].[Utility_Bill].[Status].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'״̬', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Utility_Bill', @level2type = N'COLUMN', @level2name = N'Status';


GO
PRINT N'根据新创建的约束检查现有的数据';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Utility_Bill] WITH CHECK CHECK CONSTRAINT [FK_Utility_Bill_Merchant_StoreMeter];

ALTER TABLE [dbo].[Merchant_StoreMeter] WITH CHECK CHECK CONSTRAINT [FK_Merchant_StoreMeter_Merchant];

ALTER TABLE [dbo].[Merchant_StoreMeter] WITH CHECK CHECK CONSTRAINT [FK_Merchant_StoreMeter_Merchant_Store];


GO
PRINT N'更新完成。';


GO
