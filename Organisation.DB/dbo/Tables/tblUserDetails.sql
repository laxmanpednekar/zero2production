CREATE TABLE [dbo].[tblUserDetails]
(
	[Id] VARCHAR(22)  NOT NULL,
	[Email] VARCHAR (70) NOT NULL,
	[UserName] VARCHAR (50),
	[PasswordHash] VARCHAR(250),
	[RefreshToken] VARCHAR(500),
	[RefreshTokenExpiryDate] DATETIME,
	[PagingOrder] INT NOT NULL IDENTITY(1,1),
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_tblUserDetails_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UK_tblUserDetails_PagingOrder] UNIQUE NONCLUSTERED (PagingOrder),
	CONSTRAINT [UK_tblUserDetails_Email] UNIQUE NONCLUSTERED (Email)
)
