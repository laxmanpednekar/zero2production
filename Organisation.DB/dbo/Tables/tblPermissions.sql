CREATE TABLE [dbo].[tblPermissions]
(
	[Id] VARCHAR(22)  NOT NULL,
	[Name] VARCHAR(50)  NOT NULL,
	[PagingOrder] INT NOT NULL IDENTITY(1,1),
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_tblPermissions_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UK_tblPermissions_PagingOrder] UNIQUE NONCLUSTERED (PagingOrder),
	INDEX [IX_tblPermissions_Name] NONCLUSTERED (Name) WITH (FILLFACTOR=100),
	INDEX [IX_tblPermissions_IsDeleted] NONCLUSTERED ([IsDeleted])
)