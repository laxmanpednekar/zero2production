CREATE PROCEDURE [dbo].[spGetRecords]
	@tableName VARCHAR(50),
	@columns VARCHAR(MAX) = NULL,
	@pageNumber INT = 1,
	@pageSize INT = 100
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX);	
	DECLARE @previousPagelastPageNumber INT;

	SET @previousPagelastPageNumber = (@pageNumber-1)*@pageSize;

	IF @columns IS NULL
	   SET @columns = '*';

	SET @sql= N'SELECT TOP ('+CONVERT(VARCHAR(7),@pageSize)+') '+ @columns + ' FROM ' + QUOTENAME(@tableName)+ ' WHERE PagingOrder > @previousPagelastPageNumber AND IsDeleted=0 ORDER BY PagingOrder';

	EXEC sp_executesql @sql,N'@previousPagelastPageNumber INT',@previousPagelastPageNumber;
END
