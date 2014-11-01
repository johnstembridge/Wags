
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/31/2014 17:10:48
-- Generated from EDMX file: D:\Wags\DataAccess\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Wags];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CourseCourseData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseData] DROP CONSTRAINT [FK_CourseCourseData];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerScore]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Scores] DROP CONSTRAINT [FK_PlayerScore];
GO
IF OBJECT_ID(N'[dbo].[FK_EventBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_EventBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Histories] DROP CONSTRAINT [FK_PlayerHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_MemberTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_MemberBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_ClubCourse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_ClubCourse];
GO
IF OBJECT_ID(N'[dbo].[FK_EventRound]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rounds] DROP CONSTRAINT [FK_EventRound];
GO
IF OBJECT_ID(N'[dbo].[FK_RoundScore]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Scores] DROP CONSTRAINT [FK_RoundScore];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseRound]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rounds] DROP CONSTRAINT [FK_CourseRound];
GO
IF OBJECT_ID(N'[dbo].[FK_EventTrophy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_EventTrophy];
GO
IF OBJECT_ID(N'[dbo].[FK_EventOrganiser_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventOrganiser] DROP CONSTRAINT [FK_EventOrganiser_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EventOrganiser_Member]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventOrganiser] DROP CONSTRAINT [FK_EventOrganiser_Member];
GO
IF OBJECT_ID(N'[dbo].[FK_BookingGuest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Guests] DROP CONSTRAINT [FK_BookingGuest];
GO
IF OBJECT_ID(N'[dbo].[FK_Member_inherits_Player]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Players_Member] DROP CONSTRAINT [FK_Member_inherits_Player];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Players]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Players];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[CourseData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseData];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Trophies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trophies];
GO
IF OBJECT_ID(N'[dbo].[Bookings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bookings];
GO
IF OBJECT_ID(N'[dbo].[Histories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Histories];
GO
IF OBJECT_ID(N'[dbo].[Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transactions];
GO
IF OBJECT_ID(N'[dbo].[Scores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scores];
GO
IF OBJECT_ID(N'[dbo].[Clubs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clubs];
GO
IF OBJECT_ID(N'[dbo].[Rounds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rounds];
GO
IF OBJECT_ID(N'[dbo].[Guests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Guests];
GO
IF OBJECT_ID(N'[dbo].[Players_Member]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Players_Member];
GO
IF OBJECT_ID(N'[dbo].[EventOrganiser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventOrganiser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(255)  NOT NULL,
    [LastName] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ClubId] int  NOT NULL
);
GO

-- Creating table 'CourseData'
CREATE TABLE [dbo].[CourseData] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [SSS] int  NULL,
    [Par] int  NOT NULL,
    [CourseId] int  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [MemberPrice] decimal(6,2)  NULL,
    [GuestPrice] decimal(6,2)  NULL,
    [DinnerPrice] decimal(6,2)  NULL,
    [BookingDeadline] datetime  NULL,
    [MaxPlayers] int  NULL,
    [Schedule] nvarchar(max)  NULL,
    [Notes] nvarchar(max)  NULL,
    [Url] nvarchar(max)  NULL,
    [Trophy_Id] int  NULL
);
GO

-- Creating table 'Trophies'
CREATE TABLE [dbo].[Trophies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Timestamp] datetime  NOT NULL,
    [Attending] bit  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [EventId] int  NOT NULL,
    [MemberId] int  NOT NULL
);
GO

-- Creating table 'Histories'
CREATE TABLE [dbo].[Histories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [Handicap] decimal(3,1)  NOT NULL,
    [PlayerId] int  NOT NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [DebitAmount] decimal(10,2)  NULL,
    [CreditAmount] decimal(10,2)  NULL,
    [MemberId] int  NOT NULL
);
GO

-- Creating table 'Scores'
CREATE TABLE [dbo].[Scores] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Position] int  NOT NULL,
    [Shots] int  NOT NULL,
    [Points] int  NOT NULL,
    [PlayerId] int  NOT NULL,
    [RoundId] int  NOT NULL
);
GO

-- Creating table 'Clubs'
CREATE TABLE [dbo].[Clubs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Url] nvarchar(max)  NULL,
    [Phone] nvarchar(255)  NULL,
    [Address_StreetAddress] nvarchar(max)  NULL,
    [Address_Country] nvarchar(255)  NULL,
    [Address_PostCode] nvarchar(255)  NULL,
    [Directions] nvarchar(max)  NULL
);
GO

-- Creating table 'Rounds'
CREATE TABLE [dbo].[Rounds] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [EventId] int  NOT NULL,
    [CourseId] int  NOT NULL
);
GO

-- Creating table 'Guests'
CREATE TABLE [dbo].[Guests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Handicap] decimal(3,1)  NOT NULL,
    [BookingId] int  NOT NULL
);
GO

-- Creating table 'Players_Member'
CREATE TABLE [dbo].[Players_Member] (
    [Email] nvarchar(255)  NULL,
    [Phone] nvarchar(255)  NULL,
    [Address_StreetAddress] nvarchar(max)  NULL,
    [Address_Country] nvarchar(255)  NULL,
    [Address_PostCode] nvarchar(255)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventOrganiser'
CREATE TABLE [dbo].[EventOrganiser] (
    [Events_Id] int  NOT NULL,
    [Organisers_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CourseData'
ALTER TABLE [dbo].[CourseData]
ADD CONSTRAINT [PK_CourseData]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Trophies'
ALTER TABLE [dbo].[Trophies]
ADD CONSTRAINT [PK_Trophies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [PK_Histories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [PK_Scores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clubs'
ALTER TABLE [dbo].[Clubs]
ADD CONSTRAINT [PK_Clubs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rounds'
ALTER TABLE [dbo].[Rounds]
ADD CONSTRAINT [PK_Rounds]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Guests'
ALTER TABLE [dbo].[Guests]
ADD CONSTRAINT [PK_Guests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Players_Member'
ALTER TABLE [dbo].[Players_Member]
ADD CONSTRAINT [PK_Players_Member]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Events_Id], [Organisers_Id] in table 'EventOrganiser'
ALTER TABLE [dbo].[EventOrganiser]
ADD CONSTRAINT [PK_EventOrganiser]
    PRIMARY KEY CLUSTERED ([Events_Id], [Organisers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CourseId] in table 'CourseData'
ALTER TABLE [dbo].[CourseData]
ADD CONSTRAINT [FK_CourseCourseData]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseCourseData'
CREATE INDEX [IX_FK_CourseCourseData]
ON [dbo].[CourseData]
    ([CourseId]);
GO

-- Creating foreign key on [PlayerId] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [FK_PlayerScore]
    FOREIGN KEY ([PlayerId])
    REFERENCES [dbo].[Players]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerScore'
CREATE INDEX [IX_FK_PlayerScore]
ON [dbo].[Scores]
    ([PlayerId]);
GO

-- Creating foreign key on [EventId] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_EventBooking]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventBooking'
CREATE INDEX [IX_FK_EventBooking]
ON [dbo].[Bookings]
    ([EventId]);
GO

-- Creating foreign key on [PlayerId] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_PlayerHistory]
    FOREIGN KEY ([PlayerId])
    REFERENCES [dbo].[Players]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerHistory'
CREATE INDEX [IX_FK_PlayerHistory]
ON [dbo].[Histories]
    ([PlayerId]);
GO

-- Creating foreign key on [MemberId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_MemberTransaction]
    FOREIGN KEY ([MemberId])
    REFERENCES [dbo].[Players_Member]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberTransaction'
CREATE INDEX [IX_FK_MemberTransaction]
ON [dbo].[Transactions]
    ([MemberId]);
GO

-- Creating foreign key on [MemberId] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_MemberBooking]
    FOREIGN KEY ([MemberId])
    REFERENCES [dbo].[Players_Member]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberBooking'
CREATE INDEX [IX_FK_MemberBooking]
ON [dbo].[Bookings]
    ([MemberId]);
GO

-- Creating foreign key on [ClubId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_ClubCourse]
    FOREIGN KEY ([ClubId])
    REFERENCES [dbo].[Clubs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClubCourse'
CREATE INDEX [IX_FK_ClubCourse]
ON [dbo].[Courses]
    ([ClubId]);
GO

-- Creating foreign key on [EventId] in table 'Rounds'
ALTER TABLE [dbo].[Rounds]
ADD CONSTRAINT [FK_EventRound]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventRound'
CREATE INDEX [IX_FK_EventRound]
ON [dbo].[Rounds]
    ([EventId]);
GO

-- Creating foreign key on [RoundId] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [FK_RoundScore]
    FOREIGN KEY ([RoundId])
    REFERENCES [dbo].[Rounds]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoundScore'
CREATE INDEX [IX_FK_RoundScore]
ON [dbo].[Scores]
    ([RoundId]);
GO

-- Creating foreign key on [CourseId] in table 'Rounds'
ALTER TABLE [dbo].[Rounds]
ADD CONSTRAINT [FK_CourseRound]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseRound'
CREATE INDEX [IX_FK_CourseRound]
ON [dbo].[Rounds]
    ([CourseId]);
GO

-- Creating foreign key on [Trophy_Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_EventTrophy]
    FOREIGN KEY ([Trophy_Id])
    REFERENCES [dbo].[Trophies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventTrophy'
CREATE INDEX [IX_FK_EventTrophy]
ON [dbo].[Events]
    ([Trophy_Id]);
GO

-- Creating foreign key on [Events_Id] in table 'EventOrganiser'
ALTER TABLE [dbo].[EventOrganiser]
ADD CONSTRAINT [FK_EventOrganiser_Event]
    FOREIGN KEY ([Events_Id])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Organisers_Id] in table 'EventOrganiser'
ALTER TABLE [dbo].[EventOrganiser]
ADD CONSTRAINT [FK_EventOrganiser_Member]
    FOREIGN KEY ([Organisers_Id])
    REFERENCES [dbo].[Players_Member]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventOrganiser_Member'
CREATE INDEX [IX_FK_EventOrganiser_Member]
ON [dbo].[EventOrganiser]
    ([Organisers_Id]);
GO

-- Creating foreign key on [BookingId] in table 'Guests'
ALTER TABLE [dbo].[Guests]
ADD CONSTRAINT [FK_BookingGuest]
    FOREIGN KEY ([BookingId])
    REFERENCES [dbo].[Bookings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BookingGuest'
CREATE INDEX [IX_FK_BookingGuest]
ON [dbo].[Guests]
    ([BookingId]);
GO

-- Creating foreign key on [Id] in table 'Players_Member'
ALTER TABLE [dbo].[Players_Member]
ADD CONSTRAINT [FK_Member_inherits_Player]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Players]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------