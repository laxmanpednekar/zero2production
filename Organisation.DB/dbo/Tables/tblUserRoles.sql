CREATE TABLE [dbo].[tblUserRoles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[UserId] VARCHAR(22)  NOT NULL,
	[RoleId] VARCHAR(22)  NOT NULL
	CONSTRAINT [FK_tblUserRoles_tblUserDetails] FOREIGN KEY ([UserId]) REFERENCES [dbo].[tblUserDetails] ([Id]),
	CONSTRAINT [FK_tblUserRoles_tblRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[tblRoles] ([Id])
)
