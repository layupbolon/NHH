CREATE PROCEDURE [dbo].[GetCountMCount]
 @ProductID int
AS
BEGIN

  declare @UnitType int
  set @UnitType=1

return select count(*) from Project_Unit where Project_Unit.ProjectID=@ProductID and UnitType=@UnitType
END
