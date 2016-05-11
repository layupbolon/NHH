-- =============================================
-- Author:		Jesong.Yu
-- Create date: 2015-8-24
-- Description:	导入商铺
-- =============================================
CREATE PROCEDURE [dbo].[SP_Job_Import_ProjectUnit]
	@ProjectName nvarchar(50),
	@BuildingName nvarchar(15),
	@FloorNumber int,
	@UnitNumber varchar(10),
	@UnitArea decimal(18,2),
	@InternalUnitArea decimal(18,2),
	@FloorMapFileName varchar(50),
	@UnitType nvarchar(15)
AS
BEGIN

	Declare @ProjectID Int = 0,
			@BuildingID Int = 0,
			@FloorID Int = 0;
	Declare @UnitTypeID Int=0;

	--项目
	Select @ProjectID=ProjectID From Project(Nolock) Where ProjectName=@ProjectName;
	If (@ProjectID = 0)
	Begin
		Select -1;
		Return;
	End
	--楼宇
	Select @BuildingID = BuildingID From Project_Building(Nolock) Where ProjectID=@ProjectID And BuildingName=@BuildingName
	If (@BuildingID = 0)
	Begin
		Select -2;
		Return;
	End
	--楼层
	Select @FloorID = FloorID From Project_Floor(Nolock) Where ProjectID=@ProjectID And BuildingID=@BuildingID And FloorNumber=@FloorNumber
	If (@FloorID = 0)
	Begin
		Select  -3;
		Return;
	End
	--类型
	Select @UnitTypeID = FieldValue From Dictionary(Nolock) Where FieldType=N'ProjectUnitType' And FieldName=@UnitType;
	If (@UnitTypeID = 0)
	Begin
		Select  -4;
		Return;
	End

	--插入数据库
	Insert into Project_Unit (ProjectID,BuildingID,FloorID,UnitType,UnitNumber,UnitAera, StoreID,ContractStatus,BizTypeID,BizCategoryID, [Status], EditDate, EditUser) 
	Values (@ProjectID, @BuildingID, @FloorID, @UnitTypeID, @UnitNumber, @UnitArea, 0, 0, 0, 0, 0, GETDATE(), 0);
	Select 1;
END

