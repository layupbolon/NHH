-- =============================================
-- Author:		Jesong.Yu
-- Create date: 2015-10-22
-- Description:	导入商铺平面图数据
-- =============================================
CREATE PROCEDURE [dbo].[SP_Import_FloorMap_Unit]
	@FloorID Int,
	@UnitNumber nvarchar(50),
	@UnitName nvarchar(150),
	@TextPosition varchar(50),
	@PathLine varchar(500),
	@PathQuad1 varchar(100),
	@PathQuad2 varchar(100),
	@PathQuad3 varchar(100),
	@PathQuad4 varchar(100)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Declare @UnitID int;

	Select @UnitID=UnitID From dbo.Project_Unit(Nolock)
	Where FloorID=@FloorID and UnitNumber=@UnitNumber

	Declare @Comments nvarchar(500);
	Set @Comments= @UnitName + @UnitName;

	INSERT INTO [dbo].[FloorMap_Unit]
           ([FloorID]
           ,[UnitID]
           ,[TextPosition]
           ,[PathLine]
           ,[PathQuad1]
           ,[PathQuad2]
           ,[PathQuad3]
           ,[PathQuad4]
           ,[EditDate]
           ,[EditUser]
           ,[Status]
           ,[Comments])
     VALUES
           (@FloorID
           ,@UnitID
           ,@TextPosition
           ,@PathLine
           ,@PathQuad1
           ,@PathQuad2
           ,@PathQuad3
           ,@PathQuad4
           ,GETDATE()
           ,0
           ,1
           ,@Comments)
END

