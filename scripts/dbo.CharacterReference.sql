﻿CREATE TABLE [dbo].[CharacterReference] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [CandidateFirstName]   NVARCHAR (50) NOT NULL,
    [CandidateLastName]    NVARCHAR (50) NOT NULL,
    [Position]             NVARCHAR (50) NOT NULL,
    [RelationshipDuration] NVARCHAR (50) NOT NULL,
    [Relationship]         NVARCHAR (MAX) NOT NULL,
    [CharacterEthics]      NVARCHAR (MAX) NOT NULL,
    [Qualifications]       NVARCHAR (MAX) NOT NULL,
    [FirstName]            NVARCHAR (50) NOT NULL,
    [LastName]             NVARCHAR (50) NOT NULL,
    [JobTitle]             NVARCHAR (50) NOT NULL,
    [WorkedWithCandidate]  BIT           NOT NULL,
    [ReasonToHire]         NVARCHAR (MAX) NOT NULL,
    [CreatedTime]          DATETIME2 (7) NULL,
    [CreatedBy]            NVARCHAR (50) NULL,
    [UpdatedTime]          DATETIME2 (7) NULL,
    [UpdatedBy]            NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
