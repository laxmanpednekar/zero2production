CREATE TABLE [dbo].[tblRolePermissions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[RoleId] VARCHAR(22)  NOT NULL,	
	[PermissionId] VARCHAR(22)  NOT NULL,
	CONSTRAINT [FK_tblRolePermissions_tblPermissions] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[tblPermissions] ([Id]),
	CONSTRAINT [FK_tblRolePermissions_tblRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[tblRoles] ([Id])
)
