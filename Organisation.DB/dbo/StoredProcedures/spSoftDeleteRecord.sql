CREATE PROCEDURE [dbo].[spSoftDeleteRecord]
	@tableName VARCHAR(50),
	@id VARCHAR(22)
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX)
	SET @sql = N'UPDATE ' + QUOTENAME(@tableName) + ' SET IsDeleted=1 WHERE Id=@id'
	EXEC sp_executesql @sql,N'@id VARCHAR(22)',@id
END
