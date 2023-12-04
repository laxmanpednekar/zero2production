CREATE TABLE [dbo].[tblEmployees]
(
	[Id]        VARCHAR(22)  NOT NULL,
    [PagingOrder] INT NOT NULL IDENTITY(1,1),
    [Name]      VARCHAR (50) NOT NULL,
    [Age]       INT           NOT NULL,
    [Position]  VARCHAR (50) NOT NULL,
    [CompanyId] VARCHAR(22)  NOT NULL,
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [Salary] DECIMAL(10, 2) NOT NULL, 
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employees_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[tblCompanies] ([Id]),
    CONSTRAINT [UK_Employees_PagingOrder] UNIQUE NONCLUSTERED (PagingOrder),
    CONSTRAINT [UK_Employees_Name] UNIQUE NONCLUSTERED (Name),
    INDEX [IX_Employees_CreatedOn] NONCLUSTERED (CreatedOn),
    INDEX [IX_Employees_Name] NONCLUSTERED (Name),
    INDEX [IX_Employees_IsDeleted] NONCLUSTERED ([IsDeleted])
)
