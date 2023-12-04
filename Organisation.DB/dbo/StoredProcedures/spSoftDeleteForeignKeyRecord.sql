CREATE PROCEDURE [dbo].[spSoftDeleteForeignKeyRecord]
	@tableName VARCHAR(50),
	@foreignkeyColumnName VARCHAR(50),
	@foreignkeyColumnValue VARCHAR(22)
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX)
	SET @sql = N'UPDATE ' + QUOTENAME(@tableName) + ' SET IsDeleted=1 WHERE '+@foreignkeyColumnName+'=@foreignkeyColumnValue'
	EXEC sp_executesql @sql,N'@foreignkeyColumnValue VARCHAR(22)',@foreignkeyColumnValue
END
