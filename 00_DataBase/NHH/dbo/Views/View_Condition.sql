
CREATE VIEW [dbo].[View_Condition]
AS
SELECT   FieldValue AS ConditionID, FieldName AS ConditionName
FROM      dbo.Dictionary WITH (NOLOCK)
WHERE   (FieldType = 'Condition')



