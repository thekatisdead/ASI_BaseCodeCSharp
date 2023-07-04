CREATE TABLE [dbo].[JobOpening] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Position]    NVARCHAR (50)  NOT NULL,
    [JobType]     NVARCHAR (50)  NOT NULL,
    [Salary]      DECIMAL (18)   NOT NULL,
    [Hours]       INT            NOT NULL,
    [Shift]       NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [CreatedTime] DATETIME2 (7)  NULL,
    [CreatedBy]   NVARCHAR (50)  NULL,
    [UpdatedTime] DATETIME2 (7)  NULL,
    [UpdatedBy]   NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
