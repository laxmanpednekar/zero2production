CREATE PROCEDURE [dbo].[spGetTotalRecordsCount]
	@tableName VARCHAR(50)
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX)

	SET @sql= N'SELECT COUNT(Id) FROM ' + QUOTENAME(@tableName) + ' WHERE IsDeleted=0'

	EXEC sp_executesql @sql
END