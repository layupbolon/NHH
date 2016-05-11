CREATE PROCEDURE [dbo].[GetCountPCount]
 @ProductID int
AS
BEGIN

  declare @UnitType int
  set @UnitType=2

return select count(*) from Project_Unit where Project_Unit.ProjectID=@ProductID and UnitType=@UnitType
END
