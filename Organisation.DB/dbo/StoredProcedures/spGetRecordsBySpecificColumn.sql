CREATE PROCEDURE [dbo].[spGetRecordsBySpecificColumn]
	@tableName VARCHAR(50),
	@columns VARCHAR(MAX) = NULL,
	@columnName VARCHAR(60),
	@columnValue VARCHAR(100)
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX)

	IF @columns IS NULL
	   SET @columns = '*'

	SET @sql= N'SELECT ' + @columns + ' FROM ' + QUOTENAME(@tableName) + ' WHERE '+@columnName+'=@columnValue AND IsDeleted=0'

	EXEC sp_executesql @sql,N'@columnValue VARCHAR(100)',@columnValue
END