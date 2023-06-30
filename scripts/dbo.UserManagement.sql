CREATE TABLE [dbo].[UserManagement] (
    [Id]            INT           NOT NULL,
    [FirstName]     NVARCHAR (50) NOT NULL,
    [LastName]      NVARCHAR (50) NOT NULL,
    [EmailAddress]  NVARCHAR (50) NOT NULL,
    [ContactNumber] NVARCHAR (50) NOT NULL,
    [Address]       NVARCHAR (50) NOT NULL,
    [Username]      NVARCHAR (50) NOT NULL,
    [Password]      NVARCHAR (50) NOT NULL,
    [Role]          NVARCHAR (50) NOT NULL,
	[CreatedTime] DATETIME2 NULL, 
    [CreatedBy] NVARCHAR(50) NULL, 
    [UpdatedTime] DATETIME2 NULL, 
    [UpdatedBy] NVARCHAR(50) NULL
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

