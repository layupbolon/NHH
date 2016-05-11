CREATE VIEW [dbo].[View_UnitStatus]
AS
SELECT   FieldValue AS UnitStatusID, FieldName AS UnitStatusName
FROM      dbo.Dictionary WITH (Nolock)
WHERE   (FieldType = 'ProjectUnitStatus')

