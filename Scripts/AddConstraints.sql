IF OBJECT_ID(N'[dbo].[UK_Booking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT UK_Booking;
GO
IF OBJECT_ID(N'[dbo].[UK_Guest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Guests] DROP CONSTRAINT [UK_Guest];
GO

ALTER TABLE Wags.dbo.Bookings  ADD CONSTRAINT UK_Booking UNIQUE (EventId, MemberId)
GO
ALTER TABLE Wags.dbo.Guests  ADD CONSTRAINT UK_Guest UNIQUE (BookingId, Name)
GO